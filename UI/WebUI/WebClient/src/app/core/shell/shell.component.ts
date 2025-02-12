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
import { SharedComponent } from '../../shared/shared.component';
import { HttpErrorResponse } from '@angular/common/http';
import { TemplateRef, ViewChild } from '@angular/core';
import { Router, NavigationError, Event, RouterOutlet } from '@angular/router';
import { TranslateService, TranslateDirective } from '@ngx-translate/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Subscription } from 'rxjs';
import { CoreSessionService, ThemeChanged } from '../../core/services/core.session.service';
import { CoreEnvironmentService } from '../../core/services/core.environment.service';
import { CoreAuthenticationService } from '../../core/services/core.authentication.service';
import { CoreNotificationService } from '../../core/services/core.notification.service';
import { CoreSubscriptionService } from '../../core/services/core.subscription.service';
import { LanguageService } from '../../core/services/domain/master/language.service';
import { Theme } from '../../shared/models/theme.model';
import { Language } from '../../shared/models/domain/master/language.model';
import { DefaultFields } from '../../shared/models/default-fields.model';
import { NgClass, NgStyle } from '@angular/common';
import { UserSettingsComponent } from './user-settings.component';

// Define the ShellComponent
@Component({
    // Selector for the component
    selector: 'app-shell',
    // Template URL for the component
    templateUrl: './shell.component.html',
    imports: [TranslateDirective, NgClass, NgStyle, UserSettingsComponent, RouterOutlet]
})
export class ShellComponent extends SharedComponent implements OnInit, OnDestroy {
    override coreSessionService: CoreSessionService;
    private readonly router = inject(Router);
    private readonly translateService = inject(TranslateService);
    private readonly modalService = inject(NgbModal);
    private readonly languageService = inject(LanguageService);
    private readonly coreEnvironmentService = inject(CoreEnvironmentService);
    private readonly coreAuthenticationService = inject(CoreAuthenticationService);
    private readonly coreSubscriptionService = inject(CoreSubscriptionService);

    @ViewChild('content', { static: false }) modalContent!: TemplateRef<string>;  
    closeResult!: string;
    direction!: string;
    idleHandler!: NodeJS.Timeout;
    imagePath!: string;
    isOpen!: boolean;
    isUserSettingsLoaded!: boolean;
    languageChangedSubscription!: Subscription;
    languages!: Array<Language>;
    loggedInUserEmailId!: string;
    refreshHandler!: NodeJS.Timeout;
    selectedLanguage!: Language;
    selectedLanguageCulture!: string;
    selectedTabName!: string;
    selectedTheme!: Theme;
    themeChangedSubscription!: Subscription;
    tokenHandler!: NodeJS.Timeout;

    // Constructor for the component
    constructor() {
        const coreSessionService = inject(CoreSessionService);

        super();
        this.coreSessionService = coreSessionService;

        this.routeEvent(this.router);
        this.translateService.setDefaultLang(this.coreSessionService.getLanguage());
        this.selectedTheme = this.coreSessionService.getTheme();
    }
 
    /**
	// Represents ngOnInit method
    // Lifecycle hook for initialization
	// @returns {void} - ngOnInit method returns void.
    */
    ngOnInit(): void {
        this.isUserSettingsLoaded = false;
        this.isOpen = false;

        if (this.coreSessionService.getTheme()) {
            this.loadThemes(this.coreSessionService.getTheme());
        }

        this.languageChangedSubscription = this.coreSessionService.languageChanged.subscribe((): void => {
            this.onLanguageChanged();
        });

        this.themeChangedSubscription = this.coreSessionService.themeChanged.subscribe((eventArgs: ThemeChanged): void => {
            this.loadThemes(eventArgs.Theme);
        });
        if (this.coreEnvironmentService.environment != null &&
            this.coreEnvironmentService.environment.AuthProvider === DefaultFields.AzureADAuthProvider) {
            this.coreAuthenticationService.getToken()
                .subscribe((data): void => {
                    this.coreSessionService.setSessionStorageItem(DefaultFields.AccessToken, data.access_token);
                    this.coreSessionService.setSessionStorageItem(DefaultFields.TokenExpiry, data.expiresOn.toString());
                    this.tokenRenewal();
                });
            this.idleHandler = setInterval((): void => {
                const lastCalled: Date = this.coreSessionService.getLastCalled();
                const dt: number = (new Date()).valueOf() - (lastCalled).valueOf();
                let idealTime: number;
                if (this.coreEnvironmentService.environment && this.coreEnvironmentService.environment.IdealTime && this.coreEnvironmentService.environment.IdealTime != 0) {
                    idealTime = this.coreEnvironmentService.environment.IdealTime;
                } else {
                    idealTime = 60;
                }
                if (dt > (parseInt(idealTime.toString(), 10) * 60 * 1000)) {
                    this.coreSessionService.clear();
                    this.modalService.open(this.modalContent, { backdrop: 'static', keyboard: false, size: 'sm', centered: true });
                    this.clearTimers();
                }
            }, 60 * 1000); // convert to milliseconds
        }
        require('src/themes/scripts/toolkit.js');
    }

    /**
	// Represents ngOnDestroy method
    // Lifecycle hook for destruction
	// @returns {void} - ngOnDestroy method returns void.
    */
    ngOnDestroy(): void {
		this.coreSubscriptionService.unsubscribeSubscriptions(this);
	}

    onLanguageChanged(): void {
        this.translateService.setDefaultLang(this.coreSessionService.getLanguage());
        if (this.coreSessionService.getLanguage() !== DefaultFields.EnglishLanguageUId) {
            this.imagePath = sessionStorage.getItem(DefaultFields.Culture) + '/';
        } else {
            this.imagePath = '';
        }
        this.setLanguageCulture();
  
    }

    confirmLogout(): void {
        this.coreSessionService.clear();
        if (this.coreEnvironmentService.environment) {
            if (this.coreEnvironmentService.environment.AuthProvider === DefaultFields.AzureADAuthProvider) {
                // window.location.href = this.coreEnvironmentService.environment.LogOutUrl + '/?time=' + new Date().getTime();
            }
        }
  
    }

    toggleUserSettings(): void {
        if (!this.isUserSettingsLoaded) {
            this.isUserSettingsLoaded = true;
        }

        this.isOpen = !this.isOpen;
  
    }

    /**
	// Represents navigateToPage method
	// @param {string} routeUrl - routeUrl method param.
	// @returns {void} - navigateToPage method returns void.
    */
    navigateToPage(routeUrl: string): void {
        this.selectedTabName = routeUrl ? routeUrl : 'Home';
        this.coreSessionService.setSelectedTab(this.selectedTabName);
        void this.router.navigate(['/app/' + routeUrl]);
  
    }

    loadLanguages(): void {
        this.languageService.getLanguages()
                .subscribe((data: Array<Language>): void => {
                    this.languages = data;
                    this.setLanguageCulture();
                }, (error: HttpErrorResponse): void => {
                    CoreNotificationService.handleError(error);
                });
  
    }

    setLanguageCulture(): void {
        this.direction = sessionStorage.getItem(DefaultFields.IsRTL) === 'true' ? 'rtl' : 'ltr';
  
    }

    /**
    // Represents calculateExpiryTime method
    // @returns {number} - calculateExpiryTime method returns number.
    */
    calculateExpiryTime(): number {
        const defaultValue = 40;
        const date = this.coreSessionService.getSessionStorageItem(DefaultFields.TokenExpiry);
        if (date) {
            const currentDateTime: Date = new Date();
            const accessTokenTime: Date = new Date(date);
            console.log(currentDateTime);
            console.log(accessTokenTime);
            const minutes = Math.abs(accessTokenTime.getTime() - currentDateTime.getTime()) / (1000 * 60);
            const minutesInDec: number = parseInt(minutes.toString(), 10);
            const value: number = (minutesInDec - (minutesInDec / 5));
            console.log('value', value);

            return value;
        } else {
            return defaultValue;
        }
    }

    /**
    // Represents tokenRenewal method
    // @returns {void} - tokenRenewal method returns void.
    */
    tokenRenewal() {
        this.tokenHandler = setInterval((): void => {
            this.coreAuthenticationService.getToken(true)
                .subscribe((data): void => {
                    console.log('renewing token getToken called...');
                    console.warn(data);
                    this.coreSessionService.setSessionStorageItem(DefaultFields.AccessToken, data.access_token);
                    this.coreSessionService.setSessionStorageItem(DefaultFields.TokenExpiry, data.expiresOn.toString());
                }, (): void => {
                });
        }, this.calculateExpiryTime() * 60 * 1000);
    }

    /**
    // Represents clearTimers method
    // @returns {void} - clearTimers method returns void.
    */
    clearTimers(): void {
        clearInterval(this.idleHandler);
        clearInterval(this.tokenHandler);
    }

    /**
    // Represents loadThemes method
    // @param {Theme} theme - theme method param.
    // @returns {void} - loadThemes method returns void.
    */
    loadThemes(theme: Theme): void {
        this.selectedTheme = theme;
    }

    /**
    // Represents routeEvent method
    // @param {Router} router - router method param. 
    // @returns {void} - routeEvent method returns void.
    */
    routeEvent(router: Router): void {
        router.events.subscribe((e: Event): void => {
            if (e instanceof NavigationError) {
                let msg: string = '';
                CoreNotificationService.showError(msg);
            }
        });
    }
}
