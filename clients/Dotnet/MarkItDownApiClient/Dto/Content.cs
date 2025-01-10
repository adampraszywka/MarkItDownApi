namespace MarkItDownApiClient.Dto;

public class Content(string title, string textContent)
{
    public string Title { get; } = title;
    public string TextContent { get; } = textContent;
}