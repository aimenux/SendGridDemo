using App.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace App.Examples
{
    public class Example6 : AbstractExample, IExample
    {
        public Example6(ISendGridClient client, IOptions<Settings> options, ILogger logger) : base(client, options, logger)
        {
        }

        public override string Description => "Use CreateSingleEmail helper method (html text)";

        protected override SendGridMessage BuildSendGridMessage()
        {
            var settings = Options.Value;
            var subject = BuildSubject();
            var htmlContent = settings.HtmlEmailBody;
            var from = new EmailAddress(settings.FromEmail);
            var to = new EmailAddress(settings.ToEmail);
            var message = CreateSingleHtmlEmail(from, to, subject, htmlContent);
            return message;
        }

        private static SendGridMessage CreateSingleHtmlEmail(
            EmailAddress from,
            EmailAddress to,
            string subject,
            string htmlContent)
        {
            return MailHelper.CreateSingleEmail(from, to, subject, null, htmlContent);
        }
    }
}