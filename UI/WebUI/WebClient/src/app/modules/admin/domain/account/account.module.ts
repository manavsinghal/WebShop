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
import { AccountRoutingModule } from './account-routing.module';
import { ManageAccountComponent } from '../../domain/account/manage-account/manage-account.component.designer';
import { ManageAccountStatusComponent } from '../../domain/account/manage-account-status/manage-account-status.component.designer';
import { ViewAccountComponent } from '../../domain/account/view-account/view-account.component.designer';
import { ViewAccountStatusComponent } from '../../domain/account/view-account-status/view-account-status.component.designer';

@NgModule({
    imports: [
        AccountRoutingModule,
        SharedModule,
        ManageAccountComponent,
        ManageAccountStatusComponent,
        ViewAccountComponent,
        ViewAccountStatusComponent
    ],
    providers: [SortPipe]
})
	
	

// Export the AccountModule class
export class AccountModule { }
