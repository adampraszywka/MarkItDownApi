using System.Net;

namespace MarkItDownApiClient.Exceptions;

public class UnknownProblem(HttpStatusCode statusCode, string? response, Exception? innerException) : Exception(BuildMessage(response), innerException)
{
    public HttpStatusCode StatusCode { get; } = statusCode;
    public string? Response { get; } = response;

    private static string BuildMessage(string? response) => response ?? "An unknown problem occurred.";
}