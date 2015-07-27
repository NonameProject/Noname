using Abitcareer.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using System.Threading;

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
            CacheManager.ClearCacheByRegion(Name);
        }

        protected void RemoveFromCache(string name)
        {
            CacheManager.RemoveFromCache(Name + "::" + name);
        }

        //public Abitcareer.Business.Models.BaseModel GetBaseModel()
        //{
        //    var model = CacheManager.FromCache("asdasdasd", () =>
        //    {
        //        return new Abitcareer.Business.Models.BaseModel();//get from provider
        //    });

        //    TranslateModel(model);

        //    model.Title = Localize(model, "Title", Thread.CurrentThread.CurrentUICulture.LCID, model.Title);
        //    model.Name = Localize(model, "Name", Thread.CurrentThread.CurrentUICulture.LCID, model.Name);

        //    return model;
        //}

        //private string Localize(Abitcareer.Business.Models.BaseModel model, string keyPrefix, int languageId, string defaultValue)
        //{
        //    var key = string.Format("{0}_{1}", keyPrefix, languageId);
        //    object localizedValue;
        //    if (model.Fields.TryGetValue(key, out localizedValue))
        //    {
        //        if (localizedValue != null && !string.IsNullOrEmpty(localizedValue.ToString()))
        //            return localizedValue.ToString();
        //    }

        //    return defaultValue;
        //}

        //private void TranslateModel(Abitcareer.Business.Models.BaseModel model)
        //{
        //    var translatableProperties = new List<string>();// take by attributes with cache

        //    foreach (var item in translatableProperties)
        //    {
        //        //Localize
        //    }
        //}
    }
}
