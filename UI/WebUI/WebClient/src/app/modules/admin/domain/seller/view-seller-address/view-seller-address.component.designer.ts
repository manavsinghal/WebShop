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
import { SellerAddress } from '../../../../../shared/models/domain/seller/seller-address.model';
import { SellerAddressService } from '../../../../../core/services/domain/seller/seller-address.service';
import { MasterListItem } from '../../../../../shared/models/domain/master/master-list-item.model';
import { MasterListItemService } from '../../../../../core/services/domain/master/master-list-item.service';
import { Seller } from '../../../../../shared/models/domain/seller/seller.model';
import { SellerService } from '../../../../../core/services/domain/seller/seller.service';
import { NgClass } from '@angular/common';

// Define the ViewSellerAddressComponent
@Component({
    // Selector for the component
    selector: 'app-view-seller-address',
    // Template URL for the component
    templateUrl: './view-seller-address.component.html',
    imports: [RouterLink, TranslateDirective, TreeTableModule, PrimeTemplate, NgClass, TranslatePipe]
})
export class ViewSellerAddressComponent extends SharedComponent implements OnInit, OnDestroy {
    @ViewChild('this.ViewSellerAddressesTable', { static: false }) viewSellerAddressesTable!: TreeTable;  
    manageSellerAddress: CustomTreeTableModel<TreeNode<SellerAddress>> = new CustomTreeTableModel<TreeNode<SellerAddress>>({
        data: new Array<TreeNode<SellerAddress>>()
    });
    selectedDetailNode: SellerAddress = {} as SellerAddress;
    sortCols!: Array<CustomSortModel>;
    addressTypes!: Array<MasterListItem>;
    cols!: Array<TreeTableHeaderModel>;
    doubleClicked!: boolean;
    manageSellerAddressData!: Array<SellerAddress>;
    screenGroup!: string;
    searchSellerAddress!: string;
    selectedNode!: TreeTableNode | null;
    sellerAddresses!: Array<SellerAddress>;
    sellers!: Array<Seller>;
    serviceGroup!: string;
    languageChangedSubscription!: Subscription;
    // Constructor for the component
    constructor(private readonly router: Router,
        private readonly route: ActivatedRoute,
        readonly masterListItemService: MasterListItemService,
        readonly sellerAddressService: SellerAddressService,
        readonly sellerService: SellerService,
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
                 { field: 'SellerUIdValue', header: 'Seller', class:'text-left' },          
                 { field: 'AddressTypeUIdValue', header: 'AddressType', class:'text-left' },          
                 { field: 'Line1', header: 'Line1', class: 'text-left' },
                 { field: 'Line2', header: 'Line2', class: 'text-left' },
                 { field: 'Line3', header: 'Line3', class: 'text-left' },
        ];
        this.sortCols = [
                 { field: 'SellerUIdValue', type: 'string' },
                 { field: 'AddressTypeUIdValue', type: 'string' },
                 { field: 'Line1', type: 'string' },
                 { field: 'Line2', type: 'string' },
                 { field: 'Line3', type: 'string' },
        ];
        this.getMasterData();
        this.getSellerAddresses();
      
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
       this.sellerService.getSellers().subscribe((sellers: Array<Seller>): void => {
           this.sellers = sellers;
           this.refreshSellerAddress();
        // On error, handle the error
        },(error: HttpErrorResponse): void => {
            CoreNotificationService.handleError(error);
        });
        this.masterListItemService.getMasterListItemsByCode('AddressType').subscribe((masterListItems: Array<MasterListItem>): void => {
           this.addressTypes = masterListItems;
           this.refreshSellerAddress();
        // On error, handle the error
        },(error: HttpErrorResponse): void => {
            CoreNotificationService.handleError(error);
        });
    }
    
    /**
	// Represents refreshSellerAddress method
    // Method to refresh grid data.
	// @returns {void} - refreshSellerAddress method returns void.
    */
    refreshSellerAddress(): void {
        if (this.sellerAddresses && this.sellerAddresses.length > 0) {
            this.buildSellerAddressTree(this.sellerAddresses);
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

    getSellerAddresses(): void {
       this.sellerAddressService.getSellerAddresses()
            .subscribe((sellerAddresses: Array<SellerAddress>): void => {
                if (sellerAddresses && sellerAddresses.length) {
                    this.sellerAddresses = sellerAddresses;
                    this.buildSellerAddressTree(sellerAddresses);
                }
            // On error, handle the error
            }, (error: HttpErrorResponse): void => {
                CoreNotificationService.handleError(error);
            }); 
    }

    /**
	// Represents buildSellerAddressTree method
	// @param {Array<SellerAddress>} activeSellerAddresses - activeSellerAddresses method param.
	// @returns {void} - buildSellerAddressTree method returns void.
    */
    buildSellerAddressTree(activeSellerAddresses: Array<SellerAddress>): void {
        const sellerAddresses: Array<TreeNode> = this.getSellerAddressNode(activeSellerAddresses);
        this.manageSellerAddress.data = sellerAddresses;
        this.manageSellerAddress.data = [...this.customSortMultipleColumns(this.manageSellerAddress.data, this.sortCols)];
    }

    /**
	// Represents getSellerAddressNode method
	// @param {Array<SellerAddress>} arr - arr method param.
	// @returns {Array<TreeNode>} - getSellerAddressNode method returns Array<TreeNode>.
    */
    getSellerAddressNode(arr: Array<SellerAddress>): Array<TreeNode> {
        const node: Array<TreeNode> = [];
        arr.forEach((sellerAddress: SellerAddress): void => {
            const innerNode: TreeNode = {
                data: sellerAddress
            };
            innerNode.data.AddressTypeUIdValue = this.getDetailsForUId(this.addressTypes, sellerAddress.AddressTypeUId, 'MasterListItemUId', 'Name');
                        
            innerNode.data.SellerUIdValue = this.getDetailsForUId(this.sellers, sellerAddress.SellerUId, 'SellerUId', 'Name');
                        
            node.push(innerNode);
        });

        return node;
    }

    /**
	// Represents customSearch method
	// @param {TreeTable} content - content method param.
	// @param {Event} event - event method param.
	// @returns {void} - customSearch method returns void.
    */
    customSearch(event: Event, content: TreeTable): void {
        const eventValue = (event.target as HTMLInputElement).value;
        this.searchSellerAddress = eventValue;
        if (eventValue && eventValue.length >= 3) {
            content.filterGlobal(eventValue, 'contains');
        } else {
            content.filterGlobal('', 'contains');
        }
    }

    addSellerAddress(): void {
        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageSellerAddress'],
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
	// @param {TreeNode<SellerAddress>} event - event method param.
	// @returns {void} - onNodeSelected method returns void.
    */
    onNodeSelected(event: TreeNode<SellerAddress>): void {
        setTimeout((): void => {
            if (!this.doubleClicked) {
                if (event.data) {
                    this.loadSelectedDetail(event.data.SellerAddressUId, this.manageSellerAddress.data);
                }
            } else {
                this.doubleClicked = false;
            }
        }, 100);
    }

    /**
	// Represents navigateToSellerAddress method
	// @param {SellerAddress} nodeData - nodeData method param.
	// @param {string} action - action method param.
	// @returns {void} - navigateToSellerAddress method returns void.
    */
    navigateToSellerAddress(nodeData: SellerAddress, action: string): void {
        if (nodeData) {

            void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageSellerAddress'],
                {
                    queryParams: {
                        action,
                        sellerAddressUId: nodeData.SellerAddressUId,
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

        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageSellerAddress'],
            {
                queryParams: {
                    action: 'Edit',
                    sellerAddressUId: this.selectedDetailNode.SellerAddressUId,
                    screenGroup: this.screenGroup,
                    serviceGroup: this.serviceGroup
                }
            });
    }

    /**
	// Represents loadSelectedDetail method
	// @param {Array<TreeNode>} items - items method param.
	// @param {Guid} sellerAddressUId - sellerAddressUId method param.
	// @returns {void} - loadSelectedDetail method returns void.
    */
    loadSelectedDetail(sellerAddressUId: Guid, items: Array<TreeNode>): void {
        if (items) {
            this.selectedDetailNode = items.filter((x => x.data.SellerAddressUId === sellerAddressUId))[0].data;
        }
    }
}
