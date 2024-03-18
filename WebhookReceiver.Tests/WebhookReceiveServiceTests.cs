using WebhookReceiver.Interfaces;
using WebhookReceiver.Models;

namespace WebhookReceiver.Tests
{
    public class WebhookReceiveServiceTests
    {
        [Fact]
        public void TeamCity_Webhook_Receive_Service_Gets_Deliverable()
        {
            //Arrange
            var build = new Build();
            var url = "";
            var teamCityWebhookReceiveService = new TeamCityWebhookReceiveService(build, url);
            //Act
            var deliverable = teamCityWebhookReceiveService.GetDeliverables();
            //Assert
            Assert.NotNull(deliverable);
        }

        [Fact]
        public void Octopus_Webhook_Receive_Service_Gets_Deliverable()
        {
            //Arrange
            var build = new Deploy();
            var url = "";
            var octopusWebhookReceiveService = new OctopusWebhookReceiveService(build, url);
            //Act
            var deliverable = octopusWebhookReceiveService.GetDeliverables();
            //Assert
            Assert.NotNull(deliverable);
        }

        [Theory]
        [InlineData("BUILD_STARTED")]
        [InlineData("BUILD_FINISHED")]
        [InlineData("BUILD_INTERRUPTED")]
        [InlineData("CHANGES_LOADED")]

        public void TeamCity_Webhook_Receive_Service_Deliverable_Has_Content(string eventType)
        {
            //Arrange
            var build = new Build();
            build.eventType = eventType;
            var url = "";
            var teamCityWebhookReceiveService = new TeamCityWebhookReceiveService(build, url);
            //Act
            var deliverable = teamCityWebhookReceiveService.GetDeliverables();
            //Assert
            Assert.NotNull(deliverable);
        }
    }
}
