using System;
using System.Collections.Generic;

namespace Projet_File_Rouge.Stores
{
    /// <summary>
    /// Save and load infos across pages
    /// </summary>
    public class CacheStore
    {
        /// <summary>
        /// Infos about BDD Objects
        /// </summary>
        private Dictionary<ObjectCacheStoreEnum, int> objectCache;
        /// <summary>
        /// Others infos
        /// </summary>
        private Dictionary<InfoCacheStoreEnum, string> infoCache;

        public readonly Action CloseEvent;

        public CacheStore(Action CloseEvent)
        {
            objectCache = new Dictionary<ObjectCacheStoreEnum, int>();
            infoCache = new Dictionary<InfoCacheStoreEnum, string>();

            this.CloseEvent = CloseEvent;
        }

        /// <summary>
        /// Get Infos
        /// </summary>
        public int GetObjectCache(ObjectCacheStoreEnum cacheKey) { return objectCache.GetValueOrDefault(cacheKey); }
        public string GetInfoCache(InfoCacheStoreEnum cacheKey) { return infoCache.GetValueOrDefault(cacheKey); }

        /// <summary>
        /// Check if Key info have an existing value
        /// </summary>
        public bool DoesObjectCacheExist(ObjectCacheStoreEnum cacheKey) { return objectCache.TryGetValue(cacheKey, out _); }
        public bool DoesInfoCacheExist(InfoCacheStoreEnum cacheKey) { return infoCache.TryGetValue(cacheKey, out _); }

        /// <summary>
        /// Set infos by Key/Value combo
        /// </summary>
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

        /// <summary>
        /// Remove Value by Key
        /// </summary>
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
    CommandListDetail,
    MissingCallDetail,
    missingCallNumber
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
