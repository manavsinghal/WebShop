/* Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright> 

*/ 

import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { TreeModule } from 'primeng/tree';
import { SharedModule } from '../shared/shared.module';
import { ShellComponent } from './shell/shell.component';
import { UserSettingsComponent } from './shell/user-settings.component';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './login/auth-guard';
import { CoreSessionService } from './services/core.session.service';

@NgModule({
    imports: [RouterModule, FormsModule, ReactiveFormsModule, CommonModule, SharedModule, TreeModule, ShellComponent, UserSettingsComponent,
        LoginComponent],
    providers: [CoreSessionService, AuthGuard, provideHttpClient(withInterceptorsFromDi())]
})
export class CoreModule { }

