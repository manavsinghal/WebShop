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
import { AdminComponent } from './manage-features/admin/admin.component';
import { ManageAdminEntitiesComponent } from './manage-features/manage-admin-entities/manage-admin-entities.component';
import { ManageSchemaComponent } from './manage-features/manage-schema/manage-schema.component';

const routes: Routes = [
    { path: 'manageGroupEntities', component: AdminComponent },    { path: 'manageAdminEntities', component: ManageAdminEntitiesComponent },    { path: '', component: ManageSchemaComponent }];

// Define the  AdminRoutingModule routing module
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
// Export the AdminRoutingModule class
export class AdminRoutingModule { }
