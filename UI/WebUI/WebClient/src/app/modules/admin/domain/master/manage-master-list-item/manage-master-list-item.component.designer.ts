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
import { MasterListItem } from '../../../../../shared/models/domain/master/master-list-item.model';
import { MasterListItemService } from '../../../../../core/services/domain/master/master-list-item.service';
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

// Define the ManageMasterListItemComponent
@Component({
    // Selector for the component
    selector: 'app-manage-master-list-item',
    // Template URL for the component
    templateUrl: './manage-master-list-item.component.html',
    imports: [TranslateDirective, FormsModule, WhiteSpaceValidatorDirective, InputRestrictionDirective, NgClass, EmptyGuidValidatorDirective, DatePipe, TranslatePipe]
})
export class ManageMasterListItemComponent extends SharedComponent implements OnInit, OnDestroy {
    readonly route = inject(ActivatedRoute);
    readonly router = inject(Router);
    override readonly coreSessionService: CoreSessionService;
    readonly coreSubscriptionService = inject(CoreSubscriptionService);
    readonly translateService = inject(TranslateService);
    readonly modalService = inject(NgbModal);
    readonly masterListItemService = inject(MasterListItemService);
    readonly masterListService = inject(MasterListService);
    readonly rowStatusService = inject(RowStatusService);

    action!: string;
    masterListItem!: MasterListItem;
    masterListItemUId!: string;
    masterLists!: Array<MasterList>;
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
       
        this.masterListItemUId = this.route.snapshot.queryParams['masterListItemUId'];
        this.action = this.route.snapshot.queryParams['action'];        
        this.getMasterData();
        this.getMasterListItem();
   
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
       this.masterListService.getMasterLists().subscribe((masterLists: Array<MasterList>): void => {
           this.masterLists = masterLists;
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

    getMasterListItem(): void {
        if (this.masterListItemUId) {
            this.masterListItemService.getMasterListItems(this.masterListItemUId).subscribe((masterListItems: Array<MasterListItem>): void => {
                this.masterListItem = masterListItems[0];
            }, (error: HttpErrorResponse): void => {
                CoreNotificationService.handleError(error);
            });
        } else {
            this.addMasterListItem();
        }
    }

    addMasterListItem(): void {
       this.masterListItem = new MasterListItem();
       this.masterListItem.ItemState = ItemState.Added;
       this.masterListItem.MasterListItemUId = Guid.newGuid();
       this.masterListItem.Name = '';
       this.masterListItem.Code = '';
       this.masterListItem.Description = '';
       this.masterListItem.MasterListUId = Guid.empty;
       this.masterListItem.DisplayOrder = 50;
       this.masterListItem.RowStatusUId = '00100000-0000-0000-0000-000000000000';
       this.setAuditFields(this.masterListItem);
    }

    backToManageMasterListItem(): void {
        // Navigate to the view MasterListItem page with query parameters
        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/viewMasterListItem'], {
             queryParams: {
                // Pass the screen group and service group as query parameters
                screenGroup: this.screenGroup,
                serviceGroup: this.serviceGroup    
             }   
        });
    }

    /**
	// Represents saveMasterListItem method
	// @param {NgForm} masterListItemForm - masterListItemForm method param.
	// @returns {void} - saveMasterListItem method returns void.
    */
    saveMasterListItem(masterListItemForm: NgForm): void {
        // Check if the masterListItem form is valid
        if (masterListItemForm && masterListItemForm.valid) {
           // Update the item state if it's unchanged
           this.masterListItem.ItemState = this.masterListItem.ItemState === ItemState.Unchanged ? ItemState.Modified : this.masterListItem.ItemState;
           // Set the audit fields for the masterListItem
           this.setAuditFields(this.masterListItem);
           // Merge the masterListItem changes and subscribe to the response
           this.masterListItemService.mergeMasterListItems([this.masterListItem]).subscribe(
               // On success, show a success message and navigate back to manage MasterListItem
               (): void => {
                    let successMsg: string = '';
                    this.translateService.get('SaveSuccess').subscribe((saveSuccessMsg: string): string => successMsg = saveSuccessMsg);
                    CoreNotificationService.showSuccess(successMsg);
                    this.backToManageMasterListItem();
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
        // Navigate back to the manage MasterListItem page
        this.backToManageMasterListItem();
    }

    /**
	// Represents closeMasterListItem method
	// @param {NgForm} masterListItemForm - masterListItemForm method param.
	// @param {TemplateRef<string>} content - content method param.
	// @returns {void} - closeMasterListItem method returns void.
    */
    closeMasterListItem(masterListItemForm: NgForm, content: TemplateRef<string>): void {
        // Check if the masterListItem form is dirty or touched
        if (masterListItemForm && (masterListItemForm.dirty || masterListItemForm.touched)) {
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
