using Moq;
using NUnit.Framework;

namespace URLParser.DAL.Contract.Tests
{
    [TestFixture]
    internal class DataStorageTests
    {
        [Test]
        public void BehaviorTest_Save_InvokeOnce()
        {
            var saverMock = new Mock<DataStorage<string, string>>();
            saverMock.Setup(x => x.Save(It.IsAny<string>()));

            var saver = saverMock.Object;
            saver.Save("some data");

            saverMock.Verify(x => x.Save(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void BehaviorTest_Load_InvokeOnce()
        {
            var loaderMock = new Mock<DataStorage<string, string>>();
            loaderMock.Setup(x => x.Load()).Returns(It.IsAny<string>);

            var loader = loaderMock.Object;
            loader.Load();

            loaderMock.Verify(x => x.Load(), Times.Once);
        }
    }
}
