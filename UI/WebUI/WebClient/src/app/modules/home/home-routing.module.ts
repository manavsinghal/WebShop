/* Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright> 

*/ 

import { Routes } from '@angular/router';



export const routes: Routes = [
	{ path: '', loadComponent: () => import('./home.component').then(m => m.HomeComponent) }
];

