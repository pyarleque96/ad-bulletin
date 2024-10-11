﻿using AdBulletin.Common.Constants;
using System.Net.Http.Headers;

namespace AdBulletin.Core.Handlers;

public class JwtAuthHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public JwtAuthHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // Get token from session or cookies
        var token = _httpContextAccessor.HttpContext?.Request.Cookies[Constants.System.Tokens.JWT_AUTH_TOKEN];

        if (!string.IsNullOrEmpty(token))
        {
            // Add token to Authorization header
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
