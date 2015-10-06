using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace SmartAdmin.Dto
{

    [MetadataType(typeof(MensagemDtoValidator))]
    public class MensagemDto : Base
    {
        public System.Int32 COD_AUTOR { get; set; }
        public System.String TITULO { get; set; }
        public System.String TEXTO { get; set; }
        public Nullable<System.DateTime> DTH_CRIACAO { get; set; }
        public Nullable<System.DateTime> DTH_ENVIO { get; set; }
    }
}

