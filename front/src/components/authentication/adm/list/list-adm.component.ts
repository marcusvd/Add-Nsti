
import { Component, inject, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';


import { environment } from 'environments/environment';
import { BaseForm } from 'shared/inheritance/forms/base-form';
// import { AdmService } from '../services/adm.service';

import { PasswordConfirmationValidator } from '../../validators/password-confirmation-validator';
import { PasswordValidator } from '../../validators/password-validator';
// import { IsUserRegistereValidator } from '../../authentication/is-user-registered-validator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { WarningsService } from 'components/warnings/services/warnings.service';
import { AdmService } from '../../services/adm.service';
import { ImportsListAdm } from '../imports/imports-list-adm';
import { ListControlAdm } from '../helpers/list-control-adm';


@Component({
  selector: 'list-adm',
  templateUrl: './list-adm.component.html',
  styleUrls: ['./list-adm.component.scss'],
  standalone: true,
  imports: [
    ImportsListAdm
  ],
  providers: [
    AdmService
  ]
})
export class ListAdmComponent extends ListControlAdm implements OnInit {

  constructor(
    // private _admService: AdmService,
    // private _fb: FormBuilder,
    // // private _isUserAdmedValidator: IsUserRegistereValidator,
    // private _router: Router,
    // private _warningsService: WarningsService,
    // private _snackBar: MatSnackBar
  ) { super() }


  private readonly _actRouter = inject(ActivatedRoute);


  loginErrorMessage: string = '';



  // openSnackBar(message: string, style: string, action: string = 'Fechar', duration: number = 5000, horizontalPosition: any = 'center', verticalPosition: any = 'top') {
  //   this._snackBar?.open(message, action, {
  //     duration: duration, // Tempo em milissegundos (5 segundos)
  //     panelClass: [style], // Aplica a classe personalizada
  //     horizontalPosition: horizontalPosition, // Centraliza horizontalmente
  //     verticalPosition: verticalPosition, // Posição vertical (pode ser 'top' ou 'bottom')
  //   });
  // }
  back() {
    window.history.back();
  }

  backend = `${environment._BACK_END_ROOT_URL}/authadm/GetBusinessFullAsync`

  ngOnInit(): void {
    
    const id = this._actRouter.snapshot.params['id'] as number;

    this.startSupply(this.backend, id)
  }

}
