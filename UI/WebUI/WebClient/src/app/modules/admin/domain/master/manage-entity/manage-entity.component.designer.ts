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
import { Entity } from '../../../../../shared/models/domain/master/entity.model';
import { EntityService } from '../../../../../core/services/domain/master/entity.service';
import { Account } from '../../../../../shared/models/domain/account/account.model';
import { AccountService } from '../../../../../core/services/domain/account/account.service';
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

// Define the ManageEntityComponent
@Component({
    // Selector for the component
    selector: 'app-manage-entity',
    // Template URL for the component
    templateUrl: './manage-entity.component.html',
    imports: [TranslateDirective, FormsModule, WhiteSpaceValidatorDirective, InputRestrictionDirective, NgClass, EmptyGuidValidatorDirective, DatePipe, TranslatePipe]
})
export class ManageEntityComponent extends SharedComponent implements OnInit, OnDestroy {
    action!: string;
    createdByAccounts!: Array<Account>;
    entity!: Entity;
    entityParents!: Array<Entity>;
    entityUId!: string;
    modifiedByAccounts!: Array<Account>;
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
        readonly entityService: EntityService,
        readonly accountService : AccountService,
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
       
        this.entityUId = this.route.snapshot.queryParams['entityUId'];
        this.action = this.route.snapshot.queryParams['action'];        
        this.getMasterData();
        this.getEntity();
   
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
       this.entityService.getEntities().subscribe((entities: Array<Entity>): void => {
           this.entityParents = entities;
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

    getEntity(): void {
        if (this.entityUId) {
            this.entityService.getEntities(this.entityUId).subscribe((entities: Array<Entity>): void => {
                this.entity = entities[0];
            }, (error: HttpErrorResponse): void => {
                CoreNotificationService.handleError(error);
            });
        } else {
            this.addEntity();
        }
    }

    addEntity(): void {
       this.entity = new Entity();
       this.entity.ItemState = ItemState.Added;
       this.entity.EntityUId = Guid.newGuid();
       this.entity.EntityParentUId = null;
       this.entity.Name = '';
       this.entity.Code = '';
       this.entity.Description = '';
       this.entity.DisplayOrder = 50;
       this.entity.RowStatusUId = '00100000-0000-0000-0000-000000000000';
       this.setAuditFields(this.entity);
    }

    backToManageEntity(): void {
        // Navigate to the view Entity page with query parameters
        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/viewEntity'], {
             queryParams: {
                // Pass the screen group and service group as query parameters
                screenGroup: this.screenGroup,
                serviceGroup: this.serviceGroup    
             }   
        });
    }

    /**
	// Represents saveEntity method
	// @param {NgForm} entityForm - entityForm method param.
	// @returns {void} - saveEntity method returns void.
    */
    saveEntity(entityForm: NgForm): void {
        // Check if the entity form is valid
        if (entityForm && entityForm.valid) {
           // Update the item state if it's unchanged
           this.entity.ItemState = this.entity.ItemState === ItemState.Unchanged ? ItemState.Modified : this.entity.ItemState;
           // Set the audit fields for the entity
           this.setAuditFields(this.entity);
           // Merge the entity changes and subscribe to the response
           this.entityService.mergeEntities([this.entity]).subscribe(
               // On success, show a success message and navigate back to manage Entity
               (): void => {
                    let successMsg: string = '';
                    this.translateService.get('SaveSuccess').subscribe((saveSuccessMsg: string): string => successMsg = saveSuccessMsg);
                    CoreNotificationService.showSuccess(successMsg);
                    this.backToManageEntity();
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
        // Navigate back to the manage Entity page
        this.backToManageEntity();
    }

    /**
	// Represents closeEntity method
	// @param {NgForm} entityForm - entityForm method param.
	// @param {TemplateRef<string>} content - content method param.
	// @returns {void} - closeEntity method returns void.
    */
    closeEntity(entityForm: NgForm, content: TemplateRef<string>): void {
        // Check if the entity form is dirty or touched
        if (entityForm && (entityForm.dirty || entityForm.touched)) {
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
