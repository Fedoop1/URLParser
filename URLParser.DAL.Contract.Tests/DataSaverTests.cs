using Moq;
using NUnit.Framework;

namespace URLParser.DAL.Contract.Tests
{
    [TestFixture]
    internal class DataSaverTests
    {
        [Test]
        public void BehaviorTest_Save_InvokeOnce()
        {
            var saverMock = new Mock<IDataSaver<string>>();
            saverMock.Setup(x => x.Save(It.IsAny<string>()));

            var saver = saverMock.Object;
            saver.Save("some data");

            saverMock.Verify(x => x.Save(It.IsAny<string>()), Times.Once);
        }
    }
}
