# Getting Started with Model Router in Azure AI Foundry Using C#

![header](/docs/header.png)

This repository provides a .NET 9 console application that integrates the `Model Router`. 

Build Smarter AI Apps with Model Router, Azure AI Foundry, and .NET — Route Requests Dynamically with Ease

## 🧠 Overview

- Deploy a **Model Router** from Azure AI Foundry to automatically route chat prompts to the most appropriate underlying model
- Gain cost efficiencies by using smaller models for simpler prompts and larger ones for complex reasoning
- Single deployment interface with no need to call each model individually

## 🔍 Supported Models & Versioning

As of **June 23, 2025**, *Model Router v2025‑05‑19* includes the following underlying models

| Model                | Version       |
|----------------------|---------------|
| GPT‑4.1              | 2025‑04‑14    |
| GPT‑4.1‑mini         | 2025‑04‑14    |
| GPT‑4.1‑nano         | 2025‑04‑14    |
| o4‑mini              | 2025‑04‑16    |

- If you enable **Auto‑Update** during deployment, the router will automatically receive newer model
- Each router version is tied to a specific, fixed set of underlying models. 🚧 Changes may affect performance and cost.

## ⚙️ Prerequisites

- .NET 9 SDK
- Azure AI Foundry resource with Model Router deployed
- Visual Studio 2022 or VS Code
- Azure subscription with Azure OpenAI access

## 🏃‍♂️‍➡️ Usage

Upon running the application, it will prompt you to enter your credentials and configuration.

![header](/docs/ai-model-router-01.png)

![header](/docs/ai-model-router-02.png)

![header](/docs/ai-model-router-03.png)

Now you are able to chat normally. 

![header](/docs/ai-model-router-04.png)

More complex requests will send to a greater underlying model.

![header](/docs/ai-model-router-05.png)

## 🖊️ Blog Posts

If you are more interested into details, please see the following post on [medium.com](https://medium.com/@tsjdevapps):

- [Getting Started with Model Router in Azure AI Foundry Using C#](https://medium.com/medialesson/getting-started-with-model-router-in-azure-ai-foundry-using-c-d17a10681a3f)