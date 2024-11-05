namespace OnlineShop.Core.Interfaces
{
	public interface IEmailService
	{
		Task SendMail(string address, string subject, string body);
	}
}