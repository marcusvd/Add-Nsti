import { ApplicationConfig, importProvidersFrom, inject } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { provideHttpClient, withInterceptors, withJsonpSupport } from '@angular/common/http';
import { MatPaginatorIntl } from '@angular/material/paginator';
import { registerLocaleData } from '@angular/common';
import ptBr from '@angular/common/locales/pt';
import ptBrExtra from '@angular/common/locales/extra/pt';
import { LOCALE_ID } from '@angular/core';


registerLocaleData(ptBr, 'pt-Br', ptBrExtra)

import { IConfig, NgxMaskModule } from "ngx-mask";

import { CustomMatPaginatorIntl } from 'shared/components/list-g/list/custom-mat-aginator-intl.service';
import { AuthInterceptor } from 'components/authentication/interceptors/auth-interceptor';
import { from } from 'rxjs';


const maskConfigFunction: () => Partial<IConfig> = () => {
  return {
    validation: true,
  };
};


export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    {provide:LOCALE_ID, useValue: 'pt-BR'},
    provideAnimationsAsync(),
    provideHttpClient(
      withJsonpSupport(),
      withInterceptors([AuthInterceptor])
    ),
    importProvidersFrom(NgxMaskModule.forRoot(maskConfigFunction)),
    { provide: MatPaginatorIntl, useClass: CustomMatPaginatorIntl }
  ]
};
