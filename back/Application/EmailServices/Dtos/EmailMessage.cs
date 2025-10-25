using System.Diagnostics.CodeAnalysis;

namespace Application.EmailServices.Dtos;

public class EmailMessage
{

    [SetsRequiredMembers]
    public EmailMessage(string to, string subject, string body)
    {
        To = to;
        Subject = subject;
        Body = body;
    }

    public required string To { get; set; } = string.Empty;
    public required string Subject { get; set; } = string.Empty;
    public required string Body { get; set; } = string.Empty;
    public string? From { get; set; }
    public bool IsBodyHtml { get; set; } = true;
}

public static class EmailMessageExtensions
{
    public static EmailMessage WithFrom(this EmailMessage message, string from)
    {
        message.From = from;
        return message;
    }

    public static EmailMessage WithHtmlBody(this EmailMessage message, bool isHtml = true)
    {
        message.IsBodyHtml = isHtml;
        return message;
    }
}