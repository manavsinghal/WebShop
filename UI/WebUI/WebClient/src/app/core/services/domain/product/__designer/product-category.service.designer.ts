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
import { ProductCategory } from '../../../../../shared/models/domain/product/product-category.model';
import { CoreSessionService } from '../../../core.session.service';
// Define the ProductCategoryServiceBase class using the Injectable decorator, making it a singleton provided at the root level
@Injectable({ providedIn: 'root' })
export class ProductCategoryServiceBase {
	protected readonly http = inject(HttpClient);
	protected readonly coreEnvironmentService = inject(CoreEnvironmentService);
	protected readonly coreSessionService = inject(CoreSessionService);


	getProductCategoriesUrl! : string;
	mergeProductCategoriesUrl! : string;

	getProductCategories(productCategoryUId?: string): Observable<Array<ProductCategory>> {
         // Construct the URL for the API request
        let url: string = this.coreEnvironmentService.environment.BaseUrl;
        this.getProductCategoriesUrl = this.coreEnvironmentService.serviceUrls['GetProductCategoryUrl'];

        url = url + this.getProductCategoriesUrl;
        url = url.replace('{productCategoryUId}', productCategoryUId ? productCategoryUId : Guid.empty);
        const languageUId : string = this.coreSessionService.getLanguage();
        url = url.replace('{LanguageUId}', languageUId ? languageUId : Guid.empty);
        // Set the HTTP headers for the request
        let httpHeaders: HttpHeaders = new HttpHeaders({'x-functions-key':this.coreEnvironmentService.environment.FunctionKey});
        // Make the GET request and handle the response
        return this.http.get<Array<ProductCategory>>(url,{ headers: httpHeaders })
              // Map the response data to an array of ProductCategory objects
              .pipe(map((data: Array<ProductCategory>): Array<ProductCategory> => data),
                // Handle any errors that occur during the request
                catchError( ( error : HttpErrorResponse ) => {
				CoreNotificationService.handleError( error );
				throw error; // rethrow the error to propagate it downstream
			    }
            ));
    }

    mergeProductCategories(productcategories: Array<ProductCategory>): Observable<Response> {
        let url: string = this.coreEnvironmentService.environment.BaseUrl;
        this.mergeProductCategoriesUrl = this.coreEnvironmentService.serviceUrls['MergeProductCategoryUrl'];
        
        // Construct the URL for the API request
        url = url + this.mergeProductCategoriesUrl;
        // Set the HTTP headers for the request
        let httpHeaders: HttpHeaders = new HttpHeaders({ 'Content-Type': 'application/json', 'CorrelationUId': Guid.newGuid(), 'x-functions-key': this.coreEnvironmentService.environment.FunctionKey });
       // Make the POST request and handle the response
       return this.http.post<Response>(url, JSON.stringify(productcategories),{ headers: httpHeaders })
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
