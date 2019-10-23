using System;
using System.ComponentModel.DataAnnotations;

namespace CodeCube.Mvc.Attributes.Validation
{
    [AttributeUsage(AttributeTargets.Property)]
    public class UrlValidationAttribute : RegularExpressionAttribute
    {
        public UrlValidationAttribute()
            : base(@"(http|https)://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?")
        {
        }
    }
}
