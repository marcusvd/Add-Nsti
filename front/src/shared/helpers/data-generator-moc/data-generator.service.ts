import { Injectable } from '@angular/core';
import { EmployeeDto } from "../../../components/employees/components/commons-components/dtos/employees-dto";
import { AddressDto } from "../../../shared/components/address/dtos/address-dto";
import { ContactDto } from "../../../shared/components/contact/dtos/contact-dto";
import { SocialMediasDto } from "../../../shared/components/contact/dtos/social-medias-dto";

@Injectable({
  providedIn: 'root'
})
export class DataGeneratorService {

  constructor() { }

  private minDate = new Date('0001-01-01T00:00:00');

  private socialMediaPlatforms = [
    "Facebook", "Twitter", "Instagram", "LinkedIn", "YouTube",
    "TikTok", "Pinterest", "Reddit", "WhatsApp", "Telegram"
  ];

  private generateSocialMedias(count: number): SocialMediasDto[] {
    const result: SocialMediasDto[] = [];
    const shuffledPlatforms = [...this.socialMediaPlatforms].sort(() => 0.5 - Math.random());

    for (let i = 0; i < Math.min(count, shuffledPlatforms.length); i++) {
      result.push({
        id: i + 1,
        name: shuffledPlatforms[i],
        url: `https://${shuffledPlatforms[i].toLowerCase()}.com/${shuffledPlatforms[i]}${i}`
      });
    }
    return result;
  }

  private generatePhoneNumber(): string {
    const digits = Math.floor(100000000 + Math.random() * 900000000).toString();
    return `(${digits.substring(0, 2)}) ${digits.substring(2, 7)}-${digits.substring(7)}`;
  }

  private brazilianStates = [
    "AC", "AL", "AP", "AM", "BA", "CE", "DF", "ES", "GO", "MA",
    "MT", "MS", "MG", "PA", "PB", "PR", "PE", "PI", "RJ", "RN",
    "RS", "RO", "RR", "SC", "SP", "SE", "TO"
  ];

  private citiesByState: Record<string, string[]> = {
    "SP": ["São Paulo", "Campinas", "São Bernardo do Campo", "Santo André", "Osasco"],
    "RJ": ["Rio de Janeiro", "Niterói", "Nova Iguaçu", "Duque de Caxias", "São Gonçalo"],
    "MG": ["Belo Horizonte", "Uberlândia", "Contagem", "Juiz de Fora", "Betim"],
    "RS": ["Porto Alegre", "Caxias do Sul", "Pelotas", "Canoas", "Santa Maria"],
    "PR": ["Curitiba", "Londrina", "Maringá", "Ponta Grossa", "Cascavel"],
    // Adicione mais estados e cidades conforme necessário
  };

  private generateAddress(): AddressDto {
    const state = this.brazilianStates[Math.floor(Math.random() * this.brazilianStates.length)];
    const cities = this.citiesByState[state] || [`Cidade ${Math.floor(Math.random() * 100)}`];
    const city = cities[Math.floor(Math.random() * cities.length)];

    return {
      id: Math.floor(Math.random() * 1000),
      zipCode: Math.floor(10000000 + Math.random() * 90000000).toString(),
      street: ["Rua", "Avenida", "Travessa", "Alameda", "Praça"][Math.floor(Math.random() * 5)] +
        " " +
        ["Brasil", "Paulista", "das Flores", "Rio Branco", "São João", "9 de Julho"][Math.floor(Math.random() * 6)],
      number: Math.floor(1 + Math.random() * 2000).toString(),
      district: ["Centro", "Vila Nova", "Jardim", "Bela Vista", "Liberdade"][Math.floor(Math.random() * 5)],
      city: city,
      state: state,
      complement: ["Apto 101", "Casa 2", "Bloco B", "Sala 3", ""][Math.floor(Math.random() * 5)]
    };
  }

  private generateContact(): ContactDto {
    const firstName = ["Ana", "Carlos", "João", "Maria", "Pedro", "Luiz", "Fernanda", "Ricardo", "Patricia", "Marcos"][Math.floor(Math.random() * 10)];
    const lastName = ["Silva", "Santos", "Oliveira", "Souza", "Pereira", "Costa", "Ferreira", "Rodrigues", "Almeida", "Lima"][Math.floor(Math.random() * 10)];

    return {
      id: Math.floor(Math.random() * 1000),
      email: `${firstName.toLowerCase()}.${lastName.toLowerCase()}@${["gmail", "hotmail", "outlook", "yahoo"][Math.floor(Math.random() * 4)]}.com`,
      site: `https://www.${lastName.toLowerCase()}${Math.floor(Math.random() * 100)}.com.br`,
      cel: this.generatePhoneNumber(),
      zap: this.generatePhoneNumber(),
      landline: this.generatePhoneNumber(),
      socialMedias: this.generateSocialMedias(Math.floor(1 + Math.random() * 3))
    };
  }

  private generateEmployee(id: number): EmployeeDto {
    const firstName = ["Ana", "Carlos", "João", "Maria", "Pedro", "Luiz", "Fernanda", "Ricardo", "Patricia", "Marcos"][Math.floor(Math.random() * 10)];
    const middleName = ["da", "de", "dos", "e", ""][Math.floor(Math.random() * 5)];
    const lastName = ["Silva", "Santos", "Oliveira", "Souza", "Pereira", "Costa", "Ferreira", "Rodrigues", "Almeida", "Lima"][Math.floor(Math.random() * 10)];

    const fullName = `${firstName} ${middleName ? middleName + ' ' : ''}${lastName}`;
    const abbreviatedName = `${firstName.substring(0, 1)}${lastName.substring(0, 1)}`;

    return {
      id: id,
      name: fullName,
      userId: 1,
      companyId: 1,
      abbreviatedName: abbreviatedName,
      description: `Colaborador desde ${Math.floor(2010 + Math.random() * 14)}. ${["Excelente profissional", "Dedicado", "Comprometido", "Proativo", "Criativo"][Math.floor(Math.random() * 5)]}.`,
      address: this.generateAddress(),
      contact: this.generateContact(),
      registered: new Date(Date.now() - Math.floor(Math.random() * 10000000000)),
      deleted: this.minDate
    };
  }

   fakeEmployees: EmployeeDto[] = Array.from({ length: 30 }, (_, i) => this.generateEmployee(i + 1));



}
