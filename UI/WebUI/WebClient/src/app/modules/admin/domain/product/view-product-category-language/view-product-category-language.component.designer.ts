/* Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright>
*/

// Import necessary Angular modules and services
import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { Router, NavigationExtras, ActivatedRoute } from '@angular/router';
import { TranslateService, TranslateDirective, TranslatePipe } from '@ngx-translate/core';
import { CoreSessionService } from '../../../../../core/services/core.session.service';
import { CoreSubscriptionService } from '../../../../../core/services/core.subscription.service';
import { Subscription } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { Guid } from '../../../../../core/helpers/guid';
import { SharedComponent } from '../../../../../shared/shared.component';
import { TreeNode, TreeTableNode, PrimeTemplate } from 'primeng/api';
import { TreeTable, TreeTableModule } from 'primeng/treetable';
import { CustomSortModel, CustomTreeTableModel } from '../../../../../shared/models/custom-tree-table.model';
import { TreeTableHeaderModel } from '../../../../../shared/models/tree-table.model';
import { CoreNotificationService } from '../../../../../core/services/core.notification.service';
import { ProductCategoryLanguage } from '../../../../../shared/models/domain/product/product-category-language.model';
import { ProductCategoryLanguageService } from '../../../../../core/services/domain/product/product-category-language.service';
import { Language } from '../../../../../shared/models/domain/master/language.model';
import { LanguageService } from '../../../../../core/services/domain/master/language.service';
import { ProductCategory } from '../../../../../shared/models/domain/product/product-category.model';
import { ProductCategoryService } from '../../../../../core/services/domain/product/product-category.service';
import { NgClass } from '@angular/common';

// Define the ViewProductCategoryLanguageComponent
@Component({
    // Selector for the component
    selector: 'app-view-product-category-language',
    // Template URL for the component
    templateUrl: './view-product-category-language.component.html',
    imports: [TranslateDirective, TreeTableModule, PrimeTemplate, NgClass, TranslatePipe]
})
export class ViewProductCategoryLanguageComponent extends SharedComponent implements OnInit, OnDestroy {
    @ViewChild('this.ViewProductCategoryLanguagesTable', { static: false }) viewProductCategoryLanguagesTable!: TreeTable;  
    manageProductCategoryLanguage: CustomTreeTableModel<TreeNode<ProductCategoryLanguage>> = new CustomTreeTableModel<TreeNode<ProductCategoryLanguage>>({
        data: new Array<TreeNode<ProductCategoryLanguage>>()
    });
    selectedDetailNode: ProductCategoryLanguage = {} as ProductCategoryLanguage;
    sortCols!: Array<CustomSortModel>;
    cols!: Array<TreeTableHeaderModel>;
    doubleClicked!: boolean;
    languages!: Array<Language>;
    manageProductCategoryLanguageData!: Array<ProductCategoryLanguage>;
    productCategories!: Array<ProductCategory>;
    productCategoryLanguages!: Array<ProductCategoryLanguage>;
    screenGroup!: string;
    searchProductCategoryLanguage!: string;
    selectedNode!: TreeTableNode | null;
    serviceGroup!: string;
    languageChangedSubscription!: Subscription;
    // Constructor for the component
    constructor(private readonly router: Router,
        private readonly route: ActivatedRoute,
        readonly languageService: LanguageService,
        readonly productCategoryLanguageService: ProductCategoryLanguageService,
        readonly productCategoryService: ProductCategoryService,
        override readonly coreSessionService: CoreSessionService,
        readonly coreSubscriptionService: CoreSubscriptionService,
        private readonly translateService: TranslateService) {
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
        this.cols = [
                 { field: 'ProductCategoryUIdValue', header: 'ProductCategory', class:'text-left' },          
                 { field: 'LanguageUIdValue', header: 'Language', class:'text-left' },          
                 { field: 'Name', header: 'Name', class: 'text-left' },
                 { field: 'Description', header: 'Description', class: 'text-left' },
        ];
        this.sortCols = [
                 { field: 'ProductCategoryUIdValue', type: 'string' },
                 { field: 'LanguageUIdValue', type: 'string' },
                 { field: 'Name', type: 'string' },
                 { field: 'Description', type: 'string' },
        ];
        this.getMasterData();
        this.getProductCategoryLanguages();
      
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
           this.refreshProductCategoryLanguage();
        // On error, handle the error
        },(error: HttpErrorResponse): void => {
            CoreNotificationService.handleError(error);
        });
       this.languageService.getLanguages().subscribe((languages: Array<Language>): void => {
           this.languages = languages;
           this.refreshProductCategoryLanguage();
        // On error, handle the error
        },(error: HttpErrorResponse): void => {
            CoreNotificationService.handleError(error);
        });
    }
    
    /**
	// Represents refreshProductCategoryLanguage method
    // Method to refresh grid data.
	// @returns {void} - refreshProductCategoryLanguage method returns void.
    */
    refreshProductCategoryLanguage(): void {
        if (this.productCategoryLanguages && this.productCategoryLanguages.length > 0) {
            this.buildProductCategoryLanguageTree(this.productCategoryLanguages);
        }
    }  

    navigateToManageEntity(): void {
        const navigationExtras: NavigationExtras = {
            queryParams: {
                screenGroup: this.screenGroup,
                serviceGroup: this.serviceGroup
            }
        };

        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/viewProductCategory'], navigationExtras);
    }

    backToAdministration(): void {
        void this.router.navigate(['/app/administration']);
    }

    getProductCategoryLanguages(): void {
       this.productCategoryLanguageService.getProductCategoryLanguages()
            .subscribe((productCategoryLanguages: Array<ProductCategoryLanguage>): void => {
                if (productCategoryLanguages && productCategoryLanguages.length) {
                    this.productCategoryLanguages = productCategoryLanguages;
                    this.buildProductCategoryLanguageTree(productCategoryLanguages);
                }
            // On error, handle the error
            }, (error: HttpErrorResponse): void => {
                CoreNotificationService.handleError(error);
            }); 
    }

    /**
	// Represents buildProductCategoryLanguageTree method
	// @param {Array<ProductCategoryLanguage>} activeProductCategoryLanguages - activeProductCategoryLanguages method param.
	// @returns {void} - buildProductCategoryLanguageTree method returns void.
    */
    buildProductCategoryLanguageTree(activeProductCategoryLanguages: Array<ProductCategoryLanguage>): void {
        const productCategoryLanguages: Array<TreeNode> = this.getProductCategoryLanguageNode(activeProductCategoryLanguages);
        this.manageProductCategoryLanguage.data = productCategoryLanguages;
        this.manageProductCategoryLanguage.data = [...this.customSortMultipleColumns(this.manageProductCategoryLanguage.data, this.sortCols)];
    }

    /**
	// Represents getProductCategoryLanguageNode method
	// @param {Array<ProductCategoryLanguage>} arr - arr method param.
	// @returns {Array<TreeNode>} - getProductCategoryLanguageNode method returns Array<TreeNode>.
    */
    getProductCategoryLanguageNode(arr: Array<ProductCategoryLanguage>): Array<TreeNode> {
        const node: Array<TreeNode> = [];
        arr.forEach((productCategoryLanguage: ProductCategoryLanguage): void => {
            const innerNode: TreeNode = {
                data: productCategoryLanguage
            };
            innerNode.data.LanguageUIdValue = this.getDetailsForUId(this.languages, productCategoryLanguage.LanguageUId, 'LanguageUId', 'DisplayName');
                        
            innerNode.data.ProductCategoryUIdValue = this.getDetailsForUId(this.productCategories, productCategoryLanguage.ProductCategoryUId, 'ProductCategoryUId', 'Name');
                        
            node.push(innerNode);
        });

        return node;
    }

    /**
	// Represents customSearch method
	// @param {TreeTable} content - content method param.
	// @param {Event} event - event method param.
	// @returns {void} - customSearch method returns void.
    */
    customSearch(event: Event, content: TreeTable): void {
        const eventValue = (event.target as HTMLInputElement).value;
        this.searchProductCategoryLanguage = eventValue;
        if (eventValue && eventValue.length >= 3) {
            content.filterGlobal(eventValue, 'contains');
        } else {
            content.filterGlobal('', 'contains');
        }
    }

    addProductCategoryLanguage(): void {
        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageProductCategoryLanguage'],
            {
                queryParams: {
                    action: 'Add',
                    screenGroup: this.screenGroup,
                    serviceGroup: this.serviceGroup
                }
            });
    }

    /**
	// Represents onNodeSelected method
	// @param {TreeNode<ProductCategoryLanguage>} event - event method param.
	// @returns {void} - onNodeSelected method returns void.
    */
    onNodeSelected(event: TreeNode<ProductCategoryLanguage>): void {
        setTimeout((): void => {
            if (!this.doubleClicked) {
                if (event.data) {
                    this.loadSelectedDetail(event.data.ProductCategoryLanguageUId, this.manageProductCategoryLanguage.data);
                }
            } else {
                this.doubleClicked = false;
            }
        }, 100);
    }

    /**
	// Represents navigateToProductCategoryLanguage method
	// @param {string} action - action method param.
	// @param {ProductCategoryLanguage} nodeData - nodeData method param.
	// @returns {void} - navigateToProductCategoryLanguage method returns void.
    */
    navigateToProductCategoryLanguage(nodeData: ProductCategoryLanguage, action: string): void {
        if (nodeData) {

            void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageProductCategoryLanguage'],
                {
                    queryParams: {
                        action,
                        productCategoryLanguageUId: nodeData.ProductCategoryLanguageUId,
                        screenGroup: this.screenGroup,
                        serviceGroup: this.serviceGroup
                    }
                });
        }
    }

    /**
	// Represents onRowDblclick method
	// @returns {void} - onRowDblclick method returns void.
    */
    onRowDblclick(): void {
        this.doubleClicked = true;

        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageProductCategoryLanguage'],
            {
                queryParams: {
                    action: 'Edit',
                    productCategoryLanguageUId: this.selectedDetailNode.ProductCategoryLanguageUId,
                    screenGroup: this.screenGroup,
                    serviceGroup: this.serviceGroup
                }
            });
    }

    /**
	// Represents loadSelectedDetail method
	// @param {Guid} productCategoryLanguageUId - productCategoryLanguageUId method param.
	// @param {Array<TreeNode>} items - items method param.
	// @returns {void} - loadSelectedDetail method returns void.
    */
    loadSelectedDetail(productCategoryLanguageUId: Guid, items: Array<TreeNode>): void {
        if (items) {
            this.selectedDetailNode = items.filter((x => x.data.ProductCategoryLanguageUId === productCategoryLanguageUId))[0].data;
        }
    }
}
