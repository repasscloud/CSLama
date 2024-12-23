using System.CommandLine;
using CSLama.Commands;

namespace CSLama;

class Program
{
    static async Task Main(string[] args)
    {
        // Define the root command
        var rootCommand = new RootCommand("CSLama: A CLI for interacting with the Ollama API.");

        // Add a global option for the base URL
        var urlOption = new Option<string>("--url", () => "http://localhost:11434/api/", "The base URL for the Ollama API.");
        rootCommand.AddGlobalOption(urlOption);

        // Add subcommands
        rootCommand.AddCommand(GenerateCommand.Create(urlOption));
        rootCommand.AddCommand(ListModelsCommand.Create(urlOption));
        rootCommand.AddCommand(PullModelCommand.Create(urlOption));

        // Parse and execute the command
        await rootCommand.InvokeAsync(args);
    }
}
