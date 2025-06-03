import { Injectable } from "@angular/core";
import { FormControl, UntypedFormControl } from "@angular/forms";
import { MatSnackBar as MatSnackBar, MatSnackBarConfig as MatSnackBarConfig, MatSnackBarHorizontalPosition as MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition as MatSnackBarVerticalPosition } from "@angular/material/snack-bar";
import { TooltipPosition as TooltipPosition } from '@angular/material/tooltip';
import { SnackActions } from "./snack-actions";

@Injectable({
  providedIn: 'root'
})
export class CommunicationAlerts {

  constructor(
    private _snackBar: MatSnackBar,
    private _snackActions: SnackActions
  ) { }

  defaultSnackMsg(Message: string, kind: number, btnAction?: string, duration?: number) {

    const act: string[] = [
      'ADICIONADO!',
      'EXCLUÍDO!',
      'ATUALIZOU!',
      'EDITADO!',
      'SEJA BEM VINDO!',
      'VOLTE SEMPRE.',
      'SUCESSO!',
      'CADASTRADO!',
      'ENVIADO'
    ]
    const kindAlert: string[] = [
      'login-error',
      'error-snackBar-red'
    ]

    const config = new MatSnackBarConfig();
    config.duration = duration ?? 1 * 1000
    config.data = { centered: true, textCenter: true };
    config.panelClass = [kindAlert[kind]]
    config.horizontalPosition = 'center'
    config.verticalPosition = 'top'
    if (Message?.length > 3)
      this._snackBar.open(Message, btnAction, config)
    else
      this._snackBar.open(act[parseInt(Message)], btnAction, config)

    if (btnAction)
      this._snackActions.snackActionsTrigger(btnAction)

  }
}
export class ToolTips {

  static positionOptions: TooltipPosition[] = ['below', 'above', 'left', 'right'];
  static position = new FormControl(this.positionOptions[0]);
  static positionRight = new FormControl(this.positionOptions[3]);
  static matTooltipHideDelay = "100000";
  static enableDisable = false;


}
