using Moq;
using NUnit.Framework;

namespace URLParser.BLL.Contract.Tests
{
    [TestFixture]
    internal class ServiceTests
    {
        [Test]
        public void BehaviorTest_Run_InvokeOnce()
        {
            var serviceMock = new Mock<IService>();
            serviceMock.Setup(x => x.Run());
            var service = serviceMock.Object;

            service.Run();

            serviceMock.Verify(x => x.Run(), Times.Once);
        }
    }
}
