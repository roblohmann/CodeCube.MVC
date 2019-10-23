using System;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using CodeCube.Mvc.Extensions;

namespace CodeCube.Mvc.HtmlHelpers
{
    /// <summary>
    /// Helper class to to render / generate navigationitems like breadcrumbs of listitems (li) intended for menu usage.
    /// </summary>
    public static class NavigationHelper
    {
        /// <summary>
        /// An html helper to create an breadcumb path.
        /// The information from the current request is used to build the breadcrumb.
        /// Source code from: http://www.search-this.com/website-design/aspnet-breadcrumbs-with-c-2/
        /// </summary>
        /// <param name="helper">The instance of the helper.</param>
        /// <param name="prefix">The prefix you want to add to the breadcrumb.</param>
        /// <param name="seperator">The seperator, used to seperate the crumbs.</param>
        /// <param name="overridePageTitle">Overrides the page title if desired. Will be last part of the breadcrumb.</param>
        /// <returns>Html string with the breadcrumb path.</returns>
        public static MvcHtmlString BreadCrumb(this HtmlHelper helper, string prefix = "You are here: ", string seperator = " » ", string overridePageTitle = "")
        {
            //const string separator = " » ";
            const string rootName = "Home";
            const char directoryNameSpacer = '_';
            var request = helper.ViewContext.HttpContext.Request;

            var sbResult = new StringBuilder();
            sbResult.Append("<span>" + prefix + "&nbsp;</span>");

            //Add the opening ul
            sbResult.Append("<ul>");

            // get the url root, like www.domain.com
            var strDomain = request.ServerVariables["HTTP_HOST"];
            strDomain = strDomain.Trim(); // Trim removes leading and trailing whitespace
            sbResult.Append("<li><a href='http://" + strDomain + "' title='" + rootName + "'>" + rootName + "</a>"); // + separator);

            // gets dir(s), like subdirectory/subsubdirectory/file.aspx
            var scriptName = request.ServerVariables["SCRIPT_NAME"];

            // find the last '/' and Remove the text after it as it's the file name
            var lastSlash = scriptName.LastIndexOf('/'); // returns the # of chars. from right to /
            var pathOnly = scriptName.Remove(lastSlash, (scriptName.Length - lastSlash));

            var pageTitle = scriptName.Split('/')[scriptName.Split('/').Length - 1];

            if (!String.IsNullOrWhiteSpace(overridePageTitle))
            {
                pageTitle = overridePageTitle;
            }

            if (!String.IsNullOrEmpty(pathOnly))
            {
                // create breadcrumb HTML for the directory name(s)
                // We Remove the first "/" otherwise when you split the string the first item in array is empty
                sbResult.Append(seperator + "</li>");
                pathOnly = pathOnly.Substring(1);
                var strDirs = pathOnly.Split('/');
                var nNumDirs = strDirs.Length;

                // URLs for breadcrumbs
                var strUrl = String.Empty;
                for (var i = 0; i < nNumDirs; i++)
                {
                    var counter = i + 1;

                    sbResult.Append(counter != nNumDirs ? "<li>" : "<li class=\"last\">");


                    //if (!StringExtensions.IsValidGuid(strDirs[i]))
                    //{
                    strUrl += "/" + strDirs[i];

                    // convert underscores to spaces
                    strDirs[i] = strDirs[i].Replace(directoryNameSpacer, ' ');

                    if (counter != nNumDirs)
                    {
                        String urlDisplayName = strDirs[i].Replace("-", " ").UppercaseFirstChar();
                        sbResult.Append("<a href='http://" + strDomain + strUrl + "/'>" + urlDisplayName + "</a>" + seperator);
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(pageTitle))
                        {
                            int testRes;
                            var isGuid = StringExtensions.IsValidGuid(strDirs[i]);
                            var isInt = int.TryParse(strDirs[i], out testRes);
                            if (!isGuid || !isInt)
                            {
                                var urlDisplayName = strDirs[i].Replace("-", " ").UppercaseFirstChar();
                                sbResult.Append("<a href='http://" + strDomain + strUrl + "/' title='" + urlDisplayName + "'>" + urlDisplayName + "</a>" + seperator);
                                sbResult.Append(pageTitle.Replace("-", " ").UppercaseFirstChar());
                            }
                            else
                            {
                                sbResult.Append(pageTitle.Replace("-", " ").UppercaseFirstChar());
                            }

                        }
                        else
                        {
                            // This is the last directory so don't tack on Separator
                            //sbResult.Append("<a href='http://" + strDomain + strUrl + "'>" + strDirs[i] + "</a>");
                            sbResult.Append(strDirs[i].Replace("-", " ").UppercaseFirstChar());
                        }
                    }
                    //}

                    sbResult.Append("</li>");
                }

                // write the PageTitle, pulled from the CodeBehind!
                //sbResult.Append(" : " + pageTitle);
            }
            else
            {
                sbResult.Append("<li>");
                sbResult.Append(seperator);
                sbResult.Append(pageTitle.Replace("-", " ").UppercaseFirstChar());
                sbResult.Append("</li>");
            }

            //Add the closing ul
            sbResult.Append("</ul>");

            return MvcHtmlString.Create(sbResult.ToString());
        }

        /// <summary>
        /// Renders an hyperlink to the correction page.
        /// Also an check is included to validate if it's the active page in the website.
        /// </summary>
        /// <param name="htmlHelper">The current helper</param>
        /// <param name="linkText">The text of the link to show.</param>
        /// <param name="actionName">The action name of the controller to call.</param>
        /// <param name="controllerName">The controllername to use.</param>
        /// <param name="url">The specific url to render</param>
        /// <returns>An hyperlink representing the desired menu-item</returns>
        public static MvcHtmlString MenuItem(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, string url)
        {
            string currentAction;
            string currentController;
            var className = linkText.Replace(" ", "-").ToLowerInvariant();

            //Check if the current request is a child action.
            // Then get the proper controller and action currently requested.
            if (htmlHelper.ViewContext.ParentActionViewContext != null)
            {
                currentAction = htmlHelper.ViewContext.ParentActionViewContext.RouteData.GetRequiredString("action").ToLower();
                currentController = htmlHelper.ViewContext.ParentActionViewContext.RouteData.GetRequiredString("controller").ToLower();
            }
            else
            {
                currentAction = htmlHelper.ViewContext.RouteData.GetRequiredString("action").ToLower();
                currentController = htmlHelper.ViewContext.RouteData.GetRequiredString("controller").ToLower();
            }

            //If the current request equals the provided meta url, then add a class active.
            if (actionName.ToLower() == currentAction && controllerName.ToLower() == currentController || controllerName.ToLower() == currentController)
            {
                className = $"{className} active";
            }

            //If a meta url is provided, use this to build an old fashioned href.
            if (!String.IsNullOrWhiteSpace(url))
            {
                var uri = url == "/" ? url : "/" + url;
                return new MvcHtmlString($"<a href='{uri}' class='{className}'>{linkText}</a>");
            }

            //..else render a default actionlink.
            return htmlHelper.ActionLink(
                    linkText,
                    actionName,
                    controllerName,
                    null,
                    new
                    {
                        @class = className.ToLowerInvariant()
                    });
        }
    }
}