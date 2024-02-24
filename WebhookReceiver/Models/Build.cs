using WebhookReceiver.Interfaces;

namespace WebhookReceiver.Models
{
    public class Build : IReceivable
    {
        public string eventType { get; set; } = "";
        public Payload payload { get; set; } = new Payload();
    }
    public class Payload
    {
        public BuildType buildType { get; set; } = new BuildType();
        public string number { get; set; } = "";
        public string branchName { get; set; } = "";
        public string startDate { get; set; } = "";
        public Agent agent { get; set; } = new Agent();
        public Triggered triggered { get; set; } = new Triggered();
    }
    public class BuildType
    {
        public string name { get; set; } = "";
    }
    public class Agent
    {
        public string name { get; set; } = "";
    }
    public class Triggered
    {
        public User user { get; set; } = new User();
    }
    public class User
    {
        public string name { get; set; } = "";
    }
}
