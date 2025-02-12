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
import { ShoppingCart } from '../../../../../shared/models/domain/shopping-cart/shopping-cart.model';

// Define the ShoppingCartServiceBase class using the Injectable decorator, making it a singleton provided at the root level
@Injectable({ providedIn: 'root' })
export class ShoppingCartServiceBase {

	getShoppingCartsUrl! : string;
	mergeShoppingCartsUrl! : string;

    // Constructor for the ShoppingCartServiceBase class, injecting dependencies
    constructor(
        protected readonly http: HttpClient,
        protected readonly coreEnvironmentService: CoreEnvironmentService) {
    }

	getShoppingCarts(shoppingCartUId?: string): Observable<Array<ShoppingCart>> {
         // Construct the URL for the API request
        let url: string = this.coreEnvironmentService.environment.BaseUrl;
        this.getShoppingCartsUrl = this.coreEnvironmentService.serviceUrls['GetShoppingCartUrl'];

        url = url + this.getShoppingCartsUrl;
        url = url.replace('{shoppingCartUId}', shoppingCartUId ? shoppingCartUId : Guid.empty);
        // Set the HTTP headers for the request
        let httpHeaders: HttpHeaders = new HttpHeaders({'x-functions-key':this.coreEnvironmentService.environment.FunctionKey});
        // Make the GET request and handle the response
        return this.http.get<Array<ShoppingCart>>(url,{ headers: httpHeaders })
              // Map the response data to an array of ShoppingCart objects
              .pipe(map((data: Array<ShoppingCart>): Array<ShoppingCart> => data),
                // Handle any errors that occur during the request
                catchError( ( error : HttpErrorResponse ) => {
				CoreNotificationService.handleError( error );
				throw error; // rethrow the error to propagate it downstream
			    }
            ));
    }

    mergeShoppingCarts(shoppingcarts: Array<ShoppingCart>): Observable<Response> {
        let url: string = this.coreEnvironmentService.environment.BaseUrl;
        this.mergeShoppingCartsUrl = this.coreEnvironmentService.serviceUrls['MergeShoppingCartUrl'];
        
        // Construct the URL for the API request
        url = url + this.mergeShoppingCartsUrl;
        // Set the HTTP headers for the request
        let httpHeaders: HttpHeaders = new HttpHeaders({ 'Content-Type': 'application/json', 'CorrelationUId': Guid.newGuid(), 'x-functions-key': this.coreEnvironmentService.environment.FunctionKey });
       // Make the POST request and handle the response
       return this.http.post<Response>(url, JSON.stringify(shoppingcarts),{ headers: httpHeaders })
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
