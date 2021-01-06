using Microsoft.Extensions.Caching.Distributed;
using Msdi.Core.ClassAttributes;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using static Msdi.Core.Enumerations.ServiceLifetime;

namespace Msdi.Core.CrossCuttingConcerns.Caching.Redis
{
    /// <summary>
    /// Implementation of redis caching service (Singleton)
    /// </summary>
    [Service(Lifetime.Singleton)]
    public class RedisCacheManager : ICacheManager
    {
        /// <summary>
        /// List of keys
        /// </summary>
        private List<string> StoredKeys { get; set; }

        /// <summary>
        /// JSON Serializer settings
        /// </summary>
        private JsonSerializerSettings JsonSerializerSettings { get; }


        /// <summary>
        /// Instance of redis
        /// </summary>
        private readonly IDistributedCache _cache;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cache">Distributed caching (Redis)</param>
        public RedisCacheManager(IDistributedCache cache)
        {
            _cache = cache;
        }


        /// <summary>
        /// Sets an object of type T to a redis key
        /// </summary>
        /// <param name="key">Key to access the object</param>
        /// <param name="data"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public void Add(string key, object data, int duration = 0)
        {
            
            _cache.Set(key, data as byte[]);
        }


        /// <summary>
        /// Gets the data corresponding to a redis key
        /// </summary>
        /// <typeparam name="T">Type of the object</typeparam>
        /// <param name="key">Key to access the object</param>
        /// <returns></returns>
        public T Get<T>(string key)
        {
            var value = _cache.GetString(key);
            return value == null
                    ? default
                    : JsonConvert.DeserializeObject<T>(value, JsonSerializerSettings);
        }

        /// <summary>
        /// Gets the data corresponding to a redis key
        /// </summary>
        /// <param name="key">Key to access the object</param>
        /// <returns></returns>
        public object Get(string key)
        {
            return _cache.Get(key);
        }

        /// <summary>
        /// Returns true if an entry with specified redis key exists
        /// </summary>
        /// <param name="key">Key to verify</param>
        /// <returns></returns>
        public bool IsExist(string key)
        {
            // var value = _redisCache.GetString(key);
            // return value != null;
            return StoredKeys.Contains(key);
        }

        /// <summary>
        /// Deletes a specified redis key
        /// </summary>
        /// <param name="key">Key to delete</param>
        /// <returns></returns>
        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        public void RemoveByPattern(string pattern)
        {
            throw new System.NotImplementedException();
        }


        #region -- Private Methods --

        private void ManageKeys(string key, bool delete = false)
        {
            if (delete)
            {
                if (StoredKeys.Contains(key))
                {
                    StoredKeys.Remove(key);
                }

                return;
            }

            StoredKeys.Add(key);
            StoredKeys = StoredKeys.Distinct().ToList();
        }

        private List<string> GetKeys()
        {
            return StoredKeys;
        }

        #endregion
    }
}
