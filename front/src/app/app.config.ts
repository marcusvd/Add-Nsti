import { ApplicationConfig, importProvidersFrom } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { provideHttpClient, withJsonpSupport } from '@angular/common/http';

import { IConfig, NgxMaskModule } from "ngx-mask";
import { MatPaginatorIntl } from '@angular/material/paginator';
import { CustomMatPaginatorIntl } from 'shared/components/list-g/list/custom-mat-aginator-intl.service';


const maskConfigFunction: () => Partial<IConfig> = () => {
  return {
    validation: true,
  };
};


export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes), provideAnimationsAsync(),
    provideHttpClient(withJsonpSupport()),
    importProvidersFrom(NgxMaskModule.forRoot(maskConfigFunction)),
    { provide: MatPaginatorIntl, useClass: CustomMatPaginatorIntl }
  ]
};
