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
import { CountryLanguage } from '../../../../../shared/models/domain/master/country-language.model';
import { CountryLanguageService } from '../../../../../core/services/domain/master/country-language.service';
import { Country } from '../../../../../shared/models/domain/master/country.model';
import { CountryService } from '../../../../../core/services/domain/master/country.service';
import { Language } from '../../../../../shared/models/domain/master/language.model';
import { LanguageService } from '../../../../../core/services/domain/master/language.service';
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

// Define the ManageCountryLanguageComponent
@Component({
    // Selector for the component
    selector: 'app-manage-country-language',
    // Template URL for the component
    templateUrl: './manage-country-language.component.html',
    imports: [TranslateDirective, FormsModule, WhiteSpaceValidatorDirective, InputRestrictionDirective, NgClass, EmptyGuidValidatorDirective, DatePipe, TranslatePipe]
})
export class ManageCountryLanguageComponent extends SharedComponent implements OnInit, OnDestroy {
    action!: string;
    countries!: Array<Country>;
    countryLanguage!: CountryLanguage;
    countryLanguageUId!: string;
    languages!: Array<Language>;
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
        readonly countryLanguageService: CountryLanguageService,
        readonly countryService : CountryService,
        readonly languageService : LanguageService,
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
       
        this.countryLanguageUId = this.route.snapshot.queryParams['countryLanguageUId'];
        this.action = this.route.snapshot.queryParams['action'];        
        this.getMasterData();
        this.getCountryLanguage();
   
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
       this.languageService.getLanguages().subscribe((languages: Array<Language>): void => {
           this.languages = languages;
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

    getCountryLanguage(): void {
        if (this.countryLanguageUId) {
            this.countryLanguageService.getCountryLanguages(this.countryLanguageUId).subscribe((countryLanguages: Array<CountryLanguage>): void => {
                this.countryLanguage = countryLanguages[0];
            }, (error: HttpErrorResponse): void => {
                CoreNotificationService.handleError(error);
            });
        } else {
            this.addCountryLanguage();
        }
    }

    addCountryLanguage(): void {
       this.countryLanguage = new CountryLanguage();
       this.countryLanguage.ItemState = ItemState.Added;
       this.countryLanguage.CountryLanguageUId = Guid.newGuid();
       this.countryLanguage.CountryUId = Guid.empty;
       this.countryLanguage.LanguageUId = Guid.empty;
       this.countryLanguage.Name = '';
       this.countryLanguage.RowStatusUId = '00100000-0000-0000-0000-000000000000';
       this.setAuditFields(this.countryLanguage);
    }

    backToManageCountryLanguage(): void {
        // Navigate to the view CountryLanguage page with query parameters
        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/viewCountryLanguage'], {
             queryParams: {
                // Pass the screen group and service group as query parameters
                screenGroup: this.screenGroup,
                serviceGroup: this.serviceGroup    
             }   
        });
    }

    /**
	// Represents saveCountryLanguage method
	// @param {NgForm} countryLanguageForm - countryLanguageForm method param.
	// @returns {void} - saveCountryLanguage method returns void.
    */
    saveCountryLanguage(countryLanguageForm: NgForm): void {
        // Check if the countryLanguage form is valid
        if (countryLanguageForm && countryLanguageForm.valid) {
           // Update the item state if it's unchanged
           this.countryLanguage.ItemState = this.countryLanguage.ItemState === ItemState.Unchanged ? ItemState.Modified : this.countryLanguage.ItemState;
           // Set the audit fields for the countryLanguage
           this.setAuditFields(this.countryLanguage);
           // Merge the countryLanguage changes and subscribe to the response
           this.countryLanguageService.mergeCountryLanguages([this.countryLanguage]).subscribe(
               // On success, show a success message and navigate back to manage CountryLanguage
               (): void => {
                    let successMsg: string = '';
                    this.translateService.get('SaveSuccess').subscribe((saveSuccessMsg: string): string => successMsg = saveSuccessMsg);
                    CoreNotificationService.showSuccess(successMsg);
                    this.backToManageCountryLanguage();
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
        // Navigate back to the manage CountryLanguage page
        this.backToManageCountryLanguage();
    }

    /**
	// Represents closeCountryLanguage method
	// @param {TemplateRef<string>} content - content method param.
	// @param {NgForm} countryLanguageForm - countryLanguageForm method param.
	// @returns {void} - closeCountryLanguage method returns void.
    */
    closeCountryLanguage(countryLanguageForm: NgForm, content: TemplateRef<string>): void {
        // Check if the countryLanguage form is dirty or touched
        if (countryLanguageForm && (countryLanguageForm.dirty || countryLanguageForm.touched)) {
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
