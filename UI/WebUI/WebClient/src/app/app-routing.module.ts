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



const routes: Routes = [
	{
		path: 'app',
		loadComponent: () => import('./core/shell/shell.component').then(m => m.ShellComponent),
		canActivate: [AuthGuard],
		children: [
			{
                path: 'home',
                loadChildren: () => import('./modules/home/home-routing.module')
					.then(mod => mod.routes)
			},
			{
                path: 'administration',
                loadChildren: () => import('./modules/admin/admin-routing.module')
					.then(mod => mod.routes)
			},
			{
                path: '', pathMatch: 'full', redirectTo: 'home'
			}
		]
	},
	{
		path: 'login', loadComponent: () => import('./core/login/login.component').then(m => m.LoginComponent)
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

