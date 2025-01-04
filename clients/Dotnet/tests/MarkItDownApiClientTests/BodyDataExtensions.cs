using System.Text;
using WireMock.Util;

namespace MarkItDownApiClientTests;

public static class BodyDataExtensions
{
    public static bool ContainsFile(this IBodyData? bodyData, byte[] file)
    {
        if (bodyData is null)
        {
            return false;
        }

        var fileString = Encoding.UTF8.GetString(file);
        var bodyString = Encoding.UTF8.GetString(bodyData.BodyAsBytes ?? []);

        return bodyString.Contains(fileString);
    }
}