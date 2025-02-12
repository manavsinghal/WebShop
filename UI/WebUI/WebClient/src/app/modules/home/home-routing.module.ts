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
	{ path: '', loadComponent: () => import('./home.component').then(m => m.HomeComponent) }
];

// Define the  HomeRoutingModule routing module
@NgModule({
	exports: [RouterModule],
	imports: [RouterModule.forChild(routes)]
})

// Export the HomeRoutingModule class
export class HomeRoutingModule { }

