using CodeCube.Core.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;

namespace CodeCube.Mvc.HtmlHelpers
{
    /// <summary>
    /// Helper class for HTMLHelpers involvind textboxes / editors.
    /// </summary>
    public static class Html5Helper
    {
        /// <summary>
        /// Renders a textbox for different input types including a default placeholder.
        /// </summary>
        /// <param name="htmlAttributes">Object with additional HTML attributes eg. class</param>
        public static MvcHtmlString Html5TextBoxFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, object htmlAttributes)
        {
            return Html5TextBoxFor(htmlHelper, expression, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        /// <summary>
        /// Renders a textbox for different input types including a default placeholder.
        /// </summary>
        /// <param name="htmlAttributes">Object with additional HTML attributes eg. class</param>
        /// <param name="textboxType">The type of textbox to create. Default: text</param>
        public static MvcHtmlString Html5TextBoxFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, object htmlAttributes, ETextboxType textboxType)
        {
            return Html5TextBoxFor(htmlHelper, expression, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes), textboxType);
        }

        /// <summary>
        /// Renders a textbox for different input types including a default placeholder.
        /// </summary>
        /// <param name="htmlAttributes">Object with additional HTML attributes eg. class</param>
        /// <param name="textboxType">The type of textbox to create. Default: text</param>
        /// <remarks>For more info and sourcode see: https://aspnetwebstack.codeplex.com/SourceControl/latest#src/System.Web.Mvc/Html/InputExtensions.cs</remarks>
        public static MvcHtmlString Html5TextBoxFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, IDictionary<string, object> htmlAttributes, ETextboxType textboxType = ETextboxType.Text)
        {
            //return CreateInputField<TModel, TValue>(htmlHelper, htmlAttributes, expression, textboxType);

            //Start tagbuilder
            var textboxBuilder = new TagBuilder("input");

            //HTML Attributen samenvoegen
            textboxBuilder.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            //Input type
            textboxBuilder.Attributes.Add("type", GetInputtype(textboxType));

            //Placeholder
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            var placeholderText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();

            textboxBuilder.Attributes.Add("placeholder", placeholderText);

            //Id attribute
            textboxBuilder.GenerateId(htmlFieldName);
            //AddIdAttribute(textboxBuilder, metadata.PropertyName);

            //Name attribute
            AddNameAttribute(textboxBuilder, metadata.PropertyName);

            //Get validation attributes
            textboxBuilder.MergeAttributes(htmlHelper.GetUnobtrusiveValidationAttributes(htmlFieldName, metadata));

            //Render output
            return MvcHtmlString.Create(textboxBuilder.ToString(TagRenderMode.Normal));
        }

        /// <summary>
        /// Renders a textarea including placeholder.
        /// </summary>
        /// <param name="rows">The number of rows.</param>
        /// <param name="cols">The number of columns.</param>
        /// <param name="htmlAttributes">Object with additional HTML attributes.</param>
        /// <returns></returns>
        public static MvcHtmlString Html5TextAreaFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, int rows, int cols, object htmlAttributes)
        {
            //Start tagbuilder
            var textareaBuilder = new TagBuilder("textarea");

            //HTML Attributen samenvoegen
            textareaBuilder.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            //Placeholder
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            var placeholderText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();

            textareaBuilder.Attributes.Add("placeholder", placeholderText);

            //Id attribute
            textareaBuilder.GenerateId(htmlFieldName);
            
            //Name attribute
            AddNameAttribute(textareaBuilder, metadata.PropertyName);

            //rows
            textareaBuilder.Attributes.Add("rows", rows.ToString());

            //cols
            textareaBuilder.Attributes.Add("cols", cols.ToString());

            //Get validation attributes
            textareaBuilder.MergeAttributes(htmlHelper.GetUnobtrusiveValidationAttributes(htmlFieldName, metadata));

            //Render output
            return MvcHtmlString.Create(textareaBuilder.ToString(TagRenderMode.Normal));
        }

        /// <summary>
        /// Renders an html label.
        /// </summary>
        /// <param name="htmlAttributes">Any additional HTML attributes</param>
        /// <returns></returns>
        public static MvcHtmlString Html5LabelFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, object htmlAttributes)
        {
            //Tagbuilder
            var labelBuilder = new TagBuilder("label");

            //HTML Attributen samenvoegen
            labelBuilder.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            //Set labeltext
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            var labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();

            labelBuilder.SetInnerText(labelText);

            //Id attribute
            labelBuilder.GenerateId(htmlFieldName);
            //AddIdAttribute(labelBuilder, metadata.PropertyName);

            //Name attribute
            AddNameAttribute(labelBuilder, metadata.PropertyName);

            //Render output
            return MvcHtmlString.Create(labelBuilder.ToString(TagRenderMode.Normal));
        }

        #region privates
        private static string GetInputtype(ETextboxType textboxType)
        {
            //TODO: Move HTML5 stuff to seperate html5 lib.
            switch (textboxType)
            {
                case ETextboxType.Email:
                    return "email";
                case ETextboxType.File:
                    return "file";
                case ETextboxType.Url:
                    return "url";
                case ETextboxType.Phone:
                    return "tel";
                case ETextboxType.Search:
                    return "search";
                case ETextboxType.Range:
                    return "range";
                case ETextboxType.Date:
                    return "date";
                case ETextboxType.Month:
                    return "month";
                case ETextboxType.Week:
                    return "week";
                case ETextboxType.Time:
                    return "time";
                case ETextboxType.DateTime:
                    return "datetime";
                case ETextboxType.DateTimeLocal:
                    return "datetime-local";
                case ETextboxType.Color:
                    return "color";
                case ETextboxType.Text:
                    return "text";
                default:
                    return "text";
            }
        }

        private static void AddNameAttribute(TagBuilder textboxBuilder, string attributeValue)
        {
            textboxBuilder.Attributes.Add("name", ClearValue(attributeValue));
        }

        private static string ClearValue(string value)
        {
            return value.Replace("-", "").Replace(" ", "_").ToLower();
        }        
        #endregion
    }
}
