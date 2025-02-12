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




const routes: Routes = [
    { path: 'manageGroupEntities', loadComponent: () => import('./manage-features/admin/admin.component').then(m => m.AdminComponent) },    { path: 'manageAdminEntities', loadComponent: () => import('./manage-features/manage-admin-entities/manage-admin-entities.component').then(m => m.ManageAdminEntitiesComponent) },    { path: '', loadComponent: () => import('./manage-features/manage-schema/manage-schema.component').then(m => m.ManageSchemaComponent) }];

// Define the  AdminRoutingModule routing module
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
// Export the AdminRoutingModule class
export class AdminRoutingModule { }
