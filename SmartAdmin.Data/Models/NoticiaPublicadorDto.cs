using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartAdmin.Data.Model
{
    [Serializable]
    public class NoticiaPublicadorDto : Base
    {
        public System.String NOME { get; set; }
        public System.String EMAIL { get; set; }
        public System.String STATUS { get; set; }
    }
}

