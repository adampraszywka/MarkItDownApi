# MarkItDownApi.Client - .NET Client Library for MarkItDown HTTP wrapper

This library is a part of the MarkItDownApi project. It provides a simple way to interact with the self-hosted MarkItDown HTTP wrapper using .NET applications. The library abstracts the HTTP communication with the MarkItDownApi service, allowing developers to focus on their application logic.
For more information about the MarkItDownApi project, check the README.md file in the main repository.

## How to start

### Start Docker Container

```bash
  docker run --rm --name markitdown-api -p 5000:80 ghcr.io/adampraszywka/markitdownapi:main
```

### Install the package from NuGet

```bash
  dotnet add package MarkItDownApi.Client
```

### Register client in your dependency injection container

```csharp
  builder.Services.AddMarkItDownApiClient(new Uri("http://localhost:5000"));
```

### Use the client in your code

```csharp
  app.MapGet("/", async (MarkItDownClient client) => {
      var file = await File.ReadAllBytesAsync("sample.pdf");
      var fileStream = new MemoryStream(file);
  
      var content = await client.Read(fileStream);
      
      return content;
  });
```