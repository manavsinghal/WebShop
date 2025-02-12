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
import { SellerBankAccount } from '../../../../../shared/models/domain/seller/seller-bank-account.model';
import { SellerBankAccountService } from '../../../../../core/services/domain/seller/seller-bank-account.service';
import { Account } from '../../../../../shared/models/domain/account/account.model';
import { AccountService } from '../../../../../core/services/domain/account/account.service';
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

// Define the ManageSellerBankAccountComponent
@Component({
    // Selector for the component
    selector: 'app-manage-seller-bank-account',
    // Template URL for the component
    templateUrl: './manage-seller-bank-account.component.html',
    imports: [TranslateDirective, FormsModule, WhiteSpaceValidatorDirective, InputRestrictionDirective, NgClass, EmptyGuidValidatorDirective, DatePipe, TranslatePipe]
})
export class ManageSellerBankAccountComponent extends SharedComponent implements OnInit, OnDestroy {
    readonly route = inject(ActivatedRoute);
    readonly router = inject(Router);
    override readonly coreSessionService: CoreSessionService;
    readonly coreSubscriptionService = inject(CoreSubscriptionService);
    readonly translateService = inject(TranslateService);
    readonly modalService = inject(NgbModal);
    readonly sellerBankAccountService = inject(SellerBankAccountService);
    readonly accountService = inject(AccountService);
    readonly masterListItemService = inject(MasterListItemService);
    readonly rowStatusService = inject(RowStatusService);
    readonly sellerService = inject(SellerService);

    action!: string;
    bankAccountTypes!: Array<MasterListItem>;
    createdByAccounts!: Array<Account>;
    modifiedByAccounts!: Array<Account>;
    rowStatuses!: Array<RowStatus>;
    screenGroup!: string;
    sellerBankAccount!: SellerBankAccount;
    sellerBankAccountUId!: string;
    sellers!: Array<Seller>;
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
       
        this.sellerBankAccountUId = this.route.snapshot.queryParams['sellerBankAccountUId'];
        this.action = this.route.snapshot.queryParams['action'];        
        this.getMasterData();
        this.getSellerBankAccount();
   
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
       this.sellerService.getSellers().subscribe((sellers: Array<Seller>): void => {
           this.sellers = sellers;
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

    getSellerBankAccount(): void {
        if (this.sellerBankAccountUId) {
            this.sellerBankAccountService.getSellerBankAccounts(this.sellerBankAccountUId).subscribe((sellerBankAccounts: Array<SellerBankAccount>): void => {
                this.sellerBankAccount = sellerBankAccounts[0];
            }, (error: HttpErrorResponse): void => {
                CoreNotificationService.handleError(error);
            });
        } else {
            this.addSellerBankAccount();
        }
    }

    addSellerBankAccount(): void {
       this.sellerBankAccount = new SellerBankAccount();
       this.sellerBankAccount.ItemState = ItemState.Added;
       this.sellerBankAccount.SellerBankAccountUId = Guid.newGuid();
       this.sellerBankAccount.SellerUId = Guid.empty;
       this.sellerBankAccount.BankAccountTypeUId = Guid.empty;
       this.sellerBankAccount.NameOnAccount = '';
       this.sellerBankAccount.Number = '';
       this.sellerBankAccount.RoutingNumber = '';
       this.sellerBankAccount.IsPreferred = false;
       this.sellerBankAccount.SortOrder = 50;
       this.sellerBankAccount.RowStatusUId = '00100000-0000-0000-0000-000000000000';
       this.setAuditFields(this.sellerBankAccount);
    }

    backToManageSellerBankAccount(): void {
        // Navigate to the view SellerBankAccount page with query parameters
        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/viewSellerBankAccount'], {
             queryParams: {
                // Pass the screen group and service group as query parameters
                screenGroup: this.screenGroup,
                serviceGroup: this.serviceGroup    
             }   
        });
    }

    /**
	// Represents saveSellerBankAccount method
	// @param {NgForm} sellerBankAccountForm - sellerBankAccountForm method param.
	// @returns {void} - saveSellerBankAccount method returns void.
    */
    saveSellerBankAccount(sellerBankAccountForm: NgForm): void {
        // Check if the sellerBankAccount form is valid
        if (sellerBankAccountForm && sellerBankAccountForm.valid) {
           // Update the item state if it's unchanged
           this.sellerBankAccount.ItemState = this.sellerBankAccount.ItemState === ItemState.Unchanged ? ItemState.Modified : this.sellerBankAccount.ItemState;
           // Set the audit fields for the sellerBankAccount
           this.setAuditFields(this.sellerBankAccount);
           // Merge the sellerBankAccount changes and subscribe to the response
           this.sellerBankAccountService.mergeSellerBankAccounts([this.sellerBankAccount]).subscribe(
               // On success, show a success message and navigate back to manage SellerBankAccount
               (): void => {
                    let successMsg: string = '';
                    this.translateService.get('SaveSuccess').subscribe((saveSuccessMsg: string): string => successMsg = saveSuccessMsg);
                    CoreNotificationService.showSuccess(successMsg);
                    this.backToManageSellerBankAccount();
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
        // Navigate back to the manage SellerBankAccount page
        this.backToManageSellerBankAccount();
    }

    /**
	// Represents closeSellerBankAccount method
	// @param {NgForm} sellerBankAccountForm - sellerBankAccountForm method param.
	// @param {TemplateRef<string>} content - content method param.
	// @returns {void} - closeSellerBankAccount method returns void.
    */
    closeSellerBankAccount(sellerBankAccountForm: NgForm, content: TemplateRef<string>): void {
        // Check if the sellerBankAccount form is dirty or touched
        if (sellerBankAccountForm && (sellerBankAccountForm.dirty || sellerBankAccountForm.touched)) {
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
