using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;

namespace Noname.Business.Interfaces
{
    interface ICacheManager
    {
        TValue FromCache<TValue>(string key, Func<TValue> function);
        CacheItem ToCache<TValue>(string key, Func<TValue> function);
        void RemoveFromCache(string key);
        void ClearCacheByRegion(string region);
        void ClearCacheByName(string name);
    }
}
