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
import { CustomerAddress } from '../../../../../shared/models/domain/customer/customer-address.model';
import { CustomerAddressService } from '../../../../../core/services/domain/customer/customer-address.service';
import { Account } from '../../../../../shared/models/domain/account/account.model';
import { AccountService } from '../../../../../core/services/domain/account/account.service';
import { Country } from '../../../../../shared/models/domain/master/country.model';
import { CountryService } from '../../../../../core/services/domain/master/country.service';
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

// Define the ManageCustomerAddressComponent
@Component({
    // Selector for the component
    selector: 'app-manage-customer-address',
    // Template URL for the component
    templateUrl: './manage-customer-address.component.html',
    imports: [TranslateDirective, FormsModule, WhiteSpaceValidatorDirective, InputRestrictionDirective, NgClass, EmptyGuidValidatorDirective, DatePipe, TranslatePipe]
})
export class ManageCustomerAddressComponent extends SharedComponent implements OnInit, OnDestroy {
    readonly route = inject(ActivatedRoute);
    readonly router = inject(Router);
    override readonly coreSessionService: CoreSessionService;
    readonly coreSubscriptionService = inject(CoreSubscriptionService);
    readonly translateService = inject(TranslateService);
    readonly modalService = inject(NgbModal);
    readonly customerAddressService = inject(CustomerAddressService);
    readonly accountService = inject(AccountService);
    readonly countryService = inject(CountryService);
    readonly customerService = inject(CustomerService);
    readonly masterListItemService = inject(MasterListItemService);
    readonly rowStatusService = inject(RowStatusService);

    action!: string;
    addressTypes!: Array<MasterListItem>;
    countries!: Array<Country>;
    createdByAccounts!: Array<Account>;
    customerAddress!: CustomerAddress;
    customerAddressUId!: string;
    customers!: Array<Customer>;
    modifiedByAccounts!: Array<Account>;
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
       
        this.customerAddressUId = this.route.snapshot.queryParams['customerAddressUId'];
        this.action = this.route.snapshot.queryParams['action'];        
        this.getMasterData();
        this.getCustomerAddress();
   
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
        this.countryService.getCountries().subscribe((countries: Array<Country>): void => {
            this.countries = countries;
        // On error, handle the error
        }, (error: HttpErrorResponse): void => {
            CoreNotificationService.handleError(error);
        });
        this.translateService.setDefaultLang(this.coreSessionService.getLanguage());
    }
 
    /**
	// Represents getMasterData method
    // Method to retrieve master data
	// @returns {void} - getMasterData method returns void.
    */
    getMasterData(): void {
           this.onLanguageChanged();
       this.customerService.getCustomers().subscribe((customers: Array<Customer>): void => {
           this.customers = customers;
        // On error, handle the error
        },(error: HttpErrorResponse): void => {
            CoreNotificationService.handleError(error);
        });
        this.masterListItemService.getMasterListItemsByCode('AddressType').subscribe((masterListItems: Array<MasterListItem>): void => {
           this.addressTypes = masterListItems;
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

    getCustomerAddress(): void {
        if (this.customerAddressUId) {
            this.customerAddressService.getCustomerAddresses(this.customerAddressUId).subscribe((customerAddresses: Array<CustomerAddress>): void => {
                this.customerAddress = customerAddresses[0];
            }, (error: HttpErrorResponse): void => {
                CoreNotificationService.handleError(error);
            });
        } else {
            this.addCustomerAddress();
        }
    }

    addCustomerAddress(): void {
       this.customerAddress = new CustomerAddress();
       this.customerAddress.ItemState = ItemState.Added;
       this.customerAddress.CustomerAddressUId = Guid.newGuid();
       this.customerAddress.CustomerUId = Guid.empty;
       this.customerAddress.AddressTypeUId = Guid.empty;
       this.customerAddress.Line1 = '';
       this.customerAddress.Line2 = '';
       this.customerAddress.Line3 = '';
       this.customerAddress.CountryUId = Guid.empty;
       this.customerAddress.StateCode = '';
       this.customerAddress.ZipCode = '';
       this.customerAddress.IsPreferred = false;
       this.customerAddress.SortOrder = 50;
       this.customerAddress.RowStatusUId = '00100000-0000-0000-0000-000000000000';
       this.setAuditFields(this.customerAddress);
    }

    backToManageCustomerAddress(): void {
        // Navigate to the view CustomerAddress page with query parameters
        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/viewCustomerAddress'], {
             queryParams: {
                // Pass the screen group and service group as query parameters
                screenGroup: this.screenGroup,
                serviceGroup: this.serviceGroup    
             }   
        });
    }

    /**
	// Represents saveCustomerAddress method
	// @param {NgForm} customerAddressForm - customerAddressForm method param.
	// @returns {void} - saveCustomerAddress method returns void.
    */
    saveCustomerAddress(customerAddressForm: NgForm): void {
        // Check if the customerAddress form is valid
        if (customerAddressForm && customerAddressForm.valid) {
           // Update the item state if it's unchanged
           this.customerAddress.ItemState = this.customerAddress.ItemState === ItemState.Unchanged ? ItemState.Modified : this.customerAddress.ItemState;
           // Set the audit fields for the customerAddress
           this.setAuditFields(this.customerAddress);
           // Merge the customerAddress changes and subscribe to the response
           this.customerAddressService.mergeCustomerAddresses([this.customerAddress]).subscribe(
               // On success, show a success message and navigate back to manage CustomerAddress
               (): void => {
                    let successMsg: string = '';
                    this.translateService.get('SaveSuccess').subscribe((saveSuccessMsg: string): string => successMsg = saveSuccessMsg);
                    CoreNotificationService.showSuccess(successMsg);
                    this.backToManageCustomerAddress();
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
        // Navigate back to the manage CustomerAddress page
        this.backToManageCustomerAddress();
    }

    /**
	// Represents closeCustomerAddress method
	// @param {TemplateRef<string>} content - content method param.
	// @param {NgForm} customerAddressForm - customerAddressForm method param.
	// @returns {void} - closeCustomerAddress method returns void.
    */
    closeCustomerAddress(customerAddressForm: NgForm, content: TemplateRef<string>): void {
        // Check if the customerAddress form is dirty or touched
        if (customerAddressForm && (customerAddressForm.dirty || customerAddressForm.touched)) {
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
