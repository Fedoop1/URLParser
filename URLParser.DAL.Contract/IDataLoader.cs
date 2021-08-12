namespace URLParser.DAL.Contract
{
    /// <summary>
    /// Interface that exposes behavior to classes with the ability to load data from some source.
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    public interface IDataLoader<out TSource>
    {
        /// <summary>
        /// Loads the specified data from source.
        /// </summary>
        /// <returns>Data form source.</returns>
        public TSource Load();
    }
}
