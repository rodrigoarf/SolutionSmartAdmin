using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdmin.WebUI.Infrastructure.Cache
{
    public interface ICache
    {
        TEntity Get<TEntity>(string Key);  
        void Save(string Key, object Data, int CacheTime);
        bool Find(string Key);  
        void Delete(string Key); 
        void Clear();
    }
}
