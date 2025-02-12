/* Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright> 

*/ 

import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { Subscription } from 'rxjs';
import { CoreSessionService, ThemeChanged } from './core/services/core.session.service';
import { DefaultFields } from './shared/models/default-fields.model';
import { Theme } from './shared/models/theme.model';
import { NgxUiLoaderModule } from 'ngx-ui-loader';
import { RouterOutlet } from '@angular/router';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    imports: [NgxUiLoaderModule, RouterOutlet]
})
export class AppComponent implements OnInit {
	title: string = 'Console';
	isIframe: boolean = false;
	loggedIn: boolean = false;
	languageChangedSubscription!: Subscription;
	themeChangedSubscription!: Subscription;
	languageResourceChangedSubscribtion!: Subscription;
	selectedTheme!: Theme;
	imagePath!: string;
	logoUrl!: string;
	loading!: string;
	loaderTextChangedSubscription!: Subscription;

	constructor(
		private readonly translateService: TranslateService,
		readonly coreSessionService: CoreSessionService
	) {
		this.selectedTheme = this.coreSessionService.getTheme();
	}

	ngOnInit(): void {
		if (this.coreSessionService.getTheme()) {
			this.loadThemes(this.coreSessionService.getTheme());
		}
		this.languageChangedSubscription = this.coreSessionService.languageChanged.subscribe((): void => {
			this.onLanguageChanged();
		});
		this.loaderTextChangedSubscription = this.coreSessionService.loaderTextChanged.subscribe((text: string): void => {
			this.translateService.get(text).subscribe((data: string): string => this.loading = data);
		});
		this.themeChangedSubscription = this.coreSessionService.themeChanged.subscribe((eventArgs: ThemeChanged): void => {
			this.loadThemes(eventArgs.Theme);
		});
		this.languageResourceChangedSubscribtion = this.coreSessionService.resourcesLoaded.subscribe((): void => {
			this.setLogoUrlBasedOnLanguageTheme();
		});
		this.isIframe = window !== window.parent && !window.opener;
	}

	loadThemes(theme: Theme): void {
		this.selectedTheme = theme;
		this.setLogoUrlBasedOnLanguageTheme();
	}

	onLanguageChanged(): void {
		this.translateService.setDefaultLang(this.coreSessionService.getLanguage());
		this.setLogoUrlBasedOnLanguageTheme();
	}

	setLogoUrlBasedOnLanguageTheme(): void {
		this.logoUrl = 'themes/{selectedTheme}/images/{imagePath}icon-ngx-load-logo-txt.png';
		if (this.coreSessionService.getLanguage() !== DefaultFields.EnglishLanguageUId) {
			this.imagePath = sessionStorage.getItem(DefaultFields.Culture) + '/';
		} else {
			this.imagePath = '';
		}
		this.logoUrl = this.logoUrl.replace('{selectedTheme}', this.selectedTheme?.Value);
		this.logoUrl = this.logoUrl.replace('{imagePath}', this.imagePath);
		this.translateService.get('PleaseWaitWhileProcessing').subscribe((data: string): string => this.loading = data);
	}
}

