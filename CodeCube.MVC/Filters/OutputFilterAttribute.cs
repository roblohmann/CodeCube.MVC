using System;
using System.Web.Mvc;
using CodeCube.Mvc.Coding;

namespace CodeCube.Mvc.Filters
{
    /// <summary>
    /// Attribute for output filtering.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class OutputFilterAttribute : ActionFilterAttribute
    {
        private readonly string _keyToFind;
        private readonly string _replacementValue;
        
        /// <summary>
        /// Replaces the key to find with the value provided on the moment of output.
        /// </summary>
        public OutputFilterAttribute()
        {
            _keyToFind = String.Empty;
            _replacementValue = String.Empty;
        }

        /// <summary>
        /// Replaces the key to find with the value provided on the moment of output.
        /// </summary>
        /// <param name="needle">The key to find.</param>
        /// <param name="value">Value used to replace the needle found in the haystack.</param>
        public OutputFilterAttribute(string needle, string value)
        {
            _keyToFind = needle;
            _replacementValue = value;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var response = filterContext.HttpContext.Response;

            if (filterContext.IsChildAction) return;
            if (String.IsNullOrWhiteSpace(_keyToFind) || String.IsNullOrWhiteSpace(_replacementValue)) return;

            response.Filter = new OutputFilter(response.Filter, stringvalue =>
            {
                stringvalue = stringvalue.Replace(_keyToFind, _replacementValue);

                return stringvalue;
            });
        }
    }
}
