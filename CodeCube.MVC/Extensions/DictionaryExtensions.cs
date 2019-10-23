using System.Collections.Generic;
using System.Collections.Specialized;

namespace CodeCube.Mvc.Extensions
{
    /// <summary>
    /// Class with extensionsmethods for dictionaries.
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Convert a dictionary to a namevaluecollection.
        /// </summary>
        /// <param name="dict">The dictionary to convert.</param>
        /// <typeparam name="TKey">The strongtyped key for the dictionary.</typeparam>
        /// <typeparam name="TValue">The strongtyped value for the ditionary. Can be null.</typeparam>
        /// <returns></returns>
        public static NameValueCollection ToNameValueCollection<TKey, TValue>(this IDictionary<TKey, TValue> dict)
        {
            var nameValueCollection = new NameValueCollection();

            foreach (var kvp in dict)
            {
                string value = null;
                if (kvp.Value != null)
                    value = kvp.Value.ToString();

                nameValueCollection.Add(kvp.Key.ToString(), value);
            }

            return nameValueCollection;
        }
    }
}
