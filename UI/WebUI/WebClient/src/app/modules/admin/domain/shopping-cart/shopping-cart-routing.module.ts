/* Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright>
*/

import { Routes } from '@angular/router';


// Define the routes for the ShoppingCartRoutingModule module
export const routes: Routes = [
    {
        path: 'domain',       
        children: [
            { path: 'manageShoppingCart', loadComponent: () => import('./manage-shopping-cart/manage-shopping-cart.component.designer').then(m => m.ManageShoppingCartComponent) },          
            { path: 'manageShoppingCartWishList', loadComponent: () => import('./manage-shopping-cart-wish-list/manage-shopping-cart-wish-list.component.designer').then(m => m.ManageShoppingCartWishListComponent) },          
            { path: 'viewShoppingCart', loadComponent: () => import('./view-shopping-cart/view-shopping-cart.component.designer').then(m => m.ViewShoppingCartComponent) },          
            { path: 'viewShoppingCartWishList', loadComponent: () => import('./view-shopping-cart-wish-list/view-shopping-cart-wish-list.component.designer').then(m => m.ViewShoppingCartWishListComponent) },          
        ]
    }  
];

