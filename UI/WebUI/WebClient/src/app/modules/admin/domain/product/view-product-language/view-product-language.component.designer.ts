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
import { ProductLanguage } from '../../../../../shared/models/domain/product/product-language.model';
import { ProductLanguageService } from '../../../../../core/services/domain/product/product-language.service';
import { Language } from '../../../../../shared/models/domain/master/language.model';
import { LanguageService } from '../../../../../core/services/domain/master/language.service';
import { Product } from '../../../../../shared/models/domain/product/product.model';
import { ProductService } from '../../../../../core/services/domain/product/product.service';
import { NgClass } from '@angular/common';

// Define the ViewProductLanguageComponent
@Component({
    // Selector for the component
    selector: 'app-view-product-language',
    // Template URL for the component
    templateUrl: './view-product-language.component.html',
    imports: [TranslateDirective, TreeTableModule, PrimeTemplate, NgClass, TranslatePipe]
})
export class ViewProductLanguageComponent extends SharedComponent implements OnInit, OnDestroy {
    private readonly router = inject(Router);
    private readonly route = inject(ActivatedRoute);
    readonly languageService = inject(LanguageService);
    readonly productLanguageService = inject(ProductLanguageService);
    readonly productService = inject(ProductService);
    override readonly coreSessionService: CoreSessionService;
    readonly coreSubscriptionService = inject(CoreSubscriptionService);
    private readonly translateService = inject(TranslateService);

    readonly viewProductLanguagesTable = viewChild.required<TreeTable>('this.ViewProductLanguagesTable');  
    manageProductLanguage: CustomTreeTableModel<TreeNode<ProductLanguage>> = new CustomTreeTableModel<TreeNode<ProductLanguage>>({
        data: new Array<TreeNode<ProductLanguage>>()
    });
    selectedDetailNode: ProductLanguage = {} as ProductLanguage;
    sortCols!: Array<CustomSortModel>;
    cols!: Array<TreeTableHeaderModel>;
    doubleClicked!: boolean;
    languages!: Array<Language>;
    manageProductLanguageData!: Array<ProductLanguage>;
    productLanguages!: Array<ProductLanguage>;
    products!: Array<Product>;
    screenGroup!: string;
    searchProductLanguage!: string;
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
                 { field: 'ProductUIdValue', header: 'Product', class:'text-left' },          
                 { field: 'LanguageUIdValue', header: 'Language', class:'text-left' },          
                 { field: 'Name', header: 'Name', class: 'text-left' },
        ];
        this.sortCols = [
                 { field: 'ProductUIdValue', type: 'string' },
                 { field: 'LanguageUIdValue', type: 'string' },
                 { field: 'Name', type: 'string' },
        ];
        this.getMasterData();
        this.getProductLanguages();
      
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
           this.refreshProductLanguage();
        // On error, handle the error
        },(error: HttpErrorResponse): void => {
            CoreNotificationService.handleError(error);
        });
       this.languageService.getLanguages().subscribe((languages: Array<Language>): void => {
           this.languages = languages;
           this.refreshProductLanguage();
        // On error, handle the error
        },(error: HttpErrorResponse): void => {
            CoreNotificationService.handleError(error);
        });
    }
    
    /**
	// Represents refreshProductLanguage method
    // Method to refresh grid data.
	// @returns {void} - refreshProductLanguage method returns void.
    */
    refreshProductLanguage(): void {
        if (this.productLanguages && this.productLanguages.length > 0) {
            this.buildProductLanguageTree(this.productLanguages);
        }
    }  

    navigateToManageEntity(): void {
        const navigationExtras: NavigationExtras = {
            queryParams: {
                screenGroup: this.screenGroup,
                serviceGroup: this.serviceGroup
            }
        };

        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/viewProduct'], navigationExtras);
    }

    backToAdministration(): void {
        void this.router.navigate(['/app/administration']);
    }

    getProductLanguages(): void {
       this.productLanguageService.getProductLanguages()
            .subscribe((productLanguages: Array<ProductLanguage>): void => {
                if (productLanguages && productLanguages.length) {
                    this.productLanguages = productLanguages;
                    this.buildProductLanguageTree(productLanguages);
                }
            // On error, handle the error
            }, (error: HttpErrorResponse): void => {
                CoreNotificationService.handleError(error);
            }); 
    }

    /**
	// Represents buildProductLanguageTree method
	// @param {Array<ProductLanguage>} activeProductLanguages - activeProductLanguages method param.
	// @returns {void} - buildProductLanguageTree method returns void.
    */
    buildProductLanguageTree(activeProductLanguages: Array<ProductLanguage>): void {
        const productLanguages: Array<TreeNode> = this.getProductLanguageNode(activeProductLanguages);
        this.manageProductLanguage.data = productLanguages;
        this.manageProductLanguage.data = [...this.customSortMultipleColumns(this.manageProductLanguage.data, this.sortCols)];
    }

    /**
	// Represents getProductLanguageNode method
	// @param {Array<ProductLanguage>} arr - arr method param.
	// @returns {Array<TreeNode>} - getProductLanguageNode method returns Array<TreeNode>.
    */
    getProductLanguageNode(arr: Array<ProductLanguage>): Array<TreeNode> {
        const node: Array<TreeNode> = [];
        arr.forEach((productLanguage: ProductLanguage): void => {
            const innerNode: TreeNode = {
                data: productLanguage
            };
            innerNode.data.LanguageUIdValue = this.getDetailsForUId(this.languages, productLanguage.LanguageUId, 'LanguageUId', 'DisplayName');
                        
            innerNode.data.ProductUIdValue = this.getDetailsForUId(this.products, productLanguage.ProductUId, 'ProductUId', 'Name');
                        
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
        this.searchProductLanguage = eventValue;
        if (eventValue && eventValue.length >= 3) {
            content.filterGlobal(eventValue, 'contains');
        } else {
            content.filterGlobal('', 'contains');
        }
    }

    addProductLanguage(): void {
        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageProductLanguage'],
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
	// @param {TreeNode<ProductLanguage>} event - event method param.
	// @returns {void} - onNodeSelected method returns void.
    */
    onNodeSelected(event: TreeNode<ProductLanguage>): void {
        setTimeout((): void => {
            if (!this.doubleClicked) {
                if (event.data) {
                    this.loadSelectedDetail(event.data.ProductLanguageUId, this.manageProductLanguage.data);
                }
            } else {
                this.doubleClicked = false;
            }
        }, 100);
    }

    /**
	// Represents navigateToProductLanguage method
	// @param {string} action - action method param.
	// @param {ProductLanguage} nodeData - nodeData method param.
	// @returns {void} - navigateToProductLanguage method returns void.
    */
    navigateToProductLanguage(nodeData: ProductLanguage, action: string): void {
        if (nodeData) {

            void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageProductLanguage'],
                {
                    queryParams: {
                        action,
                        productLanguageUId: nodeData.ProductLanguageUId,
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

        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageProductLanguage'],
            {
                queryParams: {
                    action: 'Edit',
                    productLanguageUId: this.selectedDetailNode.ProductLanguageUId,
                    screenGroup: this.screenGroup,
                    serviceGroup: this.serviceGroup
                }
            });
    }

    /**
	// Represents loadSelectedDetail method
	// @param {Array<TreeNode>} items - items method param.
	// @param {Guid} productLanguageUId - productLanguageUId method param.
	// @returns {void} - loadSelectedDetail method returns void.
    */
    loadSelectedDetail(productLanguageUId: Guid, items: Array<TreeNode>): void {
        if (items) {
            this.selectedDetailNode = items.filter((x => x.data.ProductLanguageUId === productLanguageUId))[0].data;
        }
    }
}
