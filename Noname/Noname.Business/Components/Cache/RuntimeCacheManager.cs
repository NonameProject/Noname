using Abitcareer.Business.Components;
using Abitcareer.Business.Interfaces;
using System;
using System.Runtime.Caching;

namespace Abitcareer.Web.Components
{
    public class RuntimeCacheManager : ICacheManager
    {
        ObjectCache cache;

        public RuntimeCacheManager()
        {
            cache = MemoryCache.Default;
        }

        public TValue FromCache<TValue>(string key, Func<TValue> function)
        {
            var itemToSave = function();

            if (itemToSave != null)
            {
                CacheItem cacheItem = ToCache<TValue>(key,
                    () =>
                    {
                        return itemToSave;
                    });
            }

            return itemToSave;
        }

        public void ClearCacheByName(string name)
        {
            ClearCacheHelper(key => key.Contains(name));

        }

        public void ClearCacheByRegion(string region)
        {
            ClearCacheHelper( key => 
            {
                return key.StartsWith(region) || key.StartsWith(EnvironmentInfo.Instance.ReminderCacheName);
            });
        }

        public CacheItem ToCache<TValue>(string key, Func<TValue> function)
        {
            var itemToSave = function();

            if (itemToSave == null)
            {
                return null;
            }

            CacheItem cacheItem = cache.GetCacheItem(key);
            if (cacheItem == null)
            {
                CacheItemPolicy policy = new CacheItemPolicy();
                policy.AbsoluteExpiration = DateTimeOffset.Now.AddHours(1);
                cacheItem = new CacheItem(key, itemToSave);
                cache.Set(cacheItem, policy);
            }
            return cacheItem;
        }

        public void RemoveFromCache(string key)
        {
            CacheItem cacheItem = cache.GetCacheItem(key);
            if (cacheItem != null)
            {
                MemoryCache.Default.Remove(cacheItem.Key);
            }
        }

        void ClearCacheHelper(Func<string, bool> func)
        {
            foreach (var item in cache)
            {
                if (func(item.Key))
                {
                    MemoryCache.Default.Remove(item.Key);
                }
            }
        }
    }
}
