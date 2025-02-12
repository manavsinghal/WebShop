// Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright>

import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { map, catchError} from 'rxjs/operators';
import { Observable } from 'rxjs';
import { Guid } from '../../../../../core/helpers/guid';
import { CoreEnvironmentService } from '../../../../services/core.environment.service';
import { CoreNotificationService } from '../../../../services/core.notification.service';
import { ShipperBankAccount } from '../../../../../shared/models/domain/shipper/shipper-bank-account.model';

// Define the ShipperBankAccountServiceBase class using the Injectable decorator, making it a singleton provided at the root level
@Injectable({ providedIn: 'root' })
export class ShipperBankAccountServiceBase {

	getShipperBankAccountsUrl! : string;
	mergeShipperBankAccountsUrl! : string;

    // Constructor for the ShipperBankAccountServiceBase class, injecting dependencies
    constructor(
        protected readonly http: HttpClient,
        protected readonly coreEnvironmentService: CoreEnvironmentService) {
    }

	getShipperBankAccounts(shipperBankAccountUId?: string): Observable<Array<ShipperBankAccount>> {
         // Construct the URL for the API request
        let url: string = this.coreEnvironmentService.environment.BaseUrl;
        this.getShipperBankAccountsUrl = this.coreEnvironmentService.serviceUrls['GetShipperBankAccountUrl'];

        url = url + this.getShipperBankAccountsUrl;
        url = url.replace('{shipperBankAccountUId}', shipperBankAccountUId ? shipperBankAccountUId : Guid.empty);
        // Set the HTTP headers for the request
        let httpHeaders: HttpHeaders = new HttpHeaders({'x-functions-key':this.coreEnvironmentService.environment.FunctionKey});
        // Make the GET request and handle the response
        return this.http.get<Array<ShipperBankAccount>>(url,{ headers: httpHeaders })
              // Map the response data to an array of ShipperBankAccount objects
              .pipe(map((data: Array<ShipperBankAccount>): Array<ShipperBankAccount> => data),
                // Handle any errors that occur during the request
                catchError( ( error : HttpErrorResponse ) => {
				CoreNotificationService.handleError( error );
				throw error; // rethrow the error to propagate it downstream
			    }
            ));
    }

    mergeShipperBankAccounts(shipperbankaccounts: Array<ShipperBankAccount>): Observable<Response> {
        let url: string = this.coreEnvironmentService.environment.BaseUrl;
        this.mergeShipperBankAccountsUrl = this.coreEnvironmentService.serviceUrls['MergeShipperBankAccountUrl'];
        
        // Construct the URL for the API request
        url = url + this.mergeShipperBankAccountsUrl;
        // Set the HTTP headers for the request
        let httpHeaders: HttpHeaders = new HttpHeaders({ 'Content-Type': 'application/json', 'CorrelationUId': Guid.newGuid(), 'x-functions-key': this.coreEnvironmentService.environment.FunctionKey });
       // Make the POST request and handle the response
       return this.http.post<Response>(url, JSON.stringify(shipperbankaccounts),{ headers: httpHeaders })
             // Map the response data to a Response object
            .pipe(map((data: Response) => data),
                // Handle any errors that occur during the request
                catchError( ( error : HttpErrorResponse ) => {
				CoreNotificationService.handleError( error );
				throw error; // rethrow the error to propagate it downstream
			    }
            ));
    }
}
