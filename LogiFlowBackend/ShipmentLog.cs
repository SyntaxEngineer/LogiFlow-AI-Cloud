using Azure;
using Azure.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogiFlowBackend
{
    public class ShipmentLog : ITableEntity
    {
        // 1. PartitionKey: Azure group similar data together for faster searching.
        public string PartitionKey { get; set; }

        // 2. RowKey: A unique ID for this specific log entry (e.g., the Shipment ID).
        public string RowKey { get; set; }

        // Standard properties required by Azure (Timestamp, ETag). 
        // We don't need to set these manually; Azure handles them.
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }

        // 3. Our Custom Data: The actual info we want to save.
        public string Status { get; set; } 
        public string Notes { get; set; }


        // NEW: Store the AI analysis
        public string Sentiment { get; set; } // "Positive", "Negative", "Neutral"
    }
}
