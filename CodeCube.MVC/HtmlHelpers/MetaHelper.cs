using System.Web;
using System.Web.Mvc;
using CodeCube.Core.Enumerations;
using CodeCube.MVC.Tagbuilders;

namespace CodeCube.MVC.HtmlHelpers
{
    public static class MetaHelper
    {
        /// <summary>
        /// Html-helper to generate a canonical tag.
        /// </summary>
        /// <param name="helper">The HTML-helper instance.</param>
        /// <param name="host">The main-host for the application.</param>
        /// <param name="protocol">The protocol to use.</param>
        /// <returns>HTML-tag for canonical</returns>
        public static MvcHtmlString CanonicalUrl(this HtmlHelper helper, string host, EProtocol protocol)
        {
            var canonical = MetaTagBuilder.GetCanonicalUrl(HttpContext.Current.Request.RequestContext.RouteData, host, protocol.ToString());
            return new MvcHtmlString(canonical.ToString(TagRenderMode.SelfClosing));
        }

        /// <summary>
        /// Html-helper to generate a canonical tag.
        /// </summary>
        /// <param name="helper">The HTML-helper instance.</param>
        /// <param name="host">The main-host for the application.</param>
        /// <param name="protocol">The protocol to use.</param>
        /// <param name="language">The language for the path.</param>
        /// <example>https://www.yourdomain.com/en/blog/myblog</example>
        /// <returns>HTML-tag for canonical</returns>
        public static MvcHtmlString CanonicalUrl(this HtmlHelper helper, string host, EProtocol protocol, string language)
        {
            var canonical = MetaTagBuilder.GetCanonicalUrl(HttpContext.Current.Request.RequestContext.RouteData, host, protocol.ToString(), language);
            return new MvcHtmlString(canonical.ToString(TagRenderMode.SelfClosing));
        }
    }
}
