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
import { Seller } from '../../../../../shared/models/domain/seller/seller.model';
import { SellerService } from '../../../../../core/services/domain/seller/seller.service';
import { MasterListItem } from '../../../../../shared/models/domain/master/master-list-item.model';
import { MasterListItemService } from '../../../../../core/services/domain/master/master-list-item.service';
import { NgClass } from '@angular/common';

// Define the ViewSellerComponent
@Component({
    // Selector for the component
    selector: 'app-view-seller',
    // Template URL for the component
    templateUrl: './view-seller.component.html',
    imports: [RouterLink, TranslateDirective, TreeTableModule, PrimeTemplate, NgClass, TranslatePipe]
})
export class ViewSellerComponent extends SharedComponent implements OnInit, OnDestroy {
    private readonly router = inject(Router);
    private readonly route = inject(ActivatedRoute);
    readonly masterListItemService = inject(MasterListItemService);
    readonly sellerService = inject(SellerService);
    override readonly coreSessionService: CoreSessionService;
    readonly coreSubscriptionService = inject(CoreSubscriptionService);
    private readonly translateService = inject(TranslateService);

    readonly viewSellersTable = viewChild.required<TreeTable>('this.ViewSellersTable');  
    manageSeller: CustomTreeTableModel<TreeNode<Seller>> = new CustomTreeTableModel<TreeNode<Seller>>({
        data: new Array<TreeNode<Seller>>()
    });
    selectedDetailNode: Seller = {} as Seller;
    sortCols!: Array<CustomSortModel>;
    cols!: Array<TreeTableHeaderModel>;
    doubleClicked!: boolean;
    manageSellerData!: Array<Seller>;
    screenGroup!: string;
    searchSeller!: string;
    selectedNode!: TreeTableNode | null;
    sellers!: Array<Seller>;
    sellerStatuses!: Array<MasterListItem>;
    serviceGroup!: string;
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
                 { field: 'SellerStatusUIdValue', header: 'SellerStatus', class:'text-left' },          
                 { field: 'SortOrder', header: 'SortOrder', class:'text-right' },           
        ];
        this.sortCols = [
                 { field: 'Name', type: 'string' },
                 { field: 'Rating', type: 'number' },
                 { field: 'SellerStatusUIdValue', type: 'string' },
                 { field: 'SortOrder', type: 'number' },
        ];
        this.getMasterData();
        this.getSellers();
      
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
        this.masterListItemService.getMasterListItemsByCode('SellerStatus').subscribe((masterListItems: Array<MasterListItem>): void => {
           this.sellerStatuses = masterListItems;
           this.refreshSeller();
        // On error, handle the error
        },(error: HttpErrorResponse): void => {
            CoreNotificationService.handleError(error);
        });
    }
    
    /**
	// Represents refreshSeller method
    // Method to refresh grid data.
	// @returns {void} - refreshSeller method returns void.
    */
    refreshSeller(): void {
        if (this.sellers && this.sellers.length > 0) {
            this.buildSellerTree(this.sellers);
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

    getSellers(): void {
       this.sellerService.getSellers()
            .subscribe((sellers: Array<Seller>): void => {
                if (sellers && sellers.length) {
                    this.sellers = sellers;
                    this.buildSellerTree(sellers);
                }
            // On error, handle the error
            }, (error: HttpErrorResponse): void => {
                CoreNotificationService.handleError(error);
            }); 
    }

    /**
	// Represents buildSellerTree method
	// @param {Array<Seller>} activeSellers - activeSellers method param.
	// @returns {void} - buildSellerTree method returns void.
    */
    buildSellerTree(activeSellers: Array<Seller>): void {
        const sellers: Array<TreeNode> = this.getSellerNode(activeSellers);
        this.manageSeller.data = sellers;
        this.manageSeller.data = [...this.customSortMultipleColumns(this.manageSeller.data, this.sortCols)];
    }

    /**
	// Represents getSellerNode method
	// @param {Array<Seller>} arr - arr method param.
	// @returns {Array<TreeNode>} - getSellerNode method returns Array<TreeNode>.
    */
    getSellerNode(arr: Array<Seller>): Array<TreeNode> {
        const node: Array<TreeNode> = [];
        arr.forEach((seller: Seller): void => {
            const innerNode: TreeNode = {
                data: seller
            };
            innerNode.data.SellerStatusUIdValue = this.getDetailsForUId(this.sellerStatuses, seller.SellerStatusUId, 'MasterListItemUId', 'Name');
                        
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
        this.searchSeller = eventValue;
        if (eventValue && eventValue.length >= 3) {
            content.filterGlobal(eventValue, 'contains');
        } else {
            content.filterGlobal('', 'contains');
        }
    }

    addSeller(): void {
        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageSeller'],
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
	// @param {TreeNode<Seller>} event - event method param.
	// @returns {void} - onNodeSelected method returns void.
    */
    onNodeSelected(event: TreeNode<Seller>): void {
        setTimeout((): void => {
            if (!this.doubleClicked) {
                if (event.data) {
                    this.loadSelectedDetail(event.data.SellerUId, this.manageSeller.data);
                }
            } else {
                this.doubleClicked = false;
            }
        }, 100);
    }

    /**
	// Represents navigateToSeller method
	// @param {Seller} nodeData - nodeData method param.
	// @param {string} action - action method param.
	// @returns {void} - navigateToSeller method returns void.
    */
    navigateToSeller(nodeData: Seller, action: string): void {
        if (nodeData) {

            void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageSeller'],
                {
                    queryParams: {
                        action,
                        sellerUId: nodeData.SellerUId,
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

        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageSeller'],
            {
                queryParams: {
                    action: 'Edit',
                    sellerUId: this.selectedDetailNode.SellerUId,
                    screenGroup: this.screenGroup,
                    serviceGroup: this.serviceGroup
                }
            });
    }

    /**
	// Represents loadSelectedDetail method
	// @param {Guid} sellerUId - sellerUId method param.
	// @param {Array<TreeNode>} items - items method param.
	// @returns {void} - loadSelectedDetail method returns void.
    */
    loadSelectedDetail(sellerUId: Guid, items: Array<TreeNode>): void {
        if (items) {
            this.selectedDetailNode = items.filter((x => x.data.SellerUId === sellerUId))[0].data;
        }
    }
}
