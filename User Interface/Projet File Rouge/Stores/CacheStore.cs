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
        /// <summary>
        /// FTP infos
        /// </summary>
        private KeyValuePair<string, string> FTPCache;

        public readonly Action CloseEvent;

        public CacheStore(Action CloseEvent)
        {
            objectCache = new Dictionary<ObjectCacheStoreEnum, int>();
            infoCache = new Dictionary<InfoCacheStoreEnum, string>();
            FTPCache = default(KeyValuePair<string, string>);

            this.CloseEvent = CloseEvent;
        }

        /// <summary>
        /// Get Infos
        /// </summary>
        public int GetObjectCache(ObjectCacheStoreEnum cacheKey) { return objectCache.GetValueOrDefault(cacheKey); }
        public string GetInfoCache(InfoCacheStoreEnum cacheKey) { return infoCache.GetValueOrDefault(cacheKey); }
        public (bool, KeyValuePair<string, string>) GetFTPCache() { return DoesCacheFTPExist() ? (true, FTPCache) : (false, default(KeyValuePair<string, string>)); }

        /// <summary>
        /// Check if Key info have an existing value
        /// </summary>
        public bool DoesObjectCacheExist(ObjectCacheStoreEnum cacheKey) { return objectCache.TryGetValue(cacheKey, out _); }
        public bool DoesInfoCacheExist(InfoCacheStoreEnum cacheKey) { return infoCache.TryGetValue(cacheKey, out _); }
        public bool DoesCacheFTPExist() { return !FTPCache.Equals(default(KeyValuePair<string, string>)); }

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
        public void SetFTPCache(string cacheKey, string cacheValue)
        {
            FTPCache = new KeyValuePair<string, string>(cacheKey, cacheValue);
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
        public void CleanFTPCache(InfoCacheStoreEnum cacheKey)
        {
            FTPCache = default(KeyValuePair<string, string>);
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
    missingCallNumber,
    DocumentListDetail
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
    CommandView,
    ReturnFolder
}
