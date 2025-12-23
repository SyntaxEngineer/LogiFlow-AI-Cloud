# 🚛 LogiFlow AI - Intelligent Cloud Logistics Platform

![License](https://img.shields.io/badge/license-MIT-blue.svg) ![Status](https://img.shields.io/badge/status-MVP%20Complete-green.svg) ![Azure](https://img.shields.io/badge/azure-%230072C6.svg?style=flat&logo=microsoftazure&logoColor=white) ![.NET](https://img.shields.io/badge/.NET-512BD4?style=flat&logo=dotnet&logoColor=white) ![React](https://img.shields.io/badge/react-%2320232a.svg?style=flat&logo=react&logoColor=%2361DAFB)

**LogiFlow AI** is a cloud-native, event-driven application designed to automate risk assessment in logistics. It ingests driver logs in real-time, uses **Azure AI** to perform sentiment analysis on the text, and flags high-risk situations (like breakdowns or accidents) on a live dashboard.

## Dashboard Screenshot
<img width="1898" height="933" alt="image" src="https://github.com/user-attachments/assets/8e34ad83-3420-4986-8a4a-3aac679ab3c5" />
<img width="1919" height="933" alt="image" src="https://github.com/user-attachments/assets/4efe5c05-2133-4cc9-a81b-2dd9c0eb67ec" />

## 🚀 Key Features
* **Event-Driven Architecture:** Uses Azure Storage Queues to decouple the frontend from the backend, allowing for high-volume data ingestion without latency.
* **AI-Powered Analysis:** Integrates Azure Cognitive Services to automatically classify driver notes as Positive, Negative (Risk), or Neutral.
* **Resilient Backend:** Implements the "Retry Pattern" and "Poison Queues" to ensure no data is lost during processing failures.
* **Serverless Cost-Efficiency:** Built on Azure Functions (Consumption Plan), scaling to zero when not in use.

## 🛠️ Tech Stack
* **Cloud:** Microsoft Azure (Functions, Queue Storage, Table Storage, AI Language Service)
* **Backend:** .NET 8 (Isolated Worker Model)
* **Frontend:** React + Vite (Single Page Application)
* **Language:** C# (Backend), JavaScript (Frontend)


## ☁️ Cloud Infrastructure
### 📸 Snapshot
<img width="1825" height="652" alt="image" src="https://github.com/user-attachments/assets/53208563-c0cb-475f-9827-c30cdfca20c0" />

## ⚙️ Key Resources Used - **Azure Functions (.NET 8 Isolated)** Acts as the serverless compute engine.
- **PostShipment** HTTP API Gateway that ingests data and immediately offloads it to a queue.
- **ProcessShipment** Background worker triggered whenever a new message hits the queue.
- **GetShipmentLogs** API to fetch processed data for the frontend.
### Azure Storage Queues - Acts as a buffer between the frontend and AI processing logic.
- **Why?** Decouples the system. If 1,000 drivers submit logs instantly, the queue holds messages so the database isn't overwhelmed.
### Azure Table Storage - A NoSQL key-value store used for high-speed writing and reading of shipment logs.
- **Why?** More cost-effective and faster for this specific use case than a relational SQL database.
### Azure AI Language Service - Provides Natural Language Processing (NLP) to detect sentiment in driver notes: **Positive, Negative, Neutral, Mixed**.


## 💻 How to Run Locally
### Prerequisites
- Visual Studio 2022 (with Azure Development workload)
- Node.js & npm
- Azure Storage Emulator (**Azurite**) running locally
- Active Azure Subscription (for AI Language Service Key)
### Step 1: Backend Setup
1. Clone the repository.
2. Open `LogiFlowBackend.sln` in Visual Studio.
3. Create a `local.settings.json` file in the project root:
{ "IsEncrypted": false, "Values": { "AzureWebJobsStorage": "UseDevelopmentStorage=true", "FUNCTIONS_WORKER_RUNTIME": "dotnet-isolated", "AI_Endpoint": "YOUR_AZURE_AI_ENDPOINT_HERE", "AI_Key": "YOUR_AZURE_AI_KEY_HERE" } }


## Future Improvements
* Authentication: Add Azure AD B2C to secure the "New Entry" form.
* Real-time Updates: Replace polling with Azure SignalR Service for instant dashboard updates.
* Maps Integration: Add Azure Maps to visualize truck locations based on city data.

## 🏗️ Architecture Flow
```mermaid
graph TD
    User([User / Truck Driver]) -->|Submits Form| React[React Frontend]
    React -->|POST JSON| HTTP_Func[Azure Function: Ingest Data]
    HTTP_Func -->|Push Message| Queue[(Azure Storage Queue)]
    
    Queue -->|Trigger| Process_Func[Azure Function: ProcessShipment]
    Process_Func -->|Analyze Text| AI[Azure AI Language Service]
    AI -->|Return Sentiment| Process_Func
    
    Process_Func -->|Save Result| Table[(Azure Table Storage)]
    
    React -->|Polls Data| Get_Func[Azure Function: Get Logs]
    Get_Func -->|Read Data| Table```
