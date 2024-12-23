using System.CommandLine;
using CSLama.Services;
using CSLama.Models;

namespace CSLama.Commands;

public static class GenerateCommand
{
    public static Command Create(Option<string> urlOption)
    {
        var modelOption = new Option<string>("--model", "The name of the model to use (e.g., llama3.2).");
        var promptOption = new Option<string>("--prompt", "The prompt to generate a response for.");
        var streamOption = new Option<bool>("--stream", () => false, "Whether to stream the response.");
        var saveOption = new Option<string?>("--save", "Path to save the generated response to.");

        var command = new Command("generate", "Generate a response for a given prompt.")
        {
            modelOption,
            promptOption,
            streamOption,
            saveOption
        };

        command.SetHandler(async (string model, string prompt, bool stream, string baseUrl, string? savePath) =>
        {
            try
            {
                var apiService = new OllamaApiService(baseUrl);
                var response = await apiService.GenerateCompletionAsync(model, prompt, stream);

                if (response is OllamaGenerateResponse generateResponse)
                {
                    // If --save is provided, write the response to the specified file
                    if (!string.IsNullOrEmpty(savePath))
                    {
                        try
                        {
                            if (File.Exists(savePath))
                            {
                                File.Delete(savePath);
                            }
                            await File.WriteAllTextAsync(savePath, generateResponse.Response ?? string.Empty);
                            Console.WriteLine($"Response saved to: {savePath}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"An error occurred: {ex.Message}");
                        }
                    }
                    else
                    {
                        // Output structured response
                        Console.WriteLine($"Model: {generateResponse.Model}");
                        Console.WriteLine($"Created At: {generateResponse.CreatedAt}");
                        Console.WriteLine($"Response:\n{generateResponse.Response}");
                        Console.WriteLine($"Done: {generateResponse.Done}");
                        Console.WriteLine($"Done Reason: {generateResponse.DoneReason}");
                        Console.WriteLine($"Context: {string.Join(", ", generateResponse.Context)}");
                        Console.WriteLine($"Total Duration: {generateResponse.TotalDuration} ns");
                        Console.WriteLine($"Load Duration: {generateResponse.LoadDuration} ns");
                        Console.WriteLine($"Prompt Eval Count: {generateResponse.PromptEvalCount}");
                        Console.WriteLine($"Prompt Eval Duration: {generateResponse.PromptEvalDuration} ns");
                        Console.WriteLine($"Eval Count: {generateResponse.EvalCount}");
                        Console.WriteLine($"Eval Duration: {generateResponse.EvalDuration} ns");
                    }
                }
                else
                {
                    Console.WriteLine("Unexpected response format.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        },
        modelOption,
        promptOption,
        streamOption,
        urlOption,
        saveOption);

        return command;
    }
}
