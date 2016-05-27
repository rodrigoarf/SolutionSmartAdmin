using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartAdmin.Data.Model
{
    [Serializable]
    public class NoticiaCategoriaDto : Base
    {
        public System.String TITULO { get; set; }
        public System.String STATUS { get; set; }
    }
}

