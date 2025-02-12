/* Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright>
*/

// Import necessary Angular modules and services
import { Component, OnInit, OnDestroy, TemplateRef } from '@angular/core';
import { Subscription } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { Guid } from '../../../../../core/helpers/guid';
import { SharedComponent } from '../../../../../shared/shared.component';
import { NgForm, FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { TranslateService, TranslateDirective, TranslatePipe } from '@ngx-translate/core';
import { CustomerPhone } from '../../../../../shared/models/domain/customer/customer-phone.model';
import { CustomerPhoneService } from '../../../../../core/services/domain/customer/customer-phone.service';
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

// Define the ManageCustomerPhoneComponent
@Component({
    // Selector for the component
    selector: 'app-manage-customer-phone',
    // Template URL for the component
    templateUrl: './manage-customer-phone.component.html',
    imports: [TranslateDirective, FormsModule, WhiteSpaceValidatorDirective, InputRestrictionDirective, NgClass, EmptyGuidValidatorDirective, DatePipe, TranslatePipe]
})
export class ManageCustomerPhoneComponent extends SharedComponent implements OnInit, OnDestroy {
    action!: string;
    countries!: Array<Country>;
    createdByAccounts!: Array<Account>;
    customerPhone!: CustomerPhone;
    customerPhoneUId!: string;
    customers!: Array<Customer>;
    modifiedByAccounts!: Array<Account>;
    phoneTypes!: Array<MasterListItem>;
    rowStatuses!: Array<RowStatus>;
    screenGroup!: string;
    serviceGroup!: string;
    emptyGuid: Guid = Guid.empty;
 
    languageChangedSubscription!: Subscription;

    // Constructor for the component
    constructor(readonly route: ActivatedRoute,
        readonly router: Router,
        override readonly coreSessionService: CoreSessionService,
        readonly coreSubscriptionService: CoreSubscriptionService,
        readonly translateService: TranslateService,
        readonly modalService: NgbModal,
        readonly customerPhoneService: CustomerPhoneService,
        readonly accountService : AccountService,
        readonly countryService : CountryService,
        readonly customerService : CustomerService,
        readonly masterListItemService : MasterListItemService,
        readonly rowStatusService : RowStatusService,
    ) {
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
       
        this.customerPhoneUId = this.route.snapshot.queryParams['customerPhoneUId'];
        this.action = this.route.snapshot.queryParams['action'];        
        this.getMasterData();
        this.getCustomerPhone();
   
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
        this.masterListItemService.getMasterListItemsByCode('PhoneType').subscribe((masterListItems: Array<MasterListItem>): void => {
           this.phoneTypes = masterListItems;
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

    getCustomerPhone(): void {
        if (this.customerPhoneUId) {
            this.customerPhoneService.getCustomerPhones(this.customerPhoneUId).subscribe((customerPhones: Array<CustomerPhone>): void => {
                this.customerPhone = customerPhones[0];
            }, (error: HttpErrorResponse): void => {
                CoreNotificationService.handleError(error);
            });
        } else {
            this.addCustomerPhone();
        }
    }

    addCustomerPhone(): void {
       this.customerPhone = new CustomerPhone();
       this.customerPhone.ItemState = ItemState.Added;
       this.customerPhone.CustomerPhoneUId = Guid.newGuid();
       this.customerPhone.CustomerUId = Guid.empty;
       this.customerPhone.PhoneTypeUId = Guid.empty;
       this.customerPhone.CountryUId = Guid.empty;
       this.customerPhone.PhoneNumber = '';
       this.customerPhone.IsPreferred = false;
       this.customerPhone.SortOrder = 50;
       this.customerPhone.RowStatusUId = '00100000-0000-0000-0000-000000000000';
       this.setAuditFields(this.customerPhone);
    }

    backToManageCustomerPhone(): void {
        // Navigate to the view CustomerPhone page with query parameters
        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/viewCustomerPhone'], {
             queryParams: {
                // Pass the screen group and service group as query parameters
                screenGroup: this.screenGroup,
                serviceGroup: this.serviceGroup    
             }   
        });
    }

    /**
	// Represents saveCustomerPhone method
	// @param {NgForm} customerPhoneForm - customerPhoneForm method param.
	// @returns {void} - saveCustomerPhone method returns void.
    */
    saveCustomerPhone(customerPhoneForm: NgForm): void {
        // Check if the customerPhone form is valid
        if (customerPhoneForm && customerPhoneForm.valid) {
           // Update the item state if it's unchanged
           this.customerPhone.ItemState = this.customerPhone.ItemState === ItemState.Unchanged ? ItemState.Modified : this.customerPhone.ItemState;
           // Set the audit fields for the customerPhone
           this.setAuditFields(this.customerPhone);
           // Merge the customerPhone changes and subscribe to the response
           this.customerPhoneService.mergeCustomerPhones([this.customerPhone]).subscribe(
               // On success, show a success message and navigate back to manage CustomerPhone
               (): void => {
                    let successMsg: string = '';
                    this.translateService.get('SaveSuccess').subscribe((saveSuccessMsg: string): string => successMsg = saveSuccessMsg);
                    CoreNotificationService.showSuccess(successMsg);
                    this.backToManageCustomerPhone();
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
        // Navigate back to the manage CustomerPhone page
        this.backToManageCustomerPhone();
    }

    /**
	// Represents closeCustomerPhone method
	// @param {TemplateRef<string>} content - content method param.
	// @param {NgForm} customerPhoneForm - customerPhoneForm method param.
	// @returns {void} - closeCustomerPhone method returns void.
    */
    closeCustomerPhone(customerPhoneForm: NgForm, content: TemplateRef<string>): void {
        // Check if the customerPhone form is dirty or touched
        if (customerPhoneForm && (customerPhoneForm.dirty || customerPhoneForm.touched)) {
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
