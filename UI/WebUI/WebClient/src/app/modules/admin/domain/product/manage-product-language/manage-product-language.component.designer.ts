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
import { ProductLanguage } from '../../../../../shared/models/domain/product/product-language.model';
import { ProductLanguageService } from '../../../../../core/services/domain/product/product-language.service';
import { Language } from '../../../../../shared/models/domain/master/language.model';
import { LanguageService } from '../../../../../core/services/domain/master/language.service';
import { Product } from '../../../../../shared/models/domain/product/product.model';
import { ProductService } from '../../../../../core/services/domain/product/product.service';
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

// Define the ManageProductLanguageComponent
@Component({
    // Selector for the component
    selector: 'app-manage-product-language',
    // Template URL for the component
    templateUrl: './manage-product-language.component.html',
    imports: [TranslateDirective, FormsModule, WhiteSpaceValidatorDirective, InputRestrictionDirective, NgClass, EmptyGuidValidatorDirective, DatePipe, TranslatePipe]
})
export class ManageProductLanguageComponent extends SharedComponent implements OnInit, OnDestroy {
    action!: string;
    languages!: Array<Language>;
    productLanguage!: ProductLanguage;
    productLanguageUId!: string;
    products!: Array<Product>;
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
        readonly productLanguageService: ProductLanguageService,
        readonly languageService : LanguageService,
        readonly productService : ProductService,
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
       
        this.productLanguageUId = this.route.snapshot.queryParams['productLanguageUId'];
        this.action = this.route.snapshot.queryParams['action'];        
        this.getMasterData();
        this.getProductLanguage();
   
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
       this.productService.getProducts().subscribe((products: Array<Product>): void => {
           this.products = products;
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

    getProductLanguage(): void {
        if (this.productLanguageUId) {
            this.productLanguageService.getProductLanguages(this.productLanguageUId).subscribe((productLanguages: Array<ProductLanguage>): void => {
                this.productLanguage = productLanguages[0];
            }, (error: HttpErrorResponse): void => {
                CoreNotificationService.handleError(error);
            });
        } else {
            this.addProductLanguage();
        }
    }

    addProductLanguage(): void {
       this.productLanguage = new ProductLanguage();
       this.productLanguage.ItemState = ItemState.Added;
       this.productLanguage.ProductLanguageUId = Guid.newGuid();
       this.productLanguage.ProductUId = Guid.empty;
       this.productLanguage.LanguageUId = Guid.empty;
       this.productLanguage.Name = '';
       this.productLanguage.RowStatusUId = '00100000-0000-0000-0000-000000000000';
       this.setAuditFields(this.productLanguage);
    }

    backToManageProductLanguage(): void {
        // Navigate to the view ProductLanguage page with query parameters
        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/viewProductLanguage'], {
             queryParams: {
                // Pass the screen group and service group as query parameters
                screenGroup: this.screenGroup,
                serviceGroup: this.serviceGroup    
             }   
        });
    }

    /**
	// Represents saveProductLanguage method
	// @param {NgForm} productLanguageForm - productLanguageForm method param.
	// @returns {void} - saveProductLanguage method returns void.
    */
    saveProductLanguage(productLanguageForm: NgForm): void {
        // Check if the productLanguage form is valid
        if (productLanguageForm && productLanguageForm.valid) {
           // Update the item state if it's unchanged
           this.productLanguage.ItemState = this.productLanguage.ItemState === ItemState.Unchanged ? ItemState.Modified : this.productLanguage.ItemState;
           // Set the audit fields for the productLanguage
           this.setAuditFields(this.productLanguage);
           // Merge the productLanguage changes and subscribe to the response
           this.productLanguageService.mergeProductLanguages([this.productLanguage]).subscribe(
               // On success, show a success message and navigate back to manage ProductLanguage
               (): void => {
                    let successMsg: string = '';
                    this.translateService.get('SaveSuccess').subscribe((saveSuccessMsg: string): string => successMsg = saveSuccessMsg);
                    CoreNotificationService.showSuccess(successMsg);
                    this.backToManageProductLanguage();
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
        // Navigate back to the manage ProductLanguage page
        this.backToManageProductLanguage();
    }

    /**
	// Represents closeProductLanguage method
	// @param {TemplateRef<string>} content - content method param.
	// @param {NgForm} productLanguageForm - productLanguageForm method param.
	// @returns {void} - closeProductLanguage method returns void.
    */
    closeProductLanguage(productLanguageForm: NgForm, content: TemplateRef<string>): void {
        // Check if the productLanguage form is dirty or touched
        if (productLanguageForm && (productLanguageForm.dirty || productLanguageForm.touched)) {
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
