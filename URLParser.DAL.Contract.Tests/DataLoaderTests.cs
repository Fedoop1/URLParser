using Moq;
using NUnit.Framework;

namespace URLParser.DAL.Contract.Tests
{
    [TestFixture]
    internal class DataLoaderTests
    {
        [Test]
        public void Load_ReturnDataFromStorage()
        {
            var source = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.";

            var loaderMock = new Mock<IDataLoader<string>>();
            loaderMock.Setup(x => x.Load()).Returns(source);

            var saver = loaderMock.Object;
            var actual = saver.Load();

            Assert.AreEqual(source, actual);
        }

        [Test]
        public void BehaviorTest_Load_InvokeOnce()
        {
            var loaderMock = new Mock<IDataLoader<string>>();
            loaderMock.Setup(x => x.Load()).Returns(It.IsAny<string>);

            var loader = loaderMock.Object;
            loader.Load();

            loaderMock.Verify(x => x.Load(), Times.Once);
        }
    }
}
