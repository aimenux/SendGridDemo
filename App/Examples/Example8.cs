using System;
using System.IO;
using App.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace App.Examples
{
    public class Example8 : AbstractExample, IExample
    {
        public Example8(ISendGridClient client, IOptions<Settings> options, ILogger logger) : base(client, options, logger)
        {
        }

        public override string Description => "Use Attachments";

        protected override SendGridMessage BuildSendGridMessage()
        {
            var settings = Options.Value;
            var subject = BuildSubject();
            var htmlContent = settings.HtmlEmailBody;
            var from = new EmailAddress(settings.FromEmail);
            var to = new EmailAddress(settings.ToEmail);
            var message = CreateSingleHtmlEmail(from, to, subject, htmlContent);
            AddAttachment(message, settings.AttachmentFilePath);
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

        private static void AddAttachment(SendGridMessage message, string filePath)
        {
            var bytes = File.ReadAllBytes(filePath);
            var content = Convert.ToBase64String(bytes);
            message.AddAttachment("Your-Attachment-File", content);
        }
    }
}