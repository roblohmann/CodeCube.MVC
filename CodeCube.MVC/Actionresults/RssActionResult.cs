using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace CodeCube.MVC.Actionresults
{
    public class RssActionResult : FileResult
    {
        private readonly SyndicationFeed _feed;

        /// <summary>
        /// Creates a new instance of RssResult
        /// </summary>
        /// <param name="feed">The feed to return the user.</param>
        public RssActionResult(SyndicationFeed feed) : base("application/rss+xml")
        {
            _feed = feed;
        }

        /// <summary>
        /// Creates a new instance of RssResult
        /// </summary>
        /// <param name="title">The title for the feed.</param>
        /// <param name="description">The description for the feed.</param>
        /// <param name="feedItems">The items of the feed.</param>
        public RssActionResult(string title, string description, List<SyndicationItem> feedItems) : base("application/rss+xml")
        {
            _feed = new SyndicationFeed(title, description, HttpContext.Current.Request.Url) { Items = feedItems };
        }

        protected override void WriteFile(HttpResponseBase response)
        {
            using (var writer = XmlWriter.Create(response.OutputStream))
            {
                _feed.GetRss20Formatter().WriteTo(writer);
            }
        }
    }
}
