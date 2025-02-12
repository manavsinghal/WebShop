// Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright>

import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { map, catchError} from 'rxjs/operators';
import { Observable } from 'rxjs';
import { Guid } from '../../../../../core/helpers/guid';
import { CoreEnvironmentService } from '../../../../services/core.environment.service';
import { CoreNotificationService } from '../../../../services/core.notification.service';
import { SellerBankAccount } from '../../../../../shared/models/domain/seller/seller-bank-account.model';

// Define the SellerBankAccountServiceBase class using the Injectable decorator, making it a singleton provided at the root level
@Injectable({ providedIn: 'root' })
export class SellerBankAccountServiceBase {
	protected readonly http = inject(HttpClient);
	protected readonly coreEnvironmentService = inject(CoreEnvironmentService);


	getSellerBankAccountsUrl! : string;
	mergeSellerBankAccountsUrl! : string;

	getSellerBankAccounts(sellerBankAccountUId?: string): Observable<Array<SellerBankAccount>> {
         // Construct the URL for the API request
        let url: string = this.coreEnvironmentService.environment.BaseUrl;
        this.getSellerBankAccountsUrl = this.coreEnvironmentService.serviceUrls['GetSellerBankAccountUrl'];

        url = url + this.getSellerBankAccountsUrl;
        url = url.replace('{sellerBankAccountUId}', sellerBankAccountUId ? sellerBankAccountUId : Guid.empty);
        // Set the HTTP headers for the request
        let httpHeaders: HttpHeaders = new HttpHeaders({'x-functions-key':this.coreEnvironmentService.environment.FunctionKey});
        // Make the GET request and handle the response
        return this.http.get<Array<SellerBankAccount>>(url,{ headers: httpHeaders })
              // Map the response data to an array of SellerBankAccount objects
              .pipe(map((data: Array<SellerBankAccount>): Array<SellerBankAccount> => data),
                // Handle any errors that occur during the request
                catchError( ( error : HttpErrorResponse ) => {
				CoreNotificationService.handleError( error );
				throw error; // rethrow the error to propagate it downstream
			    }
            ));
    }

    mergeSellerBankAccounts(sellerbankaccounts: Array<SellerBankAccount>): Observable<Response> {
        let url: string = this.coreEnvironmentService.environment.BaseUrl;
        this.mergeSellerBankAccountsUrl = this.coreEnvironmentService.serviceUrls['MergeSellerBankAccountUrl'];
        
        // Construct the URL for the API request
        url = url + this.mergeSellerBankAccountsUrl;
        // Set the HTTP headers for the request
        let httpHeaders: HttpHeaders = new HttpHeaders({ 'Content-Type': 'application/json', 'CorrelationUId': Guid.newGuid(), 'x-functions-key': this.coreEnvironmentService.environment.FunctionKey });
       // Make the POST request and handle the response
       return this.http.post<Response>(url, JSON.stringify(sellerbankaccounts),{ headers: httpHeaders })
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
