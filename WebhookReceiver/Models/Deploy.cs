using Newtonsoft.Json;
using WebhookReceiver.Interfaces;

namespace WebhookReceiver.Models
{

    public class Event
    {
    }
    [JsonObject("payload")]
    public class DeployPayload
    {
        public string ServerUri { get; set; }
        public string ServerAuditUri { get; set; }
        public Subscription Subscription { get; set; }
        public Event Event { get; set; }
        public DateTime BatchProcessingDate { get; set; }
        public string BatchId { get; set; }
        public string TotalEventsInBatch { get; set; }
        public string EventNumberInBatch { get; set; }
    }

    public class Deploy : IReceivable
    {
        public DateTime Timestamp { get; set; }
        public string EventType { get; set; }
        public DeployPayload Payload { get; set; }
    }

    public class Subscription
    {
    }
}
