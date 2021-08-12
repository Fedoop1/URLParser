using Moq;
using NUnit.Framework;

namespace URLParser.BLL.Contract.Tests
{
    internal class SerializerTests
    {
        [Test]
        public void Serialize_FromStringToJson_ReturnString()
        {
            var source = "Title:Title,Text:Text";
            string expected = "\"Title\":\"Title\",\"Text\":\"Text\"";

            var mockSerializer = new Mock<ISerializer<string, string>>();
            mockSerializer.Setup(x => x.Serialize(source)).Returns((string x) => expected);
            var serializer = mockSerializer.Object;

            var actual = serializer.Serialize(source);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void BehaviorTest_Serialize_InvokeOnce()
        {
            var mockSerializer = new Mock<ISerializer<string, string>>();
            mockSerializer.Setup(x => x.Serialize(It.IsAny<string>())).Returns(It.IsAny<string>());

            var serializer = mockSerializer.Object;

            serializer.Serialize("any data");

            mockSerializer.Verify(x => x.Serialize(It.IsAny<string>()), Times.Once);
        }
    }
}
