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
import { MasterList } from '../../../../../shared/models/domain/master/master-list.model';

// Define the MasterListServiceBase class using the Injectable decorator, making it a singleton provided at the root level
@Injectable({ providedIn: 'root' })
export class MasterListServiceBase {

	getMasterListsUrl! : string;
	mergeMasterListsUrl! : string;

    // Constructor for the MasterListServiceBase class, injecting dependencies
    constructor(
        protected readonly http: HttpClient,
        protected readonly coreEnvironmentService: CoreEnvironmentService) {
    }

	getMasterLists(masterListUId?: string): Observable<Array<MasterList>> {
         // Construct the URL for the API request
        let url: string = this.coreEnvironmentService.environment.BaseUrl;
        this.getMasterListsUrl = this.coreEnvironmentService.serviceUrls['GetMasterListUrl'];

        url = url + this.getMasterListsUrl;
        url = url.replace('{masterListUId}', masterListUId ? masterListUId : Guid.empty);
        // Set the HTTP headers for the request
        let httpHeaders: HttpHeaders = new HttpHeaders({'x-functions-key':this.coreEnvironmentService.environment.FunctionKey});
        // Make the GET request and handle the response
        return this.http.get<Array<MasterList>>(url,{ headers: httpHeaders })
              // Map the response data to an array of MasterList objects
              .pipe(map((data: Array<MasterList>): Array<MasterList> => data),
                // Handle any errors that occur during the request
                catchError( ( error : HttpErrorResponse ) => {
				CoreNotificationService.handleError( error );
				throw error; // rethrow the error to propagate it downstream
			    }
            ));
    }

    mergeMasterLists(masterlists: Array<MasterList>): Observable<Response> {
        let url: string = this.coreEnvironmentService.environment.BaseUrl;
        this.mergeMasterListsUrl = this.coreEnvironmentService.serviceUrls['MergeMasterListUrl'];
        
        // Construct the URL for the API request
        url = url + this.mergeMasterListsUrl;
        // Set the HTTP headers for the request
        let httpHeaders: HttpHeaders = new HttpHeaders({ 'Content-Type': 'application/json', 'CorrelationUId': Guid.newGuid(), 'x-functions-key': this.coreEnvironmentService.environment.FunctionKey });
       // Make the POST request and handle the response
       return this.http.post<Response>(url, JSON.stringify(masterlists),{ headers: httpHeaders })
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
