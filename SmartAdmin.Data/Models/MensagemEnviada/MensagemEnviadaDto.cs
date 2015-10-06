using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace SmartAdmin.Dto
{

    [MetadataType(typeof(MensagemEnviadaDtoValidator))]
    public class MensagemEnviadaDto : Base
    {
        public System.Int32 COD_MENSAGEM { get; set; }
        public System.Int32 COD_AUTOR { get; set; }
        public System.Int32 COD_REMETENTE { get; set; }
        public System.String STATUS_AUTOR { get; set; }
        public System.String STATUS_REMETENTE { get; set; }
    }
}

