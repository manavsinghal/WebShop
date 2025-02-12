/* Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright>
*/

import { NgModule } from '@angular/core';
import { SortPipe } from '../../../../shared/pipes/sort.pipe';
import { SharedModule } from '../../../../shared/shared.module';
import { MasterRoutingModule } from './master-routing.module';
import { ManageAppSettingComponent } from '../../domain/master/manage-app-setting/manage-app-setting.component.designer';
import { ManageCountryComponent } from '../../domain/master/manage-country/manage-country.component.designer';
import { ManageCountryLanguageComponent } from '../../domain/master/manage-country-language/manage-country-language.component.designer';
import { ManageEntityComponent } from '../../domain/master/manage-entity/manage-entity.component.designer';
import { ManageLanguageComponent } from '../../domain/master/manage-language/manage-language.component.designer';
import { ManageMasterListComponent } from '../../domain/master/manage-master-list/manage-master-list.component.designer';
import { ManageMasterListItemComponent } from '../../domain/master/manage-master-list-item/manage-master-list-item.component.designer';
import { ManageRowStatusComponent } from '../../domain/master/manage-row-status/manage-row-status.component.designer';
import { ViewAppSettingComponent } from '../../domain/master/view-app-setting/view-app-setting.component.designer';
import { ViewCountryComponent } from '../../domain/master/view-country/view-country.component.designer';
import { ViewCountryLanguageComponent } from '../../domain/master/view-country-language/view-country-language.component.designer';
import { ViewEntityComponent } from '../../domain/master/view-entity/view-entity.component.designer';
import { ViewLanguageComponent } from '../../domain/master/view-language/view-language.component.designer';
import { ViewMasterListComponent } from '../../domain/master/view-master-list/view-master-list.component.designer';
import { ViewMasterListItemComponent } from '../../domain/master/view-master-list-item/view-master-list-item.component.designer';
import { ViewRowStatusComponent } from '../../domain/master/view-row-status/view-row-status.component.designer';

@NgModule({
    imports: [
        MasterRoutingModule,
        SharedModule,
        ManageAppSettingComponent,
        ManageCountryComponent,
        ManageCountryLanguageComponent,
        ManageEntityComponent,
        ManageLanguageComponent,
        ManageMasterListComponent,
        ManageMasterListItemComponent,
        ManageRowStatusComponent,
        ViewAppSettingComponent,
        ViewCountryComponent,
        ViewCountryLanguageComponent,
        ViewEntityComponent,
        ViewLanguageComponent,
        ViewMasterListComponent,
        ViewMasterListItemComponent,
        ViewRowStatusComponent
    ],
    providers: [SortPipe]
})
	
	

// Export the MasterModule class
export class MasterModule { }
