/* Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright> 

*/ 

import { Component, OnInit, inject } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { EventEmitter, Output } from '@angular/core';
import { TranslateService, TranslateDirective, TranslatePipe } from '@ngx-translate/core';
import { CoreSessionService } from '../services/core.session.service';
import { CoreNotificationService } from '../services/core.notification.service';
import { LanguageService } from '../services/domain/master/language.service';
import { SortPipe } from '../../shared/pipes/sort.pipe';
import { Theme } from '../../shared/models/theme.model';
import { Language } from '../../shared/models/domain/master/language.model';
import { DefaultFields } from '../../shared/models/default-fields.model';
import { FormsModule } from '@angular/forms';

@Component({
    selector: 'app-user-settings',
    templateUrl: './user-settings.component.html',
    imports: [TranslateDirective, FormsModule, TranslatePipe]
})
export class UserSettingsComponent implements OnInit {
	readonly coreSessionService = inject(CoreSessionService);
	private readonly languageService = inject(LanguageService);
	private readonly translateService = inject(TranslateService);
	readonly sortPipe = inject(SortPipe);


	@Output() closeActionPane: EventEmitter<boolean> = new EventEmitter<boolean>();

	themes: Array<Theme> = Theme.Themes;
	selectedTheme!: Theme;
	languages: Array<Language> = new Array<Language>(0);
	selectedLanguage: Language = new Language();

	ngOnInit(): void {
		this.selectedTheme = Theme.Themes[0];
		this.loadLanguages();
	}

	apply(): void {
		sessionStorage.setItem(DefaultFields.Culture, this.selectedLanguage.Culture);
		sessionStorage.setItem(DefaultFields.IsRTL, this.selectedLanguage.IsRTL.toString());
		this.coreSessionService.setTheme(this.selectedTheme);
		this.coreSessionService.setSelectedTheme(this.selectedTheme);
		this.coreSessionService.setLanguage(this.selectedLanguage.LanguageUId);
		this.translateService.setDefaultLang(this.selectedLanguage.LanguageUId.toString());
		this.closeActionPane.emit(true);
	}

	loadLanguages(): void {
		this.languageService.getLanguages()
			.subscribe((data: Array<Language>): void => {
				this.languages = this.sortPipe.transform(data, DefaultFields.DisplayOrder);
        const userLanguageUId: string = this.coreSessionService.getLanguage() ? this.coreSessionService.getLanguage() : DefaultFields.EnglishLanguageUId;// this.coreSessionService.getLanguage();
                this.selectedLanguage = this.languages.find((m: Language): boolean => m.LanguageUId === userLanguageUId) || this.languages[0];
			}, (error: HttpErrorResponse): void => {
				CoreNotificationService.handleError(error);
			});
	}

    onThemeSelectionChange(event: Event): void {
        const eventValue = (event.target as HTMLInputElement).value;
        this.selectedTheme = Theme.getTheme(eventValue);
    }

    onLanguageSelectionChange(event: Event): void {
        const eventValue = (event.target as HTMLInputElement).value;
        this.selectedLanguage = this.languages.find((x: Language): boolean => x.LanguageUId === eventValue) || this.languages[0];
    }    

    cancel(): void {
        this.closeActionPane.emit(true);
    }
}

