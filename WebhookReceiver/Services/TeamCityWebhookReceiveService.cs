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
            string formattedDateString = UtilityService.FormatDateTimeString(_build.payload.startDate);
            string dateTimeString = string.Empty;
            string buildIdentifier = $"{_build.payload.buildType.projectName}.{_build.payload.buildType.name}.{_build.payload.number}";
            if (formattedDateString != string.Empty)
            {
                dateTimeString = $"{{{{DATE({formattedDateString}, SHORT)}}}} - {{{{TIME({formattedDateString})}}}} -";
            }
            switch (_build.eventType)
            {
                case "BUILD_STARTED":
                    body.text = $"{dateTimeString}{buildIdentifier} started" +
                        $"\n\r- Project: {_build.payload.buildType.projectName}" +
                        $"\n\r- Build Configuration: {_build.payload.buildType.name}" +
                        $"\n\r- Number: {_build.payload.number}" +
                        $"\n\r- Started By: {_build.payload.triggered.user.name}" +
                        $"\n\r- Branch: {_build.payload.branchName}" +
                        $"\n\r- Change Count: {_build.payload.changes.count}" +
                        $"\n\r- URL: {UtilityService.FormatAdaptiveCardHyperLink(buildIdentifier ,_build.payload.webUrl)}" +
                        $"\n\r- Build Agent: {_build.payload.agent.name}";
                    break;
                case "BUILD_FINISHED":
                    body.text = $"{dateTimeString}{buildIdentifier} completed with status {_build.payload.statusText}" +
                        $"\n\r- Project: {_build.payload.buildType.projectName}" +
                        $"\n\r- Build Configuration: {_build.payload.buildType.name}" +
                        $"\n\r- Number: {_build.payload.number}" +
                        $"\n\r- Started By: {_build.payload.triggered.user.name}" +
                        $"\n\r- Branch: {_build.payload.branchName}" +
                        $"\n\r- Change Count: {_build.payload.changes.count}" +
                        $"\n\r- URL: {UtilityService.FormatAdaptiveCardHyperLink(buildIdentifier, _build.payload.webUrl)}" +
                        $"\n\r- Build Agent: {_build.payload.agent.name}";
                    break;
                case "BUILD_INTERRUPTED":
                    body.text = $"{dateTimeString}{buildIdentifier} was interrupted with status {_build.payload.statusText}" +
                        $"\n\r- Project: {_build.payload.buildType.projectName}" +
                        $"\n\r- Build Configuration: {_build.payload.buildType.name}" +
                        $"\n\r- Number: {_build.payload.number}" +
                        $"\n\r- Started By: {_build.payload.triggered.user.name}" +
                        $"\n\r- Branch: {_build.payload.branchName}" +
                        $"\n\r- Change Count: {_build.payload.changes.count}" +
                        $"\n\r- URL: {UtilityService.FormatAdaptiveCardHyperLink(buildIdentifier, _build.payload.webUrl)}" +
                        $"\n\r- Build Agent: {_build.payload.agent.name}";
                    break;
                case "CHANGES_LOADED":
                    body.text = $"{_build.eventType} not implemented yet";
                    break;
                case null:
                    break;
            }
            var content = new Content();
            content.body.Add( body );
            deliverable.attachments.Add(new Attachment { content = content });
            cards.Add(deliverable);
            return cards;
        }
    }
}
