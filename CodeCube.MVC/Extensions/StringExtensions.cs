using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace CodeCube.Mvc.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// returns an string hashed as md5
        /// </summary>
        /// <param name="s"></param>
        /// <returns>MD5 hashed string</returns>
        public static string AsMd5(this string s)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] originalBytes = Encoding.Default.GetBytes(s);
            byte[] encodedBytes = md5.ComputeHash(originalBytes);

            //Convert encoded bytes back to a 'readable' string
            var result = new StringBuilder(encodedBytes.Length * 2);

            for (var i = 0; i < encodedBytes.Length; i++)
                result.Append(encodedBytes[i].ToString("x2"));

            return result.ToString();
        }

        /// <summary>
        /// Creates an SHA-512 hashed string from an clear password string
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string AsSha512(this string password)
        {
            var sb = new StringBuilder();

            try
            {
                var sha512 = SHA512.Create();
                byte[] inputBytes = Encoding.ASCII.GetBytes(password);
                byte[] hashBytes = sha512.ComputeHash(inputBytes);

                for (var i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }
            }
            catch (Exception ex)
            {
                //todo: logging
            }

            return sb.ToString();
        }

        /// <summary>
        /// Parses the DateTime from Twitter into a usable DateTime object
        /// </summary>
        /// <param name="date">Date from Twitter</param>
        /// <returns></returns>
        public static DateTime ParseDateTime(this string date)
        {
            var correctedDateTime = DateTime.ParseExact(date, "ddd MMM dd HH:mm:ss K yyyy", CultureInfo.InvariantCulture);
            return correctedDateTime;
            //return correctedDateTime.ToString("dd MMM yyyy HH:mm:ss");

            /*//Get the day of the week
            var dayOfWeek = date.Substring(0, 3).Trim();

            //Get the month
            var month = date.Substring(4, 3).Trim();

            //Get the day of the monty
            var dayInMonth = date.Substring(8, 2).Trim();

            //Get the time
            var time = date.Substring(11, 9).Trim();

            //Get the offset / timezone
            var offset = date.Substring(20, 5).Trim();

            //Get the year in the datetime
            var year = date.Substring(25, 5).Trim();

            //Setup the new usable datetimeobject
            var dateTime = string.Format("{0}-{1}-{2} {3}", dayInMonth, month, year, time);
            var dateTimeToReturn = DateTime.Parse(dateTime);

            //return it
            return dateTimeToReturn;*/
        }

        /// <summary>
        /// Function to generate a friendly url using a maxlength defined in a constant.
        /// </summary>
        /// <param name="text">The text to create a slug from</param>
        /// <returns>The slug</returns>
        public static string GenerateSlug(this string text)
        {
            var str = text.ToLower();

            // invalid chars, make into spaces
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");

            // convert multiple spaces/hyphens into one space      
            str = Regex.Replace(str, @"[\s-]+", " ").Trim();

            // cut and trim it
            str = str.Substring(0, str.Length <= Constants.MaxLengthUrlSlug ? str.Length : Constants.MaxLengthUrlSlug).Trim();

            // hyphens
            str = Regex.Replace(str, @"\s", "-");

            //return the value
            return str;
        }

        /// <summary>
        /// Generates a friendly url
        /// </summary>
        /// <returns>A string representing an seo friendly url</returns>
        public static string AsFriendlyUrl(this string url)
        {
            // make the url lowercase
            string encodedUrl = (url ?? String.Empty).ToLower();

            // replace & with and
            encodedUrl = Regex.Replace(encodedUrl, @"\&+", "and");

            // remove characters
            encodedUrl = encodedUrl.Replace("'", String.Empty);

            // remove invalid characters
            encodedUrl = Regex.Replace(encodedUrl, @"[^a-z0-9]", "-");

            // remove duplicates
            encodedUrl = Regex.Replace(encodedUrl, @"-+", "-");

            // trim leading & trailing characters
            encodedUrl = encodedUrl.Trim('-');

            return encodedUrl;
        }

        /// <summary>
        /// Strips all html-tags from an text and replaces them by an safely encoded string.
        /// </summary>
        /// <param name="input">The text to search trough</param>
        /// <returns>The original string with the value stripped</returns>
        public static string StripTags(this string input)
        {
            var regex = new Regex(@"(<\/?[^>]+>)");
            var str = input;
            
            foreach (Match match in regex.Matches(input))
            {    
                //str = ReplaceFirst(str.Trim(), match.Value, "");
                str = ReplaceFirst(str.Trim(), match.Value, "");
            }
            return str.Trim();
        }

        /// <summary>
        /// Strips all html-tags from an text and replaces them by an safely encoded string.
        /// </summary>
        /// <param name="input">The text to search trough</param>
        /// <param name="allowedTags">The html-tags allowed in the string. Can be left out.</param>
        /// <returns>The original string with the value stripped</returns>
        public static string StripTags(this string input, string[] allowedTags)
        {
            var regex = new Regex(@"(<\/?[^>]+>)");
            var str = input;
            foreach (Match match in regex.Matches(input))
            {
                var str2 = match.Value.ToLower();
                var flag = false;
                
                foreach (var str3 in allowedTags)
                {
                    var index = -1;
                    if (index != 0)
                    {
                        index = str2.IndexOf('<' + str3 + '>');
                    }
                    if (index != 0)
                    {
                        index = str2.IndexOf('<' + str3 + ' ');
                    }
                    if (index != 0)
                    {
                        index = str2.IndexOf("</" + str3);
                    }
                    if (index == 0)
                    {
                        flag = true;
                        break;
                    }
                }

                if (!flag)
                {
                    str = ReplaceFirst(str.Trim(), match.Value, "");
                }
            }
            return str.Trim();
        }

        /// <summary>
        /// Removes all html from the specified string.
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static string StripHTML(this string inputString)
        {
            return Regex.Replace(inputString, Constants.HtmlRegexPattern, string.Empty);
        }

        /// <summary>
        /// Replaces the first occurence in the string
        /// </summary>
        /// <param name="haystack">The text to search through</param>
        /// <param name="needle">The Value to find in the text</param>
        /// <param name="replacement">The value to replace it with</param>
        /// <returns>The string with the value replaced</returns>
        public static string ReplaceFirst(string haystack, string needle, string replacement)
        {
            return new Regex(Regex.Escape(needle)).Replace(haystack, replacement, 1);
        }


        /// <summary>
        /// Shortens the supplied text
        /// </summary>
        /// <param name="text">The text which needs to be shortened</param>
        /// <param name="amountOfCharacters">The amount of characters you want returned, default 300</param>
        /// <param name="addSuffix">Boolean indicating wether three dots should be added as suffix, default true</param>
        /// <param name="keepFullWords">Boolean indicating wether the shortended string should end with an full wordt, default true</param>
        /// <returns>An shortened string</returns>
        public static string ShortenText(this string text, int amountOfCharacters = 300, bool addSuffix = true, bool keepFullWords = true)
        {
            // replaces the truncated string to a ...
            var suffix = addSuffix ? "..." : String.Empty;

            if (amountOfCharacters <= 0) return text;

            //the maxlength without the suffix.
            var strLength = amountOfCharacters - suffix.Length;

            //If the length of the string is below 0, return the string
            if (strLength <= 0) return text;

            //If text is NULL of the length is smaller or equal to the length of the string, then just return it.
            if (text == null || text.Length <= amountOfCharacters) return text;

            //Should the shortened text end with an full word?
            if(keepFullWords)
            {
                //find the last occuring space
                var lastOccuringSpace = text.LastIndexOf(" ", amountOfCharacters);
                return $"{text.Substring(0, (lastOccuringSpace > 0) ? lastOccuringSpace : amountOfCharacters).Trim()}...";
            }

            //Cut the string
            var truncatedString = text.Substring(0, strLength);

            //Remove trailing spaces
            truncatedString = truncatedString.TrimEnd();

            //Return the string
            return String.Format("{0}" + suffix, truncatedString);
        }

        /// <summary>
        /// Makes an uppercase of the first character in the string this extension method is called on.
        /// </summary>
        /// <param name="value">The string from which the first char need to be uppercased</param>
        /// <returns>String with uppercased first char</returns>
        public static string UppercaseFirstChar(this string value)
        {
            if (value.Length > 0)
            {
                var array = value.ToCharArray();
                array[0] = char.ToUpper(array[0]);
                
                return new string(array);
            }

            return value;
        }

        /// <summary>
        /// Test if the string is a valid Guid
        /// </summary>
        /// <returns>True if an Guid, otherwise false</returns>
        public static bool IsValidGuid(string str)
        {
            Guid guid;
            return Guid.TryParse(str, out guid);
        }
    }
}
