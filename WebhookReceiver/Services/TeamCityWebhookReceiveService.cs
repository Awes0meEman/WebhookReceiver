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
                        $"\nProject: {_build.payload.buildType.projectName}" +
                        $"\nBuild Configuration: {_build.payload.buildType.name}" +
                        $"\nNumber: {_build.payload.number}" +
                        $"\nStarted By: {_build.payload.triggered.user.name}" +
                        $"\nBranch: {_build.payload.branchName}" +
                        $"\nChange Count: {_build.payload.changes.count}" +
                        $"\nURL: {UtilityService.FormatAdaptiveCardHyperLink(buildIdentifier ,_build.payload.webUrl)}" +
                        $"\nBuild Agent: {_build.payload.agent.name}";
                    break;
                case "BUILD_FINISHED":
                    body.text = $"{dateTimeString}{buildIdentifier} completed with status {_build.payload.statusText}" +
                        $"\nProject: {_build.payload.buildType.projectName}" +
                        $"\nBuild Configuration: {_build.payload.buildType.name}" +
                        $"\nNumber: {_build.payload.number}" +
                        $"\nStarted By: {_build.payload.triggered.user.name}" +
                        $"\nBranch: {_build.payload.branchName}" +
                        $"\nChange Count: {_build.payload.changes.count}" +
                        $"\nURL: {UtilityService.FormatAdaptiveCardHyperLink(buildIdentifier, _build.payload.webUrl)}" +
                        $"\nBuild Agent: {_build.payload.agent.name}";
                    break;
                case "BUILD_INTERRUPTED":
                    body.text = $"{dateTimeString}{buildIdentifier} was interrupted with status {_build.payload.statusText}" +
                        $"\nProject: {_build.payload.buildType.projectName}" +
                        $"\nBuild Configuration: {_build.payload.buildType.name}" +
                        $"\nNumber: {_build.payload.number}" +
                        $"\nStarted By: {_build.payload.triggered.user.name}" +
                        $"\nBranch: {_build.payload.branchName}" +
                        $"\nChange Count: {_build.payload.changes.count}" +
                        $"\nURL: {UtilityService.FormatAdaptiveCardHyperLink(buildIdentifier, _build.payload.webUrl)}" +
                        $"\nBuild Agent: {_build.payload.agent.name}";
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
