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
import { ManageAppSettingComponent } from './manage-app-setting/manage-app-setting.component.designer';
import { ManageCountryComponent } from './manage-country/manage-country.component.designer';
import { ManageCountryLanguageComponent } from './manage-country-language/manage-country-language.component.designer';
import { ManageEntityComponent } from './manage-entity/manage-entity.component.designer';
import { ManageLanguageComponent } from './manage-language/manage-language.component.designer';
import { ManageMasterListComponent } from './manage-master-list/manage-master-list.component.designer';
import { ManageMasterListItemComponent } from './manage-master-list-item/manage-master-list-item.component.designer';
import { ManageRowStatusComponent } from './manage-row-status/manage-row-status.component.designer';
import { ViewAppSettingComponent } from './view-app-setting/view-app-setting.component.designer';
import { ViewCountryComponent } from './view-country/view-country.component.designer';
import { ViewCountryLanguageComponent } from './view-country-language/view-country-language.component.designer';
import { ViewEntityComponent } from './view-entity/view-entity.component.designer';
import { ViewLanguageComponent } from './view-language/view-language.component.designer';
import { ViewMasterListComponent } from './view-master-list/view-master-list.component.designer';
import { ViewMasterListItemComponent } from './view-master-list-item/view-master-list-item.component.designer';
import { ViewRowStatusComponent } from './view-row-status/view-row-status.component.designer';

// Define the routes for the MasterRoutingModule module
const routes: Routes = [
    {
        path: 'domain',       
        children: [
            { path: 'manageAppSetting', component: ManageAppSettingComponent },          
            { path: 'manageCountry', component: ManageCountryComponent },          
            { path: 'manageCountryLanguage', component: ManageCountryLanguageComponent },          
            { path: 'manageEntity', component: ManageEntityComponent },          
            { path: 'manageLanguage', component: ManageLanguageComponent },          
            { path: 'manageMasterList', component: ManageMasterListComponent },          
            { path: 'manageMasterListItem', component: ManageMasterListItemComponent },          
            { path: 'manageRowStatus', component: ManageRowStatusComponent },          
            { path: 'viewAppSetting', component: ViewAppSettingComponent },          
            { path: 'viewCountry', component: ViewCountryComponent },          
            { path: 'viewCountryLanguage', component: ViewCountryLanguageComponent },          
            { path: 'viewEntity', component: ViewEntityComponent },          
            { path: 'viewLanguage', component: ViewLanguageComponent },          
            { path: 'viewMasterList', component: ViewMasterListComponent },          
            { path: 'viewMasterListItem', component: ViewMasterListItemComponent },          
            { path: 'viewRowStatus', component: ViewRowStatusComponent },          
        ]
    }  
];

// Define the  MasterRoutingModule routing module
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
// Export the MasterRoutingModule class
export class MasterRoutingModule { }
