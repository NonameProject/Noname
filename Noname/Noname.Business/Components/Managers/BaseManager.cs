using Abitcareer.Business.Interfaces;
using System;
using System.Runtime.Caching;

namespace Abitcareer.Business.Components
{
    public abstract class BaseManager
    {
        protected ICacheManager CacheManager { get; private set; }

        protected abstract string Name { get; set; }

        public BaseManager(ICacheManager cacheManager)
        {
            this.CacheManager = cacheManager;
        }

        protected TValue FromCache<TValue>(string name, Func<TValue> function)
        {
            return CacheManager.FromCache<TValue>(Name + "::" + name, function);
        }

        protected CacheItem ToCache<TValue>(string name, Func<TValue> function)
        {
            return CacheManager.ToCache<TValue>(Name + "::" + name, function);
        }

        protected void ClearCache()
        {
            CacheManager.ClearCacheByName(Name);
        }

        protected void RemoveFromCache(string name)
        {
            CacheManager.RemoveFromCache(Name + "::" + name);
        }
    }
}
