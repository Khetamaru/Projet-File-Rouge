using System;
using System.Collections.Generic;

namespace Projet_File_Rouge.Stores
{
    public class CacheStore
    {
        private Dictionary<ObjectCacheStoreEnum, int> objectCache;
        private Dictionary<InfoCacheStoreEnum, string> infoCache;

        public readonly Action CloseEvent;

        public CacheStore(Action CloseEvent)
        {
            objectCache = new Dictionary<ObjectCacheStoreEnum, int>();
            infoCache = new Dictionary<InfoCacheStoreEnum, string>();

            this.CloseEvent = CloseEvent;
        }

        public int GetObjectCache(ObjectCacheStoreEnum cacheKey) { return objectCache.GetValueOrDefault(cacheKey); }
        public string GetInfoCache(InfoCacheStoreEnum cacheKey) { return infoCache.GetValueOrDefault(cacheKey); }

        public bool DoesObjectCacheExist(ObjectCacheStoreEnum cacheKey) { return objectCache.TryGetValue(cacheKey, out _); }
        public bool DoesInfoCacheExist(InfoCacheStoreEnum cacheKey) { return infoCache.TryGetValue(cacheKey, out _); }

        public void SetObjectCache(ObjectCacheStoreEnum cacheKey, int cacheValue)
        {
            if (DoesObjectCacheExist(cacheKey)) { objectCache.Remove(cacheKey); }
            objectCache.Add(cacheKey, cacheValue);
        }
        public void SetInfoCache(InfoCacheStoreEnum cacheKey, string cacheValue)
        {
            if (DoesInfoCacheExist(cacheKey)) { infoCache.Remove(cacheKey); }
            infoCache.Add(cacheKey, cacheValue);
        }

        public void CleanObjectCache(ObjectCacheStoreEnum cacheKey)
        {
            objectCache.Remove(cacheKey);
        }
        public void CleanInfoCache(InfoCacheStoreEnum cacheKey)
        {
            infoCache.Remove(cacheKey);
        }
    }
}

public enum ObjectCacheStoreEnum
{
    ActualUser,
    UserDetail,
    RedWireDetail,
    notifNumber,
    CommandListDetail
}
public enum InfoCacheStoreEnum
{
    PreviousPageRedWire,
    SaleDocumentDetail
}
public enum PageNameEnum
{
    GlobalCenter,
    FreeFolder,
    PersoSpace,
    OldFolder,
    CommandView
}
