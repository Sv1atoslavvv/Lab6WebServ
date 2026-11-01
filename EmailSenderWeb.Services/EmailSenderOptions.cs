namespace EmailSenderWeb.Services;

public sealed class EmailSenderOptions
{
    public const string SectionName = "EmailSender";

    public string Host { get; set; } = "smtp.gmail.com";
    public int Port { get; set; } = 587;
    public bool UseStartTls { get; set; } = true;
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string FromEmail { get; set; } = string.Empty;
    public string FromName { get; set; } = "Навчальний сайт";
    public string Subject { get; set; } = "Повідомлення з навчального сайту";
}
