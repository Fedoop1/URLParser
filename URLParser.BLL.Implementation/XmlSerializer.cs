using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using URLParser.BLL.Contract;
using URLParser.BLL.Implementation.Entities;

namespace URLParser.BLL.Implementation
{
    /// <summary>
    /// Class that serialize <see cref="Url"/> instance to XML document format.
    /// </summary>
    /// <seealso cref="URLParser.BLL.Contract.ISerializer&lt;System.Collections.Generic.IEnumerable&lt;URLParser.BLL.Implementation.Entities.Url&gt;, System.String&gt;" />
    public class XmlSerializer : ISerializer<IEnumerable<Url>, string>
    {
        /// <summary>
        /// Serializes the sequence of <see cref="Url"/> to XML document and return it as string.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>XML document representation of source sequence in string format.</returns>
        /// <exception cref="ArgumentNullException">Throws when URL source is null.</exception>
        public string Serialize(IEnumerable<Url> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source), "URL source can't be null");
            }

            var doc = new XmlDocument();
            var root = doc.CreateElement("urlAddresses");

            foreach (var url in source)
            {
                var addressNode = doc.CreateElement("urlAddress");
                var hostNode = doc.CreateElement("host");
                hostNode.SetAttribute("name", url.Host);
                addressNode.AppendChild(hostNode);

                if (url.UrlPathSegments != null)
                {
                    addressNode.AppendChild(SerializeUrlSegments(url, doc));
                }

                if (url.Parameters != null)
                {
                    addressNode.AppendChild(SerializeUrlParameters(url, doc));
                }

                root.AppendChild(addressNode);
            }

            doc.AppendChild(root);
            using var stringWriter = new StringWriter();
            doc.Save(stringWriter);

            return stringWriter.ToString();
        }

        private static XmlElement SerializeUrlSegments(Url url, XmlDocument document)
        {
            var uriNode = document.CreateElement("uri");
            foreach (var segment in url.UrlPathSegments)
            {
                var segmentNode = document.CreateElement("segment");
                segmentNode.InnerText = segment;
                uriNode.AppendChild(segmentNode);
            }

            return uriNode;
        }

        private static XmlElement SerializeUrlParameters(Url url, XmlDocument document)
        {
            var parametersNode = document.CreateElement("parameters");
            foreach (var parameterKey in url.Parameters.Keys)
            {
                var parameterNode = document.CreateElement("parameter");
                parameterNode.SetAttribute("value", url.Parameters[parameterKey]);
                parameterNode.SetAttribute("key", parameterKey);
                parametersNode.AppendChild(parameterNode);
            }

            return parametersNode;
        }
    }
}
