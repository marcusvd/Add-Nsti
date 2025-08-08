
import { Injectable } from '@angular/core';

import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { AuthWarningsComponent } from '../auth-warnings.component';
import { BackEndService } from 'shared/services/back-end/backend.service';


@Injectable({
  providedIn: 'root'
})

export class WarningsService extends BackEndService<any> {

  constructor(
    private _dialog: MatDialog,
  ) { super() }


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

}

