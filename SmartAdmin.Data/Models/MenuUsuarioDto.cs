using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartAdmin.Data.Model
{
    [Serializable]
    public class MenuUsuarioDto : Base
    {
        public System.Int32 COD_MENU { get; set; }
        public System.Int32 COD_USUARIO { get; set; }
    }
}

