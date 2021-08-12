namespace URLParser.DAL.Contract
{
    /// <summary>
    /// Abstract class that declare expose base behavior to inheritors which should be able to save and load data from some source.
    /// </summary>
    /// <typeparam name="TSource">The type of the source data.</typeparam>
    /// <typeparam name="TResult">The type of the result data.</typeparam>
    /// <seealso cref="URLParser.DAL.Contract.IDataSaver&lt;TResult&gt;" />
    /// <seealso cref="URLParser.DAL.Contract.IDataLoader&lt;TSource&gt;" />
    public abstract class DataStorage<TSource, TResult> : IDataSaver<TResult>, IDataLoader<TSource>
    {
        /// <inheritdoc/>
        public abstract void Save(TResult data);

        /// <inheritdoc/>
        public abstract TSource Load();
    }
}
