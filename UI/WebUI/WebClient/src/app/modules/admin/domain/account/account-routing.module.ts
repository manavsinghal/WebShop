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





// Define the routes for the AccountRoutingModule module
const routes: Routes = [
    {
        path: 'domain',       
        children: [
            { path: 'manageAccount', loadComponent: () => import('./manage-account/manage-account.component.designer').then(m => m.ManageAccountComponent) },          
            { path: 'manageAccountStatus', loadComponent: () => import('./manage-account-status/manage-account-status.component.designer').then(m => m.ManageAccountStatusComponent) },          
            { path: 'viewAccount', loadComponent: () => import('./view-account/view-account.component.designer').then(m => m.ViewAccountComponent) },          
            { path: 'viewAccountStatus', loadComponent: () => import('./view-account-status/view-account-status.component.designer').then(m => m.ViewAccountStatusComponent) },          
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
