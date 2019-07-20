using System.Web.Mvc;
using System.Web.Routing;

namespace CodeCube.Mvc.Tagbuilders
{
    public class MetaTagBuilder
    {
        public  static TagBuilder GetCanonicalUrl(RouteData route, string host, string protocol, string language = null)
        {
            //These rely on the convention that all your links will be lowercase! 
            string actionName = route.Values["action"].ToString().ToLower();
            string controllerName = route.Values["controller"].ToString().ToLower();

            //If your app is multilanguage and your route contains a language parameter then lowercase it also to prevent EN/en/ etc....
            string finalUrl = $"{protocol}://{host}";
            if (!string.IsNullOrWhiteSpace(language))
            {
                finalUrl += $"/{language}";
            }
            finalUrl += $"/{controllerName}/{actionName}";

            var canonical = new TagBuilder("link");
            canonical.MergeAttribute("href", finalUrl);
            canonical.MergeAttribute("rel", "canonical");

            return canonical;
        }
    }
}
