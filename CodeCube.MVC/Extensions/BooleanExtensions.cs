using System;
using CodeCube.Mvc.Resources;

namespace CodeCube.Mvc.Extensions
{
    public static class BooleanExtensions
    {
        /// <summary>
        /// Converts an boolean value to yes/no.
        /// Note: This function returns an culture-specific value!
        /// </summary>
        /// <param name="value">The boolean value. Either true/false</param>
        /// <returns>Yes or No, depending on the culture.</returns>
        public static string AsReadable(this Boolean value)
        {
            var returnValue = Resource.No;
            if (value)
            {
                returnValue = Resource.Yes;
            }

            return returnValue;
        }
    }
}
