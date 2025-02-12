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
import { RowStatus } from '../../../../../shared/models/domain/master/row-status.model';
import { RowStatusService } from '../../../../../core/services/domain/master/row-status.service';
import { CoreNotificationService } from '../../../../../core/services/core.notification.service';
import { CoreSubscriptionService } from '../../../../../core/services/core.subscription.service';
import { CoreSessionService } from '../../../../../core/services/core.session.service';
import { ItemState } from '../../../../../shared/models/item-state.model';
import { InputRestrictionDirective } from '../../../../../shared/directives/restrict-input.directive';
import { WhiteSpaceValidatorDirective } from '../../../../../shared/directives/whitespace.directive';
import { NgClass, DatePipe } from '@angular/common';

// Define the ManageRowStatusComponent
@Component({
    // Selector for the component
    selector: 'app-manage-row-status',
    // Template URL for the component
    templateUrl: './manage-row-status.component.html',
    imports: [TranslateDirective, FormsModule, InputRestrictionDirective, WhiteSpaceValidatorDirective, NgClass, DatePipe, TranslatePipe]
})
export class ManageRowStatusComponent extends SharedComponent implements OnInit, OnDestroy {
    readonly route = inject(ActivatedRoute);
    readonly router = inject(Router);
    override readonly coreSessionService: CoreSessionService;
    readonly coreSubscriptionService = inject(CoreSubscriptionService);
    readonly translateService = inject(TranslateService);
    readonly modalService = inject(NgbModal);
    readonly rowStatusService = inject(RowStatusService);

    action!: string;
    rowStatus!: RowStatus;
    rowStatusUId!: string;
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
       
        this.rowStatusUId = this.route.snapshot.queryParams['rowStatusUId'];
        this.action = this.route.snapshot.queryParams['action'];        
        this.getMasterData();
        this.getRowStatus();
   
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

    getRowStatus(): void {
        if (this.rowStatusUId) {
            this.rowStatusService.getRowStatuses(this.rowStatusUId).subscribe((rowStatuses: Array<RowStatus>): void => {
                this.rowStatus = rowStatuses[0];
            }, (error: HttpErrorResponse): void => {
                CoreNotificationService.handleError(error);
            });
        } else {
            this.addRowStatus();
        }
    }

    addRowStatus(): void {
       this.rowStatus = new RowStatus();
       this.rowStatus.ItemState = ItemState.Added;
       this.rowStatus.RowStatusUId = Guid.newGuid();
       this.rowStatus.Name = '';
       this.rowStatus.Code = '';
       this.rowStatus.Description = '';
       this.rowStatus.DisplayOrder = 50;
       this.setAuditFields(this.rowStatus);
    }

    backToManageRowStatus(): void {
        // Navigate to the view RowStatus page with query parameters
        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/viewRowStatus'], {
             queryParams: {
                // Pass the screen group and service group as query parameters
                screenGroup: this.screenGroup,
                serviceGroup: this.serviceGroup    
             }   
        });
    }

    /**
	// Represents saveRowStatus method
	// @param {NgForm} rowStatusForm - rowStatusForm method param.
	// @returns {void} - saveRowStatus method returns void.
    */
    saveRowStatus(rowStatusForm: NgForm): void {
        // Check if the rowStatus form is valid
        if (rowStatusForm && rowStatusForm.valid) {
           // Update the item state if it's unchanged
           this.rowStatus.ItemState = this.rowStatus.ItemState === ItemState.Unchanged ? ItemState.Modified : this.rowStatus.ItemState;
           // Set the audit fields for the rowStatus
           this.setAuditFields(this.rowStatus);
           // Merge the rowStatus changes and subscribe to the response
           this.rowStatusService.mergeRowStatuses([this.rowStatus]).subscribe(
               // On success, show a success message and navigate back to manage RowStatus
               (): void => {
                    let successMsg: string = '';
                    this.translateService.get('SaveSuccess').subscribe((saveSuccessMsg: string): string => successMsg = saveSuccessMsg);
                    CoreNotificationService.showSuccess(successMsg);
                    this.backToManageRowStatus();
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
        // Navigate back to the manage RowStatus page
        this.backToManageRowStatus();
    }

    /**
	// Represents closeRowStatus method
	// @param {NgForm} rowStatusForm - rowStatusForm method param.
	// @param {TemplateRef<string>} content - content method param.
	// @returns {void} - closeRowStatus method returns void.
    */
    closeRowStatus(rowStatusForm: NgForm, content: TemplateRef<string>): void {
        // Check if the rowStatus form is dirty or touched
        if (rowStatusForm && (rowStatusForm.dirty || rowStatusForm.touched)) {
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
