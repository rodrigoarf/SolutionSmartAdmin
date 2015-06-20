using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;          
using System.Runtime.Caching;

namespace SmartAdmin.WebUI.Infrastructure.Cache
{
    /// <summary>
    /// Microsoft Enterprise Library Caching Application Block Pattern.
    /// http://stackoverflow.com/questions/17653124/how-to-cache-database-tables-to-prevent-many-database-queries-in-asp-net-c-sharp
    /// </summary>
    public class CacheManeger : ICache
    {          
        private ObjectCache Cache
        {
            get
            {
                return MemoryCache.Default;
            }
        }

        public T Get<T>(string Key)
        {
            return (T)Cache[Key];
        }

        public void Set(string Key, object Data, int CacheTime)
        {
            if (Data == null)
            {
                return;
            }

            CacheItemPolicy policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTime.Now + TimeSpan.FromMinutes(CacheTime);

            Cache.Add(new CacheItem(Key, Data), policy);
        }

        public bool IsSet(string Key)
        {
            return (Cache.Contains(Key));
        }

        public void Remove(string Key)
        {
            Cache.Remove(Key);
        }

        public void Clear()
        {
            foreach (var item in Cache)
            {
                Remove(item.Key);
            }
        }  
    }
}