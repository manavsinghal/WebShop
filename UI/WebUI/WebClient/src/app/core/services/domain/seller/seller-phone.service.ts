// Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright>

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CoreEnvironmentService } from '../../../services/core.environment.service';
import { SellerPhoneServiceBase } from './__designer/seller-phone.service.designer';

// Define the SellerPhoneService class using the Injectable decorator, making it a singleton provided at the root level
@Injectable({ providedIn: 'root' })
export class SellerPhoneService extends SellerPhoneServiceBase {

    // Constructor for the  SellerPhoneService class, injecting dependencies 
    constructor(
        protected override readonly http: HttpClient,
        protected override readonly coreEnvironmentService: CoreEnvironmentService) {
        super(http, coreEnvironmentService);
    }

}
