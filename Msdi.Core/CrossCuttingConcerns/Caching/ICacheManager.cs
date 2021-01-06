namespace Msdi.Core.CrossCuttingConcerns.Caching
{
    public interface ICacheManager
    {

        /// <summary>
        /// Gets the data corresponding to a redis key
        /// </summary>
        /// <typeparam name="T">Type of the object</typeparam>
        /// <param name="key">Key to access the object</param>
        /// <returns></returns>
        T Get<T>(string key);


        /// <summary>
        /// Gets the data corresponding to a redis key
        /// </summary>
        /// <param name="key">Key to access the object</param>
        /// <returns></returns>
        object Get(string key);

        /// <summary>
        /// Sets an object of type T to a redis key
        /// </summary>
        /// <typeparam name="T">Type of object to set</typeparam>
        /// <param name="key">Key to access the object</param>
        /// <param name="data"></param>
        /// <returns></returns>
        void Add(string key, object data, int duration);

        /// <summary>
        /// Returns true if an entry with specified redis key exists
        /// </summary>
        /// <param name="key">Key to verify</param>
        bool IsExist(string key);

        /// <summary>
        /// Deletes a specified redis key
        /// </summary>
        /// <param name="key">Key to delete</param>
        /// <returns></returns>
        void Remove(string key);

        /// <summary>
        /// Deletes a specified redis key
        /// </summary>
        /// <param name="pattern">Key to delete with pattern</param>
        /// <returns></returns>
        void RemoveByPattern(string pattern);
    }
}
