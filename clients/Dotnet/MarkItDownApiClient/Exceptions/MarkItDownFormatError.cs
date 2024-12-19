namespace MarkItDownApiClient.Exceptions;

public class MarkItDownFormatError(string? response, Exception? innerException) : Exception(BuildMessage(response), innerException)
{
    public string? Response { get; } = response;
    
    private static string BuildMessage(string? response) =>
        $"MarkItDown returned the following response: {response} but client was not able to deserialize it.";
}