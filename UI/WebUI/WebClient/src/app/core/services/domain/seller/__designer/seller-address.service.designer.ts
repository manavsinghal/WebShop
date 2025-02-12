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
import { SellerAddress } from '../../../../../shared/models/domain/seller/seller-address.model';

// Define the SellerAddressServiceBase class using the Injectable decorator, making it a singleton provided at the root level
@Injectable({ providedIn: 'root' })
export class SellerAddressServiceBase {

	getSellerAddressesUrl! : string;
	mergeSellerAddressesUrl! : string;

    // Constructor for the SellerAddressServiceBase class, injecting dependencies
    constructor(
        protected readonly http: HttpClient,
        protected readonly coreEnvironmentService: CoreEnvironmentService) {
    }

	getSellerAddresses(sellerAddressUId?: string): Observable<Array<SellerAddress>> {
         // Construct the URL for the API request
        let url: string = this.coreEnvironmentService.environment.BaseUrl;
        this.getSellerAddressesUrl = this.coreEnvironmentService.serviceUrls['GetSellerAddressUrl'];

        url = url + this.getSellerAddressesUrl;
        url = url.replace('{sellerAddressUId}', sellerAddressUId ? sellerAddressUId : Guid.empty);
        // Set the HTTP headers for the request
        let httpHeaders: HttpHeaders = new HttpHeaders({'x-functions-key':this.coreEnvironmentService.environment.FunctionKey});
        // Make the GET request and handle the response
        return this.http.get<Array<SellerAddress>>(url,{ headers: httpHeaders })
              // Map the response data to an array of SellerAddress objects
              .pipe(map((data: Array<SellerAddress>): Array<SellerAddress> => data),
                // Handle any errors that occur during the request
                catchError( ( error : HttpErrorResponse ) => {
				CoreNotificationService.handleError( error );
				throw error; // rethrow the error to propagate it downstream
			    }
            ));
    }

    mergeSellerAddresses(selleraddresses: Array<SellerAddress>): Observable<Response> {
        let url: string = this.coreEnvironmentService.environment.BaseUrl;
        this.mergeSellerAddressesUrl = this.coreEnvironmentService.serviceUrls['MergeSellerAddressUrl'];
        
        // Construct the URL for the API request
        url = url + this.mergeSellerAddressesUrl;
        // Set the HTTP headers for the request
        let httpHeaders: HttpHeaders = new HttpHeaders({ 'Content-Type': 'application/json', 'CorrelationUId': Guid.newGuid(), 'x-functions-key': this.coreEnvironmentService.environment.FunctionKey });
       // Make the POST request and handle the response
       return this.http.post<Response>(url, JSON.stringify(selleraddresses),{ headers: httpHeaders })
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
