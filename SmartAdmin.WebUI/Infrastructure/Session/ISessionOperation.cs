using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdmin.WebUI.Infrastructure.Session
{
    public interface ISessionOperation<TEntity> where TEntity : class
    {
        void Start(TEntity arg);
        void Finish();
        bool IsActive();
        string GetSessionId();
        TEntity GetObjectFromSession();
    }
}
