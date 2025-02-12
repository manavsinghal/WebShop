/* Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright>
*/

// Import necessary Angular modules and services
import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { Router, NavigationExtras, ActivatedRoute, RouterLink } from '@angular/router';
import { TranslateService, TranslateDirective, TranslatePipe } from '@ngx-translate/core';
import { CoreSessionService } from '../../../../../core/services/core.session.service';
import { CoreSubscriptionService } from '../../../../../core/services/core.subscription.service';
import { Subscription } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { Guid } from '../../../../../core/helpers/guid';
import { SharedComponent } from '../../../../../shared/shared.component';
import { TreeNode, TreeTableNode, PrimeTemplate } from 'primeng/api';
import { TreeTable, TreeTableModule } from 'primeng/treetable';
import { CustomSortModel, CustomTreeTableModel } from '../../../../../shared/models/custom-tree-table.model';
import { TreeTableHeaderModel } from '../../../../../shared/models/tree-table.model';
import { CoreNotificationService } from '../../../../../core/services/core.notification.service';
import { ShipperAddress } from '../../../../../shared/models/domain/shipper/shipper-address.model';
import { ShipperAddressService } from '../../../../../core/services/domain/shipper/shipper-address.service';
import { MasterListItem } from '../../../../../shared/models/domain/master/master-list-item.model';
import { MasterListItemService } from '../../../../../core/services/domain/master/master-list-item.service';
import { Shipper } from '../../../../../shared/models/domain/shipper/shipper.model';
import { ShipperService } from '../../../../../core/services/domain/shipper/shipper.service';
import { NgClass } from '@angular/common';

// Define the ViewShipperAddressComponent
@Component({
    // Selector for the component
    selector: 'app-view-shipper-address',
    // Template URL for the component
    templateUrl: './view-shipper-address.component.html',
    imports: [RouterLink, TranslateDirective, TreeTableModule, PrimeTemplate, NgClass, TranslatePipe]
})
export class ViewShipperAddressComponent extends SharedComponent implements OnInit, OnDestroy {
    @ViewChild('this.ViewShipperAddressesTable', { static: false }) viewShipperAddressesTable!: TreeTable;  
    manageShipperAddress: CustomTreeTableModel<TreeNode<ShipperAddress>> = new CustomTreeTableModel<TreeNode<ShipperAddress>>({
        data: new Array<TreeNode<ShipperAddress>>()
    });
    selectedDetailNode: ShipperAddress = {} as ShipperAddress;
    sortCols!: Array<CustomSortModel>;
    addressTypes!: Array<MasterListItem>;
    cols!: Array<TreeTableHeaderModel>;
    doubleClicked!: boolean;
    manageShipperAddressData!: Array<ShipperAddress>;
    screenGroup!: string;
    searchShipperAddress!: string;
    selectedNode!: TreeTableNode | null;
    serviceGroup!: string;
    shipperAddresses!: Array<ShipperAddress>;
    shippers!: Array<Shipper>;
    languageChangedSubscription!: Subscription;
    // Constructor for the component
    constructor(private readonly router: Router,
        private readonly route: ActivatedRoute,
        readonly masterListItemService: MasterListItemService,
        readonly shipperAddressService: ShipperAddressService,
        readonly shipperService: ShipperService,
        override readonly coreSessionService: CoreSessionService,
        readonly coreSubscriptionService: CoreSubscriptionService,
        private readonly translateService: TranslateService) {
        super(coreSessionService);
        this.translateService.setDefaultLang(this.coreSessionService.getLanguage());
    }
 
    /**
	// Represents ngOnInit method
    // Lifecycle hook for initialization
	// @returns {void} - ngOnInit method returns void.
    */
    ngOnInit(): void {
        this.languageChangedSubscription = this.coreSessionService.languageChanged.subscribe((): void => {
            this.onLanguageChanged();
        });
        this.screenGroup = this.route.snapshot.queryParams['screenGroup'];
        this.serviceGroup = this.route.snapshot.queryParams['serviceGroup'];
        this.cols = [
                 { field: 'ShipperUIdValue', header: 'Shipper', class:'text-left' },          
                 { field: 'AddressTypeUIdValue', header: 'AddressType', class:'text-left' },          
                 { field: 'Line1', header: 'Line1', class: 'text-left' },
                 { field: 'Line2', header: 'Line2', class: 'text-left' },
                 { field: 'Line3', header: 'Line3', class: 'text-left' },
        ];
        this.sortCols = [
                 { field: 'ShipperUIdValue', type: 'string' },
                 { field: 'AddressTypeUIdValue', type: 'string' },
                 { field: 'Line1', type: 'string' },
                 { field: 'Line2', type: 'string' },
                 { field: 'Line3', type: 'string' },
        ];
        this.getMasterData();
        this.getShipperAddresses();
      
        window.scroll(0, 0);
    }

    /**
	// Represents ngOnDestroy method
    // Lifecycle hook for destruction
	// @returns {void} - ngOnDestroy method returns void.
    */
    ngOnDestroy(): void {
		this.coreSubscriptionService.unsubscribeSubscriptions(this);
	}
 
    /**
	// Represents onLanguageChanged method
    // Method to handle language changes
	// @returns {void} - onLanguageChanged method returns void.
    */
    onLanguageChanged(): void {
        this.translateService.setDefaultLang(this.coreSessionService.getLanguage());
    }
 
    /**
	// Represents getMasterData method
    // Method to retrieve master data
	// @returns {void} - getMasterData method returns void.
    */
    getMasterData(): void {
       this.shipperService.getShippers().subscribe((shippers: Array<Shipper>): void => {
           this.shippers = shippers;
           this.refreshShipperAddress();
        // On error, handle the error
        },(error: HttpErrorResponse): void => {
            CoreNotificationService.handleError(error);
        });
        this.masterListItemService.getMasterListItemsByCode('AddressType').subscribe((masterListItems: Array<MasterListItem>): void => {
           this.addressTypes = masterListItems;
           this.refreshShipperAddress();
        // On error, handle the error
        },(error: HttpErrorResponse): void => {
            CoreNotificationService.handleError(error);
        });
    }
    
    /**
	// Represents refreshShipperAddress method
    // Method to refresh grid data.
	// @returns {void} - refreshShipperAddress method returns void.
    */
    refreshShipperAddress(): void {
        if (this.shipperAddresses && this.shipperAddresses.length > 0) {
            this.buildShipperAddressTree(this.shipperAddresses);
        }
    }  

    navigateToManageEntity(): void {
        const navigationExtras: NavigationExtras = {
            queryParams: {
                screenGroup: this.screenGroup,
                serviceGroup: this.serviceGroup
            }
        };

        void this.router.navigate(['/app/administration/manageAdminEntities'], navigationExtras);
    }

    backToAdministration(): void {
        void this.router.navigate(['/app/administration']);
    }

    getShipperAddresses(): void {
       this.shipperAddressService.getShipperAddresses()
            .subscribe((shipperAddresses: Array<ShipperAddress>): void => {
                if (shipperAddresses && shipperAddresses.length) {
                    this.shipperAddresses = shipperAddresses;
                    this.buildShipperAddressTree(shipperAddresses);
                }
            // On error, handle the error
            }, (error: HttpErrorResponse): void => {
                CoreNotificationService.handleError(error);
            }); 
    }

    /**
	// Represents buildShipperAddressTree method
	// @param {Array<ShipperAddress>} activeShipperAddresses - activeShipperAddresses method param.
	// @returns {void} - buildShipperAddressTree method returns void.
    */
    buildShipperAddressTree(activeShipperAddresses: Array<ShipperAddress>): void {
        const shipperAddresses: Array<TreeNode> = this.getShipperAddressNode(activeShipperAddresses);
        this.manageShipperAddress.data = shipperAddresses;
        this.manageShipperAddress.data = [...this.customSortMultipleColumns(this.manageShipperAddress.data, this.sortCols)];
    }

    /**
	// Represents getShipperAddressNode method
	// @param {Array<ShipperAddress>} arr - arr method param.
	// @returns {Array<TreeNode>} - getShipperAddressNode method returns Array<TreeNode>.
    */
    getShipperAddressNode(arr: Array<ShipperAddress>): Array<TreeNode> {
        const node: Array<TreeNode> = [];
        arr.forEach((shipperAddress: ShipperAddress): void => {
            const innerNode: TreeNode = {
                data: shipperAddress
            };
            innerNode.data.AddressTypeUIdValue = this.getDetailsForUId(this.addressTypes, shipperAddress.AddressTypeUId, 'MasterListItemUId', 'Name');
                        
            innerNode.data.ShipperUIdValue = this.getDetailsForUId(this.shippers, shipperAddress.ShipperUId, 'ShipperUId', 'Name');
                        
            node.push(innerNode);
        });

        return node;
    }

    /**
	// Represents customSearch method
	// @param {Event} event - event method param.
	// @param {TreeTable} content - content method param.
	// @returns {void} - customSearch method returns void.
    */
    customSearch(event: Event, content: TreeTable): void {
        const eventValue = (event.target as HTMLInputElement).value;
        this.searchShipperAddress = eventValue;
        if (eventValue && eventValue.length >= 3) {
            content.filterGlobal(eventValue, 'contains');
        } else {
            content.filterGlobal('', 'contains');
        }
    }

    addShipperAddress(): void {
        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageShipperAddress'],
            {
                queryParams: {
                    action: 'Add',
                    screenGroup: this.screenGroup,
                    serviceGroup: this.serviceGroup
                }
            });
    }

    /**
	// Represents onNodeSelected method
	// @param {TreeNode<ShipperAddress>} event - event method param.
	// @returns {void} - onNodeSelected method returns void.
    */
    onNodeSelected(event: TreeNode<ShipperAddress>): void {
        setTimeout((): void => {
            if (!this.doubleClicked) {
                if (event.data) {
                    this.loadSelectedDetail(event.data.ShipperAddressUId, this.manageShipperAddress.data);
                }
            } else {
                this.doubleClicked = false;
            }
        }, 100);
    }

    /**
	// Represents navigateToShipperAddress method
	// @param {string} action - action method param.
	// @param {ShipperAddress} nodeData - nodeData method param.
	// @returns {void} - navigateToShipperAddress method returns void.
    */
    navigateToShipperAddress(nodeData: ShipperAddress, action: string): void {
        if (nodeData) {

            void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageShipperAddress'],
                {
                    queryParams: {
                        action,
                        shipperAddressUId: nodeData.ShipperAddressUId,
                        screenGroup: this.screenGroup,
                        serviceGroup: this.serviceGroup
                    }
                });
        }
    }

    /**
	// Represents onRowDblclick method
	// @returns {void} - onRowDblclick method returns void.
    */
    onRowDblclick(): void {
        this.doubleClicked = true;

        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageShipperAddress'],
            {
                queryParams: {
                    action: 'Edit',
                    shipperAddressUId: this.selectedDetailNode.ShipperAddressUId,
                    screenGroup: this.screenGroup,
                    serviceGroup: this.serviceGroup
                }
            });
    }

    /**
	// Represents loadSelectedDetail method
	// @param {Guid} shipperAddressUId - shipperAddressUId method param.
	// @param {Array<TreeNode>} items - items method param.
	// @returns {void} - loadSelectedDetail method returns void.
    */
    loadSelectedDetail(shipperAddressUId: Guid, items: Array<TreeNode>): void {
        if (items) {
            this.selectedDetailNode = items.filter((x => x.data.ShipperAddressUId === shipperAddressUId))[0].data;
        }
    }
}
