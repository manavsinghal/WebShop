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

// Define the CoreTimeoutErrorHandlerService
@Injectable({
    providedIn: 'root'
})
export class CoreTimeoutErrorHandlerService {
    // EventEmitter to broadcast timeout error messages.
    @Output() timeoutErrorHandler: EventEmitter<string> = new EventEmitter<string>();

    // Emit a timeout error message with the given URL.@param url The URL related to the timeout error.
    emitMasterAssetTranslationFileChange(url: string): void {
        // Emit the timeout error message
        this.timeoutErrorHandler.emit(url);
    }
}

