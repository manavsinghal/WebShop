/* Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright> 

*/ 

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { TreeModule } from 'primeng/tree';
import { TreeTableModule } from 'primeng/treetable';
import { CalendarModule } from 'primeng/calendar';
import { SharedComponent } from './shared.component';
import { ResourceLoader } from '../core/translator/resource-loader';
import { LoaderInterceptor } from '../core/interceptors/loader.interceptor';
import { AuthorizationInterceptor } from '../core/interceptors/authorization.interceptor';
import { SafePipe } from '../shared/pipes/safe.pipe';
import { SortPipe } from '../shared/pipes/sort.pipe';
import { CoreSubscriptionService } from '../core/services/core.subscription.service';
import { InactiveRowStatusFilterPipe } from './pipes/inactive-row-status.pipe';
import { InputRestrictionDirective } from './directives/restrict-input.directive';
import { WhiteSpaceValidatorDirective } from './directives/whitespace.directive';
import { EmptyGuidValidatorDirective } from './directives/empty-guid-validator.directive';

@NgModule({
    imports: [NgbModule, TreeModule, TreeTableModule, CommonModule, CalendarModule, FormsModule, ReactiveFormsModule, TranslateModule.forRoot({
            loader: {
                provide: TranslateLoader,
                useClass: ResourceLoader,
                deps: [HttpClient]
            }
        }), SharedComponent, SafePipe, SortPipe, InactiveRowStatusFilterPipe, InputRestrictionDirective, WhiteSpaceValidatorDirective, EmptyGuidValidatorDirective],
    providers: [
        CoreSubscriptionService, SortPipe, InactiveRowStatusFilterPipe,
        { provide: HTTP_INTERCEPTORS, useClass: AuthorizationInterceptor, multi: true },
        { provide: HTTP_INTERCEPTORS, useClass: LoaderInterceptor, multi: true }
    ],
    exports: [SharedComponent, CommonModule, TreeModule, FormsModule, ReactiveFormsModule, TreeTableModule, CalendarModule, TranslateModule, SafePipe, SortPipe, InactiveRowStatusFilterPipe, InputRestrictionDirective, WhiteSpaceValidatorDirective, EmptyGuidValidatorDirective]
})
export class SharedModule { }

