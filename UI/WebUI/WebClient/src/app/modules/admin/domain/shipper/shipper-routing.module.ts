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









// Define the routes for the ShipperRoutingModule module
const routes: Routes = [
    {
        path: 'domain',       
        children: [
            { path: 'manageShipper', loadComponent: () => import('./manage-shipper/manage-shipper.component.designer').then(m => m.ManageShipperComponent) },          
            { path: 'manageShipperAddress', loadComponent: () => import('./manage-shipper-address/manage-shipper-address.component.designer').then(m => m.ManageShipperAddressComponent) },          
            { path: 'manageShipperBankAccount', loadComponent: () => import('./manage-shipper-bank-account/manage-shipper-bank-account.component.designer').then(m => m.ManageShipperBankAccountComponent) },          
            { path: 'manageShipperPhone', loadComponent: () => import('./manage-shipper-phone/manage-shipper-phone.component.designer').then(m => m.ManageShipperPhoneComponent) },          
            { path: 'viewShipper', loadComponent: () => import('./view-shipper/view-shipper.component.designer').then(m => m.ViewShipperComponent) },          
            { path: 'viewShipperAddress', loadComponent: () => import('./view-shipper-address/view-shipper-address.component.designer').then(m => m.ViewShipperAddressComponent) },          
            { path: 'viewShipperBankAccount', loadComponent: () => import('./view-shipper-bank-account/view-shipper-bank-account.component.designer').then(m => m.ViewShipperBankAccountComponent) },          
            { path: 'viewShipperPhone', loadComponent: () => import('./view-shipper-phone/view-shipper-phone.component.designer').then(m => m.ViewShipperPhoneComponent) },          
        ]
    }  
];

// Define the  ShipperRoutingModule routing module
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
// Export the ShipperRoutingModule class
export class ShipperRoutingModule { }
