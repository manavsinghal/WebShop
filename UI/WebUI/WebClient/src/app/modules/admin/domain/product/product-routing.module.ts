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









// Define the routes for the ProductRoutingModule module
const routes: Routes = [
    {
        path: 'domain',       
        children: [
            { path: 'manageProduct', loadComponent: () => import('./manage-product/manage-product.component.designer').then(m => m.ManageProductComponent) },          
            { path: 'manageProductCategory', loadComponent: () => import('./manage-product-category/manage-product-category.component.designer').then(m => m.ManageProductCategoryComponent) },          
            { path: 'manageProductCategoryLanguage', loadComponent: () => import('./manage-product-category-language/manage-product-category-language.component.designer').then(m => m.ManageProductCategoryLanguageComponent) },          
            { path: 'manageProductLanguage', loadComponent: () => import('./manage-product-language/manage-product-language.component.designer').then(m => m.ManageProductLanguageComponent) },          
            { path: 'viewProduct', loadComponent: () => import('./view-product/view-product.component.designer').then(m => m.ViewProductComponent) },          
            { path: 'viewProductCategory', loadComponent: () => import('./view-product-category/view-product-category.component.designer').then(m => m.ViewProductCategoryComponent) },          
            { path: 'viewProductCategoryLanguage', loadComponent: () => import('./view-product-category-language/view-product-category-language.component.designer').then(m => m.ViewProductCategoryLanguageComponent) },          
            { path: 'viewProductLanguage', loadComponent: () => import('./view-product-language/view-product-language.component.designer').then(m => m.ViewProductLanguageComponent) },          
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
