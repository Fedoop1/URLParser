namespace URLParser.BLL.Contract
{
    /// <summary>
    /// Interface that declare base methods to serializer classes that serialize information from one format to another.
    /// </summary>
    /// <typeparam name="TSource">The type of the source data.</typeparam>
    /// <typeparam name="TResult">The type of the result data.</typeparam>
    public interface ISerializer<in TSource, out TResult>
    {
        /// <summary>
        /// Serializes the specified data from one format to another.
        /// </summary>
        /// <param name="data">The data to serialize.</param>
        /// <returns>Serialized data.</returns>
        public TResult Serialize(TSource data);
    }
}
