import { Injectable } from "@angular/core";
import { MatDialog as MatDialog } from "@angular/material/dialog";


@Injectable({
  providedIn: 'root'
})
export class SnackActions {

  constructor(private _dialog: MatDialog) { }


  snackActionsTrigger(action: string) {
    switch (action) {
      case 'retryEmail': {

        break;
      }


      case 'abc': {
        break;
      }

      case 'abcd': {
        break;
      }

    }
  }

}
