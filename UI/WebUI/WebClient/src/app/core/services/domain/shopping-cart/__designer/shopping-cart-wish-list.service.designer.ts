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
import { ShoppingCartWishList } from '../../../../../shared/models/domain/shopping-cart/shopping-cart-wish-list.model';

// Define the ShoppingCartWishListServiceBase class using the Injectable decorator, making it a singleton provided at the root level
@Injectable({ providedIn: 'root' })
export class ShoppingCartWishListServiceBase {
	protected readonly http = inject(HttpClient);
	protected readonly coreEnvironmentService = inject(CoreEnvironmentService);


	getShoppingCartWishListsUrl! : string;
	mergeShoppingCartWishListsUrl! : string;

	getShoppingCartWishLists(shoppingCartWishListUId?: string): Observable<Array<ShoppingCartWishList>> {
         // Construct the URL for the API request
        let url: string = this.coreEnvironmentService.environment.BaseUrl;
        this.getShoppingCartWishListsUrl = this.coreEnvironmentService.serviceUrls['GetShoppingCartWishListUrl'];

        url = url + this.getShoppingCartWishListsUrl;
        url = url.replace('{shoppingCartWishListUId}', shoppingCartWishListUId ? shoppingCartWishListUId : Guid.empty);
        // Set the HTTP headers for the request
        let httpHeaders: HttpHeaders = new HttpHeaders({'x-functions-key':this.coreEnvironmentService.environment.FunctionKey});
        // Make the GET request and handle the response
        return this.http.get<Array<ShoppingCartWishList>>(url,{ headers: httpHeaders })
              // Map the response data to an array of ShoppingCartWishList objects
              .pipe(map((data: Array<ShoppingCartWishList>): Array<ShoppingCartWishList> => data),
                // Handle any errors that occur during the request
                catchError( ( error : HttpErrorResponse ) => {
				CoreNotificationService.handleError( error );
				throw error; // rethrow the error to propagate it downstream
			    }
            ));
    }

    mergeShoppingCartWishLists(shoppingcartwishlists: Array<ShoppingCartWishList>): Observable<Response> {
        let url: string = this.coreEnvironmentService.environment.BaseUrl;
        this.mergeShoppingCartWishListsUrl = this.coreEnvironmentService.serviceUrls['MergeShoppingCartWishListUrl'];
        
        // Construct the URL for the API request
        url = url + this.mergeShoppingCartWishListsUrl;
        // Set the HTTP headers for the request
        let httpHeaders: HttpHeaders = new HttpHeaders({ 'Content-Type': 'application/json', 'CorrelationUId': Guid.newGuid(), 'x-functions-key': this.coreEnvironmentService.environment.FunctionKey });
       // Make the POST request and handle the response
       return this.http.post<Response>(url, JSON.stringify(shoppingcartwishlists),{ headers: httpHeaders })
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
