using CodeCube.Core.Filters;
using System;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace CodeCube.Mvc.Filters
{
    /// <summary>
    /// A fancy attribute that reduces the output sent to your browser.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class WhitespaceFilterAttribute : ActionFilterAttribute
    {
        private readonly bool _dontFilterIfDebug;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dontFilterIfDebugging">Don't execute the filter if any debugger is attached.</param>
        public WhitespaceFilterAttribute(bool dontFilterIfDebugging = true)
        {
            _dontFilterIfDebug = dontFilterIfDebugging;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var response = filterContext.HttpContext.Response;

            //Don't execute the filter if we're debugging.
            if (filterContext.HttpContext.IsDebuggingEnabled && _dontFilterIfDebug) return;

            response.Filter = new WhitespaceFilter(response.OutputStream);
            //response.Filter = new OutputFilter(response.Filter, stringvalue =>
            //{
            //    stringvalue = Regex.Replace(stringvalue, @"\s+", " ");
            //    stringvalue = Regex.Replace(stringvalue, @"\s*\n\s*", "\n");
            //    stringvalue = Regex.Replace(stringvalue, @"\s*\>\s*\<\s*", "><");
            //    stringvalue = Regex.Replace(stringvalue, @"<!--(.*?)-->", "");   //Remove comments

            //    // single-line doctype must be preserved 
            //    var firstEndBracketPosition = stringvalue.IndexOf(">", StringComparison.Ordinal);
            //    if (firstEndBracketPosition < 0) return stringvalue;

            //    stringvalue = stringvalue.Remove(firstEndBracketPosition, 1);
            //    stringvalue = stringvalue.Insert(firstEndBracketPosition, ">");

            //    return stringvalue;
            //});
        }
    }
}
