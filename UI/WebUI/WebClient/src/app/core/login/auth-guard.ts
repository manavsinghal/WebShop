/* Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright> 

*/ 

import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, NavigationExtras } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError, concatMap } from 'rxjs/operators';
import { DefaultFields } from '../../shared/models/default-fields.model';
import { CoreAuthenticationService } from '../services/core.authentication.service';
import { CoreEnvironmentService } from '../services/core.environment.service';
import { CoreSessionService } from '../services/core.session.service';

@Injectable()
export class AuthGuard implements CanActivate {
	constructor(
		private readonly router: Router,
		private readonly coreAuthenticationService: CoreAuthenticationService,
        private readonly coreEnvironmentService: CoreEnvironmentService,
        private readonly coreSessionService: CoreSessionService
	) {

	}

	canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> {
		const navigationExtras: NavigationExtras = {
			queryParams: {
				redirectUrl: state.url,
				hasChild: route.firstChild ? true : false
			}
		};
		try {
			if (this.coreEnvironmentService.environment.AuthProvider === DefaultFields.AzureADAuthProvider) {

				return this.coreAuthenticationService.msalService.handleRedirectObservable()
					.pipe(concatMap(() => {
						if (!this.coreAuthenticationService.msalService.instance.getAllAccounts().length) {
							if (state) {
								this.coreAuthenticationService.login();
							}
							else {

								return of(false);
							}
						}
						let activeAccount = this.coreAuthenticationService.msalService.instance.getActiveAccount();

						if (!activeAccount && this.coreAuthenticationService.msalService.instance.getAllAccounts().length > 0) {
							let accounts = this.coreAuthenticationService.msalService.instance.getAllAccounts();
							this.coreAuthenticationService.msalService.instance.setActiveAccount(accounts[0]);
							this.router.navigate(['login'], navigationExtras);
						}

						return of(true);
					}),
						catchError((error: Error) => {
							console.error(error);
							return of(false);
						}));
            } else if (this.coreEnvironmentService.environment.AuthProvider === DefaultFields.DebugAuthProvider) {
                this.coreSessionService.setSessionStorageItem('returnBackUrl', state.url);

				if (this.router.url === '/') {
					this.router.navigate(['login'], navigationExtras);
				}
				else {
					return of(true);
				}
			}

		} catch (e) {
			// console.log('AuthGuard - Error' + e);
		}

		return of(false);
	}
}

