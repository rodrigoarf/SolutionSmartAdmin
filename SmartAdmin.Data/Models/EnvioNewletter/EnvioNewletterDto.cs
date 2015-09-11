using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace SmartAdmin.Dto
{

    [MetadataType(typeof(EnvioNewletterDtoValidator))]
    public class EnvioNewletterDto : Base
    {
        public System.Int32 COD_TIPO { get; set; }
        public System.String NOME { get; set; }
        public System.String HTML_BODY { get; set; }
        public System.String HTML_LINK { get; set; }
        public Nullable<System.DateTime> DTH_ENVIO { get; set; }
        public Nullable<System.DateTime> DTH_CRIACAO { get; set; }
        public System.String EXI_DESCADASTRAR { get; set; }
        public Nullable<System.Int32> TOTAL_ENVIADOS { get; set; }
        public System.String STATUS { get; set; }
    }
}

