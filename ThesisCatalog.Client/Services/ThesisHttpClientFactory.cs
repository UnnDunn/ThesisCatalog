namespace ThesisCatalog.Client.Services;

public class ThesisHttpClientFactory
{
    private IHttpClientFactory _clientFactory;

    public ThesisHttpClientFactory(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public HttpClient CreateClient(string? name = null)
    {
        var result = _clientFactory.CreateClient(name ?? string.Empty);
        result.BaseAddress = new Uri("https://localhost:8081");

        return result;
    }
}