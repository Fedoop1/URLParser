using Moq;
using NUnit.Framework;

namespace URLParser.BLL.Contract.Tests
{
    [TestFixture]
    internal class DeserializerTests
    {
        [Test]
        public void Deserialize_FromJsonToString_ReturnString()
        {
            string source = "\"Title\":\"Title\",\"Text\":\"Text\"";
            var expected = "Title:Title,Text:Text";

            var mockDeserializer = new Mock<IDeserializer<string, string>>();
            mockDeserializer.Setup(x => x.Deserialize(source)).Returns((string x) => expected);
            var deserializer = mockDeserializer.Object;

            var actual = deserializer.Deserialize(source);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void BehaviorTest_Deserialize_InvokeOnce()
        {
            var mockDeserializer = new Mock<IDeserializer<string, string>>();
            mockDeserializer.Setup(x => x.Deserialize(It.IsAny<string>())).Returns(It.IsAny<string>());

            var deserializer = mockDeserializer.Object;

            deserializer.Deserialize("any data");

            mockDeserializer.Verify(x => x.Deserialize(It.IsAny<string>()), Times.Once);
        }
    }
}
