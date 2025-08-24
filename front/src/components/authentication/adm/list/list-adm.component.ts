
import { Component, inject, OnInit } from '@angular/core';


import { environment } from 'environments/environment';
// import { AdmService } from '../services/adm.service';

// import { IsUserRegistereValidator } from '../../authentication/is-user-registered-validator';
import { MatTabsModule } from '@angular/material/tabs';
import { ActivatedRoute, RouterModule, RouterOutlet } from '@angular/router';
import { CompanyAddComponent } from 'components/company/components/add/add-company.component';
import { AdmService } from '../../services/adm.service';
import { ListControlAdm } from '../helpers/list-control-adm';
import { ImportsListAdm } from '../imports/imports-list-adm';


@Component({
  selector: 'list-adm',
  templateUrl: './list-adm.component.html',
  styleUrls: ['./list-adm.component.scss'],
  standalone: true,
  imports: [
    ImportsListAdm,
    RouterOutlet,
    RouterModule,
    MatTabsModule,
    CompanyAddComponent
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
