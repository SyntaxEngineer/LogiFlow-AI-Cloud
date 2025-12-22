using Azure;
using Azure.AI.TextAnalytics;
using Azure.Data.Tables;
using LogiFlowBackend;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;


namespace LogiFlowBackend
{
    public class ProcessShipment
    {
        private readonly ILogger<ProcessShipment> _logger;

        public ProcessShipment(ILogger<ProcessShipment> logger)
        {
            _logger = logger;
        }

        [Function("ProcessShipment")]
        [TableOutput("ShipmentLogs", Connection = "AzureWebJobsStorage")]
        public ShipmentLog Run(
            [QueueTrigger("shipment-queue", Connection = "AzureWebJobsStorage")] string queueMessage)
        {
            try
            {


                // 1. Setup AI Client
                string aiEndpoint = Environment.GetEnvironmentVariable("AI_Endpoint");
                string aiKey = Environment.GetEnvironmentVariable("AI_Key");
                var aiClient = new TextAnalyticsClient(new Uri(aiEndpoint), new AzureKeyCredential(aiKey));


                var shipmentData = JsonSerializer.Deserialize<ShipmentData>(queueMessage);


                string sentiment = "Unknown";
                try
                {
                    DocumentSentiment result = aiClient.AnalyzeSentiment(shipmentData.Notes);
                    sentiment = result.Sentiment.ToString();
                }
                catch (RequestFailedException ex)
                {

                    _logger.LogError($"AI Service failed: {ex.Message}");
                    sentiment = "Manual Review Needed";
                }


                return new ShipmentLog
                {
                    PartitionKey = shipmentData.City,
                    RowKey = shipmentData.Id,
                    Status = "Processed",
                    Notes = shipmentData.Notes,
                    Sentiment = sentiment
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Critical failure: {ex.Message}");
                throw;
            }
        }
    }


    public class ShipmentData
    {
        public string Id { get; set; }
        public string City { get; set; }
        public string Notes { get; set; }
    }

}
