/* Copyright (c) 2025 Accenture . All rights reserved.

// <copyright company="Accenture">
// Copyright (c) 2025 Accenture.  All rights reserved.
// Reproduction or transmission in whole or in part, in any form or by any means, 
// electronic, mechanical or otherwise, is prohibited without the prior written 
// consent of the copyright owner.
// </copyright> 

*/
import { NgModule } from "@angular/core";
import { NgxUiLoaderConfig } from "ngx-ui-loader";
import { CoreNotificationService } from "./core/services/core.notification.service";

 

//import { BrowserModule } from '@angular/platform-browser';
//import { APP_INITIALIZER, NgModule } from '@angular/core';
//import { FormsModule } from '@angular/forms';
//import { HTTP_INTERCEPTORS, provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
//import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
//import { CommonModule } from '@angular/common';
//import { NgxUiLoaderModule, NgxUiLoaderConfig, NgxUiLoaderRouterModule } from 'ngx-ui-loader';
//import { ToastNoAnimationModule } from 'ngx-toastr';
//import { AppComponent } from './app.component';
//import { AppRoutingModule } from './app-routing.module';
//import { CoreModule } from './core/core.module';
//import { AuthorizationInterceptor } from './core/interceptors/authorization.interceptor';
//import { CoreNotificationService } from './core/services/core.notification.service';
//import { CoreAuthenticationService } from './core/services/core.authentication.service';
//import { CoreEnvironmentService } from './core/services/core.environment.service';

//const ngxUiLoaderConfig: NgxUiLoaderConfig = {
//    fgsSize: 36,
//    hasProgressBar: false,
//    logoPosition: 'bottom-left',
//    fgsColor: '#a100ff',
//    textColor: '#d8d5d5',
//    logoSize: 400,
//};

@NgModule(/* TODO(standalone-migration): clean up removed NgModule class manually. 
//{
//    declarations: [
//        AppComponent
//    ],
//    imports: [
//       /* BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),*/
//        FormsModule,
//        AppRoutingModule,
//        CoreModule,
//        CommonModule,
//        ToastNoAnimationModule.forRoot(),
//        BrowserAnimationsModule,
//        NgxUiLoaderModule.forRoot(ngxUiLoaderConfig),
//        NgxUiLoaderRouterModule.forRoot({ showForeground: true })
//    ],
//    providers: [
//        { provide: 'APP_ID', useValue: 'ng-cli-universal' },
//        {
//            provide: APP_INITIALIZER,
//            deps: [CoreEnvironmentService, CoreAuthenticationService],
//            multi: true,
//            useFactory: (coreEnvironmentService: CoreEnvironmentService, coreAuthenticationService: CoreAuthenticationService) => (): Promise<void> => {
//                return coreEnvironmentService.initializeEnvironment().then(() => {
//                    return coreAuthenticationService.bootstrap();
//                });
//            }
//        },
//        {
//            provide: HTTP_INTERCEPTORS,
//            useClass: AuthorizationInterceptor,
//            multi: true
//        },
//        provideHttpClient(withInterceptorsFromDi())
//    ],
//    bootstrap: [AppComponent]
    //} */
)
export class AppModule {
    constructor(public coreNotificationService: CoreNotificationService) {
    }
}

