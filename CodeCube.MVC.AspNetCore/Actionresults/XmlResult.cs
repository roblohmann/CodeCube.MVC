using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace CodeCube.Mvc.AspNetCore.Actionresults
{
    public class XmlResult : ActionResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XmlResult"/> class.
        /// </summary>
        /// <param name="objectToSerialize">The object to serialize to XML.</param>
        public XmlResult(object objectToSerialize)
        {
            ObjectToSerialize = objectToSerialize;
        }

        /// <summary>
        /// Gets the object to be serialized to XML.
        /// </summary>
        private object ObjectToSerialize { get; }

        /// <summary>
        /// Serialises the object that was passed into the constructor to XML and writes the corresponding XML to the result stream.
        /// </summary>
        /// <param name="context">The controller context for the current request.</param>
        public override void ExecuteResult(ActionContext context)
        {
            //If there is nothing to serialize then return and stop execution.
            if (ObjectToSerialize == null) return;

            //context.HttpContext.Response.Clear();

            var xmlSerializer = new XmlSerializer(ObjectToSerialize.GetType());
            context.HttpContext.Response.ContentType = "text/xml";

            xmlSerializer.Serialize(context.HttpContext.Response.Body, ObjectToSerialize);
        }
    }
}
