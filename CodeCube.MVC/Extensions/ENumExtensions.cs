using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using CodeCube.Mvc.Resources;

namespace CodeCube.Mvc.Extensions
{
    /// <summary>
    /// Class with extensionmethods for enumerations.
    /// </summary>
    public static class ENumExtensions
    {
        /// <summary>
        /// Converts an ENum type to an selectlist
        /// </summary>
        /// <param name="enumObject">The enum</param>
        /// <param name="includeEmptyItem">True if an empty item should be added, otherwise false</param>
        /// <returns>An selectlist</returns>
        [Obsolete("This method will be removed in future versions. As off MVC5 you can use the @Html.EnumDropDownListFor() method")]
        public static IEnumerable<SelectListItem> ToSelectList(this Enum enumObject, bool includeEmptyItem = false)
        {
            List<SelectListItem> itemList = (from Enum e in Enum.GetValues(enumObject.GetType())
                                             select new SelectListItem
                                                        {
                                                            Selected = e.Equals(enumObject), Text = e.ToDescription(), Value = e.ToString()
                                                        }).ToList();

            if (includeEmptyItem)
            {
                itemList.Insert(0,new SelectListItem {Selected = true, Text = Resource.MakeAChoice, Value = String.Empty});
            }

            return itemList;
        }

        /// <summary>
        /// Function to read the description attributes value of an Enum.
        /// If no description attribute is found, the value will be used.
        /// </summary>
        /// <param name="value">The enum value.</param>
        /// <returns>The description.</returns>
        public static string ToDescription(this Enum value)
        {
            var attributes = (DescriptionAttribute[])value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }
    }
}
