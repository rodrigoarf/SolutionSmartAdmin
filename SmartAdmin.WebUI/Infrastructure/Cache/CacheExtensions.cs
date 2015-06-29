using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartAdmin.WebUI.Infrastructure.Cache
{
    public static class CacheExtensions
    {
        public static T Get<T>(this ICache CacheManager, string Key, Func<T> Acquire)
        {
            return Get(cacheManager, key, 60, acquire);
        }

        public static T Get<T>(this ICache CacheManager, string Key, int CacheTime, Func<T> Acquire)
        {
            if (cacheManager.IsSet(key))
            {
                return cacheManager.Get<T>(key);
            }
            else
            {
                var result = acquire();

                cacheManager.Set(key, result, cacheTime);

                return result;
            }
        }
    }
}