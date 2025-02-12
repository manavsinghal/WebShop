/* Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright> 

*/ 

import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { Token } from '../../shared/models/token.model';
import { DefaultFields } from '../../shared/models/default-fields.model';
import { CoreEnvironmentService } from '../services/core.environment.service';
import { CoreAuthenticationService } from '../services/core.authentication.service';
import { CoreSessionService } from '../services/core.session.service';

@Injectable()
export class AuthorizationInterceptor implements HttpInterceptor {
    public static lastCalled: Date = new Date();
    constructor(
        private readonly coreEnvironmentService: CoreEnvironmentService,
        private readonly coreAuthenticationService: CoreAuthenticationService,
        private readonly coreSessionService: CoreSessionService) { }

    intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
        AuthorizationInterceptor.lastCalled = new Date();
        this.coreSessionService.setLastCalled(AuthorizationInterceptor.lastCalled);
        if (this.coreEnvironmentService.environment && this.coreEnvironmentService.environment.AuthProvider === DefaultFields.AzureADAuthProvider) {
            return this.coreAuthenticationService.getToken()
                .pipe(switchMap((token: Token) => {
                    request = request.clone({
                        setHeaders: {
                            Authorization: 'Bearer ' + token.access_token
                        }
                    });

                    return next.handle(request);
                }));

        } else if (this.coreEnvironmentService.environment && this.coreEnvironmentService.environment.AuthProvider === DefaultFields.DebugAuthProvider) {
            request = request.clone({
                setHeaders: {
                    Email: this.coreEnvironmentService.environment.LoggedInUserEmail
                }
            });
        }

        return next.handle(request);
    }
}

