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
import { MasterListItem } from '../../../../../shared/models/domain/master/master-list-item.model';

// Define the MasterListItemServiceBase class using the Injectable decorator, making it a singleton provided at the root level
@Injectable({ providedIn: 'root' })
export class MasterListItemServiceBase {
	protected readonly http = inject(HttpClient);
	protected readonly coreEnvironmentService = inject(CoreEnvironmentService);


	getMasterListItemsUrl! : string;
	mergeMasterListItemsUrl! : string;
    getMasterListItemsByCodeUrl: string = '/v1/Domain/MasterLists/MasterListItemsByCode?masterListCode={masterListCode}';

	getMasterListItems(masterListItemUId?: string): Observable<Array<MasterListItem>> {
         // Construct the URL for the API request
        let url: string = this.coreEnvironmentService.environment.BaseUrl;
        this.getMasterListItemsUrl = this.coreEnvironmentService.serviceUrls['GetMasterListItemUrl'];

        url = url + this.getMasterListItemsUrl;
        url = url.replace('{masterListItemUId}', masterListItemUId ? masterListItemUId : Guid.empty);
        // Set the HTTP headers for the request
        let httpHeaders: HttpHeaders = new HttpHeaders({'x-functions-key':this.coreEnvironmentService.environment.FunctionKey});
        // Make the GET request and handle the response
        return this.http.get<Array<MasterListItem>>(url,{ headers: httpHeaders })
              // Map the response data to an array of MasterListItem objects
              .pipe(map((data: Array<MasterListItem>): Array<MasterListItem> => data),
                // Handle any errors that occur during the request
                catchError( ( error : HttpErrorResponse ) => {
				CoreNotificationService.handleError( error );
				throw error; // rethrow the error to propagate it downstream
			    }
            ));
    }

    mergeMasterListItems(masterlistitems: Array<MasterListItem>): Observable<Response> {
        let url: string = this.coreEnvironmentService.environment.BaseUrl;
        this.mergeMasterListItemsUrl = this.coreEnvironmentService.serviceUrls['MergeMasterListItemUrl'];
        
        // Construct the URL for the API request
        url = url + this.mergeMasterListItemsUrl;
        // Set the HTTP headers for the request
        let httpHeaders: HttpHeaders = new HttpHeaders({ 'Content-Type': 'application/json', 'CorrelationUId': Guid.newGuid(), 'x-functions-key': this.coreEnvironmentService.environment.FunctionKey });
       // Make the POST request and handle the response
       return this.http.post<Response>(url, JSON.stringify(masterlistitems),{ headers: httpHeaders })
             // Map the response data to a Response object
            .pipe(map((data: Response) => data),
                // Handle any errors that occur during the request
                catchError( ( error : HttpErrorResponse ) => {
				CoreNotificationService.handleError( error );
				throw error; // rethrow the error to propagate it downstream
			    }
            ));
    }

    getMasterListItemsByCode(code: string): Observable<Array<MasterListItem>> {
        let url: string = this.coreEnvironmentService.environment.BaseUrl;
        url = url + this.getMasterListItemsByCodeUrl;
        url = url.replace('{masterListCode}', code);
        // Set the HTTP headers for the request
        let httpHeaders: HttpHeaders = new HttpHeaders({ 'x-functions-key': this.coreEnvironmentService.environment.FunctionKey });
        return this.http.get<Array<MasterListItem>>(url,{ headers: httpHeaders })
            .pipe(map((data: Array<MasterListItem>) => data),
                catchError( ( error : HttpErrorResponse ) => {
				CoreNotificationService.handleError( error );
				throw error; // rethrow the error to propagate it downstream
			    }
            ));
    }
}
