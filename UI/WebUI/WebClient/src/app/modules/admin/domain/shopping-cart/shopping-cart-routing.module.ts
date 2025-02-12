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
import { ManageShoppingCartComponent } from './manage-shopping-cart/manage-shopping-cart.component.designer';
import { ManageShoppingCartWishListComponent } from './manage-shopping-cart-wish-list/manage-shopping-cart-wish-list.component.designer';
import { ViewShoppingCartComponent } from './view-shopping-cart/view-shopping-cart.component.designer';
import { ViewShoppingCartWishListComponent } from './view-shopping-cart-wish-list/view-shopping-cart-wish-list.component.designer';

// Define the routes for the ShoppingCartRoutingModule module
const routes: Routes = [
    {
        path: 'domain',       
        children: [
            { path: 'manageShoppingCart', component: ManageShoppingCartComponent },          
            { path: 'manageShoppingCartWishList', component: ManageShoppingCartWishListComponent },          
            { path: 'viewShoppingCart', component: ViewShoppingCartComponent },          
            { path: 'viewShoppingCartWishList', component: ViewShoppingCartWishListComponent },          
        ]
    }  
];

// Define the  ShoppingCartRoutingModule routing module
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
// Export the ShoppingCartRoutingModule class
export class ShoppingCartRoutingModule { }
