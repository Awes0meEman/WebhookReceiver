using Newtonsoft.Json;
using WebhookReceiver.Interfaces;

namespace WebhookReceiver.Models
{
    public class AdaptiveCard : IDeliverable
    {
        public string type { get; set; } = "message";
        public List<Attachment> attachments { get; set; } = new List<Attachment>();
    }
    public class Attachment
    {
        public string contentType { get; set; } = "application/vnd.microsoft.card.adaptive";
        public Content content { get; set; } = new Content();
    }

    public class Body
    {
        public string type { get; set; } = "TextBlock";
        public string text { get; set; } = "";
        public string wrap { get; set; } = "true";
    }

    public class MSTeams
    {
        public string width { get; set; } = "Full";
    }

    public class Content
    {
        public string type { get; set; } = "AdaptiveCard";
        public List<Body> body { get; set; } = new List<Body>();
        public MSTeams msteams { get; set; } = new MSTeams();
        [JsonProperty("$schema")]
        public string schema { get; set; } = @"http://adaptivecards.io/schemas/adaptive-card.json";
        public string version { get; set; } = "1.2";
    }


}
