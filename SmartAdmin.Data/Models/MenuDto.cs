using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartAdmin.Data.Model
{
    [Serializable]
    public class MenuDto : Base
    {
        public Nullable<System.Int32> COD_MENU_PAI { get; set; }
        public System.String NOME { get; set; }
        public System.String CONTROLLER { get; set; }
        public System.String ACTION { get; set; }
        public System.String DESCRICAO { get; set; }
        public Nullable<System.DateTime> DTH_CADASTRO { get; set; }
        public System.String ICONE { get; set; }
        public System.String STATUS { get; set; }
    }
}

