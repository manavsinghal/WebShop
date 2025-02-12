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
import { ShoppingCart } from '../../../../../shared/models/domain/shopping-cart/shopping-cart.model';
import { ShoppingCartService } from '../../../../../core/services/domain/shopping-cart/shopping-cart.service';
import { Account } from '../../../../../shared/models/domain/account/account.model';
import { AccountService } from '../../../../../core/services/domain/account/account.service';
import { Customer } from '../../../../../shared/models/domain/customer/customer.model';
import { CustomerService } from '../../../../../core/services/domain/customer/customer.service';
import { Product } from '../../../../../shared/models/domain/product/product.model';
import { ProductService } from '../../../../../core/services/domain/product/product.service';
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

// Define the ManageShoppingCartComponent
@Component({
    // Selector for the component
    selector: 'app-manage-shopping-cart',
    // Template URL for the component
    templateUrl: './manage-shopping-cart.component.html',
    imports: [TranslateDirective, FormsModule, WhiteSpaceValidatorDirective, InputRestrictionDirective, NgClass, EmptyGuidValidatorDirective, DatePipe, TranslatePipe]
})
export class ManageShoppingCartComponent extends SharedComponent implements OnInit, OnDestroy {
    readonly route = inject(ActivatedRoute);
    readonly router = inject(Router);
    override readonly coreSessionService: CoreSessionService;
    readonly coreSubscriptionService = inject(CoreSubscriptionService);
    readonly translateService = inject(TranslateService);
    readonly modalService = inject(NgbModal);
    readonly shoppingCartService = inject(ShoppingCartService);
    readonly accountService = inject(AccountService);
    readonly customerService = inject(CustomerService);
    readonly productService = inject(ProductService);
    readonly rowStatusService = inject(RowStatusService);

    action!: string;
    createdByAccounts!: Array<Account>;
    customers!: Array<Customer>;
    modifiedByAccounts!: Array<Account>;
    products!: Array<Product>;
    rowStatuses!: Array<RowStatus>;
    screenGroup!: string;
    serviceGroup!: string;
    shoppingCart!: ShoppingCart;
    shoppingCartUId!: string;
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
       
        this.shoppingCartUId = this.route.snapshot.queryParams['shoppingCartUId'];
        this.action = this.route.snapshot.queryParams['action'];        
        this.getMasterData();
        this.getShoppingCart();
   
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
       this.productService.getProducts().subscribe((products: Array<Product>): void => {
           this.products = products;
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

    getShoppingCart(): void {
        if (this.shoppingCartUId) {
            this.shoppingCartService.getShoppingCarts(this.shoppingCartUId).subscribe((shoppingCarts: Array<ShoppingCart>): void => {
                this.shoppingCart = shoppingCarts[0];
            }, (error: HttpErrorResponse): void => {
                CoreNotificationService.handleError(error);
            });
        } else {
            this.addShoppingCart();
        }
    }

    addShoppingCart(): void {
       this.shoppingCart = new ShoppingCart();
       this.shoppingCart.ItemState = ItemState.Added;
       this.shoppingCart.ShoppingCartUId = Guid.newGuid();
       this.shoppingCart.CustomerUId = Guid.empty;
       this.shoppingCart.ProductUId = Guid.empty;
       this.shoppingCart.Quantity = 0;
	   this.shoppingCart.Rate = 0;
       this.shoppingCart.SortOrder = 50;
       this.shoppingCart.RowStatusUId = '00100000-0000-0000-0000-000000000000';
       this.setAuditFields(this.shoppingCart);
    }

    backToManageShoppingCart(): void {
        // Navigate to the view ShoppingCart page with query parameters
        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/viewShoppingCart'], {
             queryParams: {
                // Pass the screen group and service group as query parameters
                screenGroup: this.screenGroup,
                serviceGroup: this.serviceGroup    
             }   
        });
    }

    /**
	// Represents saveShoppingCart method
	// @param {NgForm} shoppingCartForm - shoppingCartForm method param.
	// @returns {void} - saveShoppingCart method returns void.
    */
    saveShoppingCart(shoppingCartForm: NgForm): void {
        // Check if the shoppingCart form is valid
        if (shoppingCartForm && shoppingCartForm.valid) {
           // Update the item state if it's unchanged
           this.shoppingCart.ItemState = this.shoppingCart.ItemState === ItemState.Unchanged ? ItemState.Modified : this.shoppingCart.ItemState;
           // Set the audit fields for the shoppingCart
           this.setAuditFields(this.shoppingCart);
           // Merge the shoppingCart changes and subscribe to the response
           this.shoppingCartService.mergeShoppingCarts([this.shoppingCart]).subscribe(
               // On success, show a success message and navigate back to manage ShoppingCart
               (): void => {
                    let successMsg: string = '';
                    this.translateService.get('SaveSuccess').subscribe((saveSuccessMsg: string): string => successMsg = saveSuccessMsg);
                    CoreNotificationService.showSuccess(successMsg);
                    this.backToManageShoppingCart();
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
        // Navigate back to the manage ShoppingCart page
        this.backToManageShoppingCart();
    }

    /**
	// Represents closeShoppingCart method
	// @param {NgForm} shoppingCartForm - shoppingCartForm method param.
	// @param {TemplateRef<string>} content - content method param.
	// @returns {void} - closeShoppingCart method returns void.
    */
    closeShoppingCart(shoppingCartForm: NgForm, content: TemplateRef<string>): void {
        // Check if the shoppingCart form is dirty or touched
        if (shoppingCartForm && (shoppingCartForm.dirty || shoppingCartForm.touched)) {
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
