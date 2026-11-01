namespace EmailSenderWeb.Services;

public interface IEmailSender
{
    Task SendEmailAsync(string recipient, string body, CancellationToken cancellationToken = default);
}
