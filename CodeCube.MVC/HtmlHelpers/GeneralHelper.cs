using System;
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
    }
}
