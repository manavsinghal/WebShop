/* Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright> 

*/ 


// Import necessary Angular modules and services
import { Component, OnInit } from '@angular/core';
import { TranslateService, TranslateDirective } from '@ngx-translate/core';
import { Subscription } from 'rxjs';
import { CoreSessionService, ThemeChanged } from '../../core/services/core.session.service';
import { Theme } from '../../shared/models/theme.model';

// Define the Home Component
@Component({
    // Selector for the component
    selector: 'app-home',
    // Template URL for the component
    templateUrl: './home.component.html',
    imports: [TranslateDirective]
})
export class HomeComponent implements OnInit {
	languageChangedSubscription!: Subscription;
	selectedTheme: Theme;
	themeChangedSubscription!: Subscription;

    // Constructor for the component
	constructor(public coreSessionService: CoreSessionService,
		private readonly translateService: TranslateService) {
		this.translateService.setDefaultLang(this.coreSessionService.getLanguage());
		this.selectedTheme = this.coreSessionService.getTheme();
	}

    /**
    // Represents ngOnInit method
    // Lifecycle hook for initialization
    // @returns {void} - ngOnInit method returns void.
    */
	ngOnInit(): void {
		if (this.coreSessionService.getTheme()) {
			this.loadThemes(this.coreSessionService.getTheme());
		}
		this.languageChangedSubscription = this.coreSessionService.languageChanged.subscribe((): void => {
			this.onLanguageChanged();
		});
		this.themeChangedSubscription = this.coreSessionService.themeChanged.subscribe((eventArgs: ThemeChanged): void => {
			this.loadThemes(eventArgs.Theme);
		});
	}

    /**
    // Represents loadThemes method
    // @param WebShop theme - theme method param.
    // @returns {void} - loadThemes method returns void.
    */
	loadThemes(theme: Theme): void {
		this.selectedTheme = theme;
	}	

    /**
    // Represents onLanguageChanged method
    // @returns {void} - onLanguageChanged method returns void.
    */
	onLanguageChanged(): void {
		this.translateService.setDefaultLang(this.coreSessionService.getLanguage());
	}
}

