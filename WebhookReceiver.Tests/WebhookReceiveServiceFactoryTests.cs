using WebhookReceiver.Interfaces;

namespace WebhookReceiver.Tests
{
    public class WebhookReceiveServiceFactoryTests
    {
        private string GetTeamCityJson()
        {
            return @"{
  ""eventType"": ""eventType"",
  ""payload"": {
        ""buildType"": {
            ""name"": ""name""
        },
        ""number"": ""1234"",
        ""branchName"": ""master"",
        ""startDate"": ""1/1/2020"",
        ""agent"": {
            ""name"": ""bond, james bond""
        },
        ""triggered"": {
            ""user"": {
                ""name"" : ""username""
            }
        }
    }
}";
        }

        private Dictionary<string,string> GetCustomSection()
        {
            var configuration = new Dictionary<string, string>();
            configuration.Add("OctopusWebHookString", "octopusURL");
            configuration.Add("TeamCityWebhookString", "teamCityURL");
            return configuration;
        }

        private string GetOctopusJson()
        {
            return @"{
  ""Timestamp"": ""0001-01-01T00:00:00+00:00"",
  ""EventType"": ""SubscriptionPayload"",
  ""Payload"": {
    ""ServerUri"": ""http://my-octopus.com"",
    ""ServerAuditUri"": ""http://my-octopus.com"",
    ""Subscription"": {},
    ""Event"": {},
    ""BatchProcessingDate"": ""0001-01-01T00:00:00+00:00"",
    ""BatchId"": ""[guid]"",
    ""TotalEventsInBatch"": ""3"",
    ""EventNumberInBatch"": ""10""
  }
}";
        }

        [Fact]
        public void Webhook_Receive_Service_Factory_Does_Create_TeamCity_Webhook_Receiver_Instance()
        {
            //Arrange
            var json = GetTeamCityJson();
            var customSection = GetCustomSection();
            //Act
            IWebhookReceiveService webhookReceiveService = WebhookRecieveServiceFactory.CreateReceiveService(json, customSection);
            //Assert
            Assert.True(webhookReceiveService.GetType() == typeof(TeamCityWebhookReceiveService));
        }

        [Fact]
        public void Webhook_Receive_Service_Factory_Does_Create_Octopus_Webhook_Receiver_Instance()
        {
            //Arrange
            var json = GetOctopusJson();
            var customSection = GetCustomSection();
            //Act
            IWebhookReceiveService webhookReceiveService = WebhookRecieveServiceFactory.CreateReceiveService(json, customSection);
            //Assert
            Assert.True(webhookReceiveService.GetType() == typeof(OctopusWebhookReceiveService));
        }

        [Fact]
        public void Webhook_Receive_Service_Factory_Does_Get_Correct_TeamCity_Configuration()
        {
            //Arrange
            var json = GetTeamCityJson();
            var customSection = GetCustomSection();
            //Act
            IWebhookReceiveService webhookReceiveService = WebhookRecieveServiceFactory.CreateReceiveService(json, customSection);
            //Assert
            Assert.True(webhookReceiveService.GetUrl() == "teamCityURL");
        }

        [Fact]
        public void Webhook_Receive_Service_Factory_Does_Get_Correct_Octopus_Configuration()
        {
            //Arrange
            var json = GetOctopusJson();
            var customSection = GetCustomSection();
            //Act
            IWebhookReceiveService webhookReceiveService = WebhookRecieveServiceFactory.CreateReceiveService(json, customSection);
            //Assert
            Assert.True(webhookReceiveService.GetUrl() == "octopusURL");
        }
    }
}