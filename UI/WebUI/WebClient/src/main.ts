import { enableProdMode, APP_INITIALIZER, importProvidersFrom } from '@angular/core';
//import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

//import { AppModule } from './app/app.module';
import { environment } from './environments/environment';
import { CoreEnvironmentService } from './app/core/services/core.environment.service';
import { CoreAuthenticationService } from './app/core/services/core.authentication.service';
import { HTTP_INTERCEPTORS, HttpClient, provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { AuthorizationInterceptor } from './app/core/interceptors/authorization.interceptor';
import { FormsModule } from '@angular/forms';
import { routes } from './app/app-routing.module';
//import { CoreModule } from './app/core/core.module';
import { CommonModule } from '@angular/common';
import { ToastNoAnimationModule } from 'ngx-toastr';
import { provideAnimations } from '@angular/platform-browser/animations';
import { NgxUiLoaderModule, NgxUiLoaderConfig, NgxUiLoaderRouterModule } from 'ngx-ui-loader';
import { AppComponent } from './app/app.component';
import { bootstrapApplication } from '@angular/platform-browser';
import { LoaderInterceptor } from './app/core/interceptors/loader.interceptor';
import { provideRouter } from '@angular/router';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { ResourceLoader } from './app/core/translator/resource-loader';
import { SortPipe } from './app/shared/pipes/sort.pipe';

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

if (environment.production) {
    enableProdMode();
}

bootstrapApplication(AppComponent, {
    providers: [
        importProvidersFrom(
            FormsModule, CommonModule, ToastNoAnimationModule.forRoot(), NgxUiLoaderModule.forRoot(ngxUiLoaderConfig), NgxUiLoaderRouterModule.forRoot({ showForeground: true }), NgxUiLoaderRouterModule.forRoot({ showForeground: true }),
            TranslateModule.forRoot({
                loader: {
                    provide: TranslateLoader,
                    useClass: ResourceLoader,
                    deps: [HttpClient],
                }
            })),
            SortPipe,
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
        {
            provide: HTTP_INTERCEPTORS,
            useClass: LoaderInterceptor,
            multi: true
        },
        provideHttpClient(withInterceptorsFromDi()),
        provideAnimations(),
        provideRouter(routes)
    ]
})
    .catch(err => console.error(err));

