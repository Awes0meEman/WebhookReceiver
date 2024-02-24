using WebhookReceiver.Interfaces;
using WebhookReceiver.Models;
using WebhookReceiver.Extension;

namespace WebhookReceiver.Services
{
    public static class WebhookRecieveServiceFactory
    {
        public static IWebhookReceiveService CreateReceiveService(string parsedRequest, Dictionary<string,string> configuration)
        {
            if (parsedRequest == null)
            {
                throw new ArgumentNullException(nameof(parsedRequest));
            }
            if (parsedRequest.TryParseJson<Build>(out Build build))
            {
                return new TeamCityWebhookReceiveService(build, configuration["TeamCityWebhookString"]);
            }
            else if(parsedRequest.TryParseJson<Deploy>(out Deploy deploy))
            {
                return new OctopusWebhookReceiveService(deploy, configuration["OctopusWebHookString"]);
            }
            else
            {
                throw new NotSupportedException("Json schema not supported");
            }
        }
    }
}
