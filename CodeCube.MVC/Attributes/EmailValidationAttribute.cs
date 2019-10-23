using System;
using System.ComponentModel.DataAnnotations;

namespace CodeCube.Mvc.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class EmailValidationAttribute : RegularExpressionAttribute
    {
        public EmailValidationAttribute() : base(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+(?:[A-Z]{2}|com|org|net|edu|gov|mil|biz|info|mobi|name|aero|asia|jobs|museum)\b")
        {
        }
    }
}