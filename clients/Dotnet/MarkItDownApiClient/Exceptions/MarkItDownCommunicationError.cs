using System.Net;

namespace MarkItDownApiClient.Exceptions;

public class MarkItDownCommunicationError(HttpStatusCode statusCode, string? response) : Exception(BuildMessage(statusCode, response))
{
    public HttpStatusCode StatusCode { get; } = statusCode;
    public string? Response { get; } = response;
    
    private static string BuildMessage(HttpStatusCode statusCode, string? response) => 
        $"An exception occurred while processing your request. Server responded with status code {statusCode}: {response ?? "Empty response"}";
}