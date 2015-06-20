using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdmin.WebUI.Infrastructure.Cache
{
    public interface ICache
    {
        T Get<T>(string Key);

        void Set(string Key, object Data, int CacheTime);

        bool IsSet(string Key);

        void Remove(string Key);

        void Clear();
    }
}
