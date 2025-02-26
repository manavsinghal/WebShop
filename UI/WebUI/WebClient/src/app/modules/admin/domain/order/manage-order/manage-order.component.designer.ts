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
import { Order } from '../../../../../shared/models/domain/order/order.model';
import { OrderService } from '../../../../../core/services/domain/order/order.service';
import { Account } from '../../../../../shared/models/domain/account/account.model';
import { AccountService } from '../../../../../core/services/domain/account/account.service';
import { Customer } from '../../../../../shared/models/domain/customer/customer.model';
import { CustomerService } from '../../../../../core/services/domain/customer/customer.service';
import { MasterListItem } from '../../../../../shared/models/domain/master/master-list-item.model';
import { MasterListItemService } from '../../../../../core/services/domain/master/master-list-item.service';
import { RowStatus } from '../../../../../shared/models/domain/master/row-status.model';
import { RowStatusService } from '../../../../../core/services/domain/master/row-status.service';
import { CoreNotificationService } from '../../../../../core/services/core.notification.service';
import { CoreSubscriptionService } from '../../../../../core/services/core.subscription.service';
import { CoreSessionService } from '../../../../../core/services/core.session.service';
import { ItemState } from '../../../../../shared/models/item-state.model';
import { WhiteSpaceValidatorDirective } from '../../../../../shared/directives/whitespace.directive';
import { InputRestrictionDirective } from '../../../../../shared/directives/restrict-input.directive';
import { NgClass, DatePipe } from '@angular/common';
import { EmptyGuidValidatorDirective } from '../../../../../shared/directives/empty-guid-validator.directive';

// Define the ManageOrderComponent
@Component({
    // Selector for the component
    selector: 'app-manage-order',
    // Template URL for the component
    templateUrl: './manage-order.component.html',
    imports: [TranslateDirective, FormsModule, WhiteSpaceValidatorDirective, InputRestrictionDirective, NgClass, EmptyGuidValidatorDirective, DatePipe, TranslatePipe]
})
export class ManageOrderComponent extends SharedComponent implements OnInit, OnDestroy {
    readonly route = inject(ActivatedRoute);
    readonly router = inject(Router);
    override readonly coreSessionService: CoreSessionService;
    readonly coreSubscriptionService = inject(CoreSubscriptionService);
    readonly translateService = inject(TranslateService);
    readonly modalService = inject(NgbModal);
    readonly orderService = inject(OrderService);
    readonly accountService = inject(AccountService);
    readonly customerService = inject(CustomerService);
    readonly masterListItemService = inject(MasterListItemService);
    readonly rowStatusService = inject(RowStatusService);

    action!: string;
    createdByAccounts!: Array<Account>;
    customers!: Array<Customer>;
    modifiedByAccounts!: Array<Account>;
    order!: Order;
    orderStatuses!: Array<MasterListItem>;
    orderUId!: string;
    rowStatuses!: Array<RowStatus>;
    screenGroup!: string;
    serviceGroup!: string;
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
       
        this.orderUId = this.route.snapshot.queryParams['orderUId'];
        this.action = this.route.snapshot.queryParams['action'];        
        this.getMasterData();
        this.getOrder();
   
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
        // On error, handle the error
        },(error: HttpErrorResponse): void => {
            CoreNotificationService.handleError(error);
        });
        this.masterListItemService.getMasterListItemsByCode('OrderStatus').subscribe((masterListItems: Array<MasterListItem>): void => {
           this.orderStatuses = masterListItems;
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

    getOrder(): void {
        if (this.orderUId) {
            this.orderService.getOrders(this.orderUId).subscribe((orders: Array<Order>): void => {
                this.order = orders[0];
            }, (error: HttpErrorResponse): void => {
                CoreNotificationService.handleError(error);
            });
        } else {
            this.addOrder();
        }
    }

    addOrder(): void {
       this.order = new Order();
       this.order.ItemState = ItemState.Added;
       this.order.OrderUId = Guid.newGuid();
       this.order.CustomerUId = Guid.empty;
       this.order.Name = '';
       this.order.Date = new Date();
       this.order.Date = new Date();
       this.order.OrderStatusUId = Guid.empty;
       this.order.RowStatusUId = '00100000-0000-0000-0000-000000000000';
       this.setAuditFields(this.order);
    }

    backToManageOrder(): void {
        // Navigate to the view Order page with query parameters
        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/viewOrder'], {
             queryParams: {
                // Pass the screen group and service group as query parameters
                screenGroup: this.screenGroup,
                serviceGroup: this.serviceGroup    
             }   
        });
    }

    /**
	// Represents saveOrder method
	// @param {NgForm} orderForm - orderForm method param.
	// @returns {void} - saveOrder method returns void.
    */
    saveOrder(orderForm: NgForm): void {
        // Check if the order form is valid
        if (orderForm && orderForm.valid) {
           // Update the item state if it's unchanged
           this.order.ItemState = this.order.ItemState === ItemState.Unchanged ? ItemState.Modified : this.order.ItemState;
           // Set the audit fields for the order
           this.setAuditFields(this.order);
           // Merge the order changes and subscribe to the response
           this.orderService.mergeOrders([this.order]).subscribe(
               // On success, show a success message and navigate back to manage Order
               (): void => {
                    let successMsg: string = '';
                    this.translateService.get('SaveSuccess').subscribe((saveSuccessMsg: string): string => successMsg = saveSuccessMsg);
                    CoreNotificationService.showSuccess(successMsg);
                    this.backToManageOrder();
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
        // Navigate back to the manage Order page
        this.backToManageOrder();
    }

    /**
	// Represents closeOrder method
	// @param {TemplateRef<string>} content - content method param.
	// @param {NgForm} orderForm - orderForm method param.
	// @returns {void} - closeOrder method returns void.
    */
    closeOrder(orderForm: NgForm, content: TemplateRef<string>): void {
        // Check if the order form is dirty or touched
        if (orderForm && (orderForm.dirty || orderForm.touched)) {
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
