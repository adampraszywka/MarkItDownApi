namespace MarkItDownApiClient.Dto;

public class Error(string detail)
{
    public string Detail { get; } = detail;
}