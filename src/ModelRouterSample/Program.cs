using Azure.AI.OpenAI;
using ModelRouterSample.Utils;
using OpenAI.Chat;
using System.ClientModel;
using ChatMessage = OpenAI.Chat.ChatMessage;

Console.CancelKeyPress += (sender, e) =>
{
    // Prevent the process from terminating immediately
    e.Cancel = true;

    // Inform the user that cancellation was detected (e.g., Ctrl+C)
    ConsoleHelper.WriteToConsole($"{Environment.NewLine}[yellow]Cancellation requested. Exiting...[/]");

    // Exit the application gracefully with a success code (0)
    Environment.Exit(0);
};

// Load configuration from user input
var (endpoint, apiKey, deploymentModel) = GetConfiguration();

// Create Azure OpenAI chat client
var chatClient = new AzureOpenAIClient(
    new Uri(endpoint),
    new ApiKeyCredential(apiKey)).GetChatClient(deploymentModel);

// Display the application header
ConsoleHelper.ShowHeader();

// create a list of chat messages
List<ChatMessage> chatMessages = [];

// chat loop
while (true)
{
    try
    {
        // Prompt the user for input without clearing the console
        string userInput =
            ConsoleHelper.GetString("Enter your message:", false);

        // Add the user message to the ongoing chat history
        chatMessages.Add(ChatMessage.CreateUserMessage(userInput));

        // Print a separator and label for the AI response
        Console.WriteLine();
        Console.WriteLine("AI:");

        // Send the chat history to the Azure OpenAI model for completion
        ClientResult<ChatCompletion> result =
            await chatClient.CompleteChatAsync(chatMessages);

        // Extract the first response from the model output
        string aiResponse =
            result.Value.Content[0].Text;

        // Display the AI's response in the console using Spectre.Console markup
        ConsoleHelper.WriteToConsole(aiResponse);

        // Add the AI's response to the chat history to maintain context
        chatMessages.Add(ChatMessage.CreateAssistantMessage(aiResponse));

        // Retrieve and display token usage information
        ChatTokenUsage usage =
            result.Value.Usage;
        ConsoleHelper.WriteResponseInformation(
            result.Value.Model,
            usage.InputTokenCount,
            usage.OutputTokenCount);

        // Add extra spacing for the next input loop
        Console.WriteLine();
    }
    catch (Exception ex)
    {
        // Handle and display any unexpected errors
        // (e.g., network or API issues)
        ConsoleHelper.WriteToConsole(
            $"[red]An error occurred: {ex.Message}[/]");
        Console.WriteLine();
    }
}

/// <summary>
/// Prompts the user to enter endpoint, API key, and model name.
/// </summary>
/// <returns>A tuple containing endpoint, API key, and model name.</returns>
static (string Endpoint, string ApiKey, string ModelName) GetConfiguration()
{
    string endpoint =
        ConsoleHelper.GetUrl("Enter your [yellow]Azure OpenAI[/] endpoint:");

    string apiKey =
        ConsoleHelper.GetString("Enter your [yellow]Azure OpenAI[/] API key:");

    string modelName =
        ConsoleHelper.GetString("Enter your [yellow]Model Router[/] model name:");

    return (endpoint, apiKey, modelName);
}