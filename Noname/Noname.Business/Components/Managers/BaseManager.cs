using Abitcareer.Business.Interfaces;
using Abitcareer.Business.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Caching;
using System.Threading;
using System.Reflection;

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

        public Abitcareer.Business.Models.BaseModel GetBaseModel(BaseModel model)
        {
            TranslateModel(model);
            return model;
        }

        private string Localize(Abitcareer.Business.Models.BaseModel model, string keyPrefix, int languageId, string defaultValue)
        {
            var key = string.Format("{0}_{1}", keyPrefix, languageId);
            string localizedValue;
            if (model.Fields.TryGetValue(key, out localizedValue))
            {
                if (localizedValue != null && !string.IsNullOrEmpty(localizedValue.ToString()))
                    return localizedValue.ToString();
            }
            return defaultValue;
        }

        private void TranslateModel(Abitcareer.Business.Models.BaseModel model)
        {
            Type type = model.GetType();

            var translatableProperties = model.GetType().GetProperties().Where(
            p => p.GetCustomAttributes(typeof(LocalizableFieldAttribute), true).Length != 0);

            foreach (var item in translatableProperties)
            {
                PropertyInfo prop = type.GetProperty(item.Name);

                prop.SetValue(model,
                    Localize(model,
                        item.Name,
                        Thread.CurrentThread.CurrentUICulture.LCID,
                        (string)type.GetProperty(item.Name).GetValue(model, null)),
                    null);
            }
        }
    }
}
