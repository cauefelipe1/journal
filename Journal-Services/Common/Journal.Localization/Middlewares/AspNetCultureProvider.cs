using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;

namespace Journal.Localization.Middlewares;

public class AspNetCultureProvider : RequestCultureProvider
{
    public override Task<ProviderCultureResult?> DetermineProviderCultureResult(HttpContext httpContext)
    {
        if (httpContext is null)
            throw new ArgumentNullException(nameof(httpContext), "HttpContext is null");

        string cultureCode = string.Empty;

        if (httpContext.Request.Headers.TryGetValue(HeaderNames.AcceptLanguage, out var code))
            cultureCode = code;

        if (string.IsNullOrEmpty(cultureCode))
            return NullProviderCultureResult;

        var culture = new ProviderCultureResult(new StringSegment(cultureCode));

        return Task.FromResult(culture)!;
    }
}