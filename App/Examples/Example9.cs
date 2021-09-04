using App.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace App.Examples
{
    public class Example9 : AbstractExample, IExample
    {
        public Example9(ISendGridClient client, IOptions<Settings> options, ILogger logger) : base(client, options, logger)
        {
        }

        public override string Description => "Use Templates";

        protected override SendGridMessage BuildSendGridMessage()
        {
            var settings = Options.Value;
            var templateId = settings.TemplateId;
            var from = new EmailAddress(settings.FromEmail);
            var to = new EmailAddress(settings.ToEmail);
            var templateData = new TemplateData {Name = Name};
            var message = MailHelper.CreateSingleTemplateEmail(from, to, templateId, templateData);
            return message;
        }

        private class TemplateData
        {
            [JsonProperty("name")]
            public string Name { get; set; }
        }
    }
}