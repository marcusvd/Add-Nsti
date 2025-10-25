
import { inject, Injectable } from '@angular/core';

import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { AuthWarningsComponent } from '../auth-warnings.component';
import { BackEndService } from 'shared/services/back-end/backend.service';
import { DeleteDialogComponent } from 'shared/components/delete-dialog/delete-dialog.component';
import { SelectCompanyComponent } from 'components/authentication/select-company/select-company.component';
import { extend } from 'hammerjs';


@Injectable({
  providedIn: 'root'
})

export class WarningsService extends BackEndService<any> {

  private _dialog = inject(MatDialog);



  openAuthWarnings(data: { title: string, body: string, btnLeft: string, btnRight: string }) {

    const title: string = data.title;
    const messageBody: string = data.body;
    const btn1: string = data.btnLeft;
    const btn2: string = data.btnRight;

    const dialogRef = this._dialog.open(AuthWarningsComponent, {
      width: '350px',
      height: 'auto',
      disableClose: true,
      data: {
        title: title,
        body: messageBody,
        btnLeft: btn1,
        btnRight: btn2,
      }
    });
    return dialogRef.afterClosed();
  }

  delete(id: number, name: string) {

    const dialogRef = this._dialog.open(DeleteDialogComponent, {

      data: { id: id, btn1: 'Cancelar', btn2: 'Confirmar', messageBody: `Tem certeza que deseja deletar o item `, itemToBeDelete: `${name}` },

      disableClose: true,
      panelClass: 'custom-dialog-class',
      backdropClass: 'backdrop-dialog'

    });

    const result = dialogRef.afterClosed();

    return result;
  }
  
// selectCompany(id: number) {
//   const dialogRef = this._dialog.open(SelectCompanyComponent, {
//     data: { id: id, btn1: 'Cancelar', btn2: 'Selecionar' },
//     disableClose: true,
//     panelClass: 'full-screen-dialog', // Classe para estilos personalizados
//     backdropClass: 'backdrop-dialog',
//     width: '100vw', // Largura total da viewport
//     height: '100vh' // Altura total da viewport
//   });

//   const result = dialogRef.afterClosed();
//   return result;
// }
}
