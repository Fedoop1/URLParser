using System;
using System.Collections.Generic;
using URLParser.BLL.Contract;
using URLParser.BLL.Implementation.Entities;
using URLParser.DAL.Contract;

namespace URLParser.BLL.Implementation
{
    /// <summary>
    /// Class-service that provides an to deserialize string URL data from source, serialize it to URL in XML document format and write/send to destination.
    /// </summary>
    /// <typeparam name="TResult">The type of XML document for saving/sending to destination.</typeparam>
    /// <seealso cref="URLParser.BLL.Contract.IService" />
    public class UrlParserService<TResult> : IService
    {
        private readonly ISerializer<IEnumerable<Url>, TResult> serializer;
        private readonly IDeserializer<string, IEnumerable<Url>> deserializer;
        private readonly IDataLoader<string> dataLoader;
        private readonly IDataSaver<TResult> dataSaver;

        /// <summary>
        /// Initializes a new instance of the <see cref="UrlParserService{TResult}"/> class.
        /// </summary>
        /// <param name="serializer">The serializer instance.</param>
        /// <param name="deserializer">The deserializer instance.</param>
        /// <param name="dataLoader">The data loader instance.</param>
        /// <param name="dataSaver">The data saver instance.</param>
        /// <exception cref="System.ArgumentNullException">
        /// Throws when serialzer instance is null
        /// or
        /// Throws when deserializer instance is null
        /// or
        /// Throws when data loader is null
        /// or
        /// Throws when data saver is null.
        /// </exception>
        public UrlParserService(ISerializer<IEnumerable<Url>, TResult> serializer, IDeserializer<string, IEnumerable<Url>> deserializer, IDataLoader<string> dataLoader, IDataSaver<TResult> dataSaver)
        {
            this.serializer = serializer ?? throw new ArgumentNullException(nameof(serializer), "Serializer object can't be null");
            this.deserializer = deserializer ?? throw new ArgumentNullException(nameof(deserializer), "Deserializer object can't be null");
            this.dataLoader = dataLoader ?? throw new ArgumentNullException(nameof(dataLoader), "Data loader object can't be null");
            this.dataSaver = dataSaver ?? throw new ArgumentNullException(nameof(dataSaver), "Data saver object can't be null");
        }

        /// <summary>
        /// Runs this service.
        /// </summary>
        public void Run()
        {
            var sourceData = this.dataLoader.Load();
            var urlData = this.deserializer.Deserialize(sourceData);
            var xmlData = this.serializer.Serialize(urlData);
            this.dataSaver.Save(xmlData);
        }
    }
}
