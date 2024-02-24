using Newtonsoft.Json;
using System.Net.Http.Headers;
using WebhookReceiver.Interfaces;
using WebhookReceiver.Services;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
using ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
ILogger logger = factory.CreateLogger<Program>();

app.MapPost("/webhook", async context =>
{
    string requestBody = "";
    using (StreamReader reader = new StreamReader(context.Request.Body))
    {
        requestBody = await reader.ReadToEndAsync();
    }
    Console.WriteLine(requestBody);
    if (requestBody == null)
    {
        //bad request
        context.Response.StatusCode = 400;
        return;
    }
    Dictionary<string, string> customSection = app.Configuration
        .GetSection("WebhookStrings")
        .Get<Dictionary<string, string>>() ?? new Dictionary<string, string>();
    if (customSection == default(Dictionary<string, string>))
    {
        //bad server configuration
        context.Response.StatusCode = 500;
        return;
    }
    try
    {
        IWebhookReceiveService webhookReceiveService = WebhookRecieveServiceFactory.CreateReceiveService(requestBody, customSection);
        List<IDeliverable> deliverables = webhookReceiveService.GetDeliverables();
        foreach (var deliverable in deliverables)
        {
            if (deliverable == null)
            {
                context.Response.StatusCode = 500;
                return;
            }
            else
            {
                context.Response.StatusCode = 200;
            }
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string? serializedDeliverable = JsonConvert.SerializeObject(deliverable);
            StringContent? webhookContent = new StringContent(serializedDeliverable, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage? response = await client.PostAsync(webhookReceiveService.GetUrl(), webhookContent);
        }
    }
    catch (NotImplementedException ex)
    {
        Stream responseStream = new MemoryStream();
        context.Response.StatusCode = 500;
        string message = ex.Message;
        using (StreamWriter? writer = new StreamWriter(responseStream))
        {
            writer.Write(message);
            await responseStream.CopyToAsync(context.Response.Body);
        }
    }
});

app.Run();