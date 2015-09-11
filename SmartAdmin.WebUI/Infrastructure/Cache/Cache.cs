using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;          
using System.Runtime.Caching;

namespace SmartAdmin.WebUI.Infrastructure.Cache
{
    /// <summary>
    /// Caching enables you to store data in memory for rapid access. Applications can access the cache and not have to retrieve 
    /// the data from the original source whenever the data is accessed. This avoids repeated queries for data, and it can improve 
    /// performance and scalability. In addition, caching makes data available when the data source is temporarily unavailable.
    /// 
    /// Microsoft Enterprise Library Caching Application Block Pattern.
    /// https://msdn.microsoft.com/en-us/library/ff477235.aspx
    /// http://stackoverflow.com/questions/17653124/how-to-cache-database-tables-to-prevent-many-database-queries-in-asp-net-c-sharp
    /// </summary>
    public class CacheManager : ICache
    {          
        private ObjectCache Cache
        {
            get
            {
                return MemoryCache.Default;
            }
        }

        public TEntity Get<TEntity>(string Key)
        {
            return (TEntity)Cache[Key];
        }

        public void Save(string Key, object Data, int CacheTime)
        {
            if (Data == null)
            {
                return;
            }

            CacheItemPolicy policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTime.Now + TimeSpan.FromMinutes(CacheTime);

            Cache.Add(new CacheItem(Key, Data), policy);
        }

        public bool Find(string Key)
        {
            return (Cache.Contains(Key));
        }

        public void Delete(string Key)
        {
            Cache.Remove(Key);
        }

        public void Clear()
        {
            foreach (var item in Cache)
            {
                Delete(item.Key);
            }
        }  
    }
}