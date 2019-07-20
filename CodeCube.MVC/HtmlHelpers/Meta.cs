using System.Web;
using System.Web.Mvc;
using CodeCube.Core.Enumerations;
using CodeCube.Mvc.Tagbuilders;

namespace CodeCube.Mvc.HtmlHelpers
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

        /// <summary>
        /// Get the ip address from the machine visiting your website.
        /// </summary>
        public static MvcHtmlString GetIpAddress(this HtmlHelper htmlHelper)
        {
            return new MvcHtmlString(GetIpAddress());
        }

        #region privates
        private static string GetIpAddress()
        {
            var context = System.Web.HttpContext.Current;
            var ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (string.IsNullOrEmpty(ipAddress)) return context.Request.ServerVariables["REMOTE_ADDR"];
            var addresses = ipAddress.Split(',');

            return addresses.Length != 0 ? addresses[0] : context.Request.ServerVariables["REMOTE_ADDR"];
        }
        #endregion
    }
}
