using System.Collections.Generic;
using Projet_File_Rouge.Object;

namespace Projet_File_Rouge.Stores
{
    public class CacheStore
    {
        private Dictionary<CacheStoreEnum, BDDObject> caches;

        public CacheStore()
        {
            caches = new Dictionary<CacheStoreEnum, BDDObject>();
        }

        public BDDObject GetCache(CacheStoreEnum cacheKey)
        {
            BDDObject cacheValue = caches.GetValueOrDefault(cacheKey);

            return cacheValue != null ? cacheValue : null;
        }

        public bool DoesCacheExist(CacheStoreEnum cacheKey)
        {
            BDDObject cacheValue = caches.GetValueOrDefault(cacheKey);

            return cacheValue != null ? true : false;
        }

        public void SetCache(CacheStoreEnum cacheKey, BDDObject cacheValue)
        {
            if (caches.GetValueOrDefault(cacheKey) != null) { caches.Remove(cacheKey); }

            caches.Add(cacheKey, cacheValue);
        }
    }
}

public enum CacheStoreEnum
{
    UserDetail
}
