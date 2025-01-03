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

        if (!response.IsSuccessStatusCode)
        {
            throw new MarkItDownCommunicationError(response.StatusCode, await response.Content.ReadAsStringAsync());
        }

        try
        {
            var responseContent = await response.Content.ReadFromJsonAsync<Response>(new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
            });
            if (responseContent is null)
            {
                throw new MarkItDownFormatError(await response.Content.ReadAsStringAsync(), null);
            }

            return responseContent;
        }
        catch (Exception ex)
        {
            throw new MarkItDownFormatError(await response.Content.ReadAsStringAsync(), ex);
        }
    }
}