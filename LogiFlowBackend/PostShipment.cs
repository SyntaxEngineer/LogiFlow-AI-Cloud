using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace LogiFlowBackend;

public class PostShipment
{
    private readonly ILogger<PostShipment> _logger;

    public PostShipment(ILogger<PostShipment> logger)
    {
        _logger = logger;
    }

    [Function("PostShipment")]
    [QueueOutput("shipment-queue", Connection = "AzureWebJobsStorage")]
    public async Task<string> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
    {
        

        _logger.LogInformation("Received new shipment data from React.");

        // Read the JSON body from the React request
        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

        // Return it directly to the Queue!
        return requestBody;
    }
}