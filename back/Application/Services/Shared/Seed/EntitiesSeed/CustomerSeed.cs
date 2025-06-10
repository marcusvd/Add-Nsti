using System;
using System.Collections.Generic;
using Domain.Entities.Main.Customers;
using Domain.Entities.Main.Inheritances.Enums;
using Domain.Entities.Shared;

namespace Application.Services.Shared.Seed.EntitiesSeed;

public class CustomerSeed_NSTI
{
    public Customer MinasArCompressores()
    {
        List<SocialNetwork> socialMedias = new(){
                new SocialNetwork(){
                    CompanyId = 1,
                    Name = "Instagram", Url ="compressoresminasar"},
                new SocialNetwork(){
                    CompanyId = 1,
                    Name = "Facebook", Url ="https://www.facebook.com/profile.php?id=100063570278549&mibextid=ZbWKwL"},
            };

        Customer customer = new(1,
              "Minas Ar Compressores",
                 "Wellington Batista",
                 "13495178000100",
                 DateTime.Now,
                @"A Minas Ar Compressores atua no fornecimento de energias alternativas para movimentar equipamentos especiais com foco nos segmentos de infraestrutura, construção civil, indústria, eventos, óleo & gás, mineração, agronegócio, usinas termelétricas e comércio & serviços.
                        A Minas Ar Compressores tem como objetivo oferecer ao mercado e a seus clientes a mais completa linha de produtos relacionados a soluções de ar comprimido, tais como: compressores de parafuso, estacionários e portáteis, secadores de ar, filtros coalescentes, reservatórios (pulmões) de ar, rede de ar e instalações de centrais de ar comprimido.
                        Sempre perto de você, a Minas Ar Compressores tem como diferencial a assistência técnica 24h, disponibilizando equipes de profissionais experientes e técnicos qualificados.",
                 "Assistência Técnica",
                    new()
                    {

                        ZipCode = "31255190",
                        Street = "Rua Estoril",
                        Number = "1611",
                        District = "São Francisco",
                        City = "Belo Horizonte",
                        State = "MG",
                        Complement = null,
                        Registered = DateTime.Now,
                        Deleted = DateTime.MinValue,

                    },
                    new(
                        )
                    {

                        Email = "comercial@minasarcompressores.com.br",
                        Site = "minasarcompressores.com.br",
                        Cel = "31984194408",
                        Zap = "31984194408",
                        Landline = "3125510021",
                        Registered = DateTime.Now,
                        Deleted = DateTime.MinValue,
                        SocialMedias = socialMedias
                    },
                 DateTime.Now,
                 600,
                 new DateTime(DateTime.Now.Year, DateTime.Now.Month, 10),
                 DateTime.MinValue,
                 0,
                    EntityTypeEnum.PJ

        );

        customer.UserId = 1;
        return customer;


    }
    public Customer ArcAr()
    {

        List<SocialNetwork> socialMedias = new(){
                new SocialNetwork(){
                    CompanyId = 1,
                    Name = "Instagram", Url ="arc_arcondicionado"},
            };

        Customer customer = new(1,
                "ARC AR CONDICIONADO LTDA",
                 "Alexandre",
                 "12920789000188",
                 DateTime.Now,
                @"Atuamos na área de climatização de ambientes desde 2010, prestando serviços de instalação, manutenção preventiva e/ou corretiva, consultoria e projetos.
                      Objetivando a satisfação de nossos clientes oferecemos serviços personalizados, de alto padrão técnico e de qualidade.
                      Nossos técnicos são experientes e qualificados através da educação continuada realizada junto aos fabricantes do setor e aos melhores centros de treinamento em refrigeração.",
                 "Instalação / Assistência Técnica / Consultoria e projetos",
                    new()
                    {

                        ZipCode = "30285270",
                        Street = "Itamirim",
                        Number = "280",
                        District = "Vera Cruz",
                        City = "Belo Horizonte",
                        State = "MG",
                        Complement = null,
                        Registered = DateTime.Now,
                        Deleted = DateTime.MinValue,

                    }, new()
                    {

                        Email = "renata@brisaambientes.com.br",
                        Site = "http://arcarcondicionado.com.br/",
                        Cel = "31984194408",
                        Zap = "31984194408",
                        Landline = "3135463081",
                        Registered = DateTime.Now,
                        Deleted = DateTime.MinValue,
                        SocialMedias = socialMedias
                    },
                DateTime.Now,
                 250,
                new DateTime(DateTime.Now.Year, DateTime.Now.Month, 10),
                 DateTime.MinValue,
                 0,
                 EntityTypeEnum.PJ
        );

        customer.UserId = 1;
        return customer;


    }
    public Customer Medrado()
    {

        List<SocialNetwork> socialMedias = new(){
                new SocialNetwork(){
                    CompanyId = 1,
                    Name = "Instagram", Url ="medradoadvogados"},
                new SocialNetwork(){
                    CompanyId = 1,
                    Name = "Facebook", Url ="https://www.facebook.com/medradoadvogados"},
            };

        Customer customer = new(1,
                "MEDRADO SOCIEDADE DE ADVOGADOS",
                 "Bruno Medrado",
                 "38407737000104",
                 DateTime.Now,
                @"Escritório de Advocacia
                        Medrado Advogados
                        Atuação em todas as áreas.
                        Fundado em 1986.",
                 "Advocacia / Direito",
                    new()
                    {

                        ZipCode = "30180099",
                        Street = "Dos Guajajaras",
                        Number = "1470",
                        District = "Barro preto",
                        City = "Belo Horizonte",
                        State = "MG",
                        Complement = "10º ANDAR - SALA  1004",
                        Registered = DateTime.Now,
                        Deleted = DateTime.MinValue,

                    },
                    new()
                    {

                        Email = "contato@medradoadvogados.com.br",
                        Site = "medradoadvogados.com.br",
                        Cel = "3132929933",
                        Zap = "3132929933",
                        Landline = "3132929933",
                        Registered = DateTime.Now,
                        Deleted = DateTime.MinValue,
                        SocialMedias = socialMedias
                    },
                 DateTime.Now,
                 200,
                 new DateTime(DateTime.Now.Year, DateTime.Now.Month, 10),
                 DateTime.MinValue,
                 0,

                   EntityTypeEnum.PJ

        );
        customer.UserId = 1;
        return customer;
    }
    public Customer Crs()
    {

        List<SocialNetwork> socialMedias = new(){
                new SocialNetwork(){
                    CompanyId = 1,
                    Name = "Instagram", Url ="comercialrosasantos"},
                new SocialNetwork(){
                    CompanyId = 1,
                    Name = "Facebook", Url ="https://www.facebook.com/comercialrosasantoss"},
            };

        Customer customer = new(1,
                "COMERCIAL ROSA SANTOS LTDA",
                 "Dora",
                 "14302611000107",
                 DateTime.Now,
                @"A Comercial Rosa Santos é uma empresa no ramo de distribuição de produtos de limpeza, higiene, descartáveis e outros produtos complementares a estes.,
                     Registered = DateTime.Now,
                Deleted = DateTime.MinValue,
                    
                    A empresa está inserida no mercado desde 2011 com sede em Belo Horizonte e com atuação comercial atacadista. Atendemos em todo o território nacional.",
                 "Distribuidora / Produtos de limpeza",
                    new(
                        )
                    {

                        ZipCode = "30620270",
                        Street = "Maringa",
                        Number = "25",
                        District = "Milionarios (Barreiro)",
                        City = "Belo Horizonte",
                        State = "MG",
                    },
                    new()
                    {

                        Email = "vendas@comercialrosasantos.com.br",
                        Site = "https://comercialrosasantos.com.br/",
                        Cel = "31986347045",
                        Zap = "31986347045",
                        Landline = "3125126346",
                        Registered = DateTime.Now,
                        Deleted = DateTime.MinValue,
                        SocialMedias = socialMedias
                    },
                 DateTime.Now,
                 220,
                 new DateTime(DateTime.Now.Year, DateTime.Now.Month, 10),
                 DateTime.MinValue,
                 0,

                   EntityTypeEnum.PJ

        );

        customer.UserId = 1;
        return customer;
    }
    public Customer Inovagro()
    {

        List<SocialNetwork> socialMedias = new(){
                new SocialNetwork(){
                    CompanyId = 1,
                    Name = "Instagram", Url ="grupoinovagro"},
            };

        Customer customer = new(1,
                "INOVAGRO PREPAROS E LOCACOES LTDA",
                 "Rodrigo Duarte",
                 "19448662000157",
                 DateTime.Now,
                @"Atuamos no mercado de locação de máquinas pesadas no Brasil. 
                    Desenvolvemos técnicas de aperfeiçoamento no trabalho.",
                 "Distribuidora / Produtos de limpeza",
                    new()
                    {

                        ZipCode = "Altamiro Avelino Soares",
                        Street = "31330000",
                        Number = "342",
                        District = "Castelo",
                        City = "Belo Horizonte",
                        State = "MG",
                        Complement = "Sala 403",
                        Registered = DateTime.Now,
                        Deleted = DateTime.MinValue,

                    },
                    new()
                    {

                        Email = "administrativomg@grupoinovagro.com.br",
                        Site = null,
                        Cel = "31995330359",
                        Zap = "31995330359",
                        Landline = "3134813727",
                        Registered = DateTime.Now,
                        Deleted = DateTime.MinValue,
                        SocialMedias = socialMedias

                    },
                 DateTime.Now,
                 250,
                 new DateTime(DateTime.Now.Year, DateTime.Now.Month, 10),
                 DateTime.MinValue,
                 0,

                   EntityTypeEnum.PJ

        );

        customer.UserId = 1;
        return customer;
    }
    public Customer VipTotalTextil()
    {

        List<SocialNetwork> socialMedias = new(){
                new SocialNetwork(){
                    CompanyId = 1,
                    Name = "Instagram", Url ="totaltextil"},
            };

        Customer customer = new(1,
                "VIP TEXTIL K LTDA",
                 "Nikolas",
                 "21933500000156",
                 DateTime.Now,
                @"Somos uma empresa especializada em enxoval hospitalar e hoteleiro, localizada no bairro Santa Efigênia – Belo horizonte, fornecendo sempre produtos de alta qualidade e melhor custo benefício com o melhor prazo de entrega. Possuímos atendimento em todo território nacional.",
                 "Industria Têxtil",
                    new()
                    {

                        ZipCode = "30270360",
                        Street = "Cel. Otávio Diniz",
                        Number = "807",
                        District = "Santa Efigênia",
                        City = "Belo Horizonte",
                        State = "MG",
                        Complement = "Galpão",
                        Registered = DateTime.Now,
                        Deleted = DateTime.MinValue,

                    },
                    new()
                    {

                        Email = "contato@totaltextil.com.br",
                        Site = "https://www.totaltextil.com.br/",
                        Cel = "31994882202",
                        Zap = "988834804",
                        Landline = "3130245555",
                        Registered = DateTime.Now,
                        Deleted = DateTime.MinValue,
                        SocialMedias = socialMedias

                    },
                 DateTime.Now,
                 312,
                 new DateTime(DateTime.Now.Year, DateTime.Now.Month, 10),
                 DateTime.MinValue,
                 0,

                   EntityTypeEnum.PJ
        );

        customer.UserId = 1;
        return customer;


    }
    public Customer PinheiroMoraisHoskenAdvocacia()
    {

        List<SocialNetwork> socialMedias = new(){
                new SocialNetwork(){
                    CompanyId = 1,
                    Name = "Instagram", Url ="https://www.instagram.com/pmehadvocacia/"},
                new SocialNetwork(){
                    CompanyId = 1,
                    Name = "Facebook", Url ="https://pt-br.facebook.com/pmehadv"},
            };

        Customer customer = new(1,
                "PINHEIRO DE MORAIS E HOSKEN ADVOCACIA",
                 "Rodrigo / Luiz",
                 "08947045000180",
                 DateTime.Now,
                    @"Há mais de 17 anos, Pinheiro de Morais & Hosken Advocacia convive diariamente com as diversas situações relacionadas à atividade empresarial, com participação ativa nas decisões de seus clientes.
                        Essa trajetória possibilitou ao escritório PM&H profundo conhecimento e atuação em várias áreas jurídicas, notadamente no Direito da Construção, Trabalhista de Obras e Negócios Imobiliários, tornando-o expert neste setor, contando com um time preparado e envolvido.
                        Nossa experiência revela que uma atuação próxima, contínua e disponível propicia a ampla compreensão das reais necessidades do cliente, o que assegura a adoção das melhores estratégias e soluções.
                        Com um modelo de atuação voltado para o apoio consultivo constante, exercemos de forma efetiva o Direito Preventivo, promovendo a redução de riscos, litígios e custos aos clientes, permitindo que estes foquem naquilo que mais entendem: seu próprio negócio.
                        Empregamos todos os esforços para que sejamos reconhecidos como um investimento que contribui para o crescimento das empresas e novos negócios.
                        Atuando de forma preventiva e contenciosa, buscamos compreender as particularidades de cada cliente, em prol de um atendimento personalíssimo.
                        Esta forma diferenciada de atendimento possibilita o conhecimento da atividade desenvolvida pelo cliente, nos concedendo uma ampla visão, capaz de apresentar as mais adequadas medidas jurídicas ao cotidiano empresarial.
                        Assim, estribados na ética, transparência e honestidade, investimos em inovações para satisfazer as necessidades e expectativas dos nossos clientes.",
                 "Advocacia / Direito",
                    new()
                    {
                        ZipCode = "30494170",
                        Street = "Raja gabaglia",
                        Number = "2000",
                        District = "ALPES",
                        City = "Belo Horizonte",
                        State = "MG",
                        Complement = "Sala 818",
                        Registered = DateTime.Now,
                        Deleted = DateTime.MinValue,

                    },
                    new()
                    {

                        Email = "contato@pmeh.com.br",
                        Site = "https://www.pmeh.com.br/",
                        Cel = "3192879402",
                        Zap = "3132612355",
                        Landline = "3132612355",
                        Registered = DateTime.Now,
                        Deleted = DateTime.MinValue,
                        SocialMedias = socialMedias
                    },
                 DateTime.Now,
                 320,
                 new DateTime(DateTime.Now.Year, DateTime.Now.Month, 10),
                 DateTime.MinValue,
                 0,

                   EntityTypeEnum.PJ
        );

        customer.UserId = 1;
        return customer;

    }
    public Customer LaenderviannaSociedadeAdvogados()
    {
        Customer customer = new(1,
                "LAENDER & VIANNA SOCIEDADE DE ADVOGADOS",
                 "Lucas / Rafael",
                 "13703366000179",
                 DateTime.Now,
                    @"A Laender & Vianna Sociedade de Advogados é uma sociedade fundada em 2011, formada por profissionais que, como principais pilares na prestação de seus serviços jurídicos, prezam pela a eficiência, transparência e comprometimento com o cliente.
                          A sociedade tem como propósito fundamental viabilizar solução de problemas jurídicos e apoio integral aos seus clientes, utilizando o que existe de mais moderno em tecnologia para garantir que o trabalho seja prestado de maneira personalizada e eficiente.
                          Vislumbra no comprometimento e no trabalho em equipe, além da constante atualização, a oportunidade de superar as expectativas dos clientes, analisando e identificando as necessidades específicas de cada caso.",
                 "Advocacia / Direito",
                    new()
                    {

                        ZipCode = "30130090",
                        Street = "Padre rolim",
                        Number = "123",
                        District = "Santa Efigenia",
                        City = "Belo Horizonte",
                        State = "MG",
                        Complement = "Sala 501",
                        Registered = DateTime.Now,
                        Deleted = DateTime.MinValue,

                    },
                    new()
                    {

                        Email = "lucas.laender@laendervianna.com.br, rafael.vianna@laendervianna.com.br",
                        Site = "http://laendervianna.com.br/",
                        Cel = "31983997479",
                        Zap = "3125162327",
                        Landline = "3125162327",
                        Registered = DateTime.Now,
                        Deleted = DateTime.MinValue,
                        SocialMedias = null
                    },
                 DateTime.Now,
                 320,
                 new DateTime(DateTime.Now.Year, DateTime.Now.Month, 10),
                 DateTime.MinValue,
                 0,

                   EntityTypeEnum.PJ
        );




        customer.UserId = 1;
        return customer;


    }
    public Customer ClinicaOftam()
    {
        Customer customer = new(1,
                "ANDRE VIANNA PESSOA DE MENDONCA",
                 "Andre / Andrea",
                 "16242092620",
                 DateTime.Now,
                    @"Oftalmologista",
                 "Medicina Saúde",
                    new()
                    {


                        ZipCode = "30150221",
                        Street = "Francisco Sales",
                        Number = "1420",
                        District = "Santa Efigenia",
                        City = "Belo Horizonte",
                        State = "MG",
                        Complement = "Sala 501",
                        Registered = DateTime.Now,
                        Deleted = DateTime.MinValue,

                    },
                    new()
                    {

                        Email = "aviannapdem@yahoo.com.br, andreviannaoftalmo@gmail.com",
                        Site = "http://laendervianna.com.br/",
                        Cel = "31999671876",
                        Zap = "31982131560",
                        Landline = "312819800",
                        Registered = DateTime.Now,
                        Deleted = DateTime.MinValue,
                        SocialMedias = null
                    },
                 DateTime.Now,
                 600,
                 new DateTime(DateTime.Now.Year, DateTime.Now.Month, 10),
                 DateTime.MinValue,
                 0,

                  EntityTypeEnum.PJ
        );




        customer.UserId = 1;
        return customer;


    }
    public List<Customer> CustomerAdd()
    {
        var cust = new List<Customer>{
                MinasArCompressores(),
                ArcAr(),
                Medrado(),
                Crs(),
                Inovagro(),
                VipTotalTextil(),
                PinheiroMoraisHoskenAdvocacia(),
                LaenderviannaSociedadeAdvogados(),
            };

        return cust;
    }

}
