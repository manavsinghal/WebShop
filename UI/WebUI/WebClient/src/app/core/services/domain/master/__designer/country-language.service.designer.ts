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
import { CountryLanguage } from '../../../../../shared/models/domain/master/country-language.model';

// Define the CountryLanguageServiceBase class using the Injectable decorator, making it a singleton provided at the root level
@Injectable({ providedIn: 'root' })
export class CountryLanguageServiceBase {

	getCountryLanguagesUrl! : string;
	mergeCountryLanguagesUrl! : string;

    // Constructor for the CountryLanguageServiceBase class, injecting dependencies
    constructor(
        protected readonly http: HttpClient,
        protected readonly coreEnvironmentService: CoreEnvironmentService) {
    }

	getCountryLanguages(countryLanguageUId?: string): Observable<Array<CountryLanguage>> {
         // Construct the URL for the API request
        let url: string = this.coreEnvironmentService.environment.BaseUrl;
        this.getCountryLanguagesUrl = this.coreEnvironmentService.serviceUrls['GetCountryLanguageUrl'];

        url = url + this.getCountryLanguagesUrl;
        url = url.replace('{countryLanguageUId}', countryLanguageUId ? countryLanguageUId : Guid.empty);
        // Set the HTTP headers for the request
        let httpHeaders: HttpHeaders = new HttpHeaders({'x-functions-key':this.coreEnvironmentService.environment.FunctionKey});
        // Make the GET request and handle the response
        return this.http.get<Array<CountryLanguage>>(url,{ headers: httpHeaders })
              // Map the response data to an array of CountryLanguage objects
              .pipe(map((data: Array<CountryLanguage>): Array<CountryLanguage> => data),
                // Handle any errors that occur during the request
                catchError( ( error : HttpErrorResponse ) => {
				CoreNotificationService.handleError( error );
				throw error; // rethrow the error to propagate it downstream
			    }
            ));
    }

    mergeCountryLanguages(countrylanguages: Array<CountryLanguage>): Observable<Response> {
        let url: string = this.coreEnvironmentService.environment.BaseUrl;
        this.mergeCountryLanguagesUrl = this.coreEnvironmentService.serviceUrls['MergeCountryLanguageUrl'];
        
        // Construct the URL for the API request
        url = url + this.mergeCountryLanguagesUrl;
        // Set the HTTP headers for the request
        let httpHeaders: HttpHeaders = new HttpHeaders({ 'Content-Type': 'application/json', 'CorrelationUId': Guid.newGuid(), 'x-functions-key': this.coreEnvironmentService.environment.FunctionKey });
       // Make the POST request and handle the response
       return this.http.post<Response>(url, JSON.stringify(countrylanguages),{ headers: httpHeaders })
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
