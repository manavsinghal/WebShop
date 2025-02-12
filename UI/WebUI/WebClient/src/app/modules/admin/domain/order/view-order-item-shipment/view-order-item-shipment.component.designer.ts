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
import { OrderItemShipment } from '../../../../../shared/models/domain/order/order-item-shipment.model';
import { OrderItemShipmentService } from '../../../../../core/services/domain/order/order-item-shipment.service';
import { OrderItem } from '../../../../../shared/models/domain/order/order-item.model';
import { OrderItemService } from '../../../../../core/services/domain/order/order-item.service';
import { NgClass } from '@angular/common';

// Define the ViewOrderItemShipmentComponent
@Component({
    // Selector for the component
    selector: 'app-view-order-item-shipment',
    // Template URL for the component
    templateUrl: './view-order-item-shipment.component.html',
    imports: [RouterLink, TranslateDirective, TreeTableModule, PrimeTemplate, NgClass, TranslatePipe]
})
export class ViewOrderItemShipmentComponent extends SharedComponent implements OnInit, OnDestroy {
    private readonly router = inject(Router);
    private readonly route = inject(ActivatedRoute);
    readonly orderItemService = inject(OrderItemService);
    readonly orderItemShipmentService = inject(OrderItemShipmentService);
    override readonly coreSessionService: CoreSessionService;
    readonly coreSubscriptionService = inject(CoreSubscriptionService);
    private readonly translateService = inject(TranslateService);

    readonly viewOrderItemShipmentsTable = viewChild.required<TreeTable>('this.ViewOrderItemShipmentsTable');  
    manageOrderItemShipment: CustomTreeTableModel<TreeNode<OrderItemShipment>> = new CustomTreeTableModel<TreeNode<OrderItemShipment>>({
        data: new Array<TreeNode<OrderItemShipment>>()
    });
    selectedDetailNode: OrderItemShipment = {} as OrderItemShipment;
    sortCols!: Array<CustomSortModel>;
    cols!: Array<TreeTableHeaderModel>;
    doubleClicked!: boolean;
    manageOrderItemShipmentData!: Array<OrderItemShipment>;
    orderItems!: Array<OrderItem>;
    orderItemShipments!: Array<OrderItemShipment>;
    screenGroup!: string;
    searchOrderItemShipment!: string;
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
                 { field: 'OrderItemUIdValue', header: 'OrderItem', class:'text-left' },          
        ];
        this.sortCols = [
                 { field: 'OrderItemUIdValue', type: 'string' },
        ];
        this.getMasterData();
        this.getOrderItemShipments();
      
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
       this.orderItemService.getOrderItems().subscribe((orderItems: Array<OrderItem>): void => {
           this.orderItems = orderItems;
           this.refreshOrderItemShipment();
        // On error, handle the error
        },(error: HttpErrorResponse): void => {
            CoreNotificationService.handleError(error);
        });
    }
    
    /**
	// Represents refreshOrderItemShipment method
    // Method to refresh grid data.
	// @returns {void} - refreshOrderItemShipment method returns void.
    */
    refreshOrderItemShipment(): void {
        if (this.orderItemShipments && this.orderItemShipments.length > 0) {
            this.buildOrderItemShipmentTree(this.orderItemShipments);
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

    getOrderItemShipments(): void {
       this.orderItemShipmentService.getOrderItemShipments()
            .subscribe((orderItemShipments: Array<OrderItemShipment>): void => {
                if (orderItemShipments && orderItemShipments.length) {
                    this.orderItemShipments = orderItemShipments;
                    this.buildOrderItemShipmentTree(orderItemShipments);
                }
            // On error, handle the error
            }, (error: HttpErrorResponse): void => {
                CoreNotificationService.handleError(error);
            }); 
    }

    /**
	// Represents buildOrderItemShipmentTree method
	// @param {Array<OrderItemShipment>} activeOrderItemShipments - activeOrderItemShipments method param.
	// @returns {void} - buildOrderItemShipmentTree method returns void.
    */
    buildOrderItemShipmentTree(activeOrderItemShipments: Array<OrderItemShipment>): void {
        const orderItemShipments: Array<TreeNode> = this.getOrderItemShipmentNode(activeOrderItemShipments);
        this.manageOrderItemShipment.data = orderItemShipments;
        this.manageOrderItemShipment.data = [...this.customSortMultipleColumns(this.manageOrderItemShipment.data, this.sortCols)];
    }

    /**
	// Represents getOrderItemShipmentNode method
	// @param {Array<OrderItemShipment>} arr - arr method param.
	// @returns {Array<TreeNode>} - getOrderItemShipmentNode method returns Array<TreeNode>.
    */
    getOrderItemShipmentNode(arr: Array<OrderItemShipment>): Array<TreeNode> {
        const node: Array<TreeNode> = [];
        arr.forEach((orderItemShipment: OrderItemShipment): void => {
            const innerNode: TreeNode = {
                data: orderItemShipment
            };
            innerNode.data.OrderItemUIdValue = this.getDetailsForUId(this.orderItems, orderItemShipment.OrderItemUId, 'OrderItemUId', 'ShipperTrackingNumber');
                        
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
        this.searchOrderItemShipment = eventValue;
        if (eventValue && eventValue.length >= 3) {
            content.filterGlobal(eventValue, 'contains');
        } else {
            content.filterGlobal('', 'contains');
        }
    }

    addOrderItemShipment(): void {
        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageOrderItemShipment'],
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
	// @param {TreeNode<OrderItemShipment>} event - event method param.
	// @returns {void} - onNodeSelected method returns void.
    */
    onNodeSelected(event: TreeNode<OrderItemShipment>): void {
        setTimeout((): void => {
            if (!this.doubleClicked) {
                if (event.data) {
                    this.loadSelectedDetail(event.data.OrderItemShipmentUId, this.manageOrderItemShipment.data);
                }
            } else {
                this.doubleClicked = false;
            }
        }, 100);
    }

    /**
	// Represents navigateToOrderItemShipment method
	// @param {string} action - action method param.
	// @param {OrderItemShipment} nodeData - nodeData method param.
	// @returns {void} - navigateToOrderItemShipment method returns void.
    */
    navigateToOrderItemShipment(nodeData: OrderItemShipment, action: string): void {
        if (nodeData) {

            void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageOrderItemShipment'],
                {
                    queryParams: {
                        action,
                        orderItemShipmentUId: nodeData.OrderItemShipmentUId,
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

        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageOrderItemShipment'],
            {
                queryParams: {
                    action: 'Edit',
                    orderItemShipmentUId: this.selectedDetailNode.OrderItemShipmentUId,
                    screenGroup: this.screenGroup,
                    serviceGroup: this.serviceGroup
                }
            });
    }

    /**
	// Represents loadSelectedDetail method
	// @param {Guid} orderItemShipmentUId - orderItemShipmentUId method param.
	// @param {Array<TreeNode>} items - items method param.
	// @returns {void} - loadSelectedDetail method returns void.
    */
    loadSelectedDetail(orderItemShipmentUId: Guid, items: Array<TreeNode>): void {
        if (items) {
            this.selectedDetailNode = items.filter((x => x.data.OrderItemShipmentUId === orderItemShipmentUId))[0].data;
        }
    }
}
