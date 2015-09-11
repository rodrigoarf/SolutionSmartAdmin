using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace SmartAdmin.Dto
{

    [MetadataType(typeof(InboxDtoValidator))]
    public class InboxDto : Base
    {
        public System.Int32 COD_USUARIO { get; set; }
        public System.Int32 COD_USUARIO_REMETENTE { get; set; }
        public System.Int32 COD_LOCAL { get; set; }
        public System.String TITULO { get; set; }
        public System.String MENSAGEM { get; set; }
        public Nullable<System.DateTime> DTH_ENVIO { get; set; }
        public Nullable<System.DateTime> DTH_LEITURA { get; set; }
    }
}

