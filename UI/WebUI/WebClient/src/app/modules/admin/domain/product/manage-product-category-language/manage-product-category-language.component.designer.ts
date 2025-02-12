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
import { ProductCategoryLanguage } from '../../../../../shared/models/domain/product/product-category-language.model';
import { ProductCategoryLanguageService } from '../../../../../core/services/domain/product/product-category-language.service';
import { Language } from '../../../../../shared/models/domain/master/language.model';
import { LanguageService } from '../../../../../core/services/domain/master/language.service';
import { ProductCategory } from '../../../../../shared/models/domain/product/product-category.model';
import { ProductCategoryService } from '../../../../../core/services/domain/product/product-category.service';
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

// Define the ManageProductCategoryLanguageComponent
@Component({
    // Selector for the component
    selector: 'app-manage-product-category-language',
    // Template URL for the component
    templateUrl: './manage-product-category-language.component.html',
    imports: [TranslateDirective, FormsModule, WhiteSpaceValidatorDirective, InputRestrictionDirective, NgClass, EmptyGuidValidatorDirective, DatePipe, TranslatePipe]
})
export class ManageProductCategoryLanguageComponent extends SharedComponent implements OnInit, OnDestroy {
    readonly route = inject(ActivatedRoute);
    readonly router = inject(Router);
    override readonly coreSessionService: CoreSessionService;
    readonly coreSubscriptionService = inject(CoreSubscriptionService);
    readonly translateService = inject(TranslateService);
    readonly modalService = inject(NgbModal);
    readonly productCategoryLanguageService = inject(ProductCategoryLanguageService);
    readonly languageService = inject(LanguageService);
    readonly productCategoryService = inject(ProductCategoryService);
    readonly rowStatusService = inject(RowStatusService);

    action!: string;
    languages!: Array<Language>;
    productCategories!: Array<ProductCategory>;
    productCategoryLanguage!: ProductCategoryLanguage;
    productCategoryLanguageUId!: string;
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
       
        this.productCategoryLanguageUId = this.route.snapshot.queryParams['productCategoryLanguageUId'];
        this.action = this.route.snapshot.queryParams['action'];        
        this.getMasterData();
        this.getProductCategoryLanguage();
   
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
           this.productCategories = productCategories;
        // On error, handle the error
        },(error: HttpErrorResponse): void => {
            CoreNotificationService.handleError(error);
        });
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

    getProductCategoryLanguage(): void {
        if (this.productCategoryLanguageUId) {
            this.productCategoryLanguageService.getProductCategoryLanguages(this.productCategoryLanguageUId).subscribe((productCategoryLanguages: Array<ProductCategoryLanguage>): void => {
                this.productCategoryLanguage = productCategoryLanguages[0];
            }, (error: HttpErrorResponse): void => {
                CoreNotificationService.handleError(error);
            });
        } else {
            this.addProductCategoryLanguage();
        }
    }

    addProductCategoryLanguage(): void {
       this.productCategoryLanguage = new ProductCategoryLanguage();
       this.productCategoryLanguage.ItemState = ItemState.Added;
       this.productCategoryLanguage.ProductCategoryLanguageUId = Guid.newGuid();
       this.productCategoryLanguage.ProductCategoryUId = Guid.empty;
       this.productCategoryLanguage.LanguageUId = Guid.empty;
       this.productCategoryLanguage.Name = '';
       this.productCategoryLanguage.Description = '';
       this.productCategoryLanguage.RowStatusUId = '00100000-0000-0000-0000-000000000000';
       this.setAuditFields(this.productCategoryLanguage);
    }

    backToManageProductCategoryLanguage(): void {
        // Navigate to the view ProductCategoryLanguage page with query parameters
        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/viewProductCategoryLanguage'], {
             queryParams: {
                // Pass the screen group and service group as query parameters
                screenGroup: this.screenGroup,
                serviceGroup: this.serviceGroup    
             }   
        });
    }

    /**
	// Represents saveProductCategoryLanguage method
	// @param {NgForm} productCategoryLanguageForm - productCategoryLanguageForm method param.
	// @returns {void} - saveProductCategoryLanguage method returns void.
    */
    saveProductCategoryLanguage(productCategoryLanguageForm: NgForm): void {
        // Check if the productCategoryLanguage form is valid
        if (productCategoryLanguageForm && productCategoryLanguageForm.valid) {
           // Update the item state if it's unchanged
           this.productCategoryLanguage.ItemState = this.productCategoryLanguage.ItemState === ItemState.Unchanged ? ItemState.Modified : this.productCategoryLanguage.ItemState;
           // Set the audit fields for the productCategoryLanguage
           this.setAuditFields(this.productCategoryLanguage);
           // Merge the productCategoryLanguage changes and subscribe to the response
           this.productCategoryLanguageService.mergeProductCategoryLanguages([this.productCategoryLanguage]).subscribe(
               // On success, show a success message and navigate back to manage ProductCategoryLanguage
               (): void => {
                    let successMsg: string = '';
                    this.translateService.get('SaveSuccess').subscribe((saveSuccessMsg: string): string => successMsg = saveSuccessMsg);
                    CoreNotificationService.showSuccess(successMsg);
                    this.backToManageProductCategoryLanguage();
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
        // Navigate back to the manage ProductCategoryLanguage page
        this.backToManageProductCategoryLanguage();
    }

    /**
	// Represents closeProductCategoryLanguage method
	// @param {NgForm} productCategoryLanguageForm - productCategoryLanguageForm method param.
	// @param {TemplateRef<string>} content - content method param.
	// @returns {void} - closeProductCategoryLanguage method returns void.
    */
    closeProductCategoryLanguage(productCategoryLanguageForm: NgForm, content: TemplateRef<string>): void {
        // Check if the productCategoryLanguage form is dirty or touched
        if (productCategoryLanguageForm && (productCategoryLanguageForm.dirty || productCategoryLanguageForm.touched)) {
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
