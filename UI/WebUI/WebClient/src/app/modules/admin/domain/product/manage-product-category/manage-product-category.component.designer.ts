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
import { ProductCategory } from '../../../../../shared/models/domain/product/product-category.model';
import { ProductCategoryService } from '../../../../../core/services/domain/product/product-category.service';
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

// Define the ManageProductCategoryComponent
@Component({
    // Selector for the component
    selector: 'app-manage-product-category',
    // Template URL for the component
    templateUrl: './manage-product-category.component.html',
    imports: [TranslateDirective, FormsModule, WhiteSpaceValidatorDirective, InputRestrictionDirective, NgClass, EmptyGuidValidatorDirective, DatePipe, TranslatePipe]
})
export class ManageProductCategoryComponent extends SharedComponent implements OnInit, OnDestroy {
    action!: string;
    createdByAccounts!: Array<Account>;
    modifiedByAccounts!: Array<Account>;
    productCategory!: ProductCategory;
    productCategoryParents!: Array<ProductCategory>;
    productCategoryUId!: string;
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
        readonly productCategoryService: ProductCategoryService,
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
       
        this.productCategoryUId = this.route.snapshot.queryParams['productCategoryUId'];
        this.action = this.route.snapshot.queryParams['action'];        
        this.getMasterData();
        this.getProductCategory();
   
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
       this.productCategoryService.getProductCategories().subscribe((productCategories: Array<ProductCategory>): void => {
           this.productCategoryParents = productCategories;
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

    getProductCategory(): void {
        if (this.productCategoryUId) {
            this.productCategoryService.getProductCategories(this.productCategoryUId).subscribe((productCategories: Array<ProductCategory>): void => {
                this.productCategory = productCategories[0];
            }, (error: HttpErrorResponse): void => {
                CoreNotificationService.handleError(error);
            });
        } else {
            this.addProductCategory();
        }
    }

    addProductCategory(): void {
       this.productCategory = new ProductCategory();
       this.productCategory.ItemState = ItemState.Added;
       this.productCategory.ProductCategoryUId = Guid.newGuid();
       this.productCategory.Name = '';
       this.productCategory.ProductCategoryParentUId = null;
       this.productCategory.Description = '';
       this.productCategory.SortOrder = 50;
       this.productCategory.RowStatusUId = '00100000-0000-0000-0000-000000000000';
       this.setAuditFields(this.productCategory);
    }

    backToManageProductCategory(): void {
        // Navigate to the view ProductCategory page with query parameters
        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/viewProductCategory'], {
             queryParams: {
                // Pass the screen group and service group as query parameters
                screenGroup: this.screenGroup,
                serviceGroup: this.serviceGroup    
             }   
        });
    }

    /**
	// Represents saveProductCategory method
	// @param {NgForm} productCategoryForm - productCategoryForm method param.
	// @returns {void} - saveProductCategory method returns void.
    */
    saveProductCategory(productCategoryForm: NgForm): void {
        // Check if the productCategory form is valid
        if (productCategoryForm && productCategoryForm.valid) {
           // Update the item state if it's unchanged
           this.productCategory.ItemState = this.productCategory.ItemState === ItemState.Unchanged ? ItemState.Modified : this.productCategory.ItemState;
           // Set the audit fields for the productCategory
           this.setAuditFields(this.productCategory);
           // Merge the productCategory changes and subscribe to the response
           this.productCategoryService.mergeProductCategories([this.productCategory]).subscribe(
               // On success, show a success message and navigate back to manage ProductCategory
               (): void => {
                    let successMsg: string = '';
                    this.translateService.get('SaveSuccess').subscribe((saveSuccessMsg: string): string => successMsg = saveSuccessMsg);
                    CoreNotificationService.showSuccess(successMsg);
                    this.backToManageProductCategory();
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
        // Navigate back to the manage ProductCategory page
        this.backToManageProductCategory();
    }

    /**
	// Represents closeProductCategory method
	// @param {NgForm} productCategoryForm - productCategoryForm method param.
	// @param {TemplateRef<string>} content - content method param.
	// @returns {void} - closeProductCategory method returns void.
    */
    closeProductCategory(productCategoryForm: NgForm, content: TemplateRef<string>): void {
        // Check if the productCategory form is dirty or touched
        if (productCategoryForm && (productCategoryForm.dirty || productCategoryForm.touched)) {
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
