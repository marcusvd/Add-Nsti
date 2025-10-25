
import { inject } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { NavigationExtras, Router } from '@angular/router';

import { UserTokenDto } from 'components/authentication/dtos/user-token-dto';
import { CompanyAuth } from 'components/company/dtos/company-auth';
import * as diacritics from 'diacritics';


export class Base {

  private userLoggedToken: UserTokenDto = JSON?.parse(localStorage?.getItem('userToken') ?? '{}');
  private selectedCompany: CompanyAuth = JSON?.parse(localStorage?.getItem("selectedCompany") ?? '{}');

  private _snackBar = inject(MatSnackBar);
  minDate = new Date('0001-01-01T00:00:00');
  currentDate = new Date();
  currentDateWithoutHours = this.currentDate.setHours(0, 0, 0, 0)
  router = inject(Router);

  get getUserLogged() {
    return this.userLoggedToken
  }
  get getSelectedCompany() {
    return this.selectedCompany
  }

  get userId() {
    return this.userLoggedToken.id;
  }
  get companyId() {
    return this.selectedCompany.id;
  }
  get companyName() {
    return this.selectedCompany.name;
  }

  removeNonNumericAndConvertToNumber(str: string): number {
    return +str.replace(/\D/g, '');
  }

  removeAccentsSpecialCharacters(value: string): string {
    if (typeof value !== 'string') {
      return '';
    }
    const noAccents = diacritics.remove(value);//remove accents
    return noAccents.replace(/[^\w\s]/gi, ''); //remove special characters
  }

  openSnackBar(message: string, style: string, action: string = 'Fechar', duration: number = 5000, horizontalPosition: any = 'center', verticalPosition: any = 'bottom') {
    this._snackBar?.open(message, action, {
      duration: duration, // Tempo em milissegundos (5 segundos)
      panelClass: [style], // Aplica a classe personalizada
      horizontalPosition: horizontalPosition, // Centraliza horizontalmente
      verticalPosition: verticalPosition, // Posição vertical (pode ser 'top' ou 'bottom')
    });
  }

  callRouter(url: string, entity?: any) {
    const objectRoute: NavigationExtras = {
      state: {
        entity
      }
    };

    if (entity)
      this.router?.navigate([url], objectRoute);
    else
      this.router?.navigateByUrl(url);
  }

  fullScreen() {
    const elem = document.documentElement;
    elem.requestFullscreen().then((x) => x)
  }

  
  alertSave(form: FormGroup) {
    if (!form?.valid) {
      alert('Todos os campos com (*) e em vermelho, são de preenchimento obrigatório. Preencha corretamente e tente novamente.')
      form?.markAllAsTouched();
      return false;
    }
    else
      return true;


  }
}

