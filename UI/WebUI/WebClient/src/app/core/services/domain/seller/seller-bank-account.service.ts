// Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright>

import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CoreEnvironmentService } from '../../../services/core.environment.service';
import { SellerBankAccountServiceBase } from './__designer/seller-bank-account.service.designer';

// Define the SellerBankAccountService class using the Injectable decorator, making it a singleton provided at the root level
@Injectable({ providedIn: 'root' })
export class SellerBankAccountService extends SellerBankAccountServiceBase {
    protected override readonly http: HttpClient;
    protected override readonly coreEnvironmentService: CoreEnvironmentService;


    // Constructor for the  SellerBankAccountService class, injecting dependencies 
    constructor() {
        const http = inject(HttpClient);
        const coreEnvironmentService = inject(CoreEnvironmentService);

        super();
    
        this.http = http;
        this.coreEnvironmentService = coreEnvironmentService;
    }

}
