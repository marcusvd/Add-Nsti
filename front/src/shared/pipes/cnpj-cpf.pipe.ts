import { Pipe, PipeTransform } from "@angular/core";

@Pipe({
  name: 'cnpjCpf',
  standalone: true
})

export class CnpjCpfPipe implements PipeTransform {


  eleven(value: string) {

    const cpf = value

    const cnpjCpfFormatted: string = `${cpf}`.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/, '$1.$2.$3-$4');

    return cnpjCpfFormatted;
  }

  fourTeen(value: string) {

    const cnpj = value

    const cnpjCpfFormatted: string = `${cnpj}`.replace(/(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})/g, '$1.$2.$3/$4-$5');

    return cnpjCpfFormatted;
  }


  transform(value: string) {

    const cnpjCpf = value

    if (value?.length == 11)
      return this.eleven(cnpjCpf);


    if (value?.length == 14)
      return this.fourTeen(cnpjCpf);

    return value;
  }

}
