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
import { ShoppingCart } from '../../../../../shared/models/domain/shopping-cart/shopping-cart.model';
import { ShoppingCartService } from '../../../../../core/services/domain/shopping-cart/shopping-cart.service';
import { Customer } from '../../../../../shared/models/domain/customer/customer.model';
import { CustomerService } from '../../../../../core/services/domain/customer/customer.service';
import { Product } from '../../../../../shared/models/domain/product/product.model';
import { ProductService } from '../../../../../core/services/domain/product/product.service';
import { NgClass } from '@angular/common';

// Define the ViewShoppingCartComponent
@Component({
    // Selector for the component
    selector: 'app-view-shopping-cart',
    // Template URL for the component
    templateUrl: './view-shopping-cart.component.html',
    imports: [RouterLink, TranslateDirective, TreeTableModule, PrimeTemplate, NgClass, TranslatePipe]
})
export class ViewShoppingCartComponent extends SharedComponent implements OnInit, OnDestroy {
    @ViewChild('this.ViewShoppingCartsTable', { static: false }) viewShoppingCartsTable!: TreeTable;  
    manageShoppingCart: CustomTreeTableModel<TreeNode<ShoppingCart>> = new CustomTreeTableModel<TreeNode<ShoppingCart>>({
        data: new Array<TreeNode<ShoppingCart>>()
    });
    selectedDetailNode: ShoppingCart = {} as ShoppingCart;
    sortCols!: Array<CustomSortModel>;
    cols!: Array<TreeTableHeaderModel>;
    customers!: Array<Customer>;
    doubleClicked!: boolean;
    manageShoppingCartData!: Array<ShoppingCart>;
    products!: Array<Product>;
    screenGroup!: string;
    searchShoppingCart!: string;
    selectedNode!: TreeTableNode | null;
    serviceGroup!: string;
    shoppingCarts!: Array<ShoppingCart>;
    languageChangedSubscription!: Subscription;
    // Constructor for the component
    constructor(private readonly router: Router,
        private readonly route: ActivatedRoute,
        readonly customerService: CustomerService,
        readonly productService: ProductService,
        readonly shoppingCartService: ShoppingCartService,
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
                 { field: 'CustomerUIdValue', header: 'Customer', class:'text-left' },          
                 { field: 'ProductUIdValue', header: 'Product', class:'text-left' },          
                 { field: 'Quantity', header: 'Quantity', class:'text-right' },           
                 { field: 'Rate', header: 'Rate', class:'text-right' },           
                 { field: 'SortOrder', header: 'SortOrder', class:'text-right' },           
        ];
        this.sortCols = [
                 { field: 'CustomerUIdValue', type: 'string' },
                 { field: 'ProductUIdValue', type: 'string' },
                 { field: 'Quantity', type: 'number' },
                 { field: 'Rate', type: 'number' },
                 { field: 'SortOrder', type: 'number' },
        ];
        this.getMasterData();
        this.getShoppingCarts();
      
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
           this.refreshShoppingCart();
        // On error, handle the error
        },(error: HttpErrorResponse): void => {
            CoreNotificationService.handleError(error);
        });
       this.productService.getProducts().subscribe((products: Array<Product>): void => {
           this.products = products;
           this.refreshShoppingCart();
        // On error, handle the error
        },(error: HttpErrorResponse): void => {
            CoreNotificationService.handleError(error);
        });
    }
    
    /**
	// Represents refreshShoppingCart method
    // Method to refresh grid data.
	// @returns {void} - refreshShoppingCart method returns void.
    */
    refreshShoppingCart(): void {
        if (this.shoppingCarts && this.shoppingCarts.length > 0) {
            this.buildShoppingCartTree(this.shoppingCarts);
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

    getShoppingCarts(): void {
       this.shoppingCartService.getShoppingCarts()
            .subscribe((shoppingCarts: Array<ShoppingCart>): void => {
                if (shoppingCarts && shoppingCarts.length) {
                    this.shoppingCarts = shoppingCarts;
                    this.buildShoppingCartTree(shoppingCarts);
                }
            // On error, handle the error
            }, (error: HttpErrorResponse): void => {
                CoreNotificationService.handleError(error);
            }); 
    }

    /**
	// Represents buildShoppingCartTree method
	// @param {Array<ShoppingCart>} activeShoppingCarts - activeShoppingCarts method param.
	// @returns {void} - buildShoppingCartTree method returns void.
    */
    buildShoppingCartTree(activeShoppingCarts: Array<ShoppingCart>): void {
        const shoppingCarts: Array<TreeNode> = this.getShoppingCartNode(activeShoppingCarts);
        this.manageShoppingCart.data = shoppingCarts;
        this.manageShoppingCart.data = [...this.customSortMultipleColumns(this.manageShoppingCart.data, this.sortCols)];
    }

    /**
	// Represents getShoppingCartNode method
	// @param {Array<ShoppingCart>} arr - arr method param.
	// @returns {Array<TreeNode>} - getShoppingCartNode method returns Array<TreeNode>.
    */
    getShoppingCartNode(arr: Array<ShoppingCart>): Array<TreeNode> {
        const node: Array<TreeNode> = [];
        arr.forEach((shoppingCart: ShoppingCart): void => {
            const innerNode: TreeNode = {
                data: shoppingCart
            };
            innerNode.data.CustomerUIdValue = this.getDetailsForUId(this.customers, shoppingCart.CustomerUId, 'CustomerUId', 'Name');
                        
            innerNode.data.ProductUIdValue = this.getDetailsForUId(this.products, shoppingCart.ProductUId, 'ProductUId', 'Name');
                        
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
        this.searchShoppingCart = eventValue;
        if (eventValue && eventValue.length >= 3) {
            content.filterGlobal(eventValue, 'contains');
        } else {
            content.filterGlobal('', 'contains');
        }
    }

    addShoppingCart(): void {
        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageShoppingCart'],
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
	// @param {TreeNode<ShoppingCart>} event - event method param.
	// @returns {void} - onNodeSelected method returns void.
    */
    onNodeSelected(event: TreeNode<ShoppingCart>): void {
        setTimeout((): void => {
            if (!this.doubleClicked) {
                if (event.data) {
                    this.loadSelectedDetail(event.data.ShoppingCartUId, this.manageShoppingCart.data);
                }
            } else {
                this.doubleClicked = false;
            }
        }, 100);
    }

    /**
	// Represents navigateToShoppingCart method
	// @param {ShoppingCart} nodeData - nodeData method param.
	// @param {string} action - action method param.
	// @returns {void} - navigateToShoppingCart method returns void.
    */
    navigateToShoppingCart(nodeData: ShoppingCart, action: string): void {
        if (nodeData) {

            void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageShoppingCart'],
                {
                    queryParams: {
                        action,
                        shoppingCartUId: nodeData.ShoppingCartUId,
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

        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageShoppingCart'],
            {
                queryParams: {
                    action: 'Edit',
                    shoppingCartUId: this.selectedDetailNode.ShoppingCartUId,
                    screenGroup: this.screenGroup,
                    serviceGroup: this.serviceGroup
                }
            });
    }

    /**
	// Represents loadSelectedDetail method
	// @param {Array<TreeNode>} items - items method param.
	// @param {Guid} shoppingCartUId - shoppingCartUId method param.
	// @returns {void} - loadSelectedDetail method returns void.
    */
    loadSelectedDetail(shoppingCartUId: Guid, items: Array<TreeNode>): void {
        if (items) {
            this.selectedDetailNode = items.filter((x => x.data.ShoppingCartUId === shoppingCartUId))[0].data;
        }
    }
}
