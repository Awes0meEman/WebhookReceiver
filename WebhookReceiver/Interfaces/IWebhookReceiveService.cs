namespace WebhookReceiver.Interfaces
{
    public interface IWebhookReceiveService
    {
        public string GetUrl();
        public List<IDeliverable> GetDeliverables();
    }
}
