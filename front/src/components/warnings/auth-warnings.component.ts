import { CommonModule } from '@angular/common';
import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { BtnGComponent } from 'shared/components/btn-g/btn-g.component';
import { BackEndService } from 'shared/services/back-end/backend.service';

@Component({
  selector: 'auth-warnings',
  templateUrl: './auth-warnings.component.html',
  standalone: true,
  imports: [
    CommonModule,
    BtnGComponent,
    MatDialogModule
  ],
  styles: [
    `
.break {
    word-wrap: break-word;
}
    `
  ]
})
export class AuthWarningsComponent {
  title: string;
  messageBody: string;
  btn1: string;
  btn2: string;

  constructor(
    private _dialogRef: MatDialogRef<AuthWarningsComponent>, @Inject(MAT_DIALOG_DATA) private data: {title:string, body:string, btnLeft:string, btnRight:string},
  ) {
    this.title = this.data.title;
    this.messageBody = this.data.body;
    this.btn1 = this.data.btnLeft;
    this.btn2 = this.data.btnRight;
  }

  clickedYes() {
    this._dialogRef.close(true);
  }

  clickedNo() {
    this._dialogRef.close(false);
  }

}
