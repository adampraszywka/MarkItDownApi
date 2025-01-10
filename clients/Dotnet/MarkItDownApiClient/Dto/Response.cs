namespace MarkItDownApiClient.Dto;

public class Response(string filename, int size, Content content)
{
    public string Filename { get; } = filename;
    public int Size { get; } = size;
    public Content Content { get; } = content;
}