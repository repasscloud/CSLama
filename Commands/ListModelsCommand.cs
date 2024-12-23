using System.CommandLine;
using CSLama.Services;

namespace CSLama.Commands;

public static class ListModelsCommand
{
    public static Command Create(Option<string> urlOption)
    {
        var command = new Command("list-models", "List locally available models.");

        command.SetHandler(async (string baseUrl) =>
        {
            var apiService = new OllamaApiService(baseUrl);
            var response = await apiService.ListModelsAsync();

            if (response?.Models == null || !response.Models.Any())
            {
                Console.WriteLine("No models found.");
                return;
            }

            Console.WriteLine("Available Models:");
            foreach (var model in response.Models)
            {
                Console.WriteLine($"- Name: {model.Name}");
                Console.WriteLine($"  Model: {model.ModelName}");
                Console.WriteLine($"  Modified At: {model.ModifiedAt}");
                Console.WriteLine($"  Size: {model.Size} bytes");
                Console.WriteLine($"  Digest: {model.Digest}");
                if (model.Details != null)
                {
                    Console.WriteLine($"  Format: {model.Details.Format}");
                    Console.WriteLine($"  Parameter Size: {model.Details.ParameterSize}");
                    Console.WriteLine($"  Quantization Level: {model.Details.QuantizationLevel}");
                }
                Console.WriteLine();
            }
        }, urlOption);

        return command;
    }
}
