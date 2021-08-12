using System;
using System.IO;
using URLParser.DAL.Contract;

namespace URLParser.DAL.Implementation
{
    /// <summary>
    /// Storage that working with file system, provide read and save operations.
    /// </summary>
    public class FileSystemStorage : DataStorage<string, string>
    {
        private readonly string sourceFilePath;
        private readonly string destinationFilePath;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileSystemStorage"/> class.
        /// </summary>
        /// <param name="sourceFilePath">The source file path.</param>
        /// <param name="destinationFilePath">The destination file path.</param>
        public FileSystemStorage(string sourceFilePath, string destinationFilePath)
        {
            ValidateSourcePath(sourceFilePath);
            ValidateDestinationPath(destinationFilePath);

            this.sourceFilePath = sourceFilePath;
            this.destinationFilePath = destinationFilePath;
        }

        /// <summary>
        /// Saves the specified data to destination file.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <exception cref="ArgumentNullException">Throws when source is null.</exception>
        public override void Save(string data)
        {
            if (data is null)
            {
                throw new ArgumentNullException(nameof(data), "Data can't be null");
            }

            using var streamWriter = new StreamWriter(new FileStream(this.destinationFilePath, FileMode.Create));
            streamWriter.Write(data);
        }

        /// <summary>
        /// Loads specified data from source file.
        /// </summary>
        /// <returns>Source file data.</returns>
        public override string Load()
        {
            using var streamReader = new StreamReader(new FileStream(this.sourceFilePath, FileMode.Open));
            return streamReader.ReadToEnd();
        }

        private static void ValidateSourcePath(string sourceFilePath)
        {
            if (string.IsNullOrEmpty(sourceFilePath))
            {
                throw new ArgumentNullException(nameof(sourceFilePath), "Source file path can't be null or empty");
            }

            if (!File.Exists(sourceFilePath))
            {
                throw new ArgumentException("Source file doesn't exist");
            }
        }

        private static void ValidateDestinationPath(string destinationFilePath)
        {
            if (string.IsNullOrEmpty(destinationFilePath))
            {
                throw new ArgumentNullException(nameof(destinationFilePath), "Destination file path can't be null or empty");
            }
        }
    }
}
