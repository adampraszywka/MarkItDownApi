using System.Net;
using MarkItDownApiClient.Client;
using MarkItDownApiClient.Exceptions;
using WireMock.ResponseBuilders;

namespace MarkItDownApiClientTests;

public class HttpMarkItDownClientTests
{
    private readonly byte[] _file = [0x01, 0x02, 0x03, 0x04];

    [Test]
    public async Task Success()
    {
        using var fixture = new WireMockTestFixture(_file, Response.Create()
            .WithStatusCode(200)
            .WithBodyAsJson(new
            {
                filename = "file.md", content = new { title = "Title", text_content = "Text content" }
            })
        );

        var client = new HttpMarkItDownClient(fixture.Client);
        var stream = new MemoryStream(_file);
        
        var result = await client.Read(stream);
        
        Assert.That(result.Filename, Is.EqualTo("file.md"));
        Assert.That(result.Content.Title, Is.EqualTo("Title"));
        Assert.That(result.Content.TextContent, Is.EqualTo("Text content"));
    }

    [Test]
    public void NotSupportedFormat()
    {
        using var fixture = new WireMockTestFixture(_file, Response.Create()
            .WithStatusCode(415)
            .WithBodyAsJson(new { detail = "Not supported format" })
        );

        var client = new HttpMarkItDownClient(fixture.Client);
        var stream = new MemoryStream(_file);
        var ex = Assert.ThrowsAsync<UnsupportedMediaFormat>(() => _ = client.Read(stream));
        Assert.That(ex.Message, Is.EqualTo("Not supported format"));
    }
    
    [Test]
    public void FileConversionFailure()
    {
        using var fixture = new WireMockTestFixture(_file, Response.Create()
            .WithStatusCode(400)
            .WithBodyAsJson(new { detail = "File conversion failure" })
        );
        
        var client = new HttpMarkItDownClient(fixture.Client);
        var stream = new MemoryStream(_file);
        var ex = Assert.ThrowsAsync<FileConversionFailure>(() => _ = client.Read(stream));
        Assert.That(ex.Message, Is.EqualTo("File conversion failure"));
    }
    
    private static IEnumerable<object[]> UnknownProblemCases()
    {
        yield return [new { }, "An unknown problem occurred."];
        yield return [new { foo = "bar" }, "An unknown problem occurred."];
        yield return [new { detail = "Super fancy error" }, "Super fancy error"];
    }

    [Test]
    [TestCaseSource(nameof(UnknownProblemCases))]
    public void UnknownProblemJsonBody(object responseBody, string expectedMessage)
    {
        using var fixture = new WireMockTestFixture(_file, Response.Create()
            .WithStatusCode(500)
            .WithBodyAsJson(responseBody)
        );

        var client = new HttpMarkItDownClient(fixture.Client);

        var stream = new MemoryStream(_file);
        var ex = Assert.ThrowsAsync<UnknownProblem>(() => client.Read(stream));
        Assert.That(ex.Message, Is.EqualTo(expectedMessage));
        Assert.That(ex.StatusCode, Is.EqualTo(HttpStatusCode.InternalServerError));
    }
    
    [Test]
    public void UnknownProblemEmptyBody()
    {

        using var fixture = new WireMockTestFixture(_file, Response.Create()
            .WithStatusCode(500)
            .WithBody([]));
        var client = new HttpMarkItDownClient(fixture.Client);

        var stream = new MemoryStream(_file);
        var ex = Assert.ThrowsAsync<UnknownProblem>(() => client.Read(stream));
        Assert.That(ex.Message, Is.EqualTo("An unknown problem occurred. Cannot read error content."));
    }
}