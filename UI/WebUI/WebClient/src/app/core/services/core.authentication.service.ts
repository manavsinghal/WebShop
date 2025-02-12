/* Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright> 

*/ 

// Import necessary modules and services
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Location } from '@angular/common';
import { Observable, of } from 'rxjs';
import { map, switchMap } from 'rxjs/operators';
import { AuthenticationResult, Configuration, LogLevel, PublicClientApplication } from '@azure/msal-browser';
import { MsalBroadcastService, MsalService } from '@azure/msal-angular';
import { CoreEnvironmentService } from './core.environment.service';
import { CoreSessionService } from './core.session.service';
import { Token } from '../../shared/models/token.model';
import { UserAccount } from '../../shared/models/user-account.model';
import { DefaultFields } from '../../shared/models/default-fields.model';

// Define the CoreAuthenticationService
@Injectable({
    providedIn: 'root'
})
export class CoreAuthenticationService {
    // Initialize msalService, msalBroadcastService, accessToken, identityScopes, and apiScopes
    msalService!: MsalService;
    msalBroadcastService!: MsalBroadcastService;
    accessToken!: string;
    identityScopes: Array<string> = [];
    apiScopes: Array<string> = [];

    // Constructor to inject dependencies
    constructor(
        readonly coreEnvironmentService: CoreEnvironmentService,
        readonly coreSessionService: CoreSessionService,
        readonly router: Router,
        private location: Location
    ) {

    }

    // Login function to redirect to Azure AD login page
    login(): void {
        // Check if AuthProvider is AzureADAuthProvider
        if (this.coreEnvironmentService.environment.AuthProvider === DefaultFields.AzureADAuthProvider) {
            // Set identityScopes and redirect to Azure AD login page
            this.identityScopes = [DefaultFields.IdentityScopeUserRead, DefaultFields.IdentityScopeOpenId, this.coreEnvironmentService.environment.ApiScope];
            this.msalService.loginRedirect({ scopes: this.identityScopes });
        }
    }

    // Logout function to logout from Azure AD
    logout(): void {
        // Check if AuthProvider is AzureADAuthProvider
        if (this.coreEnvironmentService.environment.AuthProvider === DefaultFields.AzureADAuthProvider) {
            // Logout from Azure AD
            this.msalService.logout();
        }
    }

    // getToken function to acquire access token silently or force refresh
    getToken(forceRefreshFlag = false): Observable<Token> {
        // Check if AuthProvider is AzureADAuthProvider
        if (this.coreEnvironmentService.environment.AuthProvider === DefaultFields.AzureADAuthProvider) {
            // Set apiScopes and acquire token silently or force refresh
            this.apiScopes = [this.coreEnvironmentService.environment.ApiScope];

            let activeAccount = this.msalService.instance.getActiveAccount();

            if (activeAccount === null) {
                activeAccount = this.msalService.instance.getAllAccounts()[0];
            }

            return this.msalService.acquireTokenSilent({
                scopes: this.apiScopes,
                forceRefresh: forceRefreshFlag,
                account: activeAccount
            }).pipe(
                switchMap((response: AuthenticationResult) => of(response)),
                map((response: AuthenticationResult): Token => ({ access_token: response.accessToken, expiresOn: response.expiresOn } as Token))
            );
        }
        // If AuthProvider is DebugAuthProvider, return a dummy token
        else if (this.coreEnvironmentService.environment.AuthProvider === DefaultFields.DebugAuthProvider) {
            const t = new Token();

            return of(t);
        }

        // Return a new token if AuthProvider is neither AzureADAuthProvider nor DebugAuthProvider
        return of(new Token());
    }

    // getAccount function to get the current user account
    getAccount(): UserAccount {
        let user_account = new UserAccount();

        // Check if AuthProvider is AzureADAuthProvider
        if (this.coreEnvironmentService.environment !== null && this.coreEnvironmentService.environment.AuthProvider === DefaultFields.AzureADAuthProvider) {
            // Get the active account and set user_account email_id
            let account = this.msalService.instance.getActiveAccount();
            user_account.email_id = account && account.username ? account.username : '';
        }
        // If AuthProvider is DebugAuthProvider, set user_account email_id from environment
        else if (this.coreEnvironmentService.environment.AuthProvider === DefaultFields.DebugAuthProvider) {
            user_account.email_id = this.coreEnvironmentService.environment.LoggedInUserEmail;
        }

        return user_account;
    }

    bootstrap(): Promise<void> {
        // Return a new promise to handle asynchronous initialization
        return new Promise((resolve, reject) => {
            // Check if AuthProvider is AzureADAuthProvider
            if (this.coreEnvironmentService.environment.AuthProvider === DefaultFields.AzureADAuthProvider) {
                // Configure MSAL settings for Azure AD authentication
                const msalConfig: Configuration = {
                    // Authentication settings
                    auth: {
                        // Client ID from environment settings
                        clientId: this.coreEnvironmentService.environment.ClientId,
                        // Authority URL for Azure AD
                        authority: DefaultFields.MSAuthority + this.coreEnvironmentService.environment.TenantId,
                        // Redirect URI for authentication
                        redirectUri: this.coreEnvironmentService.environment.ClientUrl,
                        // Post-logout redirect URI
                        postLogoutRedirectUri: this.coreEnvironmentService.environment.ClientUrl,
                    },
                    // Cache settings
                    cache: {
                        // Cache location (session storage or local storage)
                        cacheLocation: DefaultFields.CacheLocation,
                        // storeAuthStateInCookie: isIE // set to true for IE 11
                    },
                    // System settings
                    system: {
                        // Logger options
                        loggerOptions: {
                            // Logger callback function
                            loggerCallback(logLevel: LogLevel, message: string) {
                                // Log messages to console
                                console.log(logLevel + ' ' + message);
                            },
                            // Log level (Error, Warning, Info, Verbose)
                            logLevel: LogLevel.Error,
                            // Enable or disable PII logging
                            piiLoggingEnabled: false
                        }
                    }
                };

                // Initialize PublicClientApplication with MSAL configuration
                let publicClientApplication = new PublicClientApplication(msalConfig);
                // Initialize MsalService with PublicClientApplication and location
                this.msalService = new MsalService(publicClientApplication, this.location);
                // Resolve the promise on successful initialization
                resolve();
            }
            // If AuthProvider is DebugAuthProvider, resolve the promise without initialization
            else if (this.coreEnvironmentService.environment.AuthProvider === DefaultFields.DebugAuthProvider) {
                resolve();
            }
            // Reject the promise if initialization fails
            reject();
        });
    }
}

