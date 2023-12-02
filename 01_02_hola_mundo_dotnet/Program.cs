using Azure.AI.OpenAI;

var key = Environment.GetEnvironmentVariable("OPENAI_API_KEY");

var client = new OpenAIClient(key);

var options = new ChatCompletionsOptions();
options.DeploymentName = "gpt-3.5-turbo";
options.Messages.Add(new ChatMessage(ChatRole.User, "¡Hola Mundo!"));

var response = await client.GetChatCompletionsAsync(options);

foreach (var c in response.Value.Choices)
{
    Console.WriteLine(c.Message.Content);
}