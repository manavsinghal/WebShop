/* Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright>
*/

// Import necessary Angular modules and services
import { Component, OnInit, OnDestroy, TemplateRef, inject } from '@angular/core';
import { Subscription } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { Guid } from '../../../../../core/helpers/guid';
import { SharedComponent } from '../../../../../shared/shared.component';
import { NgForm, FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { TranslateService, TranslateDirective, TranslatePipe } from '@ngx-translate/core';
import { OrderItem } from '../../../../../shared/models/domain/order/order-item.model';
import { OrderItemService } from '../../../../../core/services/domain/order/order-item.service';
import { Account } from '../../../../../shared/models/domain/account/account.model';
import { AccountService } from '../../../../../core/services/domain/account/account.service';
import { Order } from '../../../../../shared/models/domain/order/order.model';
import { OrderService } from '../../../../../core/services/domain/order/order.service';
import { Product } from '../../../../../shared/models/domain/product/product.model';
import { ProductService } from '../../../../../core/services/domain/product/product.service';
import { RowStatus } from '../../../../../shared/models/domain/master/row-status.model';
import { RowStatusService } from '../../../../../core/services/domain/master/row-status.service';
import { Shipper } from '../../../../../shared/models/domain/shipper/shipper.model';
import { ShipperService } from '../../../../../core/services/domain/shipper/shipper.service';
import { CoreNotificationService } from '../../../../../core/services/core.notification.service';
import { CoreSubscriptionService } from '../../../../../core/services/core.subscription.service';
import { CoreSessionService } from '../../../../../core/services/core.session.service';
import { ItemState } from '../../../../../shared/models/item-state.model';
import { WhiteSpaceValidatorDirective } from '../../../../../shared/directives/whitespace.directive';
import { InputRestrictionDirective } from '../../../../../shared/directives/restrict-input.directive';
import { NgClass, DatePipe } from '@angular/common';
import { EmptyGuidValidatorDirective } from '../../../../../shared/directives/empty-guid-validator.directive';

// Define the ManageOrderItemComponent
@Component({
    // Selector for the component
    selector: 'app-manage-order-item',
    // Template URL for the component
    templateUrl: './manage-order-item.component.html',
    imports: [TranslateDirective, FormsModule, WhiteSpaceValidatorDirective, InputRestrictionDirective, NgClass, EmptyGuidValidatorDirective, DatePipe, TranslatePipe]
})
export class ManageOrderItemComponent extends SharedComponent implements OnInit, OnDestroy {
    readonly route = inject(ActivatedRoute);
    readonly router = inject(Router);
    override readonly coreSessionService: CoreSessionService;
    readonly coreSubscriptionService = inject(CoreSubscriptionService);
    readonly translateService = inject(TranslateService);
    readonly modalService = inject(NgbModal);
    readonly orderItemService = inject(OrderItemService);
    readonly accountService = inject(AccountService);
    readonly orderService = inject(OrderService);
    readonly productService = inject(ProductService);
    readonly rowStatusService = inject(RowStatusService);
    readonly shipperService = inject(ShipperService);

    action!: string;
    createdByAccounts!: Array<Account>;
    modifiedByAccounts!: Array<Account>;
    orderItem!: OrderItem;
    orderItemUId!: string;
    orders!: Array<Order>;
    products!: Array<Product>;
    rowStatuses!: Array<RowStatus>;
    screenGroup!: string;
    serviceGroup!: string;
    shippers!: Array<Shipper>;
    emptyGuid: Guid = Guid.empty;
 
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
       
        this.orderItemUId = this.route.snapshot.queryParams['orderItemUId'];
        this.action = this.route.snapshot.queryParams['action'];        
        this.getMasterData();
        this.getOrderItem();
   
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
        // On error, handle the error
        },(error: HttpErrorResponse): void => {
            CoreNotificationService.handleError(error);
        });
       this.productService.getProducts().subscribe((products: Array<Product>): void => {
           this.products = products;
        // On error, handle the error
        },(error: HttpErrorResponse): void => {
            CoreNotificationService.handleError(error);
        });
       this.shipperService.getShippers().subscribe((shippers: Array<Shipper>): void => {
           this.shippers = shippers;
        // On error, handle the error
        },(error: HttpErrorResponse): void => {
            CoreNotificationService.handleError(error);
        });
       this.rowStatusService.getRowStatuses().subscribe((rowStatuses: Array<RowStatus>): void => {
           this.rowStatuses = rowStatuses;
        // On error, handle the error
        },(error: HttpErrorResponse): void => {
            CoreNotificationService.handleError(error);
        });
       this.accountService.getAccounts().subscribe((accounts: Array<Account>): void => {
           this.createdByAccounts = accounts;
        // On error, handle the error
        },(error: HttpErrorResponse): void => {
            CoreNotificationService.handleError(error);
        });
       this.accountService.getAccounts().subscribe((accounts: Array<Account>): void => {
           this.modifiedByAccounts = accounts;
        // On error, handle the error
        },(error: HttpErrorResponse): void => {
            CoreNotificationService.handleError(error);
        });
    }

    navigateToManageEntity(): void {
        void this.router.navigate(['/app/administration/manageAdminEntities'], {
            queryParams: {
               screenGroup: this.screenGroup,
               serviceGroup: this.serviceGroup
            }
        });
    }

    backToAdministration(): void {
        // Navigate to the administration page
        void this.router.navigateByUrl('/app/administration');
    }

    getOrderItem(): void {
        if (this.orderItemUId) {
            this.orderItemService.getOrderItems(this.orderItemUId).subscribe((orderItems: Array<OrderItem>): void => {
                this.orderItem = orderItems[0];
            }, (error: HttpErrorResponse): void => {
                CoreNotificationService.handleError(error);
            });
        } else {
            this.addOrderItem();
        }
    }

    addOrderItem(): void {
       this.orderItem = new OrderItem();
       this.orderItem.ItemState = ItemState.Added;
       this.orderItem.OrderItemUId = Guid.newGuid();
       this.orderItem.OrderUId = Guid.empty;
       this.orderItem.ProductUId = Guid.empty;
       this.orderItem.ShipperUId = Guid.empty;
       this.orderItem.Quantity = 0;
	   this.orderItem.Rate = 0;
       this.orderItem.ShipperTrackingNumber = '';
       this.orderItem.RowStatusUId = '00100000-0000-0000-0000-000000000000';
       this.setAuditFields(this.orderItem);
    }

    backToManageOrderItem(): void {
        // Navigate to the view OrderItem page with query parameters
        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/viewOrderItem'], {
             queryParams: {
                // Pass the screen group and service group as query parameters
                screenGroup: this.screenGroup,
                serviceGroup: this.serviceGroup    
             }   
        });
    }

    /**
	// Represents saveOrderItem method
	// @param {NgForm} orderItemForm - orderItemForm method param.
	// @returns {void} - saveOrderItem method returns void.
    */
    saveOrderItem(orderItemForm: NgForm): void {
        // Check if the orderItem form is valid
        if (orderItemForm && orderItemForm.valid) {
           // Update the item state if it's unchanged
           this.orderItem.ItemState = this.orderItem.ItemState === ItemState.Unchanged ? ItemState.Modified : this.orderItem.ItemState;
           // Set the audit fields for the orderItem
           this.setAuditFields(this.orderItem);
           // Merge the orderItem changes and subscribe to the response
           this.orderItemService.mergeOrderItems([this.orderItem]).subscribe(
               // On success, show a success message and navigate back to manage OrderItem
               (): void => {
                    let successMsg: string = '';
                    this.translateService.get('SaveSuccess').subscribe((saveSuccessMsg: string): string => successMsg = saveSuccessMsg);
                    CoreNotificationService.showSuccess(successMsg);
                    this.backToManageOrderItem();
                // On error, handle the error
                },(error: HttpErrorResponse): void => {
                    CoreNotificationService.handleError(error);
                });
        } else {
            // If the form is invalid, show a warning message for mandatory fields
            let mandatoryCheck: string = '';
            this.translateService.get('MandatoryErrorMsg').subscribe((data: string): string => mandatoryCheck = data);
            CoreNotificationService.showWarning(mandatoryCheck);
        }
    }

    cancelModalPopup(): void {
        // Dismiss all modal windows
        this.modalService.dismissAll();
        // Navigate back to the manage OrderItem page
        this.backToManageOrderItem();
    }

    /**
	// Represents closeOrderItem method
	// @param {NgForm} orderItemForm - orderItemForm method param.
	// @param {TemplateRef<string>} content - content method param.
	// @returns {void} - closeOrderItem method returns void.
    */
    closeOrderItem(orderItemForm: NgForm, content: TemplateRef<string>): void {
        // Check if the orderItem form is dirty or touched
        if (orderItemForm && (orderItemForm.dirty || orderItemForm.touched)) {
            // Open a confirmation modal window with the provided content
            this.modalService.open(content, {
                // Prevent closing the modal by clicking on the backdrop
                backdrop: 'static',
                // Prevent closing the modal by pressing the escape key
                keyboard: false,
                // Set the modal size to medium
                size: 'md'
            });
        } else {
            // If the form is not dirty or touched, cancel and dismiss the modal popup
            this.cancelModalPopup();
        }
    }
}
