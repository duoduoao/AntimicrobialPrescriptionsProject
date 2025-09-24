import { bootstrapApplication } from '@angular/platform-browser';
import { provideRouter } from '@angular/router';
import { provideHttpClient, withFetch, HTTP_INTERCEPTORS,HttpClientModule } from '@angular/common/http';
import { importProvidersFrom } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';

import { App } from './app/app';
import { routes } from './app/app.routes';
import { JwtInterceptor } from './app/interceptors/jwt-interceptor';
import { RouterModule } from '@angular/router';

bootstrapApplication(App, {
  providers: [
     importProvidersFrom(HttpClientModule),
    provideRouter(routes),                     // Routing setup
    provideHttpClient(withFetch()),            // HttpClient with fetch backend
    importProvidersFrom(ReactiveFormsModule), // Reactive Forms support
    {
      provide: HTTP_INTERCEPTORS,              // JWT interceptor adds auth token to requests
      useClass: JwtInterceptor,
      multi: true
    }
  ],
}).catch(err => console.error(err));
