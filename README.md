# CSLama

CSLama is a .NET 8 console application that interacts with the Ollama API, enabling you to manage models, generate completions, and explore available models efficiently.

## Features

- Generate text completions from a specified model.
- List all locally available models.
- Pull new models to your local environment.
- Save generated responses to a file for reuse or analysis.

## Prerequisites

1. **Install .NET SDK 8.x**  
    Ensure you have the .NET 8 SDK installed on your system.

1. **Ollama API Running**  
    Start the Ollama API server locally (default: http://localhost:11434/api/).

Example Docker setup:

```bash
docker volume create ollama-local
docker run -d -v ollama-local:/root/.ollama -p 11434:11434 --name ollama ollama/ollama
```

## Installation

1. Clone the CSLama repo:

    ```bash
    git clone https://github.com/repasscloud/CSLama.git
    cd CSLama
    ```

1. Restore dependencies:

    ```bash
    dotnet restore
    ```

1. Build the project:

    ```bash
    dotnet build
    ```

1. Run the application:

    ```bash
    dotnet run -- [command]
    ```

## Usage

### General Structure

CSLama commands follow this structure:

```bash
dotnet run -- <command> [options]
```

### Available Commands

#### Generate Text

Generate a completion using a specific model and prompt.

```bash
dotnet run -- generate --model:<model_name> --prompt:<your_prompt> [--save:<file_path>] [--stream]
```

- `--model`: The model to use (e.g., `llama3.2:3b`).
- `--prompt`: The text to prompt the model with.
- `--save`: _(Optional)_ Path to save the generated response.
- `--stream`: _(Optional)_ Stream the response in real-time.

Example:

```bash
dotnet run -- generate --model:llama3.2:3b --prompt:"Write a haiku about the ocean" --save:output.txt
```

#### List Models

List all locally available models.

```bash
dotnet run -- list-models
```

Example Output:

```yaml
Available Models:
- Name: llama3.2:3b
  Size: 4.2GB
  Format: GGUF
  Parameter Size: 3B
  Quantization Level: Q4_0
```

#### Pull a Model

Download a specific model to your local environment.

```bash
dotnet run -- pull --model:<model_name>
```

Example:

```bash
dotnet run -- pull --model:llama3.2:3b
```

## Configuration

### Change the API Base URL

If your Ollama API is not running on the default `http://localhost:11434/api/`, you can specify a custom URL using the `--url` option:

```bash
dotnet run -- <command> --url:http://your-api-url:port/api/
```

Example:

```bash
dotnet run -- generate --model:llama3.2:3b --prompt:"Hello, world!" --url:http://192.168.1.100:11434/api/
```

## Contributing

Contributions are welcome! Feel free to open an issue or submit a pull request.

## License

[MIT LICENSE](LICENSE.md)