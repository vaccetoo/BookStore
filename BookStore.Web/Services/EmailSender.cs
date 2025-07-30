using Microsoft.AspNetCore.Identity.UI.Services;

namespace BookStore.Web.Services
{
	public class EmailSender : IEmailSender
	{
		public Task SendEmailAsync(string email, string subject, string htmlMessage)
		{
			// TODO: Add logic for sending email
			return Task.CompletedTask;
		}
	}
}
