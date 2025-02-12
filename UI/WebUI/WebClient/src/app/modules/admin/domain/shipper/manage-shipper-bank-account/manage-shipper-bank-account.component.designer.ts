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
import { ShipperBankAccount } from '../../../../../shared/models/domain/shipper/shipper-bank-account.model';
import { ShipperBankAccountService } from '../../../../../core/services/domain/shipper/shipper-bank-account.service';
import { Account } from '../../../../../shared/models/domain/account/account.model';
import { AccountService } from '../../../../../core/services/domain/account/account.service';
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

// Define the ManageShipperBankAccountComponent
@Component({
    // Selector for the component
    selector: 'app-manage-shipper-bank-account',
    // Template URL for the component
    templateUrl: './manage-shipper-bank-account.component.html',
    imports: [TranslateDirective, FormsModule, WhiteSpaceValidatorDirective, InputRestrictionDirective, NgClass, EmptyGuidValidatorDirective, DatePipe, TranslatePipe]
})
export class ManageShipperBankAccountComponent extends SharedComponent implements OnInit, OnDestroy {
    readonly route = inject(ActivatedRoute);
    readonly router = inject(Router);
    override readonly coreSessionService: CoreSessionService;
    readonly coreSubscriptionService = inject(CoreSubscriptionService);
    readonly translateService = inject(TranslateService);
    readonly modalService = inject(NgbModal);
    readonly shipperBankAccountService = inject(ShipperBankAccountService);
    readonly accountService = inject(AccountService);
    readonly masterListItemService = inject(MasterListItemService);
    readonly rowStatusService = inject(RowStatusService);
    readonly shipperService = inject(ShipperService);

    action!: string;
    bankAccountTypes!: Array<MasterListItem>;
    createdByAccounts!: Array<Account>;
    modifiedByAccounts!: Array<Account>;
    rowStatuses!: Array<RowStatus>;
    screenGroup!: string;
    serviceGroup!: string;
    shipperBankAccount!: ShipperBankAccount;
    shipperBankAccountUId!: string;
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
       
        this.shipperBankAccountUId = this.route.snapshot.queryParams['shipperBankAccountUId'];
        this.action = this.route.snapshot.queryParams['action'];        
        this.getMasterData();
        this.getShipperBankAccount();
   
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
       this.shipperService.getShippers().subscribe((shippers: Array<Shipper>): void => {
           this.shippers = shippers;
        // On error, handle the error
        },(error: HttpErrorResponse): void => {
            CoreNotificationService.handleError(error);
        });
        this.masterListItemService.getMasterListItemsByCode('BankAccountType').subscribe((masterListItems: Array<MasterListItem>): void => {
           this.bankAccountTypes = masterListItems;
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

    getShipperBankAccount(): void {
        if (this.shipperBankAccountUId) {
            this.shipperBankAccountService.getShipperBankAccounts(this.shipperBankAccountUId).subscribe((shipperBankAccounts: Array<ShipperBankAccount>): void => {
                this.shipperBankAccount = shipperBankAccounts[0];
            }, (error: HttpErrorResponse): void => {
                CoreNotificationService.handleError(error);
            });
        } else {
            this.addShipperBankAccount();
        }
    }

    addShipperBankAccount(): void {
       this.shipperBankAccount = new ShipperBankAccount();
       this.shipperBankAccount.ItemState = ItemState.Added;
       this.shipperBankAccount.ShipperBankAccountUId = Guid.newGuid();
       this.shipperBankAccount.ShipperUId = Guid.empty;
       this.shipperBankAccount.BankAccountTypeUId = Guid.empty;
       this.shipperBankAccount.NameOnAccount = '';
       this.shipperBankAccount.Number = '';
       this.shipperBankAccount.RoutingNumber = '';
       this.shipperBankAccount.IsPreferred = false;
       this.shipperBankAccount.SortOrder = 50;
       this.shipperBankAccount.RowStatusUId = '00100000-0000-0000-0000-000000000000';
       this.setAuditFields(this.shipperBankAccount);
    }

    backToManageShipperBankAccount(): void {
        // Navigate to the view ShipperBankAccount page with query parameters
        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/viewShipperBankAccount'], {
             queryParams: {
                // Pass the screen group and service group as query parameters
                screenGroup: this.screenGroup,
                serviceGroup: this.serviceGroup    
             }   
        });
    }

    /**
	// Represents saveShipperBankAccount method
	// @param {NgForm} shipperBankAccountForm - shipperBankAccountForm method param.
	// @returns {void} - saveShipperBankAccount method returns void.
    */
    saveShipperBankAccount(shipperBankAccountForm: NgForm): void {
        // Check if the shipperBankAccount form is valid
        if (shipperBankAccountForm && shipperBankAccountForm.valid) {
           // Update the item state if it's unchanged
           this.shipperBankAccount.ItemState = this.shipperBankAccount.ItemState === ItemState.Unchanged ? ItemState.Modified : this.shipperBankAccount.ItemState;
           // Set the audit fields for the shipperBankAccount
           this.setAuditFields(this.shipperBankAccount);
           // Merge the shipperBankAccount changes and subscribe to the response
           this.shipperBankAccountService.mergeShipperBankAccounts([this.shipperBankAccount]).subscribe(
               // On success, show a success message and navigate back to manage ShipperBankAccount
               (): void => {
                    let successMsg: string = '';
                    this.translateService.get('SaveSuccess').subscribe((saveSuccessMsg: string): string => successMsg = saveSuccessMsg);
                    CoreNotificationService.showSuccess(successMsg);
                    this.backToManageShipperBankAccount();
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
        // Navigate back to the manage ShipperBankAccount page
        this.backToManageShipperBankAccount();
    }

    /**
	// Represents closeShipperBankAccount method
	// @param {TemplateRef<string>} content - content method param.
	// @param {NgForm} shipperBankAccountForm - shipperBankAccountForm method param.
	// @returns {void} - closeShipperBankAccount method returns void.
    */
    closeShipperBankAccount(shipperBankAccountForm: NgForm, content: TemplateRef<string>): void {
        // Check if the shipperBankAccount form is dirty or touched
        if (shipperBankAccountForm && (shipperBankAccountForm.dirty || shipperBankAccountForm.touched)) {
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
