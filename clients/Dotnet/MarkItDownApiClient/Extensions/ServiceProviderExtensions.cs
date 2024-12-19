using MarkItDownApiClient.Client;
using Microsoft.Extensions.DependencyInjection;

namespace MarkItDownApiClient.Extensions;

public static class ServiceProviderExtensions
{
    public static IHttpClientBuilder AddMarkItDownApiClient(this IServiceCollection services, Uri baseUri)
    {
        return services.AddHttpClient<MarkItDownClient>(httpClient =>
        {
            httpClient.BaseAddress = baseUri;
        });
    }
}