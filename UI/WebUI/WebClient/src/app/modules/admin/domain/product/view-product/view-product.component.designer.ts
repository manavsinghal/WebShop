/* Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright>
*/

// Import necessary Angular modules and services
import { Component, OnInit, OnDestroy, ViewChild, inject } from '@angular/core';
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
import { Product } from '../../../../../shared/models/domain/product/product.model';
import { ProductService } from '../../../../../core/services/domain/product/product.service';
import { ProductCategory } from '../../../../../shared/models/domain/product/product-category.model';
import { ProductCategoryService } from '../../../../../core/services/domain/product/product-category.service';
import { Seller } from '../../../../../shared/models/domain/seller/seller.model';
import { SellerService } from '../../../../../core/services/domain/seller/seller.service';
import { NgClass } from '@angular/common';

// Define the ViewProductComponent
@Component({
    // Selector for the component
    selector: 'app-view-product',
    // Template URL for the component
    templateUrl: './view-product.component.html',
    imports: [RouterLink, TranslateDirective, TreeTableModule, PrimeTemplate, NgClass, TranslatePipe]
})
export class ViewProductComponent extends SharedComponent implements OnInit, OnDestroy {
    private readonly router = inject(Router);
    private readonly route = inject(ActivatedRoute);
    readonly productCategoryService = inject(ProductCategoryService);
    readonly productService = inject(ProductService);
    readonly sellerService = inject(SellerService);
    override readonly coreSessionService: CoreSessionService;
    readonly coreSubscriptionService = inject(CoreSubscriptionService);
    private readonly translateService = inject(TranslateService);

    @ViewChild('this.ViewProductsTable', { static: false }) viewProductsTable!: TreeTable;  
    manageProduct: CustomTreeTableModel<TreeNode<Product>> = new CustomTreeTableModel<TreeNode<Product>>({
        data: new Array<TreeNode<Product>>()
    });
    selectedDetailNode: Product = {} as Product;
    sortCols!: Array<CustomSortModel>;
    cols!: Array<TreeTableHeaderModel>;
    doubleClicked!: boolean;
    manageProductData!: Array<Product>;
    productCategoryParents!: Array<ProductCategory>;
    products!: Array<Product>;
    screenGroup!: string;
    searchProduct!: string;
    selectedNode!: TreeTableNode | null;
    sellers!: Array<Seller>;
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
                 { field: 'SellerUIdValue', header: 'Seller', class:'text-left' },          
                 { field: 'Name', header: 'Name', class: 'text-left' },
                 { field: 'ProductCategoryParentUIdValue', header: 'ProductCategoryParent', class:'text-left' },          
                 { field: 'Rate', header: 'Rate', class:'text-right' },           
                 { field: 'TotalQuantity', header: 'TotalQuantity', class:'text-right' },           
        ];
        this.sortCols = [
                 { field: 'SellerUIdValue', type: 'string' },
                 { field: 'Name', type: 'string' },
                 { field: 'ProductCategoryParentUIdValue', type: 'string' },
                 { field: 'Rate', type: 'number' },
                 { field: 'TotalQuantity', type: 'number' },
        ];
        this.getMasterData();
        this.getProducts();
      
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
       this.sellerService.getSellers().subscribe((sellers: Array<Seller>): void => {
           this.sellers = sellers;
           this.refreshProduct();
        // On error, handle the error
        },(error: HttpErrorResponse): void => {
            CoreNotificationService.handleError(error);
        });
       this.productCategoryService.getProductCategories().subscribe((productCategories: Array<ProductCategory>): void => {
           this.productCategoryParents = productCategories;
           this.refreshProduct();
        // On error, handle the error
        },(error: HttpErrorResponse): void => {
            CoreNotificationService.handleError(error);
        });
    }
    
    /**
	// Represents refreshProduct method
    // Method to refresh grid data.
	// @returns {void} - refreshProduct method returns void.
    */
    refreshProduct(): void {
        if (this.products && this.products.length > 0) {
            this.buildProductTree(this.products);
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

    getProducts(): void {
       this.productService.getProducts()
            .subscribe((products: Array<Product>): void => {
                if (products && products.length) {
                    this.products = products;
                    this.buildProductTree(products);
                }
            // On error, handle the error
            }, (error: HttpErrorResponse): void => {
                CoreNotificationService.handleError(error);
            }); 
    }

    /**
	// Represents buildProductTree method
	// @param {Array<Product>} activeProducts - activeProducts method param.
	// @returns {void} - buildProductTree method returns void.
    */
    buildProductTree(activeProducts: Array<Product>): void {
        const products: Array<TreeNode> = this.getProductNode(activeProducts);
        this.manageProduct.data = products;
        this.manageProduct.data = [...this.customSortMultipleColumns(this.manageProduct.data, this.sortCols)];
    }

    /**
	// Represents getProductNode method
	// @param {Array<Product>} arr - arr method param.
	// @returns {Array<TreeNode>} - getProductNode method returns Array<TreeNode>.
    */
    getProductNode(arr: Array<Product>): Array<TreeNode> {
        const node: Array<TreeNode> = [];
        arr.forEach((product: Product): void => {
            const innerNode: TreeNode = {
                data: product
            };
            innerNode.data.ProductCategoryParentUIdValue = this.getDetailsForUId(this.productCategoryParents, product.ProductCategoryParentUId, 'ProductCategoryUId', 'Name');
                        
            innerNode.data.SellerUIdValue = this.getDetailsForUId(this.sellers, product.SellerUId, 'SellerUId', 'Name');
                        
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
        this.searchProduct = eventValue;
        if (eventValue && eventValue.length >= 3) {
            content.filterGlobal(eventValue, 'contains');
        } else {
            content.filterGlobal('', 'contains');
        }
    }

    addProduct(): void {
        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageProduct'],
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
	// @param {TreeNode<Product>} event - event method param.
	// @returns {void} - onNodeSelected method returns void.
    */
    onNodeSelected(event: TreeNode<Product>): void {
        setTimeout((): void => {
            if (!this.doubleClicked) {
                if (event.data) {
                    this.loadSelectedDetail(event.data.ProductUId, this.manageProduct.data);
                }
            } else {
                this.doubleClicked = false;
            }
        }, 100);
    }

    /**
	// Represents navigateToProduct method
	// @param {Product} nodeData - nodeData method param.
	// @param {string} action - action method param.
	// @returns {void} - navigateToProduct method returns void.
    */
    navigateToProduct(nodeData: Product, action: string): void {
        if (nodeData) {

            void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageProduct'],
                {
                    queryParams: {
                        action,
                        productUId: nodeData.ProductUId,
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

        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/manageProduct'],
            {
                queryParams: {
                    action: 'Edit',
                    productUId: this.selectedDetailNode.ProductUId,
                    screenGroup: this.screenGroup,
                    serviceGroup: this.serviceGroup
                }
            });
    }

    /**
	// Represents loadSelectedDetail method
	// @param {Guid} productUId - productUId method param.
	// @param {Array<TreeNode>} items - items method param.
	// @returns {void} - loadSelectedDetail method returns void.
    */
    loadSelectedDetail(productUId: Guid, items: Array<TreeNode>): void {
        if (items) {
            this.selectedDetailNode = items.filter((x => x.data.ProductUId === productUId))[0].data;
        }
    }

    NavigateToProductTranslation(): void {
        const navigationExtras: NavigationExtras = {
            queryParams: {
                screenGroup: this.screenGroup,
                serviceGroup: this.serviceGroup
            }
        };

        void this.router.navigate(['/app/administration/' + this.serviceGroup + '/viewProductLanguage'], navigationExtras);
    }
}
