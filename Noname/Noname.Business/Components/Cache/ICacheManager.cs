﻿using System;
using System.Runtime.Caching;

namespace Abitcareer.Business.Interfaces
{
    public interface ICacheManager
    {
        TValue FromCache<TValue>(string key, Func<TValue> function);
        CacheItem ToCache<TValue>(string key, Func<TValue> function);
        void RemoveFromCache(string key);
        void ClearCacheByName(string name);
        void ClearCacheByRegion(string region);
    }
}
