/* Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright>
*/

// Import necessary Angular modules and services
import { Component, OnInit, OnDestroy, ViewChild, inject } from '@angular/core';
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
import { OrderItem } from '../../../../../shared/models/domain/order/order-item.model';
import { OrderItemService } from '../../../../../core/services/domain/order/order-item.service';
import { Order } from '../../../../../shared/models/domain/order/order.model';
import { OrderService } from '../../../../../core/services/domain/order/order.service';
import { Product } from '../../../../../shared/models/domain/product/product.model';
import { ProductService } from '../../../../../core/services/domain/product/product.service';
import { Shipper } from '../../../../../shared/models/domain/shipper/shipper.model';
import { ShipperService } from '../../../../../core/services/domain/shipper/shipper.service';
import { NgClass } from '@angular/common';

// Define the ViewOrderItemComponent
@Component({
    // Selector for the component
    selector: 'app-view-order-item',
    // Template URL for the component
    templateUrl: './view-order-item.component.html',
    imports: [RouterLink, TranslateDirective, TreeTableModule, PrimeTemplate, NgClass, TranslatePipe]
})
export class ViewOrderItemComponent extends SharedComponent implements OnInit, OnDestroy {
    private readonly router = inject(Router);
    private readonly route = inject(ActivatedRoute);
    readonly orderItemService = inject(OrderItemService);
    readonly orderService = inject(OrderService);
    readonly productService = inject(ProductService);
    readonly shipperService = inject(ShipperService);
    override readonly coreSessionService: CoreSessionService;
    readonly coreSubscriptionService = inject(CoreSubscriptionService);
    private readonly translateService = inject(TranslateService);

    @ViewChild('this.ViewOrderItemsTable', { static: false }) viewOrderItemsTable!: TreeTable;  
    manageOrderItem: CustomTreeTableModel<TreeNode<OrderItem>> = new CustomTreeTableModel<TreeNode<OrderItem>>({
        data: new Array<TreeNode<OrderItem>>()
    });
    selectedDetailNode: OrderItem = {} as OrderItem;
    sortCols!: Array<CustomSortModel>;
    cols!: Array<TreeTableHeaderModel>;
    doubleClicked!: boolean;
    manageOrderItemData!: Array<OrderItem>;
    orderItems!: Array<OrderItem>;
    orders!: Array<Order>;
    products!: Array<Product>;
    screenGroup!: string;
    searchOrderItem!: string;
    selectedNode!: TreeTableNode | null;
    serviceGroup!: string;
    shippers!: Array<Shipper>;
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
                 { field: 'OrderUIdValue', header: 'Order', class:'text-left' },          
                 { field: 'ProductUIdValue', header: 'Product', class:'text-left' },          
                 { field: 'ShipperUIdValue', header: 'Shipper', class:'text-left' },          
                 { field: 'Quantity', header: 'Quantity', class:'text-right' },           
                 { field: 'Rate', header: 'Rate', class:'text-right' },           
        ];
        this.sortCols = [
                 { field: 'OrderUIdValue', type: 'string' },
                 { field: 'ProductUIdValue', type: 'string' },
                 { field: 'ShipperUIdValue', type: 'string' },
                 { field: 'Quantity', type: 'number' },
                 { field: 'Rate', type: 'number' },
        ];
        this.getMasterData();
        this.getOrderItems();
      
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
       this.orderService.getOrders().subscribe((orders: Array<Order>): void => {
           this.orders = orders;
           this.refreshOrderItem();
        // On error, handle the error
        },(error: HttpErrorResponse): void => {
            CoreNotificationService.handleError(error);
        });
       this.productService.getProducts().subscribe((products: Array<Product>): void => {
           this.products = products;
           this.refreshOrderItem();
        // On error, handle the error
        },(error: HttpErrorResponse): void => {
            CoreNotificationService.handleError(error);
        });
       this.shipperService.getShippers().subscribe((shippers: Array<Shipper>): void => {
           this.shippers = shippers;
           this.refreshOrderItem();
        // On error, handle the error
        },(error: HttpErrorResponse): void => {
            CoreNotificationService.handleError(error);
        });
    }
    
    /**
	// Represents refreshOrderItem method
    // Method to refresh grid data.
	// @returns {void} - refreshOrderItem method returns void.
    */
    refreshOrderItem(): void {
        if (this.orderItems && this.orderItems.length > 0) {
            this.buildOrderItemTree(this.orderItems);
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

    getOrderItems(): void {
       this.orderItemService.getOrderItems()
            .subscribe((orderItems: Array<OrderItem>): void => {
                if (orderItems && orderItems.length) {
                    this.orderItems = orderItems;
                    this.buildOrderItemTree(orderItems);
                }
            // On error, handle the error
            }, (error: HttpErrorResponse): void => {
                CoreNotificationService.handleError(error);
            }); 
    }

    /**
	// Represents buildOrderItemTree method
	// @param {Array<OrderItem>} activeOrderItems - activeOrderItems method param.
	// @returns {void} - buildOrderItemTree method returns void.
    */
    buildOrderItemTree(activeOrderItems: Array<OrderItem>): void {
        const orderItems: Array<TreeNode> = this.getOrderItemNode(activeOrderItems);
        this.manageOrderItem.data = orderItems;
        this.manageOrderItem.data = [...this.customSortMultipleColumns(this.manageOrderItem.data, this.sortCols)];
    }

    /**
	// Represents getOrderItemNode method
	// @param {Array<OrderItem>} arr - arr method param.
	// @returns {Array<TreeNode>} - getOrderItemNode method returns Array<TreeNode>.
    */
    getOrderItemNode(arr: Array<OrderItem>): Array<TreeNode> {
        const node: Array<TreeNode> = [];
        arr.forEach((orderItem: OrderItem): void => {
            const innerNode: TreeNode = {
                data: orderItem
            };
            innerNode.data.OrderUIdValue = this.getDetailsForUId(this.orders, orderItem.OrderUId, 'OrderUId', 'Name');
                        
            innerNode.data.ProductUIdValue = this.getDetailsForUId(this.products, orderItem.ProductUId, 'ProductUId', 'Name');
                        
            innerNode.data.ShipperUIdValue = this.getDetailsForUId(this.shippers, orderItem.ShipperUId, 'ShipperUId', 'Name');
                        
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
        this.searchOrderItem = eventValue;
        if (eventValue && eventValue.length >= 3) {
            content.filterGlobal(eventValue, 'contains');
        } else {
            content.filterGlobal('', 'contains');
        }
    }

    addOrderItem(): void {
        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageOrderItem'],
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
	// @param {TreeNode<OrderItem>} event - event method param.
	// @returns {void} - onNodeSelected method returns void.
    */
    onNodeSelected(event: TreeNode<OrderItem>): void {
        setTimeout((): void => {
            if (!this.doubleClicked) {
                if (event.data) {
                    this.loadSelectedDetail(event.data.OrderItemUId, this.manageOrderItem.data);
                }
            } else {
                this.doubleClicked = false;
            }
        }, 100);
    }

    /**
	// Represents navigateToOrderItem method
	// @param {string} action - action method param.
	// @param {OrderItem} nodeData - nodeData method param.
	// @returns {void} - navigateToOrderItem method returns void.
    */
    navigateToOrderItem(nodeData: OrderItem, action: string): void {
        if (nodeData) {

            void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageOrderItem'],
                {
                    queryParams: {
                        action,
                        orderItemUId: nodeData.OrderItemUId,
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

        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageOrderItem'],
            {
                queryParams: {
                    action: 'Edit',
                    orderItemUId: this.selectedDetailNode.OrderItemUId,
                    screenGroup: this.screenGroup,
                    serviceGroup: this.serviceGroup
                }
            });
    }

    /**
	// Represents loadSelectedDetail method
	// @param {Guid} orderItemUId - orderItemUId method param.
	// @param {Array<TreeNode>} items - items method param.
	// @returns {void} - loadSelectedDetail method returns void.
    */
    loadSelectedDetail(orderItemUId: Guid, items: Array<TreeNode>): void {
        if (items) {
            this.selectedDetailNode = items.filter((x => x.data.OrderItemUId === orderItemUId))[0].data;
        }
    }
}
