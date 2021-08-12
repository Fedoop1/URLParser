namespace URLParser.BLL.Contract
{
    /// <summary>
    /// Interface that declare base methods to deserialize classes that deserialize information from one format to another.
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    public interface IDeserializer<in TSource, out TResult>
    {
        /// <summary>
        /// Deserializes the specified data from one format to another.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>Deserialized data.</returns>
        public TResult Deserialize(TSource data);
    }
}
