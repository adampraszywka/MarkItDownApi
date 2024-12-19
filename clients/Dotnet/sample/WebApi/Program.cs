using MarkItDownApiClient.Client;
using MarkItDownApiClient.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMarkItDownApiClient(new Uri("http://127.0.0.1:8000"));

var app = builder.Build();

app.MapGet("/", async (MarkItDownClient client) =>
{
    var file = await File.ReadAllBytesAsync("sample.pdf");
    var fileStream = new MemoryStream(file);
    
    var content = await client.Read(fileStream);
    
    return content;
});

app.Run();