using Azure.Data.Tables;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace LogiFlowBackend;

public class GetShipmentLogs
{
    private readonly ILogger<GetShipmentLogs> _logger;

    public GetShipmentLogs(ILogger<GetShipmentLogs> logger)
    {
        _logger = logger;
    }

    [Function("GetShipmentLogs")]

    public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequestData req)
    {
        // Connect to Table Storage
        string connectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
        var tableClient = new TableClient(connectionString, "ShipmentLogs");

        
        var logs = new List<ShipmentLog>();

        // NOTE: In a real app, you would use pagination. Here we get all for simplicity.
        await foreach (var log in tableClient.QueryAsync<ShipmentLog>())
        {
            logs.Add(log);
        }

        // Return JSON
        var response = req.CreateResponse(System.Net.HttpStatusCode.OK);
        await response.WriteAsJsonAsync(logs);
        return response;
    }
}




