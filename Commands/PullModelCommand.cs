using System.CommandLine;
using CSLama.Services;

namespace CSLama.Commands;

public static class PullModelCommand
{
    public static Command Create(Option<string> urlOption)
    {
        // Define the model option explicitly
        var modelOption = new Option<string>("--model", "The name of the model to pull (e.g., llama3.2).");

        // Create the command and add the model option
        var command = new Command("pull", "Pull a model to local storage.")
        {
            modelOption
        };

        // Set the handler and bind the options directly
        command.SetHandler(async (string model, string baseUrl) =>
        {
            var apiService = new OllamaApiService(baseUrl);
            var response = await apiService.PullModelAsync(model);
            Console.WriteLine(response);
        }, 
        modelOption,
        urlOption);

        return command;
    }
}
