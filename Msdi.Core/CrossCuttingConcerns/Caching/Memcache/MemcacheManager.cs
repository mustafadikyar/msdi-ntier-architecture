using Enyim.Caching;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Msdi.Core.CrossCuttingConcerns.Caching.Memcache
{
    public class MemcacheManager : ICacheManager
    {
        private readonly IMemcachedClient _memcached;
        public MemcacheManager(IMemcachedClient memcached)
        {
            _memcached = memcached;
        }

        public void Add(string key, object data, int duration)
        {
            _memcached.Set(key, data, duration);
        }

        public T Get<T>(string key)
        {
            var data = _memcached.Get<T>(key);
            if (data == null)
            {
                return default(T);
            }
            else
            {
                return data;
            }
        }

        public object Get(string key)
        {
            return _memcached.Get(key);
        }

        public bool IsExist(string key)
        {
            return _memcached.TryGet(key, out _);
        }

        public void Remove(string key)
        {
            _memcached.Remove(key);
        }

        //TO DO : Test 
        public void RemoveByPattern(string pattern)
        {
            var cacheEntriesCollectionDefinition = typeof(MemcachedClient).GetProperty("EntriesCollection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var cacheEntriesCollection = cacheEntriesCollectionDefinition.GetValue(_memcached) as dynamic;
            List<dynamic> cacheCollectionValues = new List<dynamic>();

            foreach (var cacheItem in cacheEntriesCollection)
            {
                var cacheItemValue = cacheItem.GetType().GetProperty("Value").GetValue(cacheItem, null);
                cacheCollectionValues.Add(cacheItemValue);
            }

            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = cacheCollectionValues.Where(d => regex.IsMatch(d.Key.ToString())).Select(d => d.Key).ToList();

            foreach (var key in keysToRemove)
            {
                _memcached.Remove(key);
            }
        }
    }
}
