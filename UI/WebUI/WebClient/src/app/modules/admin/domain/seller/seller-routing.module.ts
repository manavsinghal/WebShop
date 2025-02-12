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
import { ManageSellerComponent } from './manage-seller/manage-seller.component.designer';
import { ManageSellerAddressComponent } from './manage-seller-address/manage-seller-address.component.designer';
import { ManageSellerBankAccountComponent } from './manage-seller-bank-account/manage-seller-bank-account.component.designer';
import { ManageSellerPhoneComponent } from './manage-seller-phone/manage-seller-phone.component.designer';
import { ViewSellerComponent } from './view-seller/view-seller.component.designer';
import { ViewSellerAddressComponent } from './view-seller-address/view-seller-address.component.designer';
import { ViewSellerBankAccountComponent } from './view-seller-bank-account/view-seller-bank-account.component.designer';
import { ViewSellerPhoneComponent } from './view-seller-phone/view-seller-phone.component.designer';

// Define the routes for the SellerRoutingModule module
const routes: Routes = [
    {
        path: 'domain',       
        children: [
            { path: 'manageSeller', component: ManageSellerComponent },          
            { path: 'manageSellerAddress', component: ManageSellerAddressComponent },          
            { path: 'manageSellerBankAccount', component: ManageSellerBankAccountComponent },          
            { path: 'manageSellerPhone', component: ManageSellerPhoneComponent },          
            { path: 'viewSeller', component: ViewSellerComponent },          
            { path: 'viewSellerAddress', component: ViewSellerAddressComponent },          
            { path: 'viewSellerBankAccount', component: ViewSellerBankAccountComponent },          
            { path: 'viewSellerPhone', component: ViewSellerPhoneComponent },          
        ]
    }  
];

// Define the  SellerRoutingModule routing module
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
// Export the SellerRoutingModule class
export class SellerRoutingModule { }
