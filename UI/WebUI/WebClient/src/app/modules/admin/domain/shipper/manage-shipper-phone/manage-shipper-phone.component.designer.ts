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
import { ShipperPhone } from '../../../../../shared/models/domain/shipper/shipper-phone.model';
import { ShipperPhoneService } from '../../../../../core/services/domain/shipper/shipper-phone.service';
import { Account } from '../../../../../shared/models/domain/account/account.model';
import { AccountService } from '../../../../../core/services/domain/account/account.service';
import { Country } from '../../../../../shared/models/domain/master/country.model';
import { CountryService } from '../../../../../core/services/domain/master/country.service';
import { MasterListItem } from '../../../../../shared/models/domain/master/master-list-item.model';
import { MasterListItemService } from '../../../../../core/services/domain/master/master-list-item.service';
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

// Define the ManageShipperPhoneComponent
@Component({
    // Selector for the component
    selector: 'app-manage-shipper-phone',
    // Template URL for the component
    templateUrl: './manage-shipper-phone.component.html',
    imports: [TranslateDirective, FormsModule, WhiteSpaceValidatorDirective, InputRestrictionDirective, NgClass, EmptyGuidValidatorDirective, DatePipe, TranslatePipe]
})
export class ManageShipperPhoneComponent extends SharedComponent implements OnInit, OnDestroy {
    readonly route = inject(ActivatedRoute);
    readonly router = inject(Router);
    override readonly coreSessionService: CoreSessionService;
    readonly coreSubscriptionService = inject(CoreSubscriptionService);
    readonly translateService = inject(TranslateService);
    readonly modalService = inject(NgbModal);
    readonly shipperPhoneService = inject(ShipperPhoneService);
    readonly accountService = inject(AccountService);
    readonly countryService = inject(CountryService);
    readonly masterListItemService = inject(MasterListItemService);
    readonly rowStatusService = inject(RowStatusService);
    readonly shipperService = inject(ShipperService);

    action!: string;
    countries!: Array<Country>;
    createdByAccounts!: Array<Account>;
    modifiedByAccounts!: Array<Account>;
    phoneTypes!: Array<MasterListItem>;
    rowStatuses!: Array<RowStatus>;
    screenGroup!: string;
    serviceGroup!: string;
    shipperPhone!: ShipperPhone;
    shipperPhoneUId!: string;
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
       
        this.shipperPhoneUId = this.route.snapshot.queryParams['shipperPhoneUId'];
        this.action = this.route.snapshot.queryParams['action'];        
        this.getMasterData();
        this.getShipperPhone();
   
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
       this.shipperService.getShippers().subscribe((shippers: Array<Shipper>): void => {
           this.shippers = shippers;
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

    getShipperPhone(): void {
        if (this.shipperPhoneUId) {
            this.shipperPhoneService.getShipperPhones(this.shipperPhoneUId).subscribe((shipperPhones: Array<ShipperPhone>): void => {
                this.shipperPhone = shipperPhones[0];
            }, (error: HttpErrorResponse): void => {
                CoreNotificationService.handleError(error);
            });
        } else {
            this.addShipperPhone();
        }
    }

    addShipperPhone(): void {
       this.shipperPhone = new ShipperPhone();
       this.shipperPhone.ItemState = ItemState.Added;
       this.shipperPhone.ShipperPhoneUId = Guid.newGuid();
       this.shipperPhone.ShipperUId = Guid.empty;
       this.shipperPhone.PhoneTypeUId = Guid.empty;
       this.shipperPhone.CountryUId = Guid.empty;
       this.shipperPhone.PhoneNumber = '';
       this.shipperPhone.IsPreferred = false;
       this.shipperPhone.SortOrder = 50;
       this.shipperPhone.RowStatusUId = '00100000-0000-0000-0000-000000000000';
       this.setAuditFields(this.shipperPhone);
    }

    backToManageShipperPhone(): void {
        // Navigate to the view ShipperPhone page with query parameters
        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/viewShipperPhone'], {
             queryParams: {
                // Pass the screen group and service group as query parameters
                screenGroup: this.screenGroup,
                serviceGroup: this.serviceGroup    
             }   
        });
    }

    /**
	// Represents saveShipperPhone method
	// @param {NgForm} shipperPhoneForm - shipperPhoneForm method param.
	// @returns {void} - saveShipperPhone method returns void.
    */
    saveShipperPhone(shipperPhoneForm: NgForm): void {
        // Check if the shipperPhone form is valid
        if (shipperPhoneForm && shipperPhoneForm.valid) {
           // Update the item state if it's unchanged
           this.shipperPhone.ItemState = this.shipperPhone.ItemState === ItemState.Unchanged ? ItemState.Modified : this.shipperPhone.ItemState;
           // Set the audit fields for the shipperPhone
           this.setAuditFields(this.shipperPhone);
           // Merge the shipperPhone changes and subscribe to the response
           this.shipperPhoneService.mergeShipperPhones([this.shipperPhone]).subscribe(
               // On success, show a success message and navigate back to manage ShipperPhone
               (): void => {
                    let successMsg: string = '';
                    this.translateService.get('SaveSuccess').subscribe((saveSuccessMsg: string): string => successMsg = saveSuccessMsg);
                    CoreNotificationService.showSuccess(successMsg);
                    this.backToManageShipperPhone();
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
        // Navigate back to the manage ShipperPhone page
        this.backToManageShipperPhone();
    }

    /**
	// Represents closeShipperPhone method
	// @param {TemplateRef<string>} content - content method param.
	// @param {NgForm} shipperPhoneForm - shipperPhoneForm method param.
	// @returns {void} - closeShipperPhone method returns void.
    */
    closeShipperPhone(shipperPhoneForm: NgForm, content: TemplateRef<string>): void {
        // Check if the shipperPhone form is dirty or touched
        if (shipperPhoneForm && (shipperPhoneForm.dirty || shipperPhoneForm.touched)) {
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
