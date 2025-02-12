/* Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright> 

*/ 

import { Component, OnInit, OnDestroy, TemplateRef, NgZone, inject, viewChild } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { SharedComponent } from '../../shared/shared.component';
import { DefaultFields } from '../../shared/models/default-fields.model';
import { Theme } from '../../shared/models/theme.model';
import { CoreAuthenticationService } from '../services/core.authentication.service';
import { CoreEnvironmentService } from '../services/core.environment.service';
import { CoreSessionService } from '../services/core.session.service';
import { CoreSubscriptionService } from '../services/core.subscription.service';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html'
})
export class LoginComponent extends SharedComponent implements OnInit, OnDestroy {
	private readonly route = inject(ActivatedRoute);
	private readonly router = inject(Router);
	private readonly subscriptionService = inject(CoreSubscriptionService);
	private readonly coreEnvironmentService = inject(CoreEnvironmentService);
	private readonly modalService = inject(NgbModal);
	override readonly coreSessionService: CoreSessionService;
	private readonly coreAuthenticationService = inject(CoreAuthenticationService);
	private readonly ngZone = inject(NgZone);

	readonly modalContent = viewChild.required<TemplateRef<string>>('content');
	loading: boolean = false;
	returnUrl!: string;
	hasChild!: string;
	loginFailure: boolean = false;
	errorMessage!: string;
	closeResult!: string;
	logoutUrl!: string;
	showLoader: boolean = true;

	constructor() {
		const coreSessionService = inject(CoreSessionService);

		super();
	
		this.coreSessionService = coreSessionService;
	}

	ngOnInit(): void {
		// debugger;
		if (this.coreSessionService.getSessionStorageItem('returnBackUrl') == null) {
			this.coreSessionService.setSessionStorageItem('returnBackUrl', this.route.snapshot.queryParams['redirectUrl'] || '/');
		}

		this.returnUrl = this.coreSessionService.getSessionStorageItem('returnBackUrl');
		this.hasChild = this.route.snapshot.queryParams['hasChild'];

		if (this.coreEnvironmentService.environment.AuthProvider === DefaultFields.AzureADAuthProvider) {
			if (this.coreAuthenticationService.getAccount() === null) {
				this.coreAuthenticationService.login();
			} else {
				console.log('LoginComponent is authenticated true');
            }

            this.coreSessionService.setTheme(Theme.Themes[0]);
            this.coreSessionService.setSelectedTheme(Theme.Themes[0]);
            this.coreSessionService.setSessionStorageItem(DefaultFields.LoggedInUserEmailId, this.coreEnvironmentService.environment.LoggedInUserEmail);

            this.ngZone.run((): void => {
                void this.router.navigateByUrl(this.returnUrl);
            });
		} else if (this.coreEnvironmentService.environment.AuthProvider === DefaultFields.DebugAuthProvider) {
			this.coreSessionService.setTheme(Theme.Themes[0]);
			this.coreSessionService.setSelectedTheme(Theme.Themes[0]);
			this.coreSessionService.setSessionStorageItem(DefaultFields.LoggedInUserEmailId, this.coreEnvironmentService.environment.LoggedInUserEmail);

			this.ngZone.run((): void => {
				void this.router.navigateByUrl(this.returnUrl);
			});
		}
	}

	ngOnDestroy(): void {
		this.subscriptionService.unsubscribeSubscriptions(this);
	}

	confirmLogout(): void {
		this.modalService.dismissAll(this.modalContent());
		this.coreSessionService.clear();
		if (this.coreEnvironmentService.environment) {
			if (this.coreEnvironmentService.environment.AuthProvider === DefaultFields.AzureADAuthProvider) {
				this.coreAuthenticationService.logout();
			}
		}
	}	
}

