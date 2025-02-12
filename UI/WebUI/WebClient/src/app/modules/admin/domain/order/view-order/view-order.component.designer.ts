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
import { Order } from '../../../../../shared/models/domain/order/order.model';
import { OrderService } from '../../../../../core/services/domain/order/order.service';
import { Customer } from '../../../../../shared/models/domain/customer/customer.model';
import { CustomerService } from '../../../../../core/services/domain/customer/customer.service';
import { MasterListItem } from '../../../../../shared/models/domain/master/master-list-item.model';
import { MasterListItemService } from '../../../../../core/services/domain/master/master-list-item.service';
import { NgClass } from '@angular/common';

// Define the ViewOrderComponent
@Component({
    // Selector for the component
    selector: 'app-view-order',
    // Template URL for the component
    templateUrl: './view-order.component.html',
    imports: [RouterLink, TranslateDirective, TreeTableModule, PrimeTemplate, NgClass, TranslatePipe]
})
export class ViewOrderComponent extends SharedComponent implements OnInit, OnDestroy {
    private readonly router = inject(Router);
    private readonly route = inject(ActivatedRoute);
    readonly customerService = inject(CustomerService);
    readonly masterListItemService = inject(MasterListItemService);
    readonly orderService = inject(OrderService);
    override readonly coreSessionService: CoreSessionService;
    readonly coreSubscriptionService = inject(CoreSubscriptionService);
    private readonly translateService = inject(TranslateService);

    readonly viewOrdersTable = viewChild.required<TreeTable>('this.ViewOrdersTable');  
    manageOrder: CustomTreeTableModel<TreeNode<Order>> = new CustomTreeTableModel<TreeNode<Order>>({
        data: new Array<TreeNode<Order>>()
    });
    selectedDetailNode: Order = {} as Order;
    sortCols!: Array<CustomSortModel>;
    cols!: Array<TreeTableHeaderModel>;
    customers!: Array<Customer>;
    doubleClicked!: boolean;
    manageOrderData!: Array<Order>;
    orders!: Array<Order>;
    orderStatuses!: Array<MasterListItem>;
    screenGroup!: string;
    searchOrder!: string;
    selectedNode!: TreeTableNode | null;
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
                 { field: 'CustomerUIdValue', header: 'Customer', class:'text-left' },          
                 { field: 'Name', header: 'Name', class: 'text-left' },
                 { field: 'Date', header: 'Date', class: 'text-center' },
                 { field: 'OrderStatusUIdValue', header: 'OrderStatus', class:'text-left' },          
        ];
        this.sortCols = [
                 { field: 'CustomerUIdValue', type: 'string' },
                 { field: 'Name', type: 'string' },
                 { field: 'Date', type: 'string' },
                 { field: 'OrderStatusUIdValue', type: 'string' },
        ];
        this.getMasterData();
        this.getOrders();
      
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
           this.refreshOrder();
        // On error, handle the error
        },(error: HttpErrorResponse): void => {
            CoreNotificationService.handleError(error);
        });
        this.masterListItemService.getMasterListItemsByCode('OrderStatus').subscribe((masterListItems: Array<MasterListItem>): void => {
           this.orderStatuses = masterListItems;
           this.refreshOrder();
        // On error, handle the error
        },(error: HttpErrorResponse): void => {
            CoreNotificationService.handleError(error);
        });
    }
    
    /**
	// Represents refreshOrder method
    // Method to refresh grid data.
	// @returns {void} - refreshOrder method returns void.
    */
    refreshOrder(): void {
        if (this.orders && this.orders.length > 0) {
            this.buildOrderTree(this.orders);
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

    getOrders(): void {
       this.orderService.getOrders()
            .subscribe((orders: Array<Order>): void => {
                if (orders && orders.length) {
                    this.orders = orders;
                    this.buildOrderTree(orders);
                }
            // On error, handle the error
            }, (error: HttpErrorResponse): void => {
                CoreNotificationService.handleError(error);
            }); 
    }

    /**
	// Represents buildOrderTree method
	// @param {Array<Order>} activeOrders - activeOrders method param.
	// @returns {void} - buildOrderTree method returns void.
    */
    buildOrderTree(activeOrders: Array<Order>): void {
        const orders: Array<TreeNode> = this.getOrderNode(activeOrders);
        this.manageOrder.data = orders;
        this.manageOrder.data = [...this.customSortMultipleColumns(this.manageOrder.data, this.sortCols)];
    }

    /**
	// Represents getOrderNode method
	// @param {Array<Order>} arr - arr method param.
	// @returns {Array<TreeNode>} - getOrderNode method returns Array<TreeNode>.
    */
    getOrderNode(arr: Array<Order>): Array<TreeNode> {
        const node: Array<TreeNode> = [];
        arr.forEach((order: Order): void => {
            const innerNode: TreeNode = {
                data: order
            };
            innerNode.data.CustomerUIdValue = this.getDetailsForUId(this.customers, order.CustomerUId, 'CustomerUId', 'Name');
                        
            innerNode.data.OrderStatusUIdValue = this.getDetailsForUId(this.orderStatuses, order.OrderStatusUId, 'MasterListItemUId', 'Name');
                        
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
        this.searchOrder = eventValue;
        if (eventValue && eventValue.length >= 3) {
            content.filterGlobal(eventValue, 'contains');
        } else {
            content.filterGlobal('', 'contains');
        }
    }

    addOrder(): void {
        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageOrder'],
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
	// @param {TreeNode<Order>} event - event method param.
	// @returns {void} - onNodeSelected method returns void.
    */
    onNodeSelected(event: TreeNode<Order>): void {
        setTimeout((): void => {
            if (!this.doubleClicked) {
                if (event.data) {
                    this.loadSelectedDetail(event.data.OrderUId, this.manageOrder.data);
                }
            } else {
                this.doubleClicked = false;
            }
        }, 100);
    }

    /**
	// Represents navigateToOrder method
	// @param {Order} nodeData - nodeData method param.
	// @param {string} action - action method param.
	// @returns {void} - navigateToOrder method returns void.
    */
    navigateToOrder(nodeData: Order, action: string): void {
        if (nodeData) {

            void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageOrder'],
                {
                    queryParams: {
                        action,
                        orderUId: nodeData.OrderUId,
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

        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageOrder'],
            {
                queryParams: {
                    action: 'Edit',
                    orderUId: this.selectedDetailNode.OrderUId,
                    screenGroup: this.screenGroup,
                    serviceGroup: this.serviceGroup
                }
            });
    }

    /**
	// Represents loadSelectedDetail method
	// @param {Guid} orderUId - orderUId method param.
	// @param {Array<TreeNode>} items - items method param.
	// @returns {void} - loadSelectedDetail method returns void.
    */
    loadSelectedDetail(orderUId: Guid, items: Array<TreeNode>): void {
        if (items) {
            this.selectedDetailNode = items.filter((x => x.data.OrderUId === orderUId))[0].data;
        }
    }
}
