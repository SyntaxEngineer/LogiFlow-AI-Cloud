# 🚛 LogiFlow AI - Intelligent Cloud Logistics Platform

**LogiFlow AI** is a cloud-native, event-driven application designed to automate risk assessment in logistics. It ingests driver logs in real-time, uses **Azure AI** to perform sentiment analysis on the text, and flags high-risk situations (like breakdowns or accidents) on a live dashboard.

![Dashboard Screenshot](path/to/your/dashboard-screenshot.png)
*(Add your screenshot to the repo and link it here)*

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

## 🏗️ Architecture Flow
```mermaid
graph TD
    User([User]) -->|1. Submit Log| Client[React Dashboard]
    Client -->|2. HTTP POST| Ingest[Azure Function: Ingest]
    Ingest -->|3. Enqueue| Queue[(Storage Queue)]
    Queue -->|4. Trigger| Processor[Azure Function: Processor]
    Processor -->|5. Analyze Sentiment| AI[Azure AI Service]
    Processor -->|6. Store Result| DB[(Table Storage)]
    Client -->|7. Poll Data| DB
