/* Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright>
*/

import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ManageProductComponent } from './manage-product/manage-product.component.designer';
import { ManageProductCategoryComponent } from './manage-product-category/manage-product-category.component.designer';
import { ManageProductCategoryLanguageComponent } from './manage-product-category-language/manage-product-category-language.component.designer';
import { ManageProductLanguageComponent } from './manage-product-language/manage-product-language.component.designer';
import { ViewProductComponent } from './view-product/view-product.component.designer';
import { ViewProductCategoryComponent } from './view-product-category/view-product-category.component.designer';
import { ViewProductCategoryLanguageComponent } from './view-product-category-language/view-product-category-language.component.designer';
import { ViewProductLanguageComponent } from './view-product-language/view-product-language.component.designer';

// Define the routes for the ProductRoutingModule module
const routes: Routes = [
    {
        path: 'domain',       
        children: [
            { path: 'manageProduct', component: ManageProductComponent },          
            { path: 'manageProductCategory', component: ManageProductCategoryComponent },          
            { path: 'manageProductCategoryLanguage', component: ManageProductCategoryLanguageComponent },          
            { path: 'manageProductLanguage', component: ManageProductLanguageComponent },          
            { path: 'viewProduct', component: ViewProductComponent },          
            { path: 'viewProductCategory', component: ViewProductCategoryComponent },          
            { path: 'viewProductCategoryLanguage', component: ViewProductCategoryLanguageComponent },          
            { path: 'viewProductLanguage', component: ViewProductLanguageComponent },          
        ]
    }  
];

// Define the  ProductRoutingModule routing module
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
// Export the ProductRoutingModule class
export class ProductRoutingModule { }
