/* Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright>
*/

import { Routes } from '@angular/router';





// Define the routes for the SellerRoutingModule module
export const routes: Routes = [
    {
        path: 'domain',       
        children: [
            { path: 'manageSeller', loadComponent: () => import('./manage-seller/manage-seller.component.designer').then(m => m.ManageSellerComponent) },          
            { path: 'manageSellerAddress', loadComponent: () => import('./manage-seller-address/manage-seller-address.component.designer').then(m => m.ManageSellerAddressComponent) },          
            { path: 'manageSellerBankAccount', loadComponent: () => import('./manage-seller-bank-account/manage-seller-bank-account.component.designer').then(m => m.ManageSellerBankAccountComponent) },          
            { path: 'manageSellerPhone', loadComponent: () => import('./manage-seller-phone/manage-seller-phone.component.designer').then(m => m.ManageSellerPhoneComponent) },          
            { path: 'viewSeller', loadComponent: () => import('./view-seller/view-seller.component.designer').then(m => m.ViewSellerComponent) },          
            { path: 'viewSellerAddress', loadComponent: () => import('./view-seller-address/view-seller-address.component.designer').then(m => m.ViewSellerAddressComponent) },          
            { path: 'viewSellerBankAccount', loadComponent: () => import('./view-seller-bank-account/view-seller-bank-account.component.designer').then(m => m.ViewSellerBankAccountComponent) },          
            { path: 'viewSellerPhone', loadComponent: () => import('./view-seller-phone/view-seller-phone.component.designer').then(m => m.ViewSellerPhoneComponent) },          
        ]
    }  
];


