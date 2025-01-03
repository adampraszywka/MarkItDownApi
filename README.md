# MarkItDownApi - HTTP wrapper for MarkItDown

## Overview

This repository provides an HTTP wrapper for Microsoft's **MarkItDown** tool, enabling users to easily integrate and utilize its functionality via RESTful API endpoints. This project simplifies the process of interacting with MarkItDown by abstracting the tool's core features into an accessible web service.

## Features

- **Easy Integration**: Use RESTful APIs to interact with MarkItDown without diving into its internal implementation.
- **Technology Agnostic**: Use Docker to deploy the service on any platform, utilize generic HTTP interface to integrate with any technology stack.
- **Client libraries**: Use the provided client libraries to interact with the service in your preferred programming language.

## Quick Start

### Start Docker Container

```bash
  docker run --rm --name markitdown-api -p 5000:80 ghcr.io/adampraszywka/markitdownapi:main
```
### Convert Markdown to HTML using HTTP request
    
Request (curl)
```bash
  curl -X POST 'http://localhost:5000/read' --form 'file=@"path/to/your/file.pdf"'
```
Example response
```json
{
    "filename": "file.pdf",
    "content": {
        "title": "Lorem Ipsum",
        "text_content": "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s..."
}
```

### .NET Client Library

1. Install the package from NuGet:
   ```bash
   dotnet add package MarkItDownApi.Client
   ```
2. Register client in your dependency injection container:
   ```csharp
   builder.Services.AddMarkItDownApiClient(new Uri("http://localhost:5000"));
   ```
3. Use the client in your code:
   ```csharp
    app.MapGet("/", async (MarkItDownClient client) => {
        var file = await File.ReadAllBytesAsync("sample.pdf");
        var fileStream = new MemoryStream(file);
    
        var content = await client.Read(fileStream);
        
        return content;
    });
    ```

## Endpoints

### POST /read
Converts a document to Markdown format.

Request:
```bash
   curl -X POST 'http://localhost:5000/read' --form 'file=@"path/to/your/file.pdf"'
   ```

Response:
```json
{
    "filename": "[FILENAME]",
    "content": {
        "title": "[TITLE EXTRACTED FROM THE DOCUMENT]",
        "text_content": "[CONTENT EXTRACTED FROM THE DOCUMENT]"
}
```

### GET /ping
Health check endpoint.

Request:
```bash
   curl -X GET 'http://localhost:5000/ping'
   ```
Response:
```json
{
    "status": "ok"
}
```
## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## Acknowledgments

- Thanks to Microsoft for the MarkItDown tool.
- Inspired by the need for simpler API integrations for Markdown processing.

---

Feel free to open an issue or contact us for support and questions.

