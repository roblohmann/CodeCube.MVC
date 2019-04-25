using System.Web.Mvc;
using CodeCube.Core.Enumerations;
using CodeCube.MVC.Tagbuilders;

namespace CodeCube.MVC.Attributes
{
    public class CanonicalUrlAttribute : ActionFilterAttribute
    {
        private readonly string _protocol;
        private readonly string _host;
        private readonly string _language;

        /// <summary>
        /// Canonical attribute which will add an 'CanonicalUrl'-property to your viewbag.
        /// </summary>
        /// <param name="host">The mainhost for the application. Eg. www.yourdomain.com</param>
        /// <param name="protocol">The mainprotocol your application is running ron. Eg. http or https</param>
        public CanonicalUrlAttribute(string host, EProtocol protocol)
        {
            _host = host;
            _protocol = protocol.ToString();
        }

        /// <summary>
        /// Canonical attribute which will add an 'CanonicalUrl'-property to your viewbag.
        /// </summary>
        /// <param name="host">The mainhost for the application. Eg. www.yourdomain.com</param>
        /// <param name="protocol">The mainprotocol your application is running ron. Eg. http or https</param>
        /// <param name="language">The language your site is running. Eg. en or nl or us</param>
        public CanonicalUrlAttribute(string host, EProtocol protocol, string language)
        {
            _host = host;
            _protocol = protocol.ToString();
            _language = language;
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var canonical = MetaTagBuilder.GetCanonicalUrl(filterContext.RouteData, _host, _protocol, _language);
            filterContext.Controller.ViewBag.CanonicalUrl = canonical.ToString();
        }
    }
}
