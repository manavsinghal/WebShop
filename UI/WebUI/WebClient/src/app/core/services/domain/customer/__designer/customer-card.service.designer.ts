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
import { CustomerCard } from '../../../../../shared/models/domain/customer/customer-card.model';

// Define the CustomerCardServiceBase class using the Injectable decorator, making it a singleton provided at the root level
@Injectable({ providedIn: 'root' })
export class CustomerCardServiceBase {
	protected readonly http = inject(HttpClient);
	protected readonly coreEnvironmentService = inject(CoreEnvironmentService);


	getCustomerCardsUrl! : string;
	mergeCustomerCardsUrl! : string;

	getCustomerCards(customerCardUId?: string): Observable<Array<CustomerCard>> {
         // Construct the URL for the API request
        let url: string = this.coreEnvironmentService.environment.BaseUrl;
        this.getCustomerCardsUrl = this.coreEnvironmentService.serviceUrls['GetCustomerCardUrl'];

        url = url + this.getCustomerCardsUrl;
        url = url.replace('{customerCardUId}', customerCardUId ? customerCardUId : Guid.empty);
        // Set the HTTP headers for the request
        let httpHeaders: HttpHeaders = new HttpHeaders({'x-functions-key':this.coreEnvironmentService.environment.FunctionKey});
        // Make the GET request and handle the response
        return this.http.get<Array<CustomerCard>>(url,{ headers: httpHeaders })
              // Map the response data to an array of CustomerCard objects
              .pipe(map((data: Array<CustomerCard>): Array<CustomerCard> => data),
                // Handle any errors that occur during the request
                catchError( ( error : HttpErrorResponse ) => {
				CoreNotificationService.handleError( error );
				throw error; // rethrow the error to propagate it downstream
			    }
            ));
    }

    mergeCustomerCards(customercards: Array<CustomerCard>): Observable<Response> {
        let url: string = this.coreEnvironmentService.environment.BaseUrl;
        this.mergeCustomerCardsUrl = this.coreEnvironmentService.serviceUrls['MergeCustomerCardUrl'];
        
        // Construct the URL for the API request
        url = url + this.mergeCustomerCardsUrl;
        // Set the HTTP headers for the request
        let httpHeaders: HttpHeaders = new HttpHeaders({ 'Content-Type': 'application/json', 'CorrelationUId': Guid.newGuid(), 'x-functions-key': this.coreEnvironmentService.environment.FunctionKey });
       // Make the POST request and handle the response
       return this.http.post<Response>(url, JSON.stringify(customercards),{ headers: httpHeaders })
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
