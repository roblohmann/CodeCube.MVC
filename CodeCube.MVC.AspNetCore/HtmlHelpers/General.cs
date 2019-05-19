using System;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace CodeCube.MVC.AspNetCore.HtmlHelpers
{
    public static class General
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
                return string.Format("{0}.{1}.{2}.{3}", version.Major, version.Minor, version.Build.ToString("00"), version.Revision.ToString("00"));
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
                return string.Format("{0}.{1}.{2}.{3}", version.Major, version.Minor, version.Build.ToString("00"), version.Revision.ToString("00"));
            }
            catch (Exception)
            {
                return "?.?.?.?";
            }
        }
    }
}
