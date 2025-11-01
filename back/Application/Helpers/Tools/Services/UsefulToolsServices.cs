namespace Application.Helpers.Tools.Services;

using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Helpers.Tools.ZipCode;
using Helpers.Tools.Cnpj;
using Helpers.Tools.CpfCnpj;
using Helpers.Tools.CpfCnpj.Dtos;
using Microsoft.Extensions.Logging;
using global::Application.Shared.Dtos;
using global::Application.Helpers.Tools.ZipCode.Dtos;
using System.Text.RegularExpressions;

public class UsefulToolsServices : ICpfCnpjGetDataServices, IZipCodeGetDataServices, IPhoneNumberValidateServices
{
    private readonly HttpClient _httpClient;
    private ViaCepResponseDto? viaCepResponse;
    private readonly ILogger<UsefulToolsServices> _logger;

    public UsefulToolsServices(HttpClient httpClient, ILogger<UsefulToolsServices> logger)
    {
        _httpClient = httpClient;
        _logger = logger;

        _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36");
        _httpClient.Timeout = TimeSpan.FromSeconds(30);
    }

    public async Task<BusinessDataDto> CpfCnpjQueryAsync(string cnpj)
    {
        if (string.IsNullOrWhiteSpace(cnpj) && !CpfCnpjValidator.IsValid(cnpj))
            throw new ArgumentException("CNPJ não pode ser vazio ou inválido.", nameof(cnpj));
        try
        {
            // Remove caracteres não numéricos
            var cnpjLimpo = new string(cnpj.Where(char.IsDigit).ToArray());

            _logger.LogInformation("Consultando CNPJ: {Cnpj}", cnpjLimpo);

            // Usando BrasilAPI (mais confiável)
            var response = await _httpClient.GetAsync($"https://receitaws.com.br/v1/cnpj/{cnpjLimpo}");

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Falha na consulta do CNPJ {Cnpj}. Status: {StatusCode}",
                    cnpjLimpo, response.StatusCode);
                return null;
            }

            var content = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var result = JsonSerializer.Deserialize<BusinessDataDto>(content, options);

            _logger.LogInformation("CNPJ {Cnpj} consultado com sucesso", cnpjLimpo);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao consultar CNPJ: {Cnpj}", cnpj);
            throw;
        }
    }
    public async Task<AddressDto> ZipCodeQueryAsync(string zipCode)
    {
        if (string.IsNullOrWhiteSpace(zipCode))
            return null;

        try
        {
            // Remove caracteres não numéricos
            var cepClean = new string(zipCode.Where(char.IsDigit).ToArray());

            // Valida se tem 8 dígitos
            if (cepClean.Length != 8)
                throw new ArgumentException("CEP deve conter 8 dígitos", nameof(zipCode));

            var url = $"https://viacep.com.br/ws/{cepClean}/json/";

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                // Log do status code de erro
                Console.WriteLine($"Erro na consulta do CEP: {response.StatusCode}");
                return null;
            }

            var content = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var viaCepResponse = new ViaCepResponseDto();

            var result = JsonSerializer.Deserialize<ViaCepResponseDto>(content, options);

            viaCepResponse = result;

            // Verifica se a API retornou erro
            if (result?.Erro == true)
            {
                Console.WriteLine("CEP não encontrado");
                return null;
            }


            return MakeAddressDto(result ?? new ViaCepResponseDto());
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao consultar CEP {zipCode}: {ex.Message}");
            throw;
        }
    }
    private AddressDto MakeAddressDto(ViaCepResponseDto request)
    {
        var address = new AddressDto()
        {
            ZipCode = request.Cep,
            Street = request.Logradouro,
            Number = "",
            District = request.Bairro,
            City = request.Localidade,
            State = request.Uf,
            Complement = ""
        };
        return address;
    }
    public string IsMobile(string numero)
    {
        if (string.IsNullOrWhiteSpace(numero))
            return PhoneTypeDto.Invalid;

        // Remove todos os caracteres não numéricos
        string numeroLimpo = Regex.Replace(numero, @"[^\d]", "");

        // Verifica se é um número válido (10 ou 11 dígitos)
        if (numeroLimpo.Length < 10 || numeroLimpo.Length > 11)
            return PhoneTypeDto.Invalid;

        // Se tem 11 dígitos, verifica se é celular
        if (numeroLimpo.Length == 11)
        {
            // Celular: geralmente começa com 9 o nono dígito
            // O nono dígito é o 3º quando consideramos DDD + número
            if (numeroLimpo[2] == '9')
                return PhoneTypeDto.Mobile;
            else
                return PhoneTypeDto.Landline;
        }

        // Se tem 10 dígitos
        if (numeroLimpo.Length == 10)
        {
            // Fixo: geralmente começa com 2, 3, 4 ou 5
            char primeiroDigitoNumero = numeroLimpo[2];
            if (primeiroDigitoNumero == '2' || primeiroDigitoNumero == '3' ||
                primeiroDigitoNumero == '4' || primeiroDigitoNumero == '5')
                return PhoneTypeDto.Landline;
            else
                return PhoneTypeDto.Mobile;
        }

        return PhoneTypeDto.Invalid;
    }

}