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
        public string number { get; set; } = "";
        public string branchName { get; set; } = "";
        public string webUrl { get; set; } = "";
        public string statusText { get; set; } = "";
        public BuildType buildType { get; set; } = new BuildType();
        public string startDate { get; set; } = "";
        public string finishDate { get; set; } = "";
        public Triggered triggered { get; set; } = new Triggered();
        public Changes changes { get; set; } = new Changes();
        public Agent agent { get; set; } = new Agent();
    }
    public class BuildType
    {
        public string name { get; set; } = "";
        public string projectName { get; set; } = "";
    }
    public class Triggered
    {
        public User user { get; set; } = new User();
    }
    public class Changes
    {
        public int count { get; set; }
    }
    public class Agent
    {
        public string name { get; set; } = "";
    }
    public class User
    {
        public string name { get; set; } = "";
    }
}
