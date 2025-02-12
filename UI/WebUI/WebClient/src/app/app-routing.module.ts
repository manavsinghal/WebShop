/* Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright> 

*/ 

import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './core/login/auth-guard';
import { LoginComponent } from './core/login/login.component';
import { ShellComponent } from './core/shell/shell.component';

const routes: Routes = [
	{
		path: 'app',
		component: ShellComponent,
		canActivate: [AuthGuard],
		children: [
			{
                path: 'home',
				loadChildren: () => import('./modules/home/home.module')
					.then(mod => mod.HomeModule)
			},
			{
				path: 'administration',
				loadChildren: () => import('./modules/admin/admin.module')
					.then(mod => mod.AdminModule)
			},
			{
                path: '', pathMatch: 'full', redirectTo: 'home'
			}
		]
	},
	{
		path: 'login', component: LoginComponent
	},
	{
        path: '', pathMatch: 'full', redirectTo: 'app/home',
	},
	{
        path: '**', pathMatch: 'full', redirectTo: 'app/home',
	}
];

@NgModule({
	imports: [RouterModule.forRoot(routes, {}),

	],
	exports: [RouterModule]
})
export class AppRoutingModule { }

