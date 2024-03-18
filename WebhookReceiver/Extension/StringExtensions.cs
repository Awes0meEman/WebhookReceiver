using Newtonsoft.Json;

namespace WebhookReceiver.Extension
{
    public static class StringExtensions
    {
        public static bool TryParseJson<T>(this string str, out T result)
        {
            bool success = true;
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                Error = (sender, args) => { success = false; args.ErrorContext.Handled = true; },
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
#pragma warning disable CS8601 // Possible null reference assignment.
            result = JsonConvert.DeserializeObject<T>(str, settings);
#pragma warning restore CS8601 // Possible null reference assignment.
            return success;
        }
    }
}
