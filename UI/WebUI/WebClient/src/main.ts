import { enableProdMode, APP_INITIALIZER, importProvidersFrom } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './app/app.module';
import { environment } from './environments/environment';
import { CoreEnvironmentService } from './app/core/services/core.environment.service';
import { CoreAuthenticationService } from './app/core/services/core.authentication.service';
import { HTTP_INTERCEPTORS, provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { AuthorizationInterceptor } from './app/core/interceptors/authorization.interceptor';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app/app-routing.module';
import { CoreModule } from './app/core/core.module';
import { CommonModule } from '@angular/common';
import { ToastNoAnimationModule } from 'ngx-toastr';
import { provideAnimations } from '@angular/platform-browser/animations';
import { NgxUiLoaderModule, NgxUiLoaderConfig, NgxUiLoaderRouterModule } from 'ngx-ui-loader';
import { AppComponent } from './app/app.component';
import { bootstrapApplication } from '@angular/platform-browser';

const ngxUiLoaderConfig: NgxUiLoaderConfig = {
    fgsSize: 36,
    hasProgressBar: false,
    logoPosition: 'bottom-left',
    fgsColor: '#a100ff',
    textColor: '#d8d5d5',
    logoSize: 400,
};



export function getBaseUrl() {
    return document.getElementsByTagName('base')[0].href;
}

const providers = [
    { provide: 'BASE_URL', useFactory: getBaseUrl, deps: [] }
];

if (environment.production) {
    enableProdMode();
}

bootstrapApplication(AppComponent, {
    providers: [
        importProvidersFrom(
        /* BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),*/
        FormsModule, AppRoutingModule, CoreModule, CommonModule, ToastNoAnimationModule.forRoot(), NgxUiLoaderModule.forRoot(ngxUiLoaderConfig), NgxUiLoaderRouterModule.forRoot({ showForeground: true })),
        { provide: 'APP_ID', useValue: 'ng-cli-universal' },
        {
            provide: APP_INITIALIZER,
            deps: [CoreEnvironmentService, CoreAuthenticationService],
            multi: true,
            useFactory: (coreEnvironmentService: CoreEnvironmentService, coreAuthenticationService: CoreAuthenticationService) => (): Promise<void> => {
                return coreEnvironmentService.initializeEnvironment().then(() => {
                    return coreAuthenticationService.bootstrap();
                });
            }
        },
        {
            provide: HTTP_INTERCEPTORS,
            useClass: AuthorizationInterceptor,
            multi: true
        },
        provideHttpClient(withInterceptorsFromDi()),
        provideAnimations()
    ]
})
    .catch(err => console.error(err));

