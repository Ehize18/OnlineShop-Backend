using Microsoft.Extensions.Options;
using OnlineShop.Core.Interfaces;
using System.Net;
using System.Net.Mail;

namespace OnlineShop.Infrastructure.Email
{
	public class EmailService : IEmailService
	{
		private readonly EmailOptions _options;

		public EmailService(IOptions<EmailOptions> options)
		{
			_options = options.Value;
		}

		public async Task SendMail(string address, string subject, string body)
		{
			var message = new MailMessage(_options.SmtpUsername, address, subject, body);
			var client = new SmtpClient(_options.SmtpHost, _options.SmtpPort);
			client.EnableSsl = true;
			client.DeliveryMethod = SmtpDeliveryMethod.Network;
			client.UseDefaultCredentials = false;
			client.Credentials = new NetworkCredential(_options.SmtpUsername, _options.SmtpPassword);
			await client.SendMailAsync(message);
		}
	}
}
