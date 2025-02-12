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
import { ManageCustomerComponent } from './manage-customer/manage-customer.component.designer';
import { ManageCustomerAddressComponent } from './manage-customer-address/manage-customer-address.component.designer';
import { ManageCustomerCardComponent } from './manage-customer-card/manage-customer-card.component.designer';
import { ManageCustomerPhoneComponent } from './manage-customer-phone/manage-customer-phone.component.designer';
import { ViewCustomerComponent } from './view-customer/view-customer.component.designer';
import { ViewCustomerAddressComponent } from './view-customer-address/view-customer-address.component.designer';
import { ViewCustomerCardComponent } from './view-customer-card/view-customer-card.component.designer';
import { ViewCustomerPhoneComponent } from './view-customer-phone/view-customer-phone.component.designer';

// Define the routes for the CustomerRoutingModule module
const routes: Routes = [
    {
        path: 'domain',       
        children: [
            { path: 'manageCustomer', component: ManageCustomerComponent },          
            { path: 'manageCustomerAddress', component: ManageCustomerAddressComponent },          
            { path: 'manageCustomerCard', component: ManageCustomerCardComponent },          
            { path: 'manageCustomerPhone', component: ManageCustomerPhoneComponent },          
            { path: 'viewCustomer', component: ViewCustomerComponent },          
            { path: 'viewCustomerAddress', component: ViewCustomerAddressComponent },          
            { path: 'viewCustomerCard', component: ViewCustomerCardComponent },          
            { path: 'viewCustomerPhone', component: ViewCustomerPhoneComponent },          
        ]
    }  
];

// Define the  CustomerRoutingModule routing module
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
// Export the CustomerRoutingModule class
export class CustomerRoutingModule { }
