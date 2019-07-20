using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Text;
using System.Xml;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodeCube.Mvc.AspNetCore.Actionresults
{
    public class RssActionResult : FileResult
    {
        private readonly SyndicationFeed _feed;
        private const string RssMimeType = "application/rss+xml";

        /// <summary>
        /// Creates a new instance of RssResult
        /// based on this sample 
        /// http://www.developerzen.com/2009/01/11/aspnet-mvc-rss-feed-action-result/
        /// </summary>
        /// <param name="feed">The feed to return the user.</param>
        public RssActionResult(SyndicationFeed feed) : base(RssMimeType)
        {
            _feed = feed;
        }

        /// <summary>
        /// Creates a new instance of RssResult
        /// </summary>
        /// <param name="title">The title for the feed.</param>
        /// <param name="description">The description for the feed.</param>
        /// <param name="alternateUrl">An alternate url for the feed.</param>
        /// <param name="feedItems">The items of the feed.</param>
        public RssActionResult(string title, string description, System.Uri alternateUrl, List<SyndicationItem> feedItems) : base(RssMimeType)
        {
            _feed = new SyndicationFeed(title, description, alternateUrl) { Items = feedItems };
        }

        /// <summary>
        /// Creates a new instance of RssResult
        /// </summary>
        /// <param name="contextAccessor">The accessor for the HTTP-Context.</param>
        /// <param name="title">The title for the feed.</param>
        /// <param name="feedItems">The items of the feed.</param>
        public RssActionResult(IHttpContextAccessor contextAccessor, string title, List<SyndicationItem> feedItems) : base(RssMimeType)
        {
            HttpHelper httpHelper = new HttpHelper(contextAccessor);
            _feed = new SyndicationFeed(title, title, httpHelper.GetAbsoluteUri()) { Items = feedItems };
        }

        public override void ExecuteResult(ActionContext context)
        {
            context.HttpContext.Response.ContentType = RssMimeType;
            var xmlWriterSettings = new XmlWriterSettings()
            {
                Async = true,
                Encoding = Encoding.UTF8
            };

            using (var xmlWriter = XmlWriter.Create(context.HttpContext.Response.Body, xmlWriterSettings))
            {
                _feed.GetRss20Formatter().WriteTo(xmlWriter);
            }
        }
    }
}
