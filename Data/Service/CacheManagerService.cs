// <copyright file="CacheManagerService.cs" company="ATEC">
// Copyright (c) ATEC. All rights reserved.
// </copyright>

using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace ATEC_API.Data.Service
{
    public class CacheManagerService
    {
        private readonly IMemoryCache _cache;

        public CacheManagerService(IMemoryCache cache)
        {
            this._cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (this._cache.TryGetValue(key, value: out T cachedValue))
            {
                return await Task.FromResult(cachedValue);
            }

            return default;
        }

        public async Task<List<T>?> GetListAsync<T>(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (this._cache.TryGetValue(key, value: out List<T> cachedList))
            {
                return await Task.FromResult(cachedList);
            }

            return null;
        }

        public async Task SetAsync<T>(string key, T value)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                           .SetPriority(CacheItemPriority.NeverRemove)
                           .SetAbsoluteExpiration(TimeSpan.FromMinutes(10));

            await Task.Run(() => this._cache.Set(key, value, cacheEntryOptions));
        }

        public async Task RemoveAsync(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            await Task.Run(() => this._cache.Remove(key));
        }
    }
}
