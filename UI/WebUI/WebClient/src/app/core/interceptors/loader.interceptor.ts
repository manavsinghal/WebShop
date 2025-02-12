/* Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright> 

*/ 

import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { NgxUiLoaderService } from 'ngx-ui-loader';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { CoreTimeoutErrorHandlerService } from '../services/core.time-out-error-handler.service';

@Injectable()
export class LoaderInterceptor<T> implements HttpInterceptor {

    public httpRequestArray: Array<HttpRequest<T>> = [];
    private isLoaderVisible: boolean = false;

    constructor(private readonly ngxService: NgxUiLoaderService,
        private readonly timeoutErrorHandlerService: CoreTimeoutErrorHandlerService,
    ) {
        this.timeoutErrorHandlerService.timeoutErrorHandler.subscribe((url: string): void => {
            if (this.httpRequestArray && this.httpRequestArray.length > 0) {
                this.httpRequestArray = this.httpRequestArray.filter((request: HttpRequest<T>): boolean => request.url !== url);
            }
            if (this.httpRequestArray.length === 0) {
                this.hideLoader();
            }
        });
    }

    intercept(request: HttpRequest<T>, next: HttpHandler): Observable<HttpEvent<T>> {
        const url: string = request.urlWithParams;
        if (!(url.endsWith('/forcedsignin') || url.endsWith('/gettoken') || request.headers.get('hide-loader') === 'true')) {
            this.httpRequestArray.push(request);
        }
        if (this.httpRequestArray.length > 0 && !this.isLoaderVisible) {
            this.showLoader();
        }

        return next.handle(request)
            .pipe(tap((event: HttpEvent<T>): void => {
                if (event instanceof HttpResponse) {
                    if (!(url.endsWith('/forcedsignin') || url.endsWith('/gettoken'))) {
                        this.httpRequestArray.pop();
                        setTimeout((): void => {
                            if (this.httpRequestArray.length === 0) {
                                this.hideLoader();
                            } else if (this.isLoaderVisible) {
                                this.showLoader();
                            }
                        }, 700);
                    }
                }
            }, (): void => {
                if (!(url.endsWith('/forcedsignin') || url.endsWith('/gettoken') || request.headers.get('hide-loader') === 'true')) {
                    this.httpRequestArray.pop();
                    setTimeout((): void => {
                        if (this.httpRequestArray.length === 0) {
                            this.hideLoader();
                        } else if (this.isLoaderVisible) {
                            this.showLoader();
                        }
                    }, 700);
                }
            }));
    }

    private hideLoader(): void {
        this.isLoaderVisible = false;
        this.ngxService.stop();
    }
    private showLoader(): void {
        this.isLoaderVisible = true;
        this.ngxService.start();
    }
}

