using System;
using System.Linq;
using NLog;
using NLog.Config;
using NLog.Targets;
using NUnit.Framework;
using URLParser.BLL.Implementation.Entities;

namespace URLParser.BLL.Implementation.Tests
{
    [TestFixture]
    internal class UrlDeserializerTests
    {
        [Test]
        public void Ctor_NullLogger_ThrowArgumentNullException() =>
            Assert.Throws<ArgumentNullException>(() => new UrlDeserializer(null), "Logger can't be null");

        [Test]
        public void Deserialize_NullData_ThrowArgumentNullException() =>
            Assert.Throws<ArgumentNullException>(() => new UrlDeserializer().Deserialize(null), "Source data can't be null or empty");

        [Test]
        public void Deserialize_CustomLogger_InvalidData_WriteToLogger()
        {
            const string invalidUrl = "docs.microsoft.com/en-gb/learn/";

            var config = new LoggingConfiguration();
            config.AddTarget(new MemoryTarget("memory") { Layout = "${message}" });
            config.AddRule(LogLevel.Error, LogLevel.Fatal, "Memory");
            LogManager.Configuration = config;

            var deserializer = new UrlDeserializer(LogManager.GetLogger("Memory"));

            deserializer.Deserialize(invalidUrl);

            var logger = LogManager.Configuration.FindTargetByName<MemoryTarget>("Memory");
            Assert.IsTrue(logger.Logs.All(x => x.Contains("Invalid URL.", StringComparison.InvariantCultureIgnoreCase)));
        }

        [TestCase("https://docs.microsoft.com/en-gb/learn/")]
        public void Deserialize_ValidData_ReturnUrl(string source)
        {
            var expected = new Url("https:", "docs.microsoft.com", new string[] { "en-gb", "learn" }, null);
            var deserializer = new UrlDeserializer();

            var actual = deserializer.Deserialize(source);

            Assert.AreEqual(expected, actual.First());
        }
    }
}
