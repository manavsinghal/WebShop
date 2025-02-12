/* Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright>
*/

// Import necessary Angular modules and services
import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, NavigationExtras, RouterLink } from '@angular/router';
import { TranslateService, TranslateDirective } from '@ngx-translate/core';
import { CoreSessionService } from '../../../../core/services/core.session.service';
import { CoreSubscriptionService } from '../../../../core/services/core.subscription.service';
import { Subscription } from 'rxjs';
import { SharedComponent } from '../../../../shared/shared.component';

// Define the ManageSchemaComponent
@Component({
    // Selector for the component
    selector: 'app-manage-schema',
    // Template URL for the component
    templateUrl: './manage-schema.component.html',
    imports: [RouterLink, TranslateDirective]
})
export class ManageSchemaComponent extends SharedComponent implements OnInit, OnDestroy {
    languageChangedSubscription!: Subscription;
    // Constructor for the component
    constructor(private readonly router: Router,
        override readonly coreSessionService: CoreSessionService,
        readonly coreSubscriptionService: CoreSubscriptionService,
        private readonly translateService: TranslateService) {
        super(coreSessionService);
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
	// Represents navigateToManageFeature method
	// @param {string} selectedSchema - selectedSchema method param.
	// @returns {void} - navigateToManageFeature method returns void.
    */
    navigateToManageFeature(selectedSchema: string): void {
        const navigationExtras: NavigationExtras = {
            queryParams: {
                serviceGroup: selectedSchema
            }
        };

        void this.router.navigate(['/app/administration/manageGroupEntities'], navigationExtras);
    }
}
