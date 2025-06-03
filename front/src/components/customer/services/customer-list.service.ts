import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";

// import { environment } from "..//environments/environment";
import { BackEndService } from "../../../shared/services/back-end/backend.service";
import { CommunicationAlerts } from "../../../shared/services/messages/snack-bar.service";
import { CustomerDto } from "../components/commons-components/dtos/customer-dto";




@Injectable()
export class CustomerListService extends BackEndService<CustomerDto> {

  constructor(
    override _http: HttpClient,
    private _communicationsAlerts: CommunicationAlerts,
  ) {
    super(_http,
      'environment._BACK_END_ROOT_URL',
    );

  }


  deleteFakeDisable(id: number) {
    if (id == 0) throw new Error('Id não pode ser 0');

    const customer = new CustomerDto();
    customer.id = id;

    this.deleteFake$<CustomerDto>('customers/DeleteFakeCustomer', customer).subscribe(
      {
        next: () => {
          this._communicationsAlerts.defaultSnackMsg('1', 0, '-1', 4);
        },
        error: (error) => {
          this._communicationsAlerts.defaultSnackMsg(error, 1);
          return false;
        }

      }
    );



  }



  getCustomersMoc(){
    const customers:CustomerDto[] = JSON.parse(this.jsonMoc);

    return customers;
  }


  jsonMoc:string = `
    [{
            "id": 1,
            "companyId": 1,
            "name": "João Silva",
            "responsible": "João Silva",
            "cnpj": "123.456.789-00",
            "entityType": 1,
            "registered": "2023-01-15T00:00:00Z",
            "description": "Cliente pessoa física - consultor",
            "businessLine": "Consultoria",
            "assured": true,
            "payment": 1500.00,
            "expiration": 30,
            "disabled": false,
            "discount": 0.0,
            "company": {
                "id": 101,
                "name": "Consultoria JS",
                "address": {
                    "id": 1001,
                    "zipCode": "01001-000",
                    "street": "Rua das Flores",
                    "number": "100",
                    "district": "Centro",
                    "city": "São Paulo",
                    "state": "SP",
                    "complement": "Apto 101"
                },
                "contact": {
                    "id": 1001,
                    "email": "joao@consultoriajs.com",
                    "site": "www.consultoriajs.com",
                    "cel": "11999999999",
                    "zap": "11999999999",
                    "landline": "1133333333",
                    "socialMedias": [{
                        "id": 10001,
                        "name": "LinkedIn",
                        "url": "linkedin.com/in/joaosilva"
                    }]
                }
            },
            "address": {
                "id": 2001,
                "zipCode": "01001-000",
                "street": "Rua das Flores",
                "number": "100",
                "district": "Centro",
                "city": "São Paulo",
                "state": "SP",
                "complement": "Apto 101"
            },
            "contact": {
                "id": 2001,
                "email": "joao.silva@gmail.com",
                "site": "",
                "cel": "11988888888",
                "zap": "11988888888",
                "landline": "",
                "socialMedias": []
            }
        },
        {
            "id": 2,
            "companyId": 1,
            "name": "Tech Solutions Ltda",
            "responsible": "Maria Oliveira",
            "cnpj": "12.345.678/0001-99",
            "entityType": 0,
            "registered": "2022-05-20T00:00:00Z",
            "description": "Empresa de tecnologia",
            "businessLine": "Tecnologia",
            "assured": false,
            "payment": 5000.00,
            "expiration": 60,
            "disabled": false,
            "discount": 10.0,
            "company": {
                "id": 102,
                "name": "Tech Solutions Ltda",
                "address": {
                    "id": 1002,
                    "zipCode": "04571-010",
                    "street": "Av. Paulista",
                    "number": "2000",
                    "district": "Bela Vista",
                    "city": "São Paulo",
                    "state": "SP",
                    "complement": "10º andar"
                },
                "contact": {
                    "id": 1002,
                    "email": "contato@techsolutions.com",
                    "site": "www.techsolutions.com",
                    "cel": "11977777777",
                    "zap": "11977777777",
                    "landline": "1144444444",
                    "socialMedias": [{
                            "id": 10002,
                            "name": "Instagram",
                            "url": "instagram.com/techsolutions"
                        },
                        {
                            "id": 10003,
                            "name": "Twitter",
                            "url": "twitter.com/techsol"
                        }
                    ]
                }
            },
            "address": {
                "id": 2002,
                "zipCode": "04571-010",
                "street": "Av. Paulista",
                "number": "2000",
                "district": "Bela Vista",
                "city": "São Paulo",
                "state": "SP",
                "complement": "10º andar"
            },
            "contact": {
                "id": 2002,
                "email": "maria.oliveira@techsolutions.com",
                "site": "",
                "cel": "11966666666",
                "zap": "11966666666",
                "landline": "",
                "socialMedias": []
            }
        },
        {
            "id": 3,
            "companyId": 1,
            "name": "Renato Carvalho",
            "responsible": "Renato Carvalho",
            "cnpj": "407.798.659-13",
            "entityType": 1,
            "registered": "2023-06-09T00:00:00Z",
            "description": "Pessoa física - Saúde",
            "businessLine": "Comércio",
            "assured": false,
            "payment": 1582.63,
            "expiration": 30,
            "disabled": false,
            "discount": 0.0,
            "company": {
                "id": 103,
                "name": "Empresa RC",
                "address": {
                    "id": 1003,
                    "zipCode": "64391-351",
                    "street": "Av. Central",
                    "number": "1546",
                    "district": "Savassi",
                    "city": "Belo Horizonte",
                    "state": "BA",
                    "complement": "Andar 7"
                },
                "contact": {
                    "id": 1003,
                    "email": "contato@empresa3.com",
                    "site": "www.empresa3.com",
                    "cel": "77998695662",
                    "zap": "77998695662",
                    "landline": "7727119901",
                    "socialMedias": []
                }
            },
            "address": {
                "id": 2003,
                "zipCode": "50983-161",
                "street": "Av. Central",
                "number": "2171",
                "district": "Centro",
                "city": "Belo Horizonte",
                "state": "RS",
                "complement": "Apto 123"
            },
            "contact": {
                "id": 2003,
                "email": "renato.carvalho@example.com",
                "site": "",
                "cel": "77998695662",
                "zap": "77998695662",
                "landline": "",
                "socialMedias": []
            }
        },
        {
            "id": 4,
            "companyId": 1,
            "name": "Empresa LF",
            "responsible": "Luciana Ferreira",
            "cnpj": "34.706.110/5539-38",
            "entityType": 0,
            "registered": "2022-06-08T00:00:00Z",
            "description": "Empresa - Marketing",
            "businessLine": "Construção",
            "assured": false,
            "payment": 1784.53,
            "expiration": 30,
            "disabled": false,
            "discount": 0.0,
            "company": {
                "id": 104,
                "name": "Empresa LF",
                "address": {
                    "id": 1004,
                    "zipCode": "82794-598",
                    "street": "Rua A",
                    "number": "4100",
                    "district": "Centro",
                    "city": "Belo Horizonte",
                    "state": "RJ",
                    "complement": "Andar 10"
                },
                "contact": {
                    "id": 1004,
                    "email": "contato@empresa4.com",
                    "site": "www.empresa4.com",
                    "cel": "87999332920",
                    "zap": "87999332920",
                    "landline": "8752071213",
                    "socialMedias": []
                }
            },
            "address": {
                "id": 2004,
                "zipCode": "45114-939",
                "street": "Rua A",
                "number": "4596",
                "district": "Moema",
                "city": "Porto Alegre",
                "state": "RJ",
                "complement": "Apto 48"
            },
            "contact": {
                "id": 2004,
                "email": "luciana.ferreira@example.com",
                "site": "",
                "cel": "87999332920",
                "zap": "87999332920",
                "landline": "",
                "socialMedias": []
            }
        },
        {
            "id": 5,
            "companyId": 1,
            "name": "Juliana Gomes",
            "responsible": "Juliana Gomes",
            "cnpj": "742.978.753-92",
            "entityType": 1,
            "registered": "2023-10-12T00:00:00Z",
            "description": "Pessoa física - Financeiro",
            "businessLine": "Transportes",
            "assured": true,
            "payment": 7479.87,
            "expiration": 15,
            "disabled": false,
            "discount": 5.0,
            "company": {
                "id": 105,
                "name": "Empresa JG",
                "address": {
                    "id": 1005,
                    "zipCode": "40760-321",
                    "street": "Av. Central",
                    "number": "2576",
                    "district": "Ipanema",
                    "city": "Rio de Janeiro",
                    "state": "BA",
                    "complement": "Andar 3"
                },
                "contact": {
                    "id": 1005,
                    "email": "contato@empresa5.com",
                    "site": "www.empresa5.com",
                    "cel": "91931006882",
                    "zap": "91931006882",
                    "landline": "9151502350",
                    "socialMedias": []
                }
            },
            "address": {
                "id": 2005,
                "zipCode": "95631-875",
                "street": "Av. Central",
                "number": "3457",
                "district": "Savassi",
                "city": "Porto Alegre",
                "state": "MG",
                "complement": "Apto 163"
            },
            "contact": {
                "id": 2005,
                "email": "juliana.gomes@example.com",
                "site": "",
                "cel": "91931006882",
                "zap": "91931006882",
                "landline": "",
                "socialMedias": []
            }
        },
        {
            "id": 6,
            "companyId": 1,
            "name": "Logística Expressa Ltda",
            "responsible": "Carlos Mendes",
            "cnpj": "23.456.789/0001-01",
            "entityType": 0,
            "registered": "2022-11-18T00:00:00Z",
            "description": "Empresa de logística",
            "businessLine": "Logística",
            "assured": true,
            "payment": 8500.00,
            "expiration": 45,
            "disabled": false,
            "discount": 7.5,
            "company": {
                "id": 106,
                "name": "Logística Expressa Ltda",
                "address": {
                    "id": 1006,
                    "zipCode": "20040-010",
                    "street": "Rua do Mercado",
                    "number": "150",
                    "district": "Centro",
                    "city": "Rio de Janeiro",
                    "state": "RJ",
                    "complement": "Sala 501"
                },
                "contact": {
                    "id": 1006,
                    "email": "contato@logexpress.com",
                    "site": "www.logexpress.com",
                    "cel": "21987654321",
                    "zap": "21987654321",
                    "landline": "2122223333",
                    "socialMedias": [{
                        "id": 10006,
                        "name": "Facebook",
                        "url": "facebook.com/logexpress"
                    }]
                }
            },
            "address": {
                "id": 2006,
                "zipCode": "20040-010",
                "street": "Rua do Mercado",
                "number": "150",
                "district": "Centro",
                "city": "Rio de Janeiro",
                "state": "RJ",
                "complement": "Sala 501"
            },
            "contact": {
                "id": 2006,
                "email": "carlos.mendes@logexpress.com",
                "site": "",
                "cel": "21976543210",
                "zap": "21976543210",
                "landline": "",
                "socialMedias": []
            }
        },
        {
            "id": 7,
            "companyId": 1,
            "name": "Fernanda Alves",
            "responsible": "Fernanda Alves",
            "cnpj": "852.963.741-25",
            "entityType": 1,
            "registered": "2023-03-22T00:00:00Z",
            "description": "Arquiteta independente",
            "businessLine": "Arquitetura",
            "assured": false,
            "payment": 3200.00,
            "expiration": 30,
            "disabled": false,
            "discount": 0.0,
            "company": {
                "id": 107,
                "name": "Studio FA",
                "address": {
                    "id": 1007,
                    "zipCode": "30140-070",
                    "street": "Rua da Bahia",
                    "number": "1200",
                    "district": "Lourdes",
                    "city": "Belo Horizonte",
                    "state": "MG",
                    "complement": "Sala 302"
                },
                "contact": {
                    "id": 1007,
                    "email": "contato@studiofa.com",
                    "site": "www.studiofa.com",
                    "cel": "31985471236",
                    "zap": "31985471236",
                    "landline": "3132324545",
                    "socialMedias": [{
                        "id": 10007,
                        "name": "Instagram",
                        "url": "instagram.com/studiofa"
                    }]
                }
            },
            "address": {
                "id": 2007,
                "zipCode": "30140-070",
                "street": "Rua da Bahia",
                "number": "1200",
                "district": "Lourdes",
                "city": "Belo Horizonte",
                "state": "MG",
                "complement": "Sala 302"
            },
            "contact": {
                "id": 2007,
                "email": "fernanda.alves@studiofa.com",
                "site": "",
                "cel": "31974582365",
                "zap": "31974582365",
                "landline": "",
                "socialMedias": []
            }
        },
        {
            "id": 8,
            "companyId": 1,
            "name": "Digital Marketing S/A",
            "responsible": "Roberto Santos",
            "cnpj": "34.567.890/0001-02",
            "entityType": 0,
            "registered": "2022-09-15T00:00:00Z",
            "description": "Agência de marketing digital",
            "businessLine": "Marketing",
            "assured": true,
            "payment": 12500.00,
            "expiration": 60,
            "disabled": false,
            "discount": 15.0,
            "company": {
                "id": 108,
                "name": "Digital Marketing S/A",
                "address": {
                    "id": 1008,
                    "zipCode": "01451-001",
                    "street": "Alameda Santos",
                    "number": "2100",
                    "district": "Jardins",
                    "city": "São Paulo",
                    "state": "SP",
                    "complement": "15º andar"
                },
                "contact": {
                    "id": 1008,
                    "email": "contato@digimarketing.com",
                    "site": "www.digimarketing.com",
                    "cel": "11987654321",
                    "zap": "11987654321",
                    "landline": "1134567890",
                    "socialMedias": [{
                            "id": 10008,
                            "name": "LinkedIn",
                            "url": "linkedin.com/company/digimarketing"
                        },
                        {
                            "id": 10009,
                            "name": "Twitter",
                            "url": "twitter.com/digimarketing"
                        }
                    ]
                }
            },
            "address": {
                "id": 2008,
                "zipCode": "01451-001",
                "street": "Alameda Santos",
                "number": "2100",
                "district": "Jardins",
                "city": "São Paulo",
                "state": "SP",
                "complement": "15º andar"
            },
            "contact": {
                "id": 2008,
                "email": "roberto.santos@digimarketing.com",
                "site": "",
                "cel": "11976543210",
                "zap": "11976543210",
                "landline": "",
                "socialMedias": []
            }
        },
        {
            "id": 9,
            "companyId": 1,
            "name": "Gustavo Lima",
            "responsible": "Gustavo Lima",
            "cnpj": "753.951.486-32",
            "entityType": 1,
            "registered": "2023-07-05T00:00:00Z",
            "description": "Consultor financeiro",
            "businessLine": "Consultoria",
            "assured": true,
            "payment": 4500.00,
            "expiration": 30,
            "disabled": false,
            "discount": 5.0,
            "company": {
                "id": 109,
                "name": "GL Consultoria",
                "address": {
                    "id": 1009,
                    "zipCode": "90470-150",
                    "street": "Rua Padre Chagas",
                    "number": "85",
                    "district": "Moinhos de Vento",
                    "city": "Porto Alegre",
                    "state": "RS",
                    "complement": "Sala 201"
                },
                "contact": {
                    "id": 1009,
                    "email": "contato@glconsultoria.com",
                    "site": "www.glconsultoria.com",
                    "cel": "51987654321",
                    "zap": "51987654321",
                    "landline": "5133445566",
                    "socialMedias": []
                }
            },
            "address": {
                "id": 2009,
                "zipCode": "90470-150",
                "street": "Rua Padre Chagas",
                "number": "85",
                "district": "Moinhos de Vento",
                "city": "Porto Alegre",
                "state": "RS",
                "complement": "Sala 201"
            },
            "contact": {
                "id": 2009,
                "email": "gustavo.lima@glconsultoria.com",
                "site": "",
                "cel": "51976543210",
                "zap": "51976543210",
                "landline": "",
                "socialMedias": []
            }
        },
        {
            "id": 10,
            "companyId": 1,
            "name": "Construções Moderna Ltda",
            "responsible": "Patrícia Oliveira",
            "cnpj": "45.678.901/0001-03",
            "entityType": 0,
            "registered": "2022-04-10T00:00:00Z",
            "description": "Construtora civil",
            "businessLine": "Construção",
            "assured": false,
            "payment": 18500.00,
            "expiration": 90,
            "disabled": false,
            "discount": 20.0,
            "company": {
                "id": 110,
                "name": "Construções Moderna Ltda",
                "address": {
                    "id": 1010,
                    "zipCode": "70070-300",
                    "street": "SHS Quadra 2",
                    "number": "100",
                    "district": "Asa Sul",
                    "city": "Brasília",
                    "state": "DF",
                    "complement": "Bloco C"
                },
                "contact": {
                    "id": 1010,
                    "email": "contato@constmoderna.com",
                    "site": "www.constmoderna.com",
                    "cel": "61987654321",
                    "zap": "61987654321",
                    "landline": "6134567890",
                    "socialMedias": [{
                        "id": 10010,
                        "name": "Instagram",
                        "url": "instagram.com/constmoderna"
                    }]
                }
            },
            "address": {
                "id": 2010,
                "zipCode": "70070-300",
                "street": "SHS Quadra 2",
                "number": "100",
                "district": "Asa Sul",
                "city": "Brasília",
                "state": "DF",
                "complement": "Bloco C"
            },
            "contact": {
                "id": 2010,
                "email": "patricia.oliveira@constmoderna.com",
                "site": "",
                "cel": "61976543210",
                "zap": "61976543210",
                "landline": "",
                "socialMedias": []
            }
        },
        {
            "id": 11,
            "companyId": 1,
            "name": "Rafaela Costa",
            "responsible": "Rafaela Costa",
            "cnpj": "159.753.486-20",
            "entityType": 1,
            "registered": "2023-02-28T00:00:00Z",
            "description": "Nutricionista clínica",
            "businessLine": "Saúde",
            "assured": true,
            "payment": 2800.00,
            "expiration": 30,
            "disabled": false,
            "discount": 0.0,
            "company": {
                "id": 111,
                "name": "Nutri Saúde",
                "address": {
                    "id": 1011,
                    "zipCode": "30150-050",
                    "street": "Rua da Consolação",
                    "number": "350",
                    "district": "Funcionários",
                    "city": "Belo Horizonte",
                    "state": "MG",
                    "complement": "Sala 105"
                },
                "contact": {
                    "id": 1011,
                    "email": "contato@nutrisaude.com",
                    "site": "www.nutrisaude.com",
                    "cel": "31987654321",
                    "zap": "31987654321",
                    "landline": "3132325656",
                    "socialMedias": []
                }
            },
            "address": {
                "id": 2011,
                "zipCode": "30150-050",
                "street": "Rua da Consolação",
                "number": "350",
                "district": "Funcionários",
                "city": "Belo Horizonte",
                "state": "MG",
                "complement": "Sala 105"
            },
            "contact": {
                "id": 2011,
                "email": "rafaela.costa@nutrisaude.com",
                "site": "",
                "cel": "31976543210",
                "zap": "31976543210",
                "landline": "",
                "socialMedias": []
            }
        },
        {
            "id": 12,
            "companyId": 1,
            "name": "SoftTech Solutions S/A",
            "responsible": "Marcos Ribeiro",
            "cnpj": "56.789.012/0001-04",
            "entityType": 0,
            "registered": "2022-08-22T00:00:00Z",
            "description": "Desenvolvimento de software",
            "businessLine": "Tecnologia",
            "assured": true,
            "payment": 25000.00,
            "expiration": 60,
            "disabled": false,
            "discount": 25.0,
            "company": {
                "id": 112,
                "name": "SoftTech Solutions S/A",
                "address": {
                    "id": 1012,
                    "zipCode": "05426-100",
                    "street": "Avenida Brigadeiro Faria Lima",
                    "number": "3900",
                    "district": "Itaim Bibi",
                    "city": "São Paulo",
                    "state": "SP",
                    "complement": "12º andar"
                },
                "contact": {
                    "id": 1012,
                    "email": "contato@softtech.com",
                    "site": "www.softtech.com",
                    "cel": "11987654321",
                    "zap": "11987654321",
                    "landline": "1145678901",
                    "socialMedias": [{
                        "id": 10012,
                        "name": "GitHub",
                        "url": "github.com/softtech"
                    }]
                }
            },
            "address": {
                "id": 2012,
                "zipCode": "05426-100",
                "street": "Avenida Brigadeiro Faria Lima",
                "number": "3900",
                "district": "Itaim Bibi",
                "city": "São Paulo",
                "state": "SP",
                "complement": "12º andar"
            },
            "contact": {
                "id": 2012,
                "email": "marcos.ribeiro@softtech.com",
                "site": "",
                "cel": "11976543210",
                "zap": "11976543210",
                "landline": "",
                "socialMedias": []
            }
        },
        {
            "id": 13,
            "companyId": 1,
            "name": "Luciano Martins",
            "responsible": "Luciano Martins",
            "cnpj": "357.951.468-20",
            "entityType": 1,
            "registered": "2023-05-17T00:00:00Z",
            "description": "Personal trainer",
            "businessLine": "Esportes",
            "assured": false,
            "payment": 1800.00,
            "expiration": 30,
            "disabled": false,
            "discount": 0.0,
            "company": {
                "id": 113,
                "name": "LM Fitness",
                "address": {
                    "id": 1013,
                    "zipCode": "22071-010",
                    "street": "Rua Nascimento Silva",
                    "number": "107",
                    "district": "Ipanema",
                    "city": "Rio de Janeiro",
                    "state": "RJ",
                    "complement": ""
                },
                "contact": {
                    "id": 1013,
                    "email": "contato@lmfitness.com",
                    "site": "www.lmfitness.com",
                    "cel": "21987654321",
                    "zap": "21987654321",
                    "landline": "2125253636",
                    "socialMedias": [{
                        "id": 10013,
                        "name": "Instagram",
                        "url": "instagram.com/lmfitness"
                    }]
                }
            },
            "address": {
                "id": 2013,
                "zipCode": "22071-010",
                "street": "Rua Nascimento Silva",
                "number": "107",
                "district": "Ipanema",
                "city": "Rio de Janeiro",
                "state": "RJ",
                "complement": ""
            },
            "contact": {
                "id": 2013,
                "email": "luciano.martins@lmfitness.com",
                "site": "",
                "cel": "21976543210",
                "zap": "21976543210",
                "landline": "",
                "socialMedias": []
            }
        },
        {
            "id": 14,
            "companyId": 1,
            "name": "Educar Editora Ltda",
            "responsible": "Ana Beatriz Souza",
            "cnpj": "67.890.123/0001-05",
            "entityType": 0,
            "registered": "2022-03-14T00:00:00Z",
            "description": "Editora de materiais educacionais",
            "businessLine": "Educação",
            "assured": true,
            "payment": 9800.00,
            "expiration": 60,
            "disabled": false,
            "discount": 12.0,
            "company": {
                "id": 114,
                "name": "Educar Editora Ltda",
                "address": {
                    "id": 1014,
                    "zipCode": "01310-100",
                    "street": "Avenida Paulista",
                    "number": "900",
                    "district": "Bela Vista",
                    "city": "São Paulo",
                    "state": "SP",
                    "complement": "8º andar"
                },
                "contact": {
                    "id": 1014,
                    "email": "contato@educareditora.com",
                    "site": "www.educareditora.com",
                    "cel": "11987654321",
                    "zap": "11987654321",
                    "landline": "1156789012",
                    "socialMedias": []
                }
            },
            "address": {
                "id": 2014,
                "zipCode": "01310-100",
                "street": "Avenida Paulista",
                "number": "900",
                "district": "Bela Vista",
                "city": "São Paulo",
                "state": "SP",
                "complement": "8º andar"
            },
            "contact": {
                "id": 2014,
                "email": "ana.souza@educareditora.com",
                "site": "",
                "cel": "11976543210",
                "zap": "11976543210",
                "landline": "",
                "socialMedias": []
            }
        },
        {
            "id": 15,
            "companyId": 1,
            "name": "Pedro Henrique Almeida",
            "responsible": "Pedro Henrique Almeida",
            "cnpj": "753.159.486-20",
            "entityType": 1,
            "registered": "2023-01-30T00:00:00Z",
            "description": "Fotógrafo profissional",
            "businessLine": "Fotografia",
            "assured": true,
            "payment": 3500.00,
            "expiration": 30,
            "disabled": false,
            "discount": 5.0,
            "company": {
                "id": 115,
                "name": "PH Fotografia",
                "address": {
                    "id": 1015,
                    "zipCode": "22451-000",
                    "street": "Rua Visconde de Pirajá",
                    "number": "550",
                    "district": "Ipanema",
                    "city": "Rio de Janeiro",
                    "state": "RJ",
                    "complement": "Sala 203"
                },
                "contact": {
                    "id": 1015,
                    "email": "contato@phfotografia.com",
                    "site": "www.phfotografia.com",
                    "cel": "21987654321",
                    "zap": "21987654321",
                    "landline": "2126263737",
                    "socialMedias": [{
                        "id": 10015,
                        "name": "Instagram",
                        "url": "instagram.com/phfotografia"
                    }]
                }
            },
            "address": {
                "id": 2015,
                "zipCode": "22451-000",
                "street": "Rua Visconde de Pirajá",
                "number": "550",
                "district": "Ipanema",
                "city": "Rio de Janeiro",
                "state": "RJ",
                "complement": "Sala 203"
            },
            "contact": {
                "id": 2015,
                "email": "pedro.almeida@phfotografia.com",
                "site": "",
                "cel": "21976543210",
                "zap": "21976543210",
                "landline": "",
                "socialMedias": []
            }
        },
        {
            "id": 16,
            "companyId": 1,
            "name": "LegalAdvocacia Associados",
            "responsible": "Camila Fernandes",
            "cnpj": "78.901.234/0001-06",
            "entityType": 0,
            "registered": "2022-07-19T00:00:00Z",
            "description": "Escritório de advocacia",
            "businessLine": "Direito",
            "assured": false,
            "payment": 22000.00,
            "expiration": 90,
            "disabled": false,
            "discount": 30.0,
            "company": {
                "id": 116,
                "name": "LegalAdvocacia Associados",
                "address": {
                    "id": 1016,
                    "zipCode": "70002-900",
                    "street": "SBS Quadra 2",
                    "number": "12",
                    "district": "Asa Sul",
                    "city": "Brasília",
                    "state": "DF",
                    "complement": "Bloco A"
                },
                "contact": {
                    "id": 1016,
                    "email": "contato@legaladv.com",
                    "site": "www.legaladv.com",
                    "cel": "61987654321",
                    "zap": "61987654321",
                    "landline": "6134567890",
                    "socialMedias": []
                }
            },
            "address": {
                "id": 2016,
                "zipCode": "70002-900",
                "street": "SBS Quadra 2",
                "number": "12",
                "district": "Asa Sul",
                "city": "Brasília",
                "state": "DF",
                "complement": "Bloco A"
            },
            "contact": {
                "id": 2016,
                "email": "camila.fernandes@legaladv.com",
                "site": "",
                "cel": "61976543210",
                "zap": "61976543210",
                "landline": "",
                "socialMedias": []
            }
        },
        {
            "id": 17,
            "companyId": 1,
            "name": "Mariana Santos",
            "responsible": "Mariana Santos",
            "cnpj": "258.369.147-30",
            "entityType": 1,
            "registered": "2023-04-05T00:00:00Z",
            "description": "Designer de interiores",
            "businessLine": "Design",
            "assured": true,
            "payment": 4200.00,
            "expiration": 30,
            "disabled": false,
            "discount": 0.0,
            "company": {
                "id": 117,
                "name": "MS Interiores",
                "address": {
                    "id": 1017,
                    "zipCode": "01418-100",
                    "street": "Rua Bela Cintra",
                    "number": "800",
                    "district": "Consolação",
                    "city": "São Paulo",
                    "state": "SP",
                    "complement": "Sala 405"
                },
                "contact": {
                    "id": 1017,
                    "email": "contato@msinteriores.com",
                    "site": "www.msinteriores.com",
                    "cel": "11987654321",
                    "zap": "11987654321",
                    "landline": "1145678901",
                    "socialMedias": [{
                        "id": 10017,
                        "name": "Pinterest",
                        "url": "pinterest.com/msinteriores"
                    }]
                }
            },
            "address": {
                "id": 2017,
                "zipCode": "01418-100",
                "street": "Rua Bela Cintra",
                "number": "800",
                "district": "Consolação",
                "city": "São Paulo",
                "state": "SP",
                "complement": "Sala 405"
            },
            "contact": {
                "id": 2017,
                "email": "mariana.santos@msinteriores.com",
                "site": "",
                "cel": "11976543210",
                "zap": "11976543210",
                "landline": "",
                "socialMedias": []
            }
        },
        {
            "id": 18,
            "companyId": 1,
            "name": "Turismo & Viagens Ltda",
            "responsible": "Ricardo Oliveira",
            "cnpj": "89.012.345/0001-07",
            "entityType": 0,
            "registered": "2022-10-11T00:00:00Z",
            "description": "Agência de viagens",
            "businessLine": "Turismo",
            "assured": true,
            "payment": 15000.00,
            "expiration": 60,
            "disabled": false,
            "discount": 20.0,
            "company": {
                "id": 118,
                "name": "Turismo & Viagens Ltda",
                "address": {
                    "id": 1018,
                    "zipCode": "40010-010",
                    "street": "Avenida Sete de Setembro",
                    "number": "200",
                    "district": "Centro",
                    "city": "Salvador",
                    "state": "BA",
                    "complement": "Loja 12"
                },
                "contact": {
                    "id": 1018,
                    "email": "contato@turismoeviagens.com",
                    "site": "www.turismoeviagens.com",
                    "cel": "71987654321",
                    "zap": "71987654321",
                    "landline": "7134567890",
                    "socialMedias": []
                }
            },
            "address": {
                "id": 2018,
                "zipCode": "40010-010",
                "street": "Avenida Sete de Setembro",
                "number": "200",
                "district": "Centro",
                "city": "Salvador",
                "state": "BA",
                "complement": "Loja 12"
            },
            "contact": {
                "id": 2018,
                "email": "ricardo.oliveira@turismoeviagens.com",
                "site": "",
                "cel": "71976543210",
                "zap": "71976543210",
                "landline": "",
                "socialMedias": []
            }
        },
        {
            "id": 19,
            "companyId": 1,
            "name": "Daniel Costa",
            "responsible": "Daniel Costa",
            "cnpj": "654.987.321-50",
            "entityType": 1,
            "registered": "2023-08-22T00:00:00Z",
            "description": "Engenheiro civil",
            "businessLine": "Engenharia",
            "assured": false,
            "payment": 6800.00,
            "expiration": 30,
            "disabled": false,
            "discount": 0.0,
            "company": {
                "id": 119,
                "name": "DC Engenharia",
                "address": {
                    "id": 1019,
                    "zipCode": "30130-010",
                    "street": "Rua da Bahia",
                    "number": "1500",
                    "district": "Lourdes",
                    "city": "Belo Horizonte",
                    "state": "MG",
                    "complement": "Sala 601"
                },
                "contact": {
                    "id": 1019,
                    "email": "contato@dcengenharia.com",
                    "site": "www.dcengenharia.com",
                    "cel": "31987654321",
                    "zap": "31987654321",
                    "landline": "3132325656",
                    "socialMedias": []
                }
            },
            "address": {
                "id": 2019,
                "zipCode": "30130-010",
                "street": "Rua da Bahia",
                "number": "1500",
                "district": "Lourdes",
                "city": "Belo Horizonte",
                "state": "MG",
                "complement": "Sala 601"
            },
            "contact": {
                "id": 2019,
                "email": "daniel.costa@dcengenharia.com",
                "site": "",
                "cel": "31976543210",
                "zap": "31976543210",
                "landline": "",
                "socialMedias": []
            }
        },
        {
            "id": 20,
            "companyId": 1,
            "name": "FoodDeliver Serviços S/A",
            "responsible": "Beatriz Martins",
            "cnpj": "90.123.456/0001-08",
            "entityType": 0,
            "registered": "2022-12-15T00:00:00Z",
            "description": "Plataforma de delivery de comida",
            "businessLine": "Alimentação",
            "assured": true,
            "payment": 32000.00,
            "expiration": 90,
            "disabled": false,
            "discount": 35.0,
            "company": {
                "id": 120,
                "name": "FoodDeliver Serviços S/A",
                "address": {
                    "id": 1020,
                    "zipCode": "04543-000",
                    "street": "Avenida Engenheiro Luís Carlos Berrini",
                    "number": "1200",
                    "district": "Itaim Bibi",
                    "city": "São Paulo",
                    "state": "SP",
                    "complement": "7º andar"
                },
                "contact": {
                    "id": 1020,
                    "email": "contato@fooddeliver.com",
                    "site": "www.fooddeliver.com",
                    "cel": "11987654321",
                    "zap": "11987654321",
                    "landline": "1156789012",
                    "socialMedias": [{
                        "id": 10020,
                        "name": "Instagram",
                        "url": "instagram.com/fooddeliver"
                    }]
                }
            },
            "address": {
                "id": 2020,
                "zipCode": "04543-000",
                "street": "Avenida Engenheiro Luís Carlos Berrini",
                "number": "1200",
                "district": "Itaim Bibi",
                "city": "São Paulo",
                "state": "SP",
                "complement": "7º andar"
            },
            "contact": {
                "id": 2020,
                "email": "beatriz.martins@fooddeliver.com",
                "site": "",
                "cel": "11976543210",
                "zap": "11976543210",
                "landline": "",
                "socialMedias": []
            }
        },
        {
            "id": 21,
            "companyId": 1,
            "name": "Carolina Ribeiro",
            "responsible": "Carolina Ribeiro",
            "cnpj": "951.753.864-20",
            "entityType": 1,
            "registered": "2023-03-08T00:00:00Z",
            "description": "Psicóloga clínica",
            "businessLine": "Saúde",
            "assured": true,
            "payment": 2500.00,
            "expiration": 30,
            "disabled": false,
            "discount": 0.0,
            "company": {
                "id": 121,
                "name": "CR Psicologia",
                "address": {
                    "id": 1021,
                    "zipCode": "90480-000",
                    "street": "Rua dos Andradas",
                    "number": "1000",
                    "district": "Centro",
                    "city": "Porto Alegre",
                    "state": "RS",
                    "complement": "Sala 302"
                },
                "contact": {
                    "id": 1021,
                    "email": "contato@crpsicologia.com",
                    "site": "www.crpsicologia.com",
                    "cel": "51987654321",
                    "zap": "51987654321",
                    "landline": "5133445566",
                    "socialMedias": []
                }
            },
            "address": {
                "id": 2021,
                "zipCode": "90480-000",
                "street": "Rua dos Andradas",
                "number": "1000",
                "district": "Centro",
                "city": "Porto Alegre",
                "state": "RS",
                "complement": "Sala 302"
            },
            "contact": {
                "id": 2021,
                "email": "carolina.ribeiro@crpsicologia.com",
                "site": "",
                "cel": "51976543210",
                "zap": "51976543210",
                "landline": "",
                "socialMedias": []
            }
        },
        {
            "id": 22,
            "companyId": 1,
            "name": "ModaFashion Ltda",
            "responsible": "Vanessa Lima",
            "cnpj": "01.234.567/0001-09",
            "entityType": 0,
            "registered": "2022-06-25T00:00:00Z",
            "description": "Varejo de moda feminina",
            "businessLine": "Moda",
            "assured": false,
            "payment": 18000.00,
            "expiration": 60,
            "disabled": false,
            "discount": 25.0,
            "company": {
                "id": 122,
                "name": "ModaFashion Ltda",
                "address": {
                    "id": 1022,
                    "zipCode": "01311-100",
                    "street": "Rua Augusta",
                    "number": "1500",
                    "district": "Consolação",
                    "city": "São Paulo",
                    "state": "SP",
                    "complement": "Loja 3"
                },
                "contact": {
                    "id": 1022,
                    "email": "contato@modafashion.com",
                    "site": "www.modafashion.com",
                    "cel": "11987654321",
                    "zap": "11987654321",
                    "landline": "1145678901",
                    "socialMedias": [{
                        "id": 10022,
                        "name": "Instagram",
                        "url": "instagram.com/modafashion"
                    }]
                }
            },
            "address": {
                "id": 2022,
                "zipCode": "01311-100",
                "street": "Rua Augusta",
                "number": "1500",
                "district": "Consolação",
                "city": "São Paulo",
                "state": "SP",
                "complement": "Loja 3"
            },
            "contact": {
                "id": 2022,
                "email": "vanessa.lima@modafashion.com",
                "site": "",
                "cel": "11976543210",
                "zap": "11976543210",
                "landline": "",
                "socialMedias": []
            }
        },
        {
            "id": 23,
            "companyId": 1,
            "name": "Roberto Nascimento",
            "responsible": "Roberto Nascimento",
            "cnpj": "753.258.469-10",
            "entityType": 1,
            "registered": "2023-07-14T00:00:00Z",
            "description": "Consultor de negócios",
            "businessLine": "Consultoria",
            "assured": true,
            "payment": 5500.00,
            "expiration": 30,
            "disabled": false,
            "discount": 10.0,
            "company": {
                "id": 123,
                "name": "RN Consultoria",
                "address": {
                    "id": 1023,
                    "zipCode": "20040-020",
                    "street": "Rua do Ouvidor",
                    "number": "50",
                    "district": "Centro",
                    "city": "Rio de Janeiro",
                    "state": "RJ",
                    "complement": "Sala 1001"
                },
                "contact": {
                    "id": 1023,
                    "email": "contato@rnconsultoria.com",
                    "site": "www.rnconsultoria.com",
                    "cel": "21987654321",
                    "zap": "21987654321",
                    "landline": "2125253636",
                    "socialMedias": []
                }
            },
            "address": {
                "id": 2023,
                "zipCode": "20040-020",
                "street": "Rua do Ouvidor",
                "number": "50",
                "district": "Centro",
                "city": "Rio de Janeiro",
                "state": "RJ",
                "complement": "Sala 1001"
            },
            "contact": {
                "id": 2023,
                "email": "roberto.nascimento@rnconsultoria.com",
                "site": "",
                "cel": "21976543210",
                "zap": "21976543210",
                "landline": "",
                "socialMedias": []
            }
        },
        {
            "id": 24,
            "companyId": 1,
            "name": "CleanService Higiene Profissional",
            "responsible": "Felipe Costa",
            "cnpj": "12.345.678/0001-10",
            "entityType": 0,
            "registered": "2022-04-18T00:00:00Z",
            "description": "Serviços de limpeza profissional",
            "businessLine": "Limpeza",
            "assured": true,
            "payment": 12500.00,
            "expiration": 60,
            "disabled": false,
            "discount": 15.0,
            "company": {
                "id": 124,
                "name": "CleanService Higiene Profissional",
                "address": {
                    "id": 1024,
                    "zipCode": "30150-100",
                    "street": "Rua da Bahia",
                    "number": "2000",
                    "district": "Lourdes",
                    "city": "Belo Horizonte",
                    "state": "MG",
                    "complement": ""
                },
                "contact": {
                    "id": 1024,
                    "email": "contato@cleanservice.com",
                    "site": "www.cleanservice.com",
                    "cel": "31987654321",
                    "zap": "31987654321",
                    "landline": "3132325656",
                    "socialMedias": []
                }
            },
            "address": {
                "id": 2024,
                "zipCode": "30150-100",
                "street": "Rua da Bahia",
                "number": "2000",
                "district": "Lourdes",
                "city": "Belo Horizonte",
                "state": "MG",
                "complement": ""
            },
            "contact": {
                "id": 2024,
                "email": "felipe.costa@cleanservice.com",
                "site": "",
                "cel": "31976543210",
                "zap": "31976543210",
                "landline": "",
                "socialMedias": []
            }
        },
        {
            "id": 25,
            "companyId": 1,
            "name": "Amanda Vieira",
            "responsible": "Amanda Vieira",
            "cnpj": "357.159.486-20",
            "entityType": 1,
            "registered": "2023-02-10T00:00:00Z",
            "description": "Dentista especialista em ortodontia",
            "businessLine": "Saúde",
            "assured": true,
            "payment": 4800.00,
            "expiration": 30,
            "disabled": false,
            "discount": 5.0,
            "company": {
                "id": 125,
                "name": "Clínica AV Odonto",
                "address": {
                    "id": 1025,
                    "zipCode": "01415-000",
                    "street": "Alameda Franca",
                    "number": "300",
                    "district": "Jardim Paulista",
                    "city": "São Paulo",
                    "state": "SP",
                    "complement": "Sala 102"
                },
                "contact": {
                    "id": 1025,
                    "email": "contato@avodonto.com",
                    "site": "www.avodonto.com",
                    "cel": "11987654321",
                    "zap": "11987654321",
                    "landline": "1145678901",
                    "socialMedias": []
                }
            },
            "address": {
                "id": 2025,
                "zipCode": "01415-000",
                "street": "Alameda Franca",
                "number": "300",
                "district": "Jardim Paulista",
                "city": "São Paulo",
                "state": "SP",
                "complement": "Sala 102"
            },
            "contact": {
                "id": 2025,
                "email": "amanda.vieira@avodonto.com",
                "site": "",
                "cel": "11976543210",
                "zap": "11976543210",
                "landline": "",
                "socialMedias": []
            }
        },
        {
            "id": 26,
            "companyId": 1,
            "name": "SeguraMais Seguros Ltda",
            "responsible": "Marcos Vinicius",
            "cnpj": "23.456.789/0001-11",
            "entityType": 0,
            "registered": "2022-11-30T00:00:00Z",
            "description": "Corretora de seguros",
            "businessLine": "Seguros",
            "assured": false,
            "payment": 28000.00,
            "expiration": 90,
            "disabled": false,
            "discount": 30.0,
            "company": {
                "id": 126,
                "name": "SeguraMais Seguros Ltda",
                "address": {
                    "id": 1026,
                    "zipCode": "70070-100",
                    "street": "SBS Quadra 1",
                    "number": "50",
                    "district": "Asa Sul",
                    "city": "Brasília",
                    "state": "DF",
                    "complement": "Bloco B"
                },
                "contact": {
                    "id": 1026,
                    "email": "contato@seguramais.com",
                    "site": "www.seguramais.com",
                    "cel": "61987654321",
                    "zap": "61987654321",
                    "landline": "6134567890",
                    "socialMedias": []
                }
            },
            "address": {
                "id": 2026,
                "zipCode": "70070-100",
                "street": "SBS Quadra 1",
                "number": "50",
                "district": "Asa Sul",
                "city": "Brasília",
                "state": "DF",
                "complement": "Bloco B"
            },
            "contact": {
                "id": 2026,
                "email": "marcos.vinicius@seguramais.com",
                "site": "",
                "cel": "61976543210",
                "zap": "61976543210",
                "landline": "",
                "socialMedias": []
            }
        },
        {
            "id": 27,
            "companyId": 1,
            "name": "Larissa Mendonça",
            "responsible": "Larissa Mendonça",
            "cnpj": "852.741.963-50",
            "entityType": 1,
            "registered": "2023-05-25T00:00:00Z",
            "description": "Fisioterapeuta esportiva",
            "businessLine": "Saúde",
            "assured": true,
            "payment": 3200.00,
            "expiration": 30,
            "disabled": false,
            "discount": 0.0,
            "company": {
                "id": 127,
                "name": "LM Fisioterapia",
                "address": {
                    "id": 1027,
                    "zipCode": "22071-000",
                    "street": "Rua Visconde de Pirajá",
                    "number": "500",
                    "district": "Ipanema",
                    "city": "Rio de Janeiro",
                    "state": "RJ",
                    "complement": "Sala 304"
                },
                "contact": {
                    "id": 1027,
                    "email": "contato@lmfisioterapia.com",
                    "site": "www.lmfisioterapia.com",
                    "cel": "21987654321",
                    "zap": "21987654321",
                    "landline": "2125253636",
                    "socialMedias": []
                }
            },
            "address": {
                "id": 2027,
                "zipCode": "22071-000",
                "street": "Rua Visconde de Pirajá",
                "number": "500",
                "district": "Ipanema",
                "city": "Rio de Janeiro",
                "state": "RJ",
                "complement": "Sala 304"
            },
            "contact": {
                "id": 2027,
                "email": "larissa.mendonca@lmfisioterapia.com",
                "site": "",
                "cel": "21976543210",
                "zap": "21976543210",
                "landline": "",
                "socialMedias": []
            }
        },
        {
            "id": 28,
            "companyId": 1,
            "name": "PetLove Cuidados S/A",
            "responsible": "Thiago Silva",
            "cnpj": "34.567.890/0001-12",
            "entityType": 0,
            "registered": "2022-08-17T00:00:00Z",
            "description": "Cuidados e produtos para pets",
            "businessLine": "Pet",
            "assured": true,
            "payment": 22000.00,
            "expiration": 60,
            "disabled": false,
            "discount": 25.0,
            "company": {
                "id": 128,
                "name": "PetLove Cuidados S/A",
                "address": {
                    "id": 1028,
                    "zipCode": "05426-200",
                    "street": "Avenida Brigadeiro Faria Lima",
                    "number": "3500",
                    "district": "Itaim Bibi",
                    "city": "São Paulo",
                    "state": "SP",
                    "complement": "5º andar"
                },
                "contact": {
                    "id": 1028,
                    "email": "contato@petlove.com",
                    "site": "www.petlove.com",
                    "cel": "11987654321",
                    "zap": "11987654321",
                    "landline": "1156789012",
                    "socialMedias": [{
                        "id": 10028,
                        "name": "Instagram",
                        "url": "instagram.com/petlove"
                    }]
                }
            },
            "address": {
                "id": 2028,
                "zipCode": "05426-200",
                "street": "Avenida Brigadeiro Faria Lima",
                "number": "3500",
                "district": "Itaim Bibi",
                "city": "São Paulo",
                "state": "SP",
                "complement": "5º andar"
            },
            "contact": {
                "id": 2028,
                "email": "thiago.silva@petlove.com",
                "site": "",
                "cel": "11976543210",
                "zap": "11976543210",
                "landline": "",
                "socialMedias": []
            }
        },
        {
            "id": 29,
            "companyId": 1,
            "name": "Eduardo Rocha",
            "responsible": "Eduardo Rocha",
            "cnpj": "456.123.789-30",
            "entityType": 1,
            "registered": "2023-09-03T00:00:00Z",
            "description": "Coach executivo",
            "businessLine": "Consultoria",
            "assured": false,
            "payment": 6500.00,
            "expiration": 30,
            "disabled": false,
            "discount": 10.0,
            "company": {
                "id": 129,
                "name": "ER Coaching",
                "address": {
                    "id": 1029,
                    "zipCode": "04538-000",
                    "street": "Avenida Juscelino Kubitschek",
                    "number": "900",
                    "district": "Itaim Bibi",
                    "city": "São Paulo",
                    "state": "SP",
                    "complement": "Sala 801"
                },
                "contact": {
                    "id": 1029,
                    "email": "contato@ercoaching.com",
                    "site": "www.ercoaching.com",
                    "cel": "11987654321",
                    "zap": "11987654321",
                    "landline": "1145678901",
                    "socialMedias": []
                }
            },
            "address": {
                "id": 2029,
                "zipCode": "04538-000",
                "street": "Avenida Juscelino Kubitschek",
                "number": "900",
                "district": "Itaim Bibi",
                "city": "São Paulo",
                "state": "SP",
                "complement": "Sala 801"
            },
            "contact": {
                "id": 2029,
                "email": "eduardo.rocha@ercoaching.com",
                "site": "",
                "cel": "11976543210",
                "zap": "11976543210",
                "landline": "",
                "socialMedias": []
            }
        },
        {
            "id": 30,
            "companyId": 1,
            "name": "Ana Costa",
            "responsible": "Ana Costa",
            "cnpj": "987.654.321-00",
            "entityType": 1,
            "registered": "2023-10-05T00:00:00Z",
            "description": "Designer freelancer",
            "businessLine": "Design",
            "assured": true,
            "payment": 2000.00,
            "expiration": 15,
            "disabled": false,
            "discount": 5.0,
            "company": {
                "id": 130,
                "name": "Design AC",
                "address": {
                    "id": 1030,
                    "zipCode": "22070-010",
                    "street": "Av. Atlântica",
                    "number": "500",
                    "district": "Copacabana",
                    "city": "Rio de Janeiro",
                    "state": "RJ",
                    "complement": "Bloco B"
                },
                "contact": {
                    "id": 1030,
                    "email": "contato@designac.com",
                    "site": "www.designac.com",
                    "cel": "21955555555",
                    "zap": "21955555555",
                    "landline": "2125555555",
                    "socialMedias": [{
                        "id": 10030,
                        "name": "Behance",
                        "url": "behance.net/anacosta"
                    }]
                }
            },
            "address": {
                "id": 2030,
                "zipCode": "22070-010",
                "street": "Av. Atlântica",
                "number": "500",
                "district": "Copacabana",
                "city": "Rio de Janeiro",
                "state": "RJ",
                "complement": "Bloco B"
            },
            "contact": {
                "id": 2030,
                "email": "ana.costa@hotmail.com",
                "site": "",
                "cel": "21944444444",
                "zap": "21944444444",
                "landline": "",
                "socialMedias": []
            }
        }
    ]
  `




}
