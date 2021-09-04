using App.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace App.Examples
{
    public class Example4 : AbstractExample, IExample
    {
        public Example4(ISendGridClient client, IOptions<Settings> options, ILogger logger) : base(client, options, logger)
        {
        }

        public override string Description => "Use SendGridMessage constructor and AddContent method (html text)";

        protected override SendGridMessage BuildSendGridMessage()
        {
            var settings = Options.Value;
            var subject = BuildSubject();
            var htmlContent = settings.HtmlEmailBody;
            var from = new EmailAddress(settings.FromEmail);
            var to = new EmailAddress(settings.ToEmail);
            var message = new SendGridMessage
            {
                From = from,
                Subject = subject,
            };
            message.AddContent(MimeType.Html, htmlContent);
            message.AddTo(to);
            return message;
        }
    }
}