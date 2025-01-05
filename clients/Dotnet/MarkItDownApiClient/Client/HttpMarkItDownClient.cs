using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using MarkItDownApiClient.Dto;
using MarkItDownApiClient.Exceptions;

namespace MarkItDownApiClient.Client;

public class HttpMarkItDownClient(HttpClient client) : MarkItDownClient
{
    private const string ReadEndpoint = "read";
    
    public async Task<Response> Read(Stream stream)
    {
        using var content = new StreamContent(stream);
        
        using var formData = new MultipartFormDataContent();
        formData.Add(content, "file", "file.txt");
        
        using var response = await client.PostAsync(ReadEndpoint, formData);

        switch (response.StatusCode)
        {
            case HttpStatusCode.UnsupportedMediaType:
            {
                var error = await ReadErrorBody(response);
                throw new UnsupportedMediaFormat(error.Detail);
            }
            case HttpStatusCode.BadRequest:
            {
                var error = await ReadErrorBody(response);
                throw new FileConversionFailure(error.Detail);
            }
            case HttpStatusCode.RequestEntityTooLarge:
            {
                throw new FileTooLarge();
            }
        }

        if (!response.IsSuccessStatusCode)
        {
            var error = await ReadErrorBody(response);
            throw new UnknownProblem(response.StatusCode, error?.Detail, null);
        }
        
        try
        {
            var responseContent = await response.Content.ReadFromJsonAsync<Response>(new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
            });
            if (responseContent is null)
            {
                var error = await ReadErrorBody(response);
                throw new UnknownProblem(response.StatusCode, error?.Detail, null);
            }

            return responseContent;
        }
        catch (Exception ex)
        {
            var error = await ReadErrorBody(response);
            throw new UnknownProblem(response.StatusCode, error?.Detail, ex);
        }
    }

    private static async Task<Error> ReadErrorBody(HttpResponseMessage response)
    {
        try
        {
            var error = await response.Content.ReadFromJsonAsync<Error>();
            return error ?? new Error("An unknown problem occurred. Cannot read error content.");
        }
        catch (Exception)
        {
            return new Error("An unknown problem occurred. Cannot read error content.");
        }
    }
}