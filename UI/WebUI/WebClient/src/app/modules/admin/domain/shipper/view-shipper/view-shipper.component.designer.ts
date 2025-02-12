/* Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright>
*/

// Import necessary Angular modules and services
import { Component, OnInit, OnDestroy, inject, viewChild } from '@angular/core';
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
import { Shipper } from '../../../../../shared/models/domain/shipper/shipper.model';
import { ShipperService } from '../../../../../core/services/domain/shipper/shipper.service';
import { MasterListItem } from '../../../../../shared/models/domain/master/master-list-item.model';
import { MasterListItemService } from '../../../../../core/services/domain/master/master-list-item.service';
import { NgClass } from '@angular/common';

// Define the ViewShipperComponent
@Component({
    // Selector for the component
    selector: 'app-view-shipper',
    // Template URL for the component
    templateUrl: './view-shipper.component.html',
    imports: [RouterLink, TranslateDirective, TreeTableModule, PrimeTemplate, NgClass, TranslatePipe]
})
export class ViewShipperComponent extends SharedComponent implements OnInit, OnDestroy {
    private readonly router = inject(Router);
    private readonly route = inject(ActivatedRoute);
    readonly masterListItemService = inject(MasterListItemService);
    readonly shipperService = inject(ShipperService);
    override readonly coreSessionService: CoreSessionService;
    readonly coreSubscriptionService = inject(CoreSubscriptionService);
    private readonly translateService = inject(TranslateService);

    readonly viewShippersTable = viewChild.required<TreeTable>('this.ViewShippersTable');  
    manageShipper: CustomTreeTableModel<TreeNode<Shipper>> = new CustomTreeTableModel<TreeNode<Shipper>>({
        data: new Array<TreeNode<Shipper>>()
    });
    selectedDetailNode: Shipper = {} as Shipper;
    sortCols!: Array<CustomSortModel>;
    cols!: Array<TreeTableHeaderModel>;
    doubleClicked!: boolean;
    manageShipperData!: Array<Shipper>;
    screenGroup!: string;
    searchShipper!: string;
    selectedNode!: TreeTableNode | null;
    serviceGroup!: string;
    shippers!: Array<Shipper>;
    shipperStatuses!: Array<MasterListItem>;
    languageChangedSubscription!: Subscription;
    // Constructor for the component
    constructor() {
        const coreSessionService = inject(CoreSessionService);

        super();
        this.coreSessionService = coreSessionService;

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
                 { field: 'Name', header: 'Name', class: 'text-left' },
                 { field: 'Rating', header: 'Rating', class:'text-right' },           
                 { field: 'ShipperStatusUIdValue', header: 'ShipperStatus', class:'text-left' },          
                 { field: 'SortOrder', header: 'SortOrder', class:'text-right' },           
        ];
        this.sortCols = [
                 { field: 'Name', type: 'string' },
                 { field: 'Rating', type: 'number' },
                 { field: 'ShipperStatusUIdValue', type: 'string' },
                 { field: 'SortOrder', type: 'number' },
        ];
        this.getMasterData();
        this.getShippers();
      
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
        this.masterListItemService.getMasterListItemsByCode('ShipperStatus').subscribe((masterListItems: Array<MasterListItem>): void => {
           this.shipperStatuses = masterListItems;
           this.refreshShipper();
        // On error, handle the error
        },(error: HttpErrorResponse): void => {
            CoreNotificationService.handleError(error);
        });
    }
    
    /**
	// Represents refreshShipper method
    // Method to refresh grid data.
	// @returns {void} - refreshShipper method returns void.
    */
    refreshShipper(): void {
        if (this.shippers && this.shippers.length > 0) {
            this.buildShipperTree(this.shippers);
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

    getShippers(): void {
       this.shipperService.getShippers()
            .subscribe((shippers: Array<Shipper>): void => {
                if (shippers && shippers.length) {
                    this.shippers = shippers;
                    this.buildShipperTree(shippers);
                }
            // On error, handle the error
            }, (error: HttpErrorResponse): void => {
                CoreNotificationService.handleError(error);
            }); 
    }

    /**
	// Represents buildShipperTree method
	// @param {Array<Shipper>} activeShippers - activeShippers method param.
	// @returns {void} - buildShipperTree method returns void.
    */
    buildShipperTree(activeShippers: Array<Shipper>): void {
        const shippers: Array<TreeNode> = this.getShipperNode(activeShippers);
        this.manageShipper.data = shippers;
        this.manageShipper.data = [...this.customSortMultipleColumns(this.manageShipper.data, this.sortCols)];
    }

    /**
	// Represents getShipperNode method
	// @param {Array<Shipper>} arr - arr method param.
	// @returns {Array<TreeNode>} - getShipperNode method returns Array<TreeNode>.
    */
    getShipperNode(arr: Array<Shipper>): Array<TreeNode> {
        const node: Array<TreeNode> = [];
        arr.forEach((shipper: Shipper): void => {
            const innerNode: TreeNode = {
                data: shipper
            };
            innerNode.data.ShipperStatusUIdValue = this.getDetailsForUId(this.shipperStatuses, shipper.ShipperStatusUId, 'MasterListItemUId', 'Name');
                        
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
        this.searchShipper = eventValue;
        if (eventValue && eventValue.length >= 3) {
            content.filterGlobal(eventValue, 'contains');
        } else {
            content.filterGlobal('', 'contains');
        }
    }

    addShipper(): void {
        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageShipper'],
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
	// @param {TreeNode<Shipper>} event - event method param.
	// @returns {void} - onNodeSelected method returns void.
    */
    onNodeSelected(event: TreeNode<Shipper>): void {
        setTimeout((): void => {
            if (!this.doubleClicked) {
                if (event.data) {
                    this.loadSelectedDetail(event.data.ShipperUId, this.manageShipper.data);
                }
            } else {
                this.doubleClicked = false;
            }
        }, 100);
    }

    /**
	// Represents navigateToShipper method
	// @param {Shipper} nodeData - nodeData method param.
	// @param {string} action - action method param.
	// @returns {void} - navigateToShipper method returns void.
    */
    navigateToShipper(nodeData: Shipper, action: string): void {
        if (nodeData) {

            void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageShipper'],
                {
                    queryParams: {
                        action,
                        shipperUId: nodeData.ShipperUId,
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

        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageShipper'],
            {
                queryParams: {
                    action: 'Edit',
                    shipperUId: this.selectedDetailNode.ShipperUId,
                    screenGroup: this.screenGroup,
                    serviceGroup: this.serviceGroup
                }
            });
    }

    /**
	// Represents loadSelectedDetail method
	// @param {Guid} shipperUId - shipperUId method param.
	// @param {Array<TreeNode>} items - items method param.
	// @returns {void} - loadSelectedDetail method returns void.
    */
    loadSelectedDetail(shipperUId: Guid, items: Array<TreeNode>): void {
        if (items) {
            this.selectedDetailNode = items.filter((x => x.data.ShipperUId === shipperUId))[0].data;
        }
    }
}
