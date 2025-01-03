using MarkItDownApiClient.Dto;

namespace MarkItDownApiClient.Client;

public interface MarkItDownClient
{
    public Task<Response> Read(Stream stream);
}