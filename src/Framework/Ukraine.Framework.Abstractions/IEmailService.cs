namespace Ukraine.Framework.Abstractions;

public interface IEmailService
{
	Task SendEmailAsync(object data, string emailTo, string subject);
}