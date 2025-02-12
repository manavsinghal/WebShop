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
import { Country } from '../../../../../shared/models/domain/master/country.model';
import { CoreSessionService } from '../../../core.session.service';
// Define the CountryServiceBase class using the Injectable decorator, making it a singleton provided at the root level
@Injectable({ providedIn: 'root' })
export class CountryServiceBase {

	getCountriesUrl! : string;
	mergeCountriesUrl! : string;

    // Constructor for the CountryServiceBase class, injecting dependencies
    constructor(
        protected readonly http: HttpClient,
        protected readonly coreEnvironmentService: CoreEnvironmentService,
        protected readonly coreSessionService: CoreSessionService ) {
    }

	getCountries(countryUId?: string): Observable<Array<Country>> {
         // Construct the URL for the API request
        let url: string = this.coreEnvironmentService.environment.BaseUrl;
        this.getCountriesUrl = this.coreEnvironmentService.serviceUrls['GetCountryUrl'];

        url = url + this.getCountriesUrl;
        url = url.replace('{countryUId}', countryUId ? countryUId : Guid.empty);
        const languageUId : string = this.coreSessionService.getLanguage();
        url = url.replace('{LanguageUId}', languageUId ? languageUId : Guid.empty);
        // Set the HTTP headers for the request
        let httpHeaders: HttpHeaders = new HttpHeaders({'x-functions-key':this.coreEnvironmentService.environment.FunctionKey});
        // Make the GET request and handle the response
        return this.http.get<Array<Country>>(url,{ headers: httpHeaders })
              // Map the response data to an array of Country objects
              .pipe(map((data: Array<Country>): Array<Country> => data),
                // Handle any errors that occur during the request
                catchError( ( error : HttpErrorResponse ) => {
				CoreNotificationService.handleError( error );
				throw error; // rethrow the error to propagate it downstream
			    }
            ));
    }

    mergeCountries(countries: Array<Country>): Observable<Response> {
        let url: string = this.coreEnvironmentService.environment.BaseUrl;
        this.mergeCountriesUrl = this.coreEnvironmentService.serviceUrls['MergeCountryUrl'];
        
        // Construct the URL for the API request
        url = url + this.mergeCountriesUrl;
        // Set the HTTP headers for the request
        let httpHeaders: HttpHeaders = new HttpHeaders({ 'Content-Type': 'application/json', 'CorrelationUId': Guid.newGuid(), 'x-functions-key': this.coreEnvironmentService.environment.FunctionKey });
       // Make the POST request and handle the response
       return this.http.post<Response>(url, JSON.stringify(countries),{ headers: httpHeaders })
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
