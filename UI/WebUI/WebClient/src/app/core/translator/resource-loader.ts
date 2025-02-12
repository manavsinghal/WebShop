/* Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright> 

*/ 

import { TranslateLoader } from '@ngx-translate/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { CoreEnvironmentService } from '../../core/services/core.environment.service';

export class LanguageResource {
	public Language!: string;
	public Literals!: Literal;
}

export class ResourceLoader implements TranslateLoader {
	private static readonly languages: Array<LanguageResource> = new Array<LanguageResource>();

	constructor(
		readonly http: HttpClient,
		readonly coreEnvironmentService: CoreEnvironmentService,
	) {

	}
    
    getTranslation(lang: string): Observable<Literal> {
        const languageResource: LanguageResource = this.getLanguageResources(lang);

        if (languageResource) {
            return of(languageResource.Literals);
        }

        const url: string = CoreEnvironmentService.Environment.BaseUrl;
        const apiAddress: string = `${url}/v1/Domain/MasterLists/languageResources/${lang}`;

        return this.http.get<LanguageResourceResponse>(apiAddress).pipe(
            map((res: LanguageResourceResponse): Literal => {
                this.setLanguageResources(lang, res.Literals);
                return res.Literals;
            }),
            catchError((error: HttpErrorResponse): Observable<Literal> => {
                console.error('Error fetching language resources:', error);
                return this.http.get<LanguageResourceResponse>(`${apiAddress}${lang}`).pipe(
                    map((res: LanguageResourceResponse): Literal => {
                        this.setLanguageResources(lang, res.Literals);
                        return res.Literals;
                    }),
                    catchError((errorData: HttpErrorResponse): Observable<Literal> => {
                        console.error('Error fetching fallback language resources:', errorData);
                        return of({} as Literal); // Return an empty Literal or handle as needed
                    })
                );
            })
        );
    }

	private getLanguageResources(lang: string): LanguageResource {
		const result: Array<LanguageResource> = ResourceLoader.languages.filter((m: LanguageResource): boolean => m.Language === lang);

		return result[0];
	}

	private setLanguageResources(lang: string, data: Literal): void {
		const languageResource: LanguageResource = new LanguageResource();
		languageResource.Language = lang;
		languageResource.Literals = data;
		ResourceLoader.languages.push(languageResource);
	}
}

interface LanguageResourceResponse {
	Culture: string;
	Literals: Literal;
}

interface Literal {
	[id: string]: string;
}

