using System;
using Microsoft.AspNetCore.Http;

namespace CodeCube.MVC.AspNetCore
{
    public sealed class HttpHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public HttpHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Retrieve the absolute URI.
        /// </summary>
        /// <returns>Absolute URL for the request in URI-format.</returns>
        public Uri GetAbsoluteUri()
        {
            var request = _httpContextAccessor.HttpContext.Request;

            UriBuilder uriBuilder = new UriBuilder
            {
                Scheme = request.Scheme,
                Host = request.Host.Host,
                Path = request.Path.ToString(),
                Query = request.QueryString.ToString()
            };

            return uriBuilder.Uri;
        }
    }
}
