import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { FormGroup } from "@angular/forms";
import { Router } from "@angular/router";


import { BackEndService } from "../../../shared/services/back-end/backend.service";
import { CommunicationAlerts } from "../../../shared/services/messages/snack-bar.service";
import { CustomerDto } from "../components/commons-components/dtos/customer-dto";
import { MatSnackBar } from "@angular/material/snack-bar";
import { CustomerListService } from "./customer-list.service";


@Injectable({ providedIn: 'root' })
export class CustomerAddService extends BackEndService<CustomerDto> {


  // private _valueDate: boolean;

  constructor(
    override _http: HttpClient,
    private _route: Router,
    private _customerServices: CustomerListService,
    private _communicationsAlerts: CommunicationAlerts,
    private snackBar: MatSnackBar
  ) {
    super(
      _http, 'environment._CUSTOMERS'

    );
  }



  //companyId: number = JSON.parse(localStorage.getItem('companyId'));
  openSnackBar(message: string, style: string, action: string = 'Fechar') {
    this.snackBar.open(message, action, {
      duration: 5000, // Tempo em milissegundos (5 segundos)
      panelClass: [style], // Aplica a classe personalizada
      horizontalPosition: 'center', // Centraliza horizontalmente
      verticalPosition: 'top', // Posição vertical (pode ser 'top' ou 'bottom')
    });
  }


  save(form: FormGroup) {
    if (form.get('entityType')?.value)
      form.get('entityType')?.setValue(0);
    else
      form.get('entityType')?.setValue(1);

    const toSave: CustomerDto = { ...form.value }
    this._customerServices.getCustomersMoc().push(toSave);
    this.openSnackBar('Cadastrado, com sucesso., ' + toSave.name + '!', 'login-success');
    this._route.navigateByUrl(`/customers/list`);
    //  this.openSnackBar('Nome de usuário ou senha inválidos', 'login-error');


  }


  // save(form: FormGroup) {
  //   if (form.get('entityType')?.value)
  //     form.get('entityType')?.setValue(0);
  //   else
  //     form.get('entityType')?.setValue(1);

  //   const toSave: CustomerDto = { ...form.value }

  //   this.add$<CustomerDto>(toSave, 'AddCustomer').subscribe({
  //     next: (_cli: CustomerDto) => {
  //       //  this._communicationsAlerts.defaultSnackMsg('0', 0);

  //       this._route.navigateByUrl(`/side-nav/list/${this.companyId ?? 0}`)
  //     },
  //     error: (err) => {
  //       const erroCode: string = err.error.Message
  //       // this._communicationsAlerts.defaultSnackMsg(erroCode, 1);

  //     }
  //   })



  // }




}
