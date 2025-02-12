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
import { ShoppingCartWishList } from '../../../../../shared/models/domain/shopping-cart/shopping-cart-wish-list.model';
import { ShoppingCartWishListService } from '../../../../../core/services/domain/shopping-cart/shopping-cart-wish-list.service';
import { Customer } from '../../../../../shared/models/domain/customer/customer.model';
import { CustomerService } from '../../../../../core/services/domain/customer/customer.service';
import { Product } from '../../../../../shared/models/domain/product/product.model';
import { ProductService } from '../../../../../core/services/domain/product/product.service';
import { NgClass } from '@angular/common';

// Define the ViewShoppingCartWishListComponent
@Component({
    // Selector for the component
    selector: 'app-view-shopping-cart-wish-list',
    // Template URL for the component
    templateUrl: './view-shopping-cart-wish-list.component.html',
    imports: [RouterLink, TranslateDirective, TreeTableModule, PrimeTemplate, NgClass, TranslatePipe]
})
export class ViewShoppingCartWishListComponent extends SharedComponent implements OnInit, OnDestroy {
    private readonly router = inject(Router);
    private readonly route = inject(ActivatedRoute);
    readonly customerService = inject(CustomerService);
    readonly productService = inject(ProductService);
    readonly shoppingCartWishListService = inject(ShoppingCartWishListService);
    override readonly coreSessionService: CoreSessionService;
    readonly coreSubscriptionService = inject(CoreSubscriptionService);
    private readonly translateService = inject(TranslateService);

    readonly viewShoppingCartWishListsTable = viewChild.required<TreeTable>('this.ViewShoppingCartWishListsTable');  
    manageShoppingCartWishList: CustomTreeTableModel<TreeNode<ShoppingCartWishList>> = new CustomTreeTableModel<TreeNode<ShoppingCartWishList>>({
        data: new Array<TreeNode<ShoppingCartWishList>>()
    });
    selectedDetailNode: ShoppingCartWishList = {} as ShoppingCartWishList;
    sortCols!: Array<CustomSortModel>;
    cols!: Array<TreeTableHeaderModel>;
    customers!: Array<Customer>;
    doubleClicked!: boolean;
    manageShoppingCartWishListData!: Array<ShoppingCartWishList>;
    products!: Array<Product>;
    screenGroup!: string;
    searchShoppingCartWishList!: string;
    selectedNode!: TreeTableNode | null;
    serviceGroup!: string;
    shoppingCartWishLists!: Array<ShoppingCartWishList>;
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
                 { field: 'CustomerUIdValue', header: 'Customer', class:'text-left' },          
                 { field: 'ProductUIdValue', header: 'Product', class:'text-left' },          
                 { field: 'SortOrder', header: 'SortOrder', class:'text-right' },           
        ];
        this.sortCols = [
                 { field: 'CustomerUIdValue', type: 'string' },
                 { field: 'ProductUIdValue', type: 'string' },
                 { field: 'SortOrder', type: 'number' },
        ];
        this.getMasterData();
        this.getShoppingCartWishLists();
      
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
       this.customerService.getCustomers().subscribe((customers: Array<Customer>): void => {
           this.customers = customers;
           this.refreshShoppingCartWishList();
        // On error, handle the error
        },(error: HttpErrorResponse): void => {
            CoreNotificationService.handleError(error);
        });
       this.productService.getProducts().subscribe((products: Array<Product>): void => {
           this.products = products;
           this.refreshShoppingCartWishList();
        // On error, handle the error
        },(error: HttpErrorResponse): void => {
            CoreNotificationService.handleError(error);
        });
    }
    
    /**
	// Represents refreshShoppingCartWishList method
    // Method to refresh grid data.
	// @returns {void} - refreshShoppingCartWishList method returns void.
    */
    refreshShoppingCartWishList(): void {
        if (this.shoppingCartWishLists && this.shoppingCartWishLists.length > 0) {
            this.buildShoppingCartWishListTree(this.shoppingCartWishLists);
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

    getShoppingCartWishLists(): void {
       this.shoppingCartWishListService.getShoppingCartWishLists()
            .subscribe((shoppingCartWishLists: Array<ShoppingCartWishList>): void => {
                if (shoppingCartWishLists && shoppingCartWishLists.length) {
                    this.shoppingCartWishLists = shoppingCartWishLists;
                    this.buildShoppingCartWishListTree(shoppingCartWishLists);
                }
            // On error, handle the error
            }, (error: HttpErrorResponse): void => {
                CoreNotificationService.handleError(error);
            }); 
    }

    /**
	// Represents buildShoppingCartWishListTree method
	// @param {Array<ShoppingCartWishList>} activeShoppingCartWishLists - activeShoppingCartWishLists method param.
	// @returns {void} - buildShoppingCartWishListTree method returns void.
    */
    buildShoppingCartWishListTree(activeShoppingCartWishLists: Array<ShoppingCartWishList>): void {
        const shoppingCartWishLists: Array<TreeNode> = this.getShoppingCartWishListNode(activeShoppingCartWishLists);
        this.manageShoppingCartWishList.data = shoppingCartWishLists;
        this.manageShoppingCartWishList.data = [...this.customSortMultipleColumns(this.manageShoppingCartWishList.data, this.sortCols)];
    }

    /**
	// Represents getShoppingCartWishListNode method
	// @param {Array<ShoppingCartWishList>} arr - arr method param.
	// @returns {Array<TreeNode>} - getShoppingCartWishListNode method returns Array<TreeNode>.
    */
    getShoppingCartWishListNode(arr: Array<ShoppingCartWishList>): Array<TreeNode> {
        const node: Array<TreeNode> = [];
        arr.forEach((shoppingCartWishList: ShoppingCartWishList): void => {
            const innerNode: TreeNode = {
                data: shoppingCartWishList
            };
            innerNode.data.CustomerUIdValue = this.getDetailsForUId(this.customers, shoppingCartWishList.CustomerUId, 'CustomerUId', 'Name');
                        
            innerNode.data.ProductUIdValue = this.getDetailsForUId(this.products, shoppingCartWishList.ProductUId, 'ProductUId', 'Name');
                        
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
        this.searchShoppingCartWishList = eventValue;
        if (eventValue && eventValue.length >= 3) {
            content.filterGlobal(eventValue, 'contains');
        } else {
            content.filterGlobal('', 'contains');
        }
    }

    addShoppingCartWishList(): void {
        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageShoppingCartWishList'],
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
	// @param {TreeNode<ShoppingCartWishList>} event - event method param.
	// @returns {void} - onNodeSelected method returns void.
    */
    onNodeSelected(event: TreeNode<ShoppingCartWishList>): void {
        setTimeout((): void => {
            if (!this.doubleClicked) {
                if (event.data) {
                    this.loadSelectedDetail(event.data.ShoppingCartWishListUId, this.manageShoppingCartWishList.data);
                }
            } else {
                this.doubleClicked = false;
            }
        }, 100);
    }

    /**
	// Represents navigateToShoppingCartWishList method
	// @param {ShoppingCartWishList} nodeData - nodeData method param.
	// @param {string} action - action method param.
	// @returns {void} - navigateToShoppingCartWishList method returns void.
    */
    navigateToShoppingCartWishList(nodeData: ShoppingCartWishList, action: string): void {
        if (nodeData) {

            void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageShoppingCartWishList'],
                {
                    queryParams: {
                        action,
                        shoppingCartWishListUId: nodeData.ShoppingCartWishListUId,
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

        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageShoppingCartWishList'],
            {
                queryParams: {
                    action: 'Edit',
                    shoppingCartWishListUId: this.selectedDetailNode.ShoppingCartWishListUId,
                    screenGroup: this.screenGroup,
                    serviceGroup: this.serviceGroup
                }
            });
    }

    /**
	// Represents loadSelectedDetail method
	// @param {Guid} shoppingCartWishListUId - shoppingCartWishListUId method param.
	// @param {Array<TreeNode>} items - items method param.
	// @returns {void} - loadSelectedDetail method returns void.
    */
    loadSelectedDetail(shoppingCartWishListUId: Guid, items: Array<TreeNode>): void {
        if (items) {
            this.selectedDetailNode = items.filter((x => x.data.ShoppingCartWishListUId === shoppingCartWishListUId))[0].data;
        }
    }
}
