/* Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright>
*/

import { Routes } from '@angular/router';






// Define the routes for the CustomerRoutingModule module
export const routes: Routes = [
    {
        path: 'domain',       
        children: [
            { path: 'manageCustomer', loadComponent: () => import('./manage-customer/manage-customer.component.designer').then(m => m.ManageCustomerComponent) },          
            { path: 'manageCustomerAddress', loadComponent: () => import('./manage-customer-address/manage-customer-address.component.designer').then(m => m.ManageCustomerAddressComponent) },          
            { path: 'manageCustomerCard', loadComponent: () => import('./manage-customer-card/manage-customer-card.component.designer').then(m => m.ManageCustomerCardComponent) },          
            { path: 'manageCustomerPhone', loadComponent: () => import('./manage-customer-phone/manage-customer-phone.component.designer').then(m => m.ManageCustomerPhoneComponent) },          
            { path: 'viewCustomer', loadComponent: () => import('./view-customer/view-customer.component.designer').then(m => m.ViewCustomerComponent) },          
            { path: 'viewCustomerAddress', loadComponent: () => import('./view-customer-address/view-customer-address.component.designer').then(m => m.ViewCustomerAddressComponent) },          
            { path: 'viewCustomerCard', loadComponent: () => import('./view-customer-card/view-customer-card.component.designer').then(m => m.ViewCustomerCardComponent) },          
            { path: 'viewCustomerPhone', loadComponent: () => import('./view-customer-phone/view-customer-phone.component.designer').then(m => m.ViewCustomerPhoneComponent) },          
        ]
    }  
];
