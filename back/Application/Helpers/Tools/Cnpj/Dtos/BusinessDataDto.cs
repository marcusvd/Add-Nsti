namespace Application.Helpers.Tools.CpfCnpj.Dtos;

public class BusinessDataDto
{
    public string Abertura { get; set; } = string.Empty;
    public string Situacao { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public string Porte { get; set; } = string.Empty;
    public string NaturezaJuridica { get; set; } = string.Empty;
    public List<Atividade> AtividadePrincipal { get; set; } = new List<Atividade>();
    public List<Atividade> AtividadesSecundarias { get; set; } = new List<Atividade>();
    public string Logradouro { get; set; } = string.Empty;
    public string Numero { get; set; } = string.Empty;
    public string Municipio { get; set; } = string.Empty;
    public string Bairro { get; set; } = string.Empty;
    public string Uf { get; set; } = string.Empty;
    public string Cep { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string DataSituacao { get; set; } = string.Empty;
    public string Cnpj { get; set; } = string.Empty;
    public string UltimaAtualizacao { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Fantasia { get; set; } = string.Empty;
    public string Complemento { get; set; } = string.Empty;
    public string Efr { get; set; } = string.Empty;
    public string MotivoSituacao { get; set; } = string.Empty;
    public string SituacaoEspecial { get; set; } = string.Empty;
    public string DataSituacaoEspecial { get; set; } = string.Empty;
    public string CapitalSocial { get; set; } = string.Empty;
    public List<object> Qsa { get; set; } = new List<object>();
    public object? Extra { get; set; }
    public Billing Billing { get; set; } = new Billing();
}

public class Atividade
{
    public string Code { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
}

public class Billing
{
    public bool Free { get; set; }
    public bool Database { get; set; }
}