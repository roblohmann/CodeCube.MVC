using System;
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
    }
}
