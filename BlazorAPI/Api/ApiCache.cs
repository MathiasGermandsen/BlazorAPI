using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorAPI.Api.APIUrls
{
    public class ApiCache<T>
    {
        private readonly Dictionary<string, (T Data, DateTime Expiration)> _cache = new();
        private readonly TimeSpan _cacheDuration;

        public ApiCache(TimeSpan cacheDuration)
        {
            _cacheDuration = cacheDuration;
        }

        public void AddToCache(string key, T data)
        {
            var expiration = DateTime.UtcNow.Add(_cacheDuration);
            _cache[key] = (data, expiration);
        }

        public bool TryGetFromCache(string key, out T data)
        {
            if (_cache.TryGetValue(key, out var cachedValue))
            {
                if (cachedValue.Expiration > DateTime.UtcNow)
                {
                    data = cachedValue.Data;
                    return true;
                }

                // Remove expired entry
                _cache.Remove(key);
            }

            data = default;
            return false;
        }
    }
}