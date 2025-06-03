import { Component, Inject, ViewEncapsulation } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';


import { DeleteGDialogImports } from './imports/delete-dialog-imports';

@Component({
  selector: 'delete-dialog',
  standalone: true,
  encapsulation: ViewEncapsulation.None,
  imports: [
    DeleteGDialogImports
  ],
  templateUrl: './delete-dialog.component.html',
  styleUrls: ['./delete-dialog.component.scss']
})
export class DeleteDialogComponent {

  messageBody: string;
  itemToBeDelete: string;
  id: number;

  constructor(
    private _dialogRef: MatDialogRef<DeleteDialogComponent>, @Inject(MAT_DIALOG_DATA) private data: any,
  ) {
    this.messageBody = this.data.messageBody;
    this.itemToBeDelete = this.data.itemToBeDelete;
    this.id = this.data.id;
  }

  clickedYes(id: number, yes: string) {
    this._dialogRef.close({ id: id });
  }
  clickedNo(no: string) {
    this._dialogRef.close(no);
  }
}
