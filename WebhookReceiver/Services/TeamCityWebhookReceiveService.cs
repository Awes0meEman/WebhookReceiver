using WebhookReceiver.Interfaces;
using WebhookReceiver.Models;

namespace WebhookReceiver.Services
{
    public class TeamCityWebhookReceiveService : IWebhookReceiveService
    {
        private bool _disposed = false;
        private Build _build {  get; set; }
        private readonly string _url;
        public TeamCityWebhookReceiveService(Build build, string url) 
        {
            _build = build;
            _url = url;
        }
        public string GetUrl()
        {
            return _url;
        }
        public List<IDeliverable> GetDeliverables()
        {
            List<IDeliverable> cards = new List<IDeliverable>();
            AdaptiveCard deliverable = new AdaptiveCard();
            Body body = new Body();
            switch (_build.eventType)
            {
                case "BUILD_STARTED":
                    //Console.WriteLine(_build);
                    body.text = $"{{{{DATE({_build.payload.startDate}Z, SHORT)}}}} {_build.payload.triggered.user.name} started a build on configuration {_build.payload.buildType.name}";
                    break;
                case "BUILD_FINISHED":
                    body.text = $"{_build.eventType} not implemented yet";
                    break;
                case "BUILD_INTERRUPTED":
                    body.text = $"{_build.eventType} not implemented yet";
                    break;
                case "CHANGES_LOADED":
                    body.text = $"{_build.eventType} not implemented yet";
                    break;
                case null:
                    break;
            }
            deliverable.attachments.First().content.body.Add(body);
            cards.Add(deliverable);
            return cards;
        }
    }
}
