import { ApplicationConfig, importProvidersFrom, inject } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { provideHttpClient, withInterceptors, withJsonpSupport } from '@angular/common/http';

import { IConfig, NgxMaskModule } from "ngx-mask";
import { MatPaginatorIntl } from '@angular/material/paginator';
import { CustomMatPaginatorIntl } from 'shared/components/list-g/list/custom-mat-aginator-intl.service';
import { AuthInterceptor } from 'components/authentication/interceptors/auth-interceptor';
// import { AuthInterceptor } from 'components/authentication/interceptors/auth-interceptor';


const maskConfigFunction: () => Partial<IConfig> = () => {
  return {
    validation: true,
  };
};


export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideAnimationsAsync(),
    provideHttpClient(
      withJsonpSupport(),
      withInterceptors([AuthInterceptor])
    ),
    importProvidersFrom(NgxMaskModule.forRoot(maskConfigFunction)),
    { provide: MatPaginatorIntl, useClass: CustomMatPaginatorIntl }
  ]
};
