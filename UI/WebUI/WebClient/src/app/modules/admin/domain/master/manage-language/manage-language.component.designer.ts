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

// Define the ManageLanguageComponent
@Component({
    // Selector for the component
    selector: 'app-manage-language',
    // Template URL for the component
    templateUrl: './manage-language.component.html',
    imports: [TranslateDirective, FormsModule, WhiteSpaceValidatorDirective, InputRestrictionDirective, NgClass, EmptyGuidValidatorDirective, DatePipe, TranslatePipe]
})
export class ManageLanguageComponent extends SharedComponent implements OnInit, OnDestroy {
    action!: string;
    language!: Language;
    languageUId!: string;
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
        readonly languageService: LanguageService,
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
       
        this.languageUId = this.route.snapshot.queryParams['languageUId'];
        this.action = this.route.snapshot.queryParams['action'];        
        this.getMasterData();
        this.getLanguage();
   
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

    getLanguage(): void {
        if (this.languageUId) {
            this.languageService.getLanguages(this.languageUId).subscribe((languages: Array<Language>): void => {
                this.language = languages[0];
            }, (error: HttpErrorResponse): void => {
                CoreNotificationService.handleError(error);
            });
        } else {
            this.addLanguage();
        }
    }

    addLanguage(): void {
       this.language = new Language();
       this.language.ItemState = ItemState.Added;
       this.language.LanguageUId = Guid.newGuid();
       this.language.Name = '';
       this.language.LocalizedName = '';
       this.language.DisplayName = '';
       this.language.Culture = '';
       this.language.AzureCulture = '';
       this.language.ImageUrl = '';
       this.language.ImageBinary = '';
       this.language.Description = '';
       this.language.IsRTL = false;
       this.language.DisplayOrder = 50;
       this.language.RowStatusUId = '00100000-0000-0000-0000-000000000000';
       this.setAuditFields(this.language);
    }

    backToManageLanguage(): void {
        // Navigate to the view Language page with query parameters
        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/viewLanguage'], {
             queryParams: {
                // Pass the screen group and service group as query parameters
                screenGroup: this.screenGroup,
                serviceGroup: this.serviceGroup    
             }   
        });
    }

    /**
	// Represents saveLanguage method
	// @param {NgForm} languageForm - languageForm method param.
	// @returns {void} - saveLanguage method returns void.
    */
    saveLanguage(languageForm: NgForm): void {
        // Check if the language form is valid
        if (languageForm && languageForm.valid) {
           // Update the item state if it's unchanged
           this.language.ItemState = this.language.ItemState === ItemState.Unchanged ? ItemState.Modified : this.language.ItemState;
           // Set the audit fields for the language
           this.setAuditFields(this.language);
           // Merge the language changes and subscribe to the response
           this.languageService.mergeLanguages([this.language]).subscribe(
               // On success, show a success message and navigate back to manage Language
               (): void => {
                    let successMsg: string = '';
                    this.translateService.get('SaveSuccess').subscribe((saveSuccessMsg: string): string => successMsg = saveSuccessMsg);
                    CoreNotificationService.showSuccess(successMsg);
                    this.backToManageLanguage();
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
        // Navigate back to the manage Language page
        this.backToManageLanguage();
    }

    /**
	// Represents closeLanguage method
	// @param {NgForm} languageForm - languageForm method param.
	// @param {TemplateRef<string>} content - content method param.
	// @returns {void} - closeLanguage method returns void.
    */
    closeLanguage(languageForm: NgForm, content: TemplateRef<string>): void {
        // Check if the language form is dirty or touched
        if (languageForm && (languageForm.dirty || languageForm.touched)) {
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
