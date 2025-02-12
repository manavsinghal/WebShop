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
import { OrderItemShipment } from '../../../../../shared/models/domain/order/order-item-shipment.model';
import { OrderItemShipmentService } from '../../../../../core/services/domain/order/order-item-shipment.service';
import { Account } from '../../../../../shared/models/domain/account/account.model';
import { AccountService } from '../../../../../core/services/domain/account/account.service';
import { OrderItem } from '../../../../../shared/models/domain/order/order-item.model';
import { OrderItemService } from '../../../../../core/services/domain/order/order-item.service';
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

// Define the ManageOrderItemShipmentComponent
@Component({
    // Selector for the component
    selector: 'app-manage-order-item-shipment',
    // Template URL for the component
    templateUrl: './manage-order-item-shipment.component.html',
    imports: [TranslateDirective, FormsModule, WhiteSpaceValidatorDirective, InputRestrictionDirective, NgClass, EmptyGuidValidatorDirective, DatePipe, TranslatePipe]
})
export class ManageOrderItemShipmentComponent extends SharedComponent implements OnInit, OnDestroy {
    readonly route = inject(ActivatedRoute);
    readonly router = inject(Router);
    override readonly coreSessionService: CoreSessionService;
    readonly coreSubscriptionService = inject(CoreSubscriptionService);
    readonly translateService = inject(TranslateService);
    readonly modalService = inject(NgbModal);
    readonly orderItemShipmentService = inject(OrderItemShipmentService);
    readonly accountService = inject(AccountService);
    readonly orderItemService = inject(OrderItemService);
    readonly rowStatusService = inject(RowStatusService);

    action!: string;
    createdByAccounts!: Array<Account>;
    modifiedByAccounts!: Array<Account>;
    orderItems!: Array<OrderItem>;
    orderItemShipment!: OrderItemShipment;
    orderItemShipmentUId!: string;
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
       
        this.orderItemShipmentUId = this.route.snapshot.queryParams['orderItemShipmentUId'];
        this.action = this.route.snapshot.queryParams['action'];        
        this.getMasterData();
        this.getOrderItemShipment();
   
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

    getOrderItemShipment(): void {
        if (this.orderItemShipmentUId) {
            this.orderItemShipmentService.getOrderItemShipments(this.orderItemShipmentUId).subscribe((orderItemShipments: Array<OrderItemShipment>): void => {
                this.orderItemShipment = orderItemShipments[0];
            }, (error: HttpErrorResponse): void => {
                CoreNotificationService.handleError(error);
            });
        } else {
            this.addOrderItemShipment();
        }
    }

    addOrderItemShipment(): void {
       this.orderItemShipment = new OrderItemShipment();
       this.orderItemShipment.ItemState = ItemState.Added;
       this.orderItemShipment.OrderItemShipmentUId = Guid.newGuid();
       this.orderItemShipment.OrderItemUId = Guid.empty;
       this.orderItemShipment.RowStatusUId = '00100000-0000-0000-0000-000000000000';
       this.setAuditFields(this.orderItemShipment);
    }

    backToManageOrderItemShipment(): void {
        // Navigate to the view OrderItemShipment page with query parameters
        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/viewOrderItemShipment'], {
             queryParams: {
                // Pass the screen group and service group as query parameters
                screenGroup: this.screenGroup,
                serviceGroup: this.serviceGroup    
             }   
        });
    }

    /**
	// Represents saveOrderItemShipment method
	// @param {NgForm} orderItemShipmentForm - orderItemShipmentForm method param.
	// @returns {void} - saveOrderItemShipment method returns void.
    */
    saveOrderItemShipment(orderItemShipmentForm: NgForm): void {
        // Check if the orderItemShipment form is valid
        if (orderItemShipmentForm && orderItemShipmentForm.valid) {
           // Update the item state if it's unchanged
           this.orderItemShipment.ItemState = this.orderItemShipment.ItemState === ItemState.Unchanged ? ItemState.Modified : this.orderItemShipment.ItemState;
           // Set the audit fields for the orderItemShipment
           this.setAuditFields(this.orderItemShipment);
           // Merge the orderItemShipment changes and subscribe to the response
           this.orderItemShipmentService.mergeOrderItemShipments([this.orderItemShipment]).subscribe(
               // On success, show a success message and navigate back to manage OrderItemShipment
               (): void => {
                    let successMsg: string = '';
                    this.translateService.get('SaveSuccess').subscribe((saveSuccessMsg: string): string => successMsg = saveSuccessMsg);
                    CoreNotificationService.showSuccess(successMsg);
                    this.backToManageOrderItemShipment();
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
        // Navigate back to the manage OrderItemShipment page
        this.backToManageOrderItemShipment();
    }

    /**
	// Represents closeOrderItemShipment method
	// @param {TemplateRef<string>} content - content method param.
	// @param {NgForm} orderItemShipmentForm - orderItemShipmentForm method param.
	// @returns {void} - closeOrderItemShipment method returns void.
    */
    closeOrderItemShipment(orderItemShipmentForm: NgForm, content: TemplateRef<string>): void {
        // Check if the orderItemShipment form is dirty or touched
        if (orderItemShipmentForm && (orderItemShipmentForm.dirty || orderItemShipmentForm.touched)) {
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
