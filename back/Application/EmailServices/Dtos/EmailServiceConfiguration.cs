using System.ComponentModel.DataAnnotations;

namespace Application.EmailServices.Dtos;

public sealed class EmailServiceConfiguration
{

    // public EmailServiceConfiguration(string smtpServer, int port, bool useSsl, string userName, string password, string defaultFrom, int timeoutSeconds)
    // {
    //     SmtpServer = smtpServer;
    //     Port = port;
    //     UseSsl = useSsl;
    //     UserName = userName;
    //     Password = password;
    //     DefaultFrom = defaultFrom;
    //     TimeoutSeconds = timeoutSeconds;
    // }
    public const string SectionName = "Email";

    [Required]
    public required string SmtpServer { get; set; }

    [Range(1, 65535)]
    public int Port { get; set; } = 587;

    public bool UseSsl { get; set; } = false;

    [Required]
    public required string UserName { get; set; }

    [Required]
    public required string Password { get; set; }

    [Required]
    public required string DefaultFrom { get; set; }

    [Range(1, 300)]
    public int TimeoutSeconds { get; set; } = 30;
    // public const string SectionName = "Email";

    // [Required]
    // public required string SmtpServer { get; init; }

    // [Range(1, 65535)]
    // public int Port { get; init; } = 587;

    // public bool UseSsl { get; init; } = false;

    // [Required]
    // public required string UserName { get; init; }

    // [Required]
    // public required string Password { get; init; }

    // [Required]
    // public required string DefaultFrom { get; init; }

    // [Range(1, 300)]
    // public int TimeoutSeconds { get; init; } = 30;
}
