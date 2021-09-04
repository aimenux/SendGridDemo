using App.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace App.Examples
{
    public class Example7 : AbstractExample, IExample
    {
        public Example7(ISendGridClient client, IOptions<Settings> options, ILogger logger) : base(client, options, logger)
        {
        }

        public override string Description => "Use Substitutions";

        protected override SendGridMessage BuildSendGridMessage()
        {
            var settings = Options.Value;
            const string subjectSubstitutionKey = "Example7";
            const string subjectSubstitutionValue = "EXAMPLE7";
            const string contentSubstitutionKey = "html";
            const string contentSubstitutionValue = "HTML";
            var subject = BuildSubject();
            var htmlContent = settings.HtmlEmailBody;
            var from = new EmailAddress(settings.FromEmail);
            var to = new EmailAddress(settings.ToEmail);
            var message = CreateSingleHtmlEmail(from, to, subject, htmlContent);
            message.AddSubstitution(subjectSubstitutionKey, subjectSubstitutionValue);
            message.AddSubstitution(contentSubstitutionKey, contentSubstitutionValue);
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