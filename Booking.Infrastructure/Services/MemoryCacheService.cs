using Booking.Application.Common.Services;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Infrastructure.Services
{
    public class MemoryCacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ConcurrentDictionary<string, string> _keys = new();
        private readonly ConcurrentDictionary<string, HashSet<string>> _taggedKeys = new();

        public MemoryCacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public async Task<T> GetAsync<T>(string key, Func<Task<T>> factory, TimeSpan? expiration = null)
        {
            if(_memoryCache.TryGetValue(key, out T cachedValue))
            {
                return cachedValue;
            }

            var value = await factory();

            var cacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration ?? TimeSpan.FromMinutes(5)
            };

            _memoryCache.Set(key, value, cacheEntryOptions);
            _keys.TryAdd(key, null);
            return value;
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
            _keys.TryRemove(key, out _);
        }

        public Task RemoveByPatternAsync(string pattern)
        {
            var keysToRemove = _keys.Keys
                .Where(k => IsMatch(k, pattern))
                .ToList();

            foreach (var key in keysToRemove)
            {
                Remove(key);
            }

            return Task.CompletedTask;
        }

        private bool IsMatch(string key, string pattern)
        {
            if (pattern.EndsWith("*"))
            {
                var prefix = pattern[..^1];
                return key.StartsWith(prefix, StringComparison.OrdinalIgnoreCase);
            }

            return key.Equals(pattern, StringComparison.OrdinalIgnoreCase);
        }

        public Task RemoveByTagAsync(string tag)
        {
            if (_taggedKeys.TryGetValue(tag, out var keys))
            {
                foreach (var key in keys.ToList())
                {
                    Remove(key);
                }
                _taggedKeys.TryRemove(tag, out _);
            }
            return Task.CompletedTask;
        }

        public Task SetAsync<T>(string key, T value, TimeSpan? expiration = null, params string[] tags)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration ?? TimeSpan.FromMinutes(5)
            };
            _memoryCache.Set(key, value, cacheEntryOptions);
            _keys.TryAdd(key, null);
            foreach (var tag in tags)
            {
                var keys = _taggedKeys.GetOrAdd(tag, _ => new HashSet<string>());
                lock (keys)
                {
                    keys.Add(key);
                }
            }

            return Task.CompletedTask;
        }
    }
}
