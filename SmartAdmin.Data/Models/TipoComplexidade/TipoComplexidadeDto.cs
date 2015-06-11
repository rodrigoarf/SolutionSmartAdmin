using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartAdmin.Dto
{
    public class TipoComplexidadeDto : Base
    {
        public System.String NOME { get; set; }
        public System.Int32 PARAMET_INICIAL { get; set; }
        public System.Int32 PARAMET_FINAL { get; set; }
        public System.String STATUS { get; set; }
    }
}

