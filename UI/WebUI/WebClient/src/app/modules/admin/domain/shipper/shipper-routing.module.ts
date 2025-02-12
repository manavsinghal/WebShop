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
import { ManageShipperComponent } from './manage-shipper/manage-shipper.component.designer';
import { ManageShipperAddressComponent } from './manage-shipper-address/manage-shipper-address.component.designer';
import { ManageShipperBankAccountComponent } from './manage-shipper-bank-account/manage-shipper-bank-account.component.designer';
import { ManageShipperPhoneComponent } from './manage-shipper-phone/manage-shipper-phone.component.designer';
import { ViewShipperComponent } from './view-shipper/view-shipper.component.designer';
import { ViewShipperAddressComponent } from './view-shipper-address/view-shipper-address.component.designer';
import { ViewShipperBankAccountComponent } from './view-shipper-bank-account/view-shipper-bank-account.component.designer';
import { ViewShipperPhoneComponent } from './view-shipper-phone/view-shipper-phone.component.designer';

// Define the routes for the ShipperRoutingModule module
const routes: Routes = [
    {
        path: 'domain',       
        children: [
            { path: 'manageShipper', component: ManageShipperComponent },          
            { path: 'manageShipperAddress', component: ManageShipperAddressComponent },          
            { path: 'manageShipperBankAccount', component: ManageShipperBankAccountComponent },          
            { path: 'manageShipperPhone', component: ManageShipperPhoneComponent },          
            { path: 'viewShipper', component: ViewShipperComponent },          
            { path: 'viewShipperAddress', component: ViewShipperAddressComponent },          
            { path: 'viewShipperBankAccount', component: ViewShipperBankAccountComponent },          
            { path: 'viewShipperPhone', component: ViewShipperPhoneComponent },          
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
