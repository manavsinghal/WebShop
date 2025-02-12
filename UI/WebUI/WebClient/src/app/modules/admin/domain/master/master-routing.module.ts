/* Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright>
*/

import { Routes } from '@angular/router';










// Define the routes for the MasterRoutingModule module
export const routes: Routes = [
    {
        path: 'domain',       
        children: [
            { path: 'manageAppSetting', loadComponent: () => import('./manage-app-setting/manage-app-setting.component.designer').then(m => m.ManageAppSettingComponent) },          
            { path: 'manageCountry', loadComponent: () => import('./manage-country/manage-country.component.designer').then(m => m.ManageCountryComponent) },          
            { path: 'manageCountryLanguage', loadComponent: () => import('./manage-country-language/manage-country-language.component.designer').then(m => m.ManageCountryLanguageComponent) },          
            { path: 'manageEntity', loadComponent: () => import('./manage-entity/manage-entity.component.designer').then(m => m.ManageEntityComponent) },          
            { path: 'manageLanguage', loadComponent: () => import('./manage-language/manage-language.component.designer').then(m => m.ManageLanguageComponent) },          
            { path: 'manageMasterList', loadComponent: () => import('./manage-master-list/manage-master-list.component.designer').then(m => m.ManageMasterListComponent) },          
            { path: 'manageMasterListItem', loadComponent: () => import('./manage-master-list-item/manage-master-list-item.component.designer').then(m => m.ManageMasterListItemComponent) },          
            { path: 'manageRowStatus', loadComponent: () => import('./manage-row-status/manage-row-status.component.designer').then(m => m.ManageRowStatusComponent) },          
            { path: 'viewAppSetting', loadComponent: () => import('./view-app-setting/view-app-setting.component.designer').then(m => m.ViewAppSettingComponent) },          
            { path: 'viewCountry', loadComponent: () => import('./view-country/view-country.component.designer').then(m => m.ViewCountryComponent) },          
            { path: 'viewCountryLanguage', loadComponent: () => import('./view-country-language/view-country-language.component.designer').then(m => m.ViewCountryLanguageComponent) },          
            { path: 'viewEntity', loadComponent: () => import('./view-entity/view-entity.component.designer').then(m => m.ViewEntityComponent) },          
            { path: 'viewLanguage', loadComponent: () => import('./view-language/view-language.component.designer').then(m => m.ViewLanguageComponent) },          
            { path: 'viewMasterList', loadComponent: () => import('./view-master-list/view-master-list.component.designer').then(m => m.ViewMasterListComponent) },          
            { path: 'viewMasterListItem', loadComponent: () => import('./view-master-list-item/view-master-list-item.component.designer').then(m => m.ViewMasterListItemComponent) },          
            { path: 'viewRowStatus', loadComponent: () => import('./view-row-status/view-row-status.component.designer').then(m => m.ViewRowStatusComponent) },          
        ]
    }  
];
