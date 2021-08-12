using System;
using System.Collections.Generic;
using NUnit.Framework;
using URLParser.BLL.Implementation.Entities;

namespace URLParser.BLL.Implementation.Tests
{
    [TestFixture]
    internal class XmlSerializerTests
    {
        private static IEnumerable<TestCaseData> SerializeTestCases
        {
            get
            {
                yield return new TestCaseData(
                    new List<Url>
                    {
                        new("https:", "github.com", new string[] { "AnzhelikaKravchuk" },
                            new Dictionary<string, string>() { ["tab"] = "repositories" })
                    },
                    "<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<urlAddresses>\r\n  <urlAddress>\r\n    <host name=\"github.com\" />\r\n    <uri>\r\n      <segment>AnzhelikaKravchuk</segment>\r\n    </uri>\r\n    <parameters>\r\n      <parameter value=\"repositories\" key=\"tab\" />\r\n    </parameters>\r\n  </urlAddress>\r\n</urlAddresses>");
            }
        }

        [Test]
        public void Serialize_NullSource_ThrowArgumentNullException() =>
            Assert.Throws<ArgumentNullException>(() => new XmlSerializer().Serialize(null), "URL source can't be null");

        [TestCaseSource(nameof(SerializeTestCases))]
        public void Serialize_ValidData_ReturnXmlAsStringRepresentation(IEnumerable<Url> source, string expected)
        {
            var serializer = new XmlSerializer();
            var actual = serializer.Serialize(source);

            Assert.AreEqual(expected, actual);
        }
    }
}
