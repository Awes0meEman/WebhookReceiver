using WebhookReceiver.Interfaces;
using WebhookReceiver.Models;

namespace WebhookReceiver.Services
{
    public class OctopusWebhookReceiveService : IWebhookReceiveService
    {
        private Deploy _deploy { get; set; }
        private string _url;
        public OctopusWebhookReceiveService(Deploy deploy, string url)
        {
            _deploy = deploy;
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
            body.text = "Not implemented yet";
            deliverable.attachments.First().content.body.Add(body);
            cards.Add(deliverable);
            return cards;
        }
    }
}
