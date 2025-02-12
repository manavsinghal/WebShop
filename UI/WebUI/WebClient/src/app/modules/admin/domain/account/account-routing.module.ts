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
import { ManageAccountComponent } from './manage-account/manage-account.component.designer';
import { ManageAccountStatusComponent } from './manage-account-status/manage-account-status.component.designer';
import { ViewAccountComponent } from './view-account/view-account.component.designer';
import { ViewAccountStatusComponent } from './view-account-status/view-account-status.component.designer';

// Define the routes for the AccountRoutingModule module
const routes: Routes = [
    {
        path: 'domain',       
        children: [
            { path: 'manageAccount', component: ManageAccountComponent },          
            { path: 'manageAccountStatus', component: ManageAccountStatusComponent },          
            { path: 'viewAccount', component: ViewAccountComponent },          
            { path: 'viewAccountStatus', component: ViewAccountStatusComponent },          
        ]
    }  
];

// Define the  AccountRoutingModule routing module
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
// Export the AccountRoutingModule class
export class AccountRoutingModule { }
