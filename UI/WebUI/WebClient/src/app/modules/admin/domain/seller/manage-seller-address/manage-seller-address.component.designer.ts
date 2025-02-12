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
import { SellerAddress } from '../../../../../shared/models/domain/seller/seller-address.model';
import { SellerAddressService } from '../../../../../core/services/domain/seller/seller-address.service';
import { Account } from '../../../../../shared/models/domain/account/account.model';
import { AccountService } from '../../../../../core/services/domain/account/account.service';
import { Country } from '../../../../../shared/models/domain/master/country.model';
import { CountryService } from '../../../../../core/services/domain/master/country.service';
import { MasterListItem } from '../../../../../shared/models/domain/master/master-list-item.model';
import { MasterListItemService } from '../../../../../core/services/domain/master/master-list-item.service';
import { RowStatus } from '../../../../../shared/models/domain/master/row-status.model';
import { RowStatusService } from '../../../../../core/services/domain/master/row-status.service';
import { Seller } from '../../../../../shared/models/domain/seller/seller.model';
import { SellerService } from '../../../../../core/services/domain/seller/seller.service';
import { CoreNotificationService } from '../../../../../core/services/core.notification.service';
import { CoreSubscriptionService } from '../../../../../core/services/core.subscription.service';
import { CoreSessionService } from '../../../../../core/services/core.session.service';
import { ItemState } from '../../../../../shared/models/item-state.model';
import { WhiteSpaceValidatorDirective } from '../../../../../shared/directives/whitespace.directive';
import { InputRestrictionDirective } from '../../../../../shared/directives/restrict-input.directive';
import { NgClass, DatePipe } from '@angular/common';
import { EmptyGuidValidatorDirective } from '../../../../../shared/directives/empty-guid-validator.directive';

// Define the ManageSellerAddressComponent
@Component({
    // Selector for the component
    selector: 'app-manage-seller-address',
    // Template URL for the component
    templateUrl: './manage-seller-address.component.html',
    imports: [TranslateDirective, FormsModule, WhiteSpaceValidatorDirective, InputRestrictionDirective, NgClass, EmptyGuidValidatorDirective, DatePipe, TranslatePipe]
})
export class ManageSellerAddressComponent extends SharedComponent implements OnInit, OnDestroy {
    action!: string;
    addressTypes!: Array<MasterListItem>;
    countries!: Array<Country>;
    createdByAccounts!: Array<Account>;
    modifiedByAccounts!: Array<Account>;
    rowStatuses!: Array<RowStatus>;
    screenGroup!: string;
    sellerAddress!: SellerAddress;
    sellerAddressUId!: string;
    sellers!: Array<Seller>;
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
        readonly sellerAddressService: SellerAddressService,
        readonly accountService : AccountService,
        readonly countryService : CountryService,
        readonly masterListItemService : MasterListItemService,
        readonly rowStatusService : RowStatusService,
        readonly sellerService : SellerService,
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
       
        this.sellerAddressUId = this.route.snapshot.queryParams['sellerAddressUId'];
        this.action = this.route.snapshot.queryParams['action'];        
        this.getMasterData();
        this.getSellerAddress();
   
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
       this.sellerService.getSellers().subscribe((sellers: Array<Seller>): void => {
           this.sellers = sellers;
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

    getSellerAddress(): void {
        if (this.sellerAddressUId) {
            this.sellerAddressService.getSellerAddresses(this.sellerAddressUId).subscribe((sellerAddresses: Array<SellerAddress>): void => {
                this.sellerAddress = sellerAddresses[0];
            }, (error: HttpErrorResponse): void => {
                CoreNotificationService.handleError(error);
            });
        } else {
            this.addSellerAddress();
        }
    }

    addSellerAddress(): void {
       this.sellerAddress = new SellerAddress();
       this.sellerAddress.ItemState = ItemState.Added;
       this.sellerAddress.SellerAddressUId = Guid.newGuid();
       this.sellerAddress.SellerUId = Guid.empty;
       this.sellerAddress.AddressTypeUId = Guid.empty;
       this.sellerAddress.Line1 = '';
       this.sellerAddress.Line2 = '';
       this.sellerAddress.Line3 = '';
       this.sellerAddress.CountryUId = Guid.empty;
       this.sellerAddress.StateCode = '';
       this.sellerAddress.ZipCode = '';
       this.sellerAddress.IsPreferred = false;
       this.sellerAddress.SortOrder = 50;
       this.sellerAddress.RowStatusUId = '00100000-0000-0000-0000-000000000000';
       this.setAuditFields(this.sellerAddress);
    }

    backToManageSellerAddress(): void {
        // Navigate to the view SellerAddress page with query parameters
        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/viewSellerAddress'], {
             queryParams: {
                // Pass the screen group and service group as query parameters
                screenGroup: this.screenGroup,
                serviceGroup: this.serviceGroup    
             }   
        });
    }

    /**
	// Represents saveSellerAddress method
	// @param {NgForm} sellerAddressForm - sellerAddressForm method param.
	// @returns {void} - saveSellerAddress method returns void.
    */
    saveSellerAddress(sellerAddressForm: NgForm): void {
        // Check if the sellerAddress form is valid
        if (sellerAddressForm && sellerAddressForm.valid) {
           // Update the item state if it's unchanged
           this.sellerAddress.ItemState = this.sellerAddress.ItemState === ItemState.Unchanged ? ItemState.Modified : this.sellerAddress.ItemState;
           // Set the audit fields for the sellerAddress
           this.setAuditFields(this.sellerAddress);
           // Merge the sellerAddress changes and subscribe to the response
           this.sellerAddressService.mergeSellerAddresses([this.sellerAddress]).subscribe(
               // On success, show a success message and navigate back to manage SellerAddress
               (): void => {
                    let successMsg: string = '';
                    this.translateService.get('SaveSuccess').subscribe((saveSuccessMsg: string): string => successMsg = saveSuccessMsg);
                    CoreNotificationService.showSuccess(successMsg);
                    this.backToManageSellerAddress();
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
        // Navigate back to the manage SellerAddress page
        this.backToManageSellerAddress();
    }

    /**
	// Represents closeSellerAddress method
	// @param {NgForm} sellerAddressForm - sellerAddressForm method param.
	// @param {TemplateRef<string>} content - content method param.
	// @returns {void} - closeSellerAddress method returns void.
    */
    closeSellerAddress(sellerAddressForm: NgForm, content: TemplateRef<string>): void {
        // Check if the sellerAddress form is dirty or touched
        if (sellerAddressForm && (sellerAddressForm.dirty || sellerAddressForm.touched)) {
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
