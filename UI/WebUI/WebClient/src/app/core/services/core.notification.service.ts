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
import { HttpErrorResponse } from '@angular/common/http';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';

// Define the CoreNotificationService
@Injectable({
    providedIn: 'root'
})
export class CoreNotificationService {
    // Static toastr and translateService variables
    static toastr: ToastrService;
    static translateService: TranslateService;

    // Constructor to inject ToastrService and TranslateService
    constructor(
        private readonly toastr: ToastrService,
        private readonly translateService: TranslateService) {
        // Set static variables
        CoreNotificationService.toastr = this.toastr;
        // Configure toastr settings
        CoreNotificationService.toastr.toastrConfig.closeButton = true;
        CoreNotificationService.toastr.toastrConfig.timeOut = 5000;
        CoreNotificationService.translateService = this.translateService;
    }

    // Log message to console
    public static logMessage(msg: string): void {
        // Check if message is null or undefined
        if (msg === null || msg === undefined) {

            return;
        }
        // Log message to console if console is available
        if (console) {
            console.log(msg);
        }
    }

    // Show message using toastr
    public static showMessage(msg: string): void {
        this.toastr.info(msg);
    }

    // Show error message using toastr
    public static showError(msg: string): void {
        this.toastr.error(msg);
    }

    // Show warning message using toastr
    public static showWarning(msg: string): void {
        this.toastr.warning(msg, '', {
            enableHtml: true
        });
    }

    // Show success message using toastr
    public static showSuccess(msg: string): void {
        this.toastr.success(msg);
    }

    // Handle HTTP call errors
    public static handleError(error: HttpErrorResponse): void {
        // Check error status code
        if (error.status >= 500 && error.status <= 510) {
            // If error is an array, iterate and show error messages
            if (Array.isArray(error.error)) {
                error.error.forEach((err: { Message: string }): void => {
                    const fault: { Message: string } = err;
                    if (fault && fault.Message) {
                        CoreNotificationService.showError(fault.Message);
                    } else {
                        // Get error message from translation service
                        this.translateService.get('ErrorMsg').subscribe((data: string) => {
                            const msg: string = data;
                            if (msg) {
                                CoreNotificationService.showError(msg);
                            }
                        });
                    }
                });
            } else {
                // Show error message
                CoreNotificationService.showError(error.message);
            }
        } else if (error.status === 0) {
            // Handle connection failure
            let msg: string = 'Unable to connect to Services. Please check your internet connection and try again.' as string;
            this.translateService.get('ConnectionFailureMsg').subscribe((data: string): string => msg = data);
            CoreNotificationService.showError(msg);
        } else if (error.status === 401) {
            // Handle session expiration
            let msg: string = 'Your Session has been expired. Please close the browser and login again.' as string;
            this.translateService.get('SessionExpiredMsg').subscribe((data: string): string => msg = data);
            CoreNotificationService.showError(msg);
        } else {
            // Handle other errors
            const translation: string = error.error as string;
            const msg: string = error.status ? error.status.toString() : '' + ' ' + translation;
            CoreNotificationService.showError(msg);
        }
    }
}

