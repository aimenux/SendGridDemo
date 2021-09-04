using System;
using System.Threading;
using System.Threading.Tasks;
using App.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace App.Examples
{
    public abstract class AbstractExample : IExample
    {
        protected readonly ISendGridClient Client;
        protected readonly IOptions<Settings> Options;
        protected readonly ILogger Logger;

        protected AbstractExample(ISendGridClient client, IOptions<Settings> options, ILogger logger)
        {
            Client = client;
            Options = options;
            Logger = logger;
        }

        public string Name => GetType().Name;

        public abstract string Description { get; }

        public virtual async Task RunAsync(CancellationToken cancellationToken = default)
        {
            LogBeginRunning();

            var message = BuildSendGridMessage();
            var response = await Client.SendEmailAsync(message, cancellationToken);

            LogEndRunning(response);
        }

        protected abstract SendGridMessage BuildSendGridMessage();

        protected string BuildSubject() => $"{Name} {Options.Value.Subject} {DateTime.Now:G}";

        private void LogBeginRunning()
        {
            Logger.LogInformation("Begin running {example} : {description}", Name, Description);
        }

        private void LogEndRunning(Response response)
        {
            var message = response.IsSuccessStatusCode
                ? "Email delivery is succeeded"
                : "Email delivery is failed";

            Logger.LogInformation("End running {example} : {message}", Name, message);
        }
    }
}