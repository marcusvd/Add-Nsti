import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { DeleteDialogComponent } from '../../../../../src/shared/components/delete-dialog/delete-dialog.component';

@Injectable({
  providedIn: 'root'
})
export class DeleteServices {

  constructor(private _dialog: MatDialog) { }

  delete(id: number, name:string) {

    const dialogRef = this._dialog.open(DeleteDialogComponent, {

      data: { id: id, btn1: 'Cancelar', btn2: 'Confirmar', messageBody: `Tem certeza que deseja deletar o item `, itemToBeDelete: `${name}` },

      disableClose: true,
      panelClass: 'custom-dialog-class',
      backdropClass: 'backdrop-dialog'

    });

    const result = dialogRef.afterClosed();

    return result;
  }

}
