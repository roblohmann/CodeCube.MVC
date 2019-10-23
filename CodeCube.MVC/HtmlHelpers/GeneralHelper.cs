using System;
using System.Reflection;
using System.Web.Mvc;

namespace Borlo.HtmlHelpers
{
    /// <summary>
    /// Helperclass for the simple stuff.
    /// </summary>
    public static class GeneralHelper
    {
        /// <summary>
        /// Html helper function that get the page url
        /// This is done by taking the absolute uri from the request.
        /// </summary>
        /// <returns>PageUrl as string</returns>
        public static MvcHtmlString PageUrl(this HtmlHelper helper)
        {
            // ReSharper disable PossibleNullReferenceException
            var request = helper.ViewContext.HttpContext.Request;
            var url = request.Url.AbsoluteUri;

            return new MvcHtmlString(url);
            // ReSharper restore PossibleNullReferenceException
        }

        /// <summary>
        /// Validates a string against a provided value
        /// </summary>
        /// <param name="value">The value to validate</param>
        /// <param name="evaluation">The evaluation to make</param>
        /// <example>@Html.ActionLink("Create New", "Create").If(User.IsInRole("SomeRole"))</example>
        /// <returns>The string if it matches, otherwise an empty string.</returns>
        public static MvcHtmlString If(this MvcHtmlString value, bool evaluation)
        {
            return evaluation ? value : MvcHtmlString.Empty;
        }

        /// <summary>
        /// Return the Current Version from the AssemblyInfo.cs file.
        /// </summary>
        public static string CurrentVersion(this HtmlHelper helper)
        {
            try
            {
                var version = Assembly.GetExecutingAssembly().GetName().Version;
                return String.Format("{0}.{1}.{2}.{3}", version.Major, version.Minor, version.Build.ToString("000"), version.Revision.ToString("000"));
            }
            catch (Exception ex)
            {
                return "?.?.?.?";
            }
        }

        /// <summary>
        /// Return the Current Version from the AssemblyInfo.cs file.
        /// </summary>
        public static string CurrentVersion(this HtmlHelper helper, String assemblyName)
        {
            try
            {
                var version = Assembly.Load(assemblyName).GetName().Version;
                return String.Format("{0}.{1}.{2}.{3}", version.Major, version.Minor, version.Build.ToString("000"), version.Revision.ToString("000"));
            }
            catch (Exception ex)
            {
                return "?.?.?.?";
            }
        }
    }
}
