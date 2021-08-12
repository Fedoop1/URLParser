namespace URLParser.DAL.Contract
{
    /// <summary>
    /// Interface that exposes behavior to classes with the ability to save data to some source.
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    public interface IDataSaver<in TSource>
    {
        /// <summary>
        /// Saves the specified data to source.
        /// </summary>
        /// <param name="data">The data for saving.</param>
        public void Save(TSource data);
    }
}
