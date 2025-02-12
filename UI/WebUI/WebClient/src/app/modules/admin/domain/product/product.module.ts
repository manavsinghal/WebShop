/* Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright>
*/

import { NgModule } from '@angular/core';
import { SortPipe } from '../../../../shared/pipes/sort.pipe';
import { SharedModule } from '../../../../shared/shared.module';
import { ProductRoutingModule } from './product-routing.module';
import { ManageProductComponent } from '../../domain/product/manage-product/manage-product.component.designer';
import { ManageProductCategoryComponent } from '../../domain/product/manage-product-category/manage-product-category.component.designer';
import { ManageProductCategoryLanguageComponent } from '../../domain/product/manage-product-category-language/manage-product-category-language.component.designer';
import { ManageProductLanguageComponent } from '../../domain/product/manage-product-language/manage-product-language.component.designer';
import { ViewProductComponent } from '../../domain/product/view-product/view-product.component.designer';
import { ViewProductCategoryComponent } from '../../domain/product/view-product-category/view-product-category.component.designer';
import { ViewProductCategoryLanguageComponent } from '../../domain/product/view-product-category-language/view-product-category-language.component.designer';
import { ViewProductLanguageComponent } from '../../domain/product/view-product-language/view-product-language.component.designer';

@NgModule({
    imports: [
        ProductRoutingModule,
        SharedModule,
        ManageProductComponent,
        ManageProductCategoryComponent,
        ManageProductCategoryLanguageComponent,
        ManageProductLanguageComponent,
        ViewProductComponent,
        ViewProductCategoryComponent,
        ViewProductCategoryLanguageComponent,
        ViewProductLanguageComponent
    ],
    providers: [SortPipe]
})
	
	

// Export the ProductModule class
export class ProductModule { }
