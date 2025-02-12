/* Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright>
*/

// Import necessary Angular modules and services
import { Component, OnInit, OnDestroy, inject, viewChild } from '@angular/core';
import { Router, NavigationExtras, ActivatedRoute, RouterLink } from '@angular/router';
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
import { ProductCategory } from '../../../../../shared/models/domain/product/product-category.model';
import { ProductCategoryService } from '../../../../../core/services/domain/product/product-category.service';
import { NgClass } from '@angular/common';

// Define the ViewProductCategoryComponent
@Component({
    // Selector for the component
    selector: 'app-view-product-category',
    // Template URL for the component
    templateUrl: './view-product-category.component.html',
    imports: [RouterLink, TranslateDirective, TreeTableModule, PrimeTemplate, NgClass, TranslatePipe]
})
export class ViewProductCategoryComponent extends SharedComponent implements OnInit, OnDestroy {
    private readonly router = inject(Router);
    private readonly route = inject(ActivatedRoute);
    readonly productCategoryService = inject(ProductCategoryService);
    override readonly coreSessionService: CoreSessionService;
    readonly coreSubscriptionService = inject(CoreSubscriptionService);
    private readonly translateService = inject(TranslateService);

    readonly viewProductCategoriesTable = viewChild.required<TreeTable>('this.ViewProductCategoriesTable');  
    manageProductCategory: CustomTreeTableModel<TreeNode<ProductCategory>> = new CustomTreeTableModel<TreeNode<ProductCategory>>({
        data: new Array<TreeNode<ProductCategory>>()
    });
    selectedDetailNode: ProductCategory = {} as ProductCategory;
    sortCols!: Array<CustomSortModel>;
    cols!: Array<TreeTableHeaderModel>;
    doubleClicked!: boolean;
    manageProductCategoryData!: Array<ProductCategory>;
    productCategories!: Array<ProductCategory>;
    productCategoryParents!: Array<ProductCategory>;
    screenGroup!: string;
    searchProductCategory!: string;
    selectedNode!: TreeTableNode | null;
    serviceGroup!: string;
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
        this.cols = [
                 { field: 'Name', header: 'Name', class: 'text-left' },
                 { field: 'ProductCategoryParentUIdValue', header: 'ProductCategoryParent', class:'text-left' },          
                 { field: 'Description', header: 'Description', class: 'text-left' },
                 { field: 'SortOrder', header: 'SortOrder', class:'text-right' },           
        ];
        this.sortCols = [
                 { field: 'Name', type: 'string' },
                 { field: 'ProductCategoryParentUIdValue', type: 'string' },
                 { field: 'Description', type: 'string' },
                 { field: 'SortOrder', type: 'number' },
        ];
        this.getMasterData();
        this.getProductCategories();
      
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
           this.refreshProductCategory();
        // On error, handle the error
        },(error: HttpErrorResponse): void => {
            CoreNotificationService.handleError(error);
        });
    }
    
    /**
	// Represents refreshProductCategory method
    // Method to refresh grid data.
	// @returns {void} - refreshProductCategory method returns void.
    */
    refreshProductCategory(): void {
        if (this.productCategories && this.productCategories.length > 0) {
            this.buildProductCategoryTree(this.productCategories);
        }
    }  

    navigateToManageEntity(): void {
        const navigationExtras: NavigationExtras = {
            queryParams: {
                screenGroup: this.screenGroup,
                serviceGroup: this.serviceGroup
            }
        };

        void this.router.navigate(['/app/administration/manageAdminEntities'], navigationExtras);
    }

    backToAdministration(): void {
        void this.router.navigate(['/app/administration']);
    }

    getProductCategories(): void {
       this.productCategoryService.getProductCategories()
            .subscribe((productCategories: Array<ProductCategory>): void => {
                if (productCategories && productCategories.length) {
                    this.productCategories = productCategories;
                    this.buildProductCategoryTree(productCategories);
                }
            // On error, handle the error
            }, (error: HttpErrorResponse): void => {
                CoreNotificationService.handleError(error);
            }); 
    }

    /**
	// Represents buildProductCategoryTree method
	// @param {Array<ProductCategory>} activeProductCategories - activeProductCategories method param.
	// @returns {void} - buildProductCategoryTree method returns void.
    */
    buildProductCategoryTree(activeProductCategories: Array<ProductCategory>): void {
        const productCategories: Array<TreeNode> = this.getProductCategoryNode(activeProductCategories);
        this.manageProductCategory.data = productCategories;
        this.manageProductCategory.data = [...this.customSortMultipleColumns(this.manageProductCategory.data, this.sortCols)];
    }

    /**
	// Represents getProductCategoryNode method
	// @param {Array<ProductCategory>} arr - arr method param.
	// @returns {Array<TreeNode>} - getProductCategoryNode method returns Array<TreeNode>.
    */
    getProductCategoryNode(arr: Array<ProductCategory>): Array<TreeNode> {
        const node: Array<TreeNode> = [];
        arr.forEach((productCategory: ProductCategory): void => {
            const innerNode: TreeNode = {
                data: productCategory
            };
            innerNode.data.ProductCategoryParentUIdValue = this.getDetailsForUId(this.productCategoryParents, productCategory.ProductCategoryParentUId, 'ProductCategoryUId', 'Name');
                        
            node.push(innerNode);
        });

        return node;
    }

    /**
	// Represents customSearch method
	// @param {Event} event - event method param.
	// @param {TreeTable} content - content method param.
	// @returns {void} - customSearch method returns void.
    */
    customSearch(event: Event, content: TreeTable): void {
        const eventValue = (event.target as HTMLInputElement).value;
        this.searchProductCategory = eventValue;
        if (eventValue && eventValue.length >= 3) {
            content.filterGlobal(eventValue, 'contains');
        } else {
            content.filterGlobal('', 'contains');
        }
    }

    addProductCategory(): void {
        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageProductCategory'],
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
	// @param {TreeNode<ProductCategory>} event - event method param.
	// @returns {void} - onNodeSelected method returns void.
    */
    onNodeSelected(event: TreeNode<ProductCategory>): void {
        setTimeout((): void => {
            if (!this.doubleClicked) {
                if (event.data) {
                    this.loadSelectedDetail(event.data.ProductCategoryUId, this.manageProductCategory.data);
                }
            } else {
                this.doubleClicked = false;
            }
        }, 100);
    }

    /**
	// Represents navigateToProductCategory method
	// @param {string} action - action method param.
	// @param {ProductCategory} nodeData - nodeData method param.
	// @returns {void} - navigateToProductCategory method returns void.
    */
    navigateToProductCategory(nodeData: ProductCategory, action: string): void {
        if (nodeData) {

            void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageProductCategory'],
                {
                    queryParams: {
                        action,
                        productCategoryUId: nodeData.ProductCategoryUId,
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

        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageProductCategory'],
            {
                queryParams: {
                    action: 'Edit',
                    productCategoryUId: this.selectedDetailNode.ProductCategoryUId,
                    screenGroup: this.screenGroup,
                    serviceGroup: this.serviceGroup
                }
            });
    }

    /**
	// Represents loadSelectedDetail method
	// @param {Guid} productCategoryUId - productCategoryUId method param.
	// @param {Array<TreeNode>} items - items method param.
	// @returns {void} - loadSelectedDetail method returns void.
    */
    loadSelectedDetail(productCategoryUId: Guid, items: Array<TreeNode>): void {
        if (items) {
            this.selectedDetailNode = items.filter((x => x.data.ProductCategoryUId === productCategoryUId))[0].data;
        }
    }

    NavigateToProductCategoryTranslation(): void {
        const navigationExtras: NavigationExtras = {
            queryParams: {
                screenGroup: this.screenGroup,
                serviceGroup: this.serviceGroup
            }
        };

        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/viewProductCategoryLanguage'], navigationExtras);
    }
}
