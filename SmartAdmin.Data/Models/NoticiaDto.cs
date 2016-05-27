using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartAdmin.Data.Model
{
    [Serializable]
    public class NoticiaDto : Base
    {
        public System.Int32 COD_CATEGORIA { get; set; }
        public System.Int32 COD_PUBLICADOR { get; set; }
        public System.String TITULO { get; set; }
        public System.String TEXTO { get; set; }
        public Nullable<System.DateTime> DTH_CADASTRO { get; set; }
        public System.String STATUS { get; set; }
    }
}

