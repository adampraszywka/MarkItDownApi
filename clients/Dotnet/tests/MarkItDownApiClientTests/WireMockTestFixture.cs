using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace MarkItDownApiClientTests;

public class WireMockTestFixture : IDisposable
{
    private readonly WireMockServer _wireMockServer;
    private readonly HttpClient _httpClient;

    public HttpClient Client => _httpClient;
    
    public WireMockTestFixture(byte[] file, IResponseBuilder responseBuilder)
    {
        _wireMockServer = WireMockServer.Start();
        _httpClient = _wireMockServer.CreateClient();

        _wireMockServer
            .Given(Request.Create()
                .WithPath("/read")
                .UsingPost()
                .WithBody(body => body.ContainsFile(file))
            )
            .RespondWith(responseBuilder);
    }
    
    public void Dispose()
    {
        _wireMockServer.Dispose();
        _httpClient.Dispose();
    }
}