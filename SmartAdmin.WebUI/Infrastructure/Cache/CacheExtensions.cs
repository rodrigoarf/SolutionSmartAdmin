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
            return Get(CacheManager, Key, 60, Acquire);
        }

        public static T Get<T>(this ICache CacheManager, string Key, int CacheTime, Func<T> Acquire)
        {
            if (CacheManager.Find(Key))
            {
                return CacheManager.Get<T>(Key);
            }
            else
            {
                var result = Acquire(); 
                CacheManager.Save(Key, result, CacheTime);
                return result;
            }
        }
    }
}