using Microsoft.Extensions.Caching.Distributed;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Msdi.Core.CrossCuttingConcerns.Caching.Redis
{
    public class RedisCacheManager : ICacheManager
    {
        private readonly IDistributedCache _cache;
        public RedisCacheManager(IDistributedCache cache)
        {
            _cache = cache;
        }

        public void Add(string key, object data, int duration = 0)
        {
            
            _cache.Set(key, data as byte[]);
        }

        public T Get<T>(string key)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (var memoryStream = new MemoryStream())
            {
                var bytes = _cache.Get(key);
                if (bytes is null) return default(T);

                memoryStream.Write(bytes, 0, bytes.Length);

                memoryStream.Seek(0, SeekOrigin.Begin);

                return (T)binaryFormatter.Deserialize(memoryStream);
            }
        }

        public object Get(string key)
        {
            return _cache.Get(key);
        }

        public bool IsExist(string key)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        public void RemoveByPattern(string pattern)
        {
            throw new System.NotImplementedException();
        }
    }
}
