import { BreakpointObserver } from "@angular/cdk/layout";
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { BaseForm } from '../../../../shared/inheritance/forms/base-form';
import { AddressDto, ViaCepDto } from "../dtos/address-dto";


@Injectable()
export class AddressService extends BaseForm {

  constructor(
    private _fb: FormBuilder,
    private _http: HttpClient,

  ) { super() }

  formLoad(addr?: AddressDto): FormGroup {
    return this.formMain = this._fb.group({
      id: [addr?.id ?? 0, [Validators.required]],
      zipcode: [addr?.zipCode ?? '', [Validators.minLength(8), Validators.maxLength(10)]],
      street: [addr?.street ?? '', [Validators.required, Validators.maxLength(150)]],
      number: [addr?.number ?? '', [Validators.required, Validators.maxLength(15)]],
      district: [addr?.district ?? '', [Validators.required, Validators.maxLength(150)]],
      city: [addr?.city ?? '', [Validators.required, Validators.maxLength(150)]],
      state: [addr?.state ?? '', [Validators.required, Validators.maxLength(3)]],
      complement: [addr?.complement ?? '', [Validators.maxLength(500)]]
    });
  }

  objAddressNoRegister() {
    return {
      id:0,
      zipcode: "00000000",
      street: "Não Cadastrado",
      number: "Não Cadastrado",
      district: "Não Cadastrado",
      city: "Não Cadastrado",
      state: "NO",
      complement: "Não Cadastrado"
    }
  }


  validationCep!: RegExp;
  query(cep: string) {
    // var validationCep = '0000';
    if (cep) {
      cep = cep.replace('.', '')
      cep = cep.replace('-', '')
      const cepClean = cep;
      var Url = (`//viacep.com.br/ws/${cepClean}/json`);
      //remove all characters that no be a number.
      cep = cep.replace(/\D/g, '');
      //check if the field are empty
      if (cep != "") {
        //check if number is valid
        this.validationCep = /^[0-9]{8}$/;
      }
      if (this.validationCep.test(cep)) {
        this._http.get(Url).subscribe((paramCep: any) => {
          this.seedForm(paramCep)
        });
      }
    }
  }

  seedForm(cepParam: ViaCepDto) {
    this.formMain.controls['street'].setValue(cepParam.logradouro);
    this.formMain.controls['district'].setValue(cepParam.bairro);
    this.formMain.controls['city'].setValue(cepParam.localidade);
    this.formMain.controls['state'].setValue(cepParam.uf);
  }


}
