using System;
using System.Reflection;
using System.Web.Mvc;

namespace CodeCube.MVC.HtmlHelpers
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
        public static string CurrentVersion(this HtmlHelper helper, String assemblyName)
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
    }
}