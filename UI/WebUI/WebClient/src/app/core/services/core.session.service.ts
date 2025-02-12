/* Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright> 

*/ 

// Import necessary modules and services
import { EventEmitter, Injectable, Output } from '@angular/core';
import { Guid } from '../../core/helpers/guid';
import { DefaultFields } from '../../shared/models/default-fields.model';
import { Theme } from '../../shared/models/theme.model';

// Define the CoreSessionService
@Injectable()
export class CoreSessionService {
    // EventEmitter to broadcast language change events.
    @Output() languageChanged: EventEmitter<LanguageChanged> = new EventEmitter<LanguageChanged>();

    // EventEmitter to broadcast theme change events.
    @Output() themeChanged: EventEmitter<ThemeChanged> = new EventEmitter<ThemeChanged>();

    // EventEmitter to broadcast resources loaded events.
    @Output() resourcesLoaded: EventEmitter<boolean> = new EventEmitter<boolean>();

    // EventEmitter to broadcast loader text change events.
    @Output() loaderTextChanged: EventEmitter<string> = new EventEmitter<string>();

    // Last called date timestamp.
    lastCalled: Date | null = null;

    // Get a session storage item by ID,@param id The ID of the session storage item, @returns The session storage item value.
    public getSessionStorageItem = (id: string): string => sessionStorage.getItem(id) || '';

    // Set a session storage item by ID,@param id The ID of the session storage item, @param value The value of the session storage item.
    public setSessionStorageItem = (id: string, value: string): void => {
        sessionStorage.setItem(id, value);
    };

    // Set a local storage item by ID,@param id The ID of the local storage item, @param value The value of the local storage item.
    public setLocalStorageItem = (id: string, value: string): void => {
        localStorage.setItem(id, value);
    };

    // Get the created by user, @returns The created by user.
    public getCreatedByUser(): string {
        sessionStorage.setItem(DefaultFields.CreatedByUser, this.getUserEmailId());

        return sessionStorage.getItem(DefaultFields.CreatedByUser) || '';
    }

    // Get the created by app, @returns The created by app.
    public getCreatedByApp(): string {
        sessionStorage.setItem(DefaultFields.CreatedByApp, DefaultFields.WebShop);

        return sessionStorage.getItem(DefaultFields.CreatedByApp) || '';
    }

    // Get the executed by app, @returns The executed by app.
     public getExecutedByApp(): string {
        let executedByApp: string = '';

        executedByApp = executedByApp === DefaultFields.ExecutedByApp ? DefaultFields.WebShop : executedByApp;
        if (executedByApp) {
            sessionStorage.setItem(DefaultFields.ExecutedByApp, executedByApp);
        } else {
            sessionStorage.setItem(DefaultFields.ExecutedByApp, DefaultFields.WebShop);
        }

        return sessionStorage.getItem(DefaultFields.ExecutedByApp) || '';
    }

    // Get the user name, @returns The user name.
    public getUserName = (): string => sessionStorage.getItem(DefaultFields.LoggedInUserName) || '';

    // Set the user name, @param username The user name.
    public setUserName(username: string): void {
        sessionStorage.setItem(DefaultFields.LoggedInUserName, username);
    }

    // Get the access role, @returns The access role.
    public getAccessRole = (): string => sessionStorage.getItem(DefaultFields.LoggedInUserAccessRole) || '';

    // Get the access role UId, @returns The access role UId.
    public getAccessRoleUId = (): string => sessionStorage.getItem(DefaultFields.LoggedInUserAccessRoleUId) || '';

    // Get the user email ID, @returns The user email ID.
    public getUserEmailId = (): string => sessionStorage.getItem(DefaultFields.LoggedInUserEmailId) || '';

    // Get the user UId, @returns The user email ID.
    public getUserUId = (): string => sessionStorage.getItem(DefaultFields.LoggedInUserAccountUId) || '';

    // Clear session and local storage.
    public clear = (): void => {
        sessionStorage.clear();
        localStorage.clear();
    };

    public getLanguage(): string {
        let langUId = this.getSessionStorageItem(DefaultFields.LanguageUId);
        let languageUId: string = '';
        if (langUId !== '') {
            languageUId = JSON.parse(this.getSessionStorageItem(DefaultFields.LanguageUId));
            if (languageUId === null || languageUId === undefined || languageUId === 'null') {
                languageUId = DefaultFields.EnglishLanguageUId;
            }
        } else {
            languageUId = DefaultFields.EnglishLanguageUId;
        }


        return languageUId;
    }

    public setLanguage(languageUId: Guid): void {
        let tempLanguageUId = this.getSessionStorageItem(DefaultFields.LanguageUId);
        let langUId: string = '';
        if (tempLanguageUId !== '') {
            langUId = JSON.parse(this.getSessionStorageItem(DefaultFields.LanguageUId));
        }
        if (languageUId !== langUId) {
            this.setSessionStorageItem(DefaultFields.LanguageUId, JSON.stringify(languageUId));
            const languageChanged: LanguageChanged = new LanguageChanged();
            languageChanged.LanguageUId = languageUId;
            this.languageChanged.emit(languageChanged);
        }
    }

    public getSelectedTheme(): Theme {
        let theme = this.getSessionStorageItem(DefaultFields.SelectedTheme);
        if (theme !== '') {
            return JSON.parse(theme) as Theme;
        } else {
            return {} as Theme;
        }
    }

    public setSelectedTheme(selectedTheme: Theme): void {
        const storedTheme: string = this.getSessionStorageItem(DefaultFields.SelectedTheme);
        let theme: Theme;

        if (storedTheme !== null && storedTheme !== '') {
            theme = JSON.parse(this.getSessionStorageItem(DefaultFields.SelectedTheme));
            if (selectedTheme !== theme) {
                this.setSessionStorageItem(DefaultFields.SelectedTheme, JSON.stringify(selectedTheme));
                const themeChanged: ThemeChanged = new ThemeChanged();
                themeChanged.Theme = selectedTheme;
                this.themeChanged.emit(themeChanged);
            }
        }

    }

    public setTheme(theme: Theme): void {
        this.setSessionStorageItem(DefaultFields.Theme, JSON.stringify(theme));
    }

    public getTheme(): Theme {
        let theme = this.getSessionStorageItem(DefaultFields.Theme);
        if (theme !== '') {
            return JSON.parse(theme) as Theme;
        } else {
            return {} as Theme;
        }
    }

    public setSelectedTab(tab: string): void {
        this.setSessionStorageItem('selectedTab', tab);
    }

    public getSelectedTab(): string {
        let tab = this.getSessionStorageItem('selectedTab');
        return tab || '';

    }

    public setLastCalled(lastCall: Date): void {
        this.lastCalled = lastCall;
    }

    public getLastCalled(): Date {
        return this.lastCalled || new Date();
    }

    public getCulture = (): string => sessionStorage.getItem(DefaultFields.Culture) || '';

    public getDirection(): string {
        const direction: boolean = JSON.parse(this.getSessionStorageItem(DefaultFields.IsRTL));

        return direction === true ? 'rtl' : 'ltr';
    }

    public languageResourcesLoaded(value?: boolean): void {
        this.resourcesLoaded.emit(value);
    }

    public setLoaderText(loaderText: string): void {
        this.loaderTextChanged.emit(loaderText);
    }
}

export class LanguageChanged {
    public LanguageUId!: Guid;
}

export class ThemeChanged {
    public Theme!: Theme;
    public SelectedTheme!: Theme;
}

