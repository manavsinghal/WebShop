/* Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright>
*/

// Import necessary Angular modules and services
import { Component, OnInit, OnDestroy, inject } from '@angular/core';
import { Router, NavigationExtras, ActivatedRoute, RouterLink } from '@angular/router';
import { TranslateService, TranslateDirective } from '@ngx-translate/core';
import { CoreSessionService } from '../../../../core/services/core.session.service';
import { CoreSubscriptionService } from '../../../../core/services/core.subscription.service';
import { Subscription } from 'rxjs';
import { SharedComponent } from '../../../../shared/shared.component';

// Define the AdminComponent
@Component({
    // Selector for the component
    selector: 'app-admin',
    // Template URL for the component
    templateUrl: './admin.component.html',
    imports: [RouterLink, TranslateDirective]
})
export class AdminComponent extends SharedComponent implements OnInit, OnDestroy {
    private readonly router = inject(Router);
    private readonly route = inject(ActivatedRoute);
    override readonly coreSessionService: CoreSessionService;
    readonly coreSubscriptionService = inject(CoreSubscriptionService);
    private readonly translateService = inject(TranslateService);

    serviceGroup!: string;
    languageChangedSubscription!: Subscription;
    // Constructor for the component
    constructor() {
        const coreSessionService = inject(CoreSessionService);

        super();
        this.coreSessionService = coreSessionService;

        this.translateService.setDefaultLang(this.coreSessionService.getLanguage());
    }
 
    /**
	// Represents ngOnInit method
    // Lifecycle hook for initialization
	// @returns {void} - ngOnInit method returns void.
    */
    ngOnInit(): void {
        this.languageChangedSubscription = this.coreSessionService.languageChanged.subscribe((): void => {
            this.onLanguageChanged();
        });
        this.serviceGroup = this.route.snapshot.queryParams['serviceGroup'];
        window.scroll(0, 0);
    }

    /**
	// Represents ngOnDestroy method
    // Lifecycle hook for destruction
	// @returns {void} - ngOnDestroy method returns void.
    */
    ngOnDestroy(): void {
		this.coreSubscriptionService.unsubscribeSubscriptions(this);
	}

    /**
	// Represents onLanguageChanged method
    // Method to handle language changes
	// @returns {void} - onLanguageChanged method returns void.
    */
    onLanguageChanged(): void {
        this.translateService.setDefaultLang(this.coreSessionService.getLanguage());
    }

    /**
	// Represents navigateToManageEntity method
	// @param {string} selectedServiceGroup - selectedServiceGroup method param.
	// @returns {void} - navigateToManageEntity method returns void.
    */
    navigateToManageEntity(selectedServiceGroup: string): void {
        const navigationExtras: NavigationExtras = {
            queryParams: {
                screenGroup: selectedServiceGroup,
                serviceGroup: this.serviceGroup
            }
        };

        void this.router.navigate(['/app/administration/manageAdminEntities'], navigationExtras);
    }
}
