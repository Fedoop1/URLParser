using System;
using System.Collections.Generic;
using NUnit.Framework;
using URLParser.BLL.Contract;
using URLParser.BLL.Implementation.Entities;
using Moq;
using URLParser.DAL.Contract;

namespace URLParser.BLL.Implementation.Tests
{
    [TestFixture]
    internal class UrlParserServiceTests
    {
        private Mock<ISerializer<IEnumerable<Url>, string>> serializerMock;
        private Mock<IDeserializer<string, IEnumerable<Url>>> deserializerMock;
        private Mock<IDataLoader<string>> dataLoaderMock;
        private Mock<IDataSaver<string>> dataSaverMock;

        [OneTimeSetUp]
        public void Setup()
        {
            serializerMock = new Mock<ISerializer<IEnumerable<Url>, string>>();
            deserializerMock = new Mock<IDeserializer<string, IEnumerable<Url>>>();
            dataLoaderMock = new Mock<IDataLoader<string>>();
            dataSaverMock = new Mock<IDataSaver<string>>();
        }

        [Test]
        public void Ctor_NullSerializer_ThrowArgumentNullException() => Assert.Throws<ArgumentNullException>(() =>
            new UrlParserService<string>(null, deserializerMock.Object, dataLoaderMock.Object, dataSaverMock.Object), "Serializer object can't be null");

        [Test]
        public void Ctor_NullDeserializer_ThrowArgumentNullException() => Assert.Throws<ArgumentNullException>(() =>
            new UrlParserService<string>(serializerMock.Object, null, dataLoaderMock.Object, dataSaverMock.Object), "Deserializer object can't be null");

        [Test]
        public void Ctor_NullDataLoader_ThrowArgumentNullException() => Assert.Throws<ArgumentNullException>(() =>
            new UrlParserService<string>(serializerMock.Object, deserializerMock.Object, null, dataSaverMock.Object), "Data loader object can't be null");

        [Test]
        public void Ctor_NullDataSaver_ThrowArgumentNullException() => Assert.Throws<ArgumentNullException>(() =>
            new UrlParserService<string>(null, deserializerMock.Object, dataLoaderMock.Object, null), "Data saver object can't be null");

        [Test]
        public void BehaviorTest_ServiceInvokeDependenciesOnce()
        {
            var service = new UrlParserService<string>(serializerMock.Object, deserializerMock.Object,
                dataLoaderMock.Object, dataSaverMock.Object);

            service.Run();

            serializerMock.Verify();
            deserializerMock.Verify();
            dataLoaderMock.Verify();
            dataSaverMock.Verify();
        }
    }
}
