using System;
using System.Runtime.Caching;

namespace Common.Utils
{
    public class MemCache<TKey, TValue> : IDisposable
    {
        private readonly MemoryCache _cache;
        private readonly CacheItemPolicy _cacheItemPolicy;

        public MemCache(string name, TimeSpan slidingExpiration)
            : this(name, ObjectCache.InfiniteAbsoluteExpiration, slidingExpiration)
        {
        }

        public MemCache(string name, DateTimeOffset absoluteExpiration)
            : this(name, absoluteExpiration, ObjectCache.NoSlidingExpiration)
        {
        }

        private MemCache(string name, DateTimeOffset absoluteExpiration, TimeSpan slidingExpiration)
        {
            _cache = new MemoryCache(name);
            _cacheItemPolicy = new CacheItemPolicy()
            {
                AbsoluteExpiration = absoluteExpiration,
                SlidingExpiration = slidingExpiration
            };
        }

        public void Add(TKey key, TValue val)
        {
            string kv = key.ToString();
            if (!_cache.Add(kv, val, _cacheItemPolicy))
                _cache[kv] = val;
        }

        public TValue this[TKey key]
        {
            get { return (TValue)_cache[key.ToString()]; }
            set { Add(key, value); }
        }

        public bool Contains(TKey key)
        {
            return _cache.Contains(key.ToString());
        }

        public void Remove(TKey key)
        {
            _cache.Remove(key.ToString());
        }

        public void Dispose()
        {
            _cache.Dispose();
        }
    }
}
