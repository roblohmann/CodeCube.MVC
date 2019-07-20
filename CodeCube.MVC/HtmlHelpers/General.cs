using System;
using System.Reflection;
using System.Web.Mvc;

namespace CodeCube.Mvc.HtmlHelpers
{
    public static class GeneralHelper
    {
        /// <summary>
        /// Retrieves the name of the current machine handling the request.
        /// </summary>
        /// <param name="helper">The MVC htmlhelper instance.</param>
        /// <returns>The name of the machine.</returns>
        public static string CurrentMachine(this HtmlHelper helper)
        {
            return $"{Environment.MachineName}";
        }

        /// <summary>
        /// Get the version from the current assembly as string.
        /// </summary>
        /// <returns>The assembly version as string.</returns>
        public static string CurrentVersion(this HtmlHelper helper)
        {
            try
            {
                var version = Assembly.GetExecutingAssembly().GetName().Version;
                return $"{version.Major}.{version.Minor}.{version.Build:0}.{version.Revision:0}";
            }
            catch (Exception)
            {
                return "?.?.?.?";
            }
        }

        /// <summary>
        /// Get the version for the specified assembly as string.
        /// </summary>
        /// <param name="assemblyName">The assembly to get the version for.</param>
        /// <returns>The assembly version as string.</returns>
        public static string CurrentVersion(this HtmlHelper helper, string assemblyName)
        {
            try
            {
                var version = Assembly.Load(assemblyName).GetName().Version;
                return $"{version.Major}.{version.Minor}.{version.Build:0}.{version.Revision:0}";
            }
            catch (Exception)
            {
                return "?.?.?.?";
            }
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
    }
}