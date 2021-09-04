using App.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace App.Examples
{
    public class Example5 : AbstractExample, IExample
    {
        public Example5(ISendGridClient client, IOptions<Settings> options, ILogger logger) : base(client, options, logger)
        {
        }

        public override string Description => "Use CreateSingleEmail helper method (plain text)";

        protected override SendGridMessage BuildSendGridMessage()
        {
            var settings = Options.Value;
            var subject = BuildSubject();
            var textContent = settings.TextEmailBody;
            var from = new EmailAddress(settings.FromEmail);
            var to = new EmailAddress(settings.ToEmail);
            var message = CreateSingleTextEmail(from, to, subject, textContent);
            return message;
        }

        private static SendGridMessage CreateSingleTextEmail(
            EmailAddress from,
            EmailAddress to,
            string subject,
            string textContent)
        {
            return MailHelper.CreateSingleEmail(from, to, subject, textContent, null);
        }
    }
}