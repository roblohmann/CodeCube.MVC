using System;
using System.ComponentModel.DataAnnotations;

namespace CodeCube.Mvc.Attributes.Validation
{
    [AttributeUsage(AttributeTargets.Property)]
    public class IntegerValidationAttribute : RegularExpressionAttribute
    {
        public IntegerValidationAttribute()
            : base(@"^\d+$")
        {
        }
    }
}