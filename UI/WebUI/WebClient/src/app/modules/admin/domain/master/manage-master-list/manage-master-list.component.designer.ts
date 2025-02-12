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
import { MasterList } from '../../../../../shared/models/domain/master/master-list.model';
import { MasterListService } from '../../../../../core/services/domain/master/master-list.service';
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

// Define the ManageMasterListComponent
@Component({
    // Selector for the component
    selector: 'app-manage-master-list',
    // Template URL for the component
    templateUrl: './manage-master-list.component.html',
    imports: [TranslateDirective, FormsModule, WhiteSpaceValidatorDirective, InputRestrictionDirective, NgClass, EmptyGuidValidatorDirective, DatePipe, TranslatePipe]
})
export class ManageMasterListComponent extends SharedComponent implements OnInit, OnDestroy {
    action!: string;
    masterList!: MasterList;
    masterListUId!: string;
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
        readonly masterListService: MasterListService,
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
       
        this.masterListUId = this.route.snapshot.queryParams['masterListUId'];
        this.action = this.route.snapshot.queryParams['action'];        
        this.getMasterData();
        this.getMasterList();
   
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

    getMasterList(): void {
        if (this.masterListUId) {
            this.masterListService.getMasterLists(this.masterListUId).subscribe((masterLists: Array<MasterList>): void => {
                this.masterList = masterLists[0];
            }, (error: HttpErrorResponse): void => {
                CoreNotificationService.handleError(error);
            });
        } else {
            this.addMasterList();
        }
    }

    addMasterList(): void {
       this.masterList = new MasterList();
       this.masterList.ItemState = ItemState.Added;
       this.masterList.MasterListUId = Guid.newGuid();
       this.masterList.Name = '';
       this.masterList.Code = '';
       this.masterList.Description = '';
       this.masterList.Source = '';
       this.masterList.ImageUrl = '';
       this.masterList.DisplayOrder = 50;
       this.masterList.RowStatusUId = '00100000-0000-0000-0000-000000000000';
       this.setAuditFields(this.masterList);
    }

    backToManageMasterList(): void {
        // Navigate to the view MasterList page with query parameters
        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/viewMasterList'], {
             queryParams: {
                // Pass the screen group and service group as query parameters
                screenGroup: this.screenGroup,
                serviceGroup: this.serviceGroup    
             }   
        });
    }

    /**
	// Represents saveMasterList method
	// @param {NgForm} masterListForm - masterListForm method param.
	// @returns {void} - saveMasterList method returns void.
    */
    saveMasterList(masterListForm: NgForm): void {
        // Check if the masterList form is valid
        if (masterListForm && masterListForm.valid) {
           // Update the item state if it's unchanged
           this.masterList.ItemState = this.masterList.ItemState === ItemState.Unchanged ? ItemState.Modified : this.masterList.ItemState;
           // Set the audit fields for the masterList
           this.setAuditFields(this.masterList);
           // Merge the masterList changes and subscribe to the response
           this.masterListService.mergeMasterLists([this.masterList]).subscribe(
               // On success, show a success message and navigate back to manage MasterList
               (): void => {
                    let successMsg: string = '';
                    this.translateService.get('SaveSuccess').subscribe((saveSuccessMsg: string): string => successMsg = saveSuccessMsg);
                    CoreNotificationService.showSuccess(successMsg);
                    this.backToManageMasterList();
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
        // Navigate back to the manage MasterList page
        this.backToManageMasterList();
    }

    /**
	// Represents closeMasterList method
	// @param {NgForm} masterListForm - masterListForm method param.
	// @param {TemplateRef<string>} content - content method param.
	// @returns {void} - closeMasterList method returns void.
    */
    closeMasterList(masterListForm: NgForm, content: TemplateRef<string>): void {
        // Check if the masterList form is dirty or touched
        if (masterListForm && (masterListForm.dirty || masterListForm.touched)) {
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
