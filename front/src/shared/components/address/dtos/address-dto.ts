export class AddressDto {
  id: number = 0;
  zipCode: string = '';
  street: string = '';
  number: string = '';
  district: string = '';
  city: string = '';
  state: string = '';
  complement: string = '';
}
export class ViaCepDto {
  id!: number;

  bairro!: string;

  cep!: string;

  complemento!: string;

  ddd!: string;

  gia!: string;

  ibge!: string;

  localidade!: string;

  logradouro!: string;

  siafi!: string;

  uf!: string;



}
