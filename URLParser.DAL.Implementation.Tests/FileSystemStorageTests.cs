using System;
using System.IO;
using NUnit.Framework;

namespace URLParser.DAL.Implementation.Tests
{
    [TestFixture]
    internal class FileSystemStorageTests
    {
        private const string SourceFilePath = "dummy.txt";
        private const string DestinationFilePath = "destination.txt";

        [Test]
        public void Ctor_SourceFileDoesntExist_ThrowArgumentException() =>
            Assert.Throws<ArgumentException>(() => new FileSystemStorage("doesntExistPath", DestinationFilePath), "Source file doesn't exist");

        [Test]
        public void Ctor_NullSourceFile_ThrowArgumentNullException() =>
            Assert.Throws<ArgumentNullException>(() => new FileSystemStorage(null, DestinationFilePath), "Source file path can't be null or empty");

        [Test]
        public void Ctor_NullDestinationFile_ThrowArgumentNullException() =>
            Assert.Throws<ArgumentException>(() => new FileSystemStorage(SourceFilePath, null), "Destination file path can't be null or empty");

        [Test]
        public void Save_NullData_ThrowArgumentNullException() =>
            Assert.Throws<ArgumentException>(() => new FileSystemStorage(SourceFilePath, DestinationFilePath).Save(null), "Data can't be null");

        [TestCase("Lorem ipsum dolor sit amet")]
        public void Save_ValidData_CreateAndSaveToFile(string data)
        {
            var storage = new FileSystemStorage(SourceFilePath, DestinationFilePath);
            storage.Save(data);

            using var streamReader = new StreamReader(DestinationFilePath);
            var fileContent = streamReader.ReadToEnd();

            Assert.IsTrue(fileContent.Contains(data, StringComparison.InvariantCultureIgnoreCase));
        }

        [Test]
        public void Load_OpenAndReadFromSourceFile_ReturnFileContent()
        {
            var expected = "Simple dummy text file\r\n@Malukov Nikita 2021";
            var storage = new FileSystemStorage(SourceFilePath, DestinationFilePath);

            var actual = storage.Load();

            Assert.AreEqual(expected, actual);
        }
    }
}
