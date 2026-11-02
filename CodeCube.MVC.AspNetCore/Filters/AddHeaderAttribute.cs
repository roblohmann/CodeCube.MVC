using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CodeCube.Mvc.AspNetCore.Filters
{
    /// <summary>
    /// Use this attribute on a controller to add values to the responseheader.
    /// </summary>
    public class AddHeaderAttribute : ResultFilterAttribute
    {
        private readonly string _key;
        private readonly string _value;

        /// <summary>
        /// Add the provided key and value to the header.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public AddHeaderAttribute(string key, string value)
        {
            _key = key;
            _value = value;
        }

        /// <inheritdoc />
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            context.HttpContext.Response.Headers.Append(_key, _value);
            base.OnResultExecuting(context);
        }
    }
}
