using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace SmartAdmin.Dto
{

    [MetadataType(typeof(ContatoDtoValidator))]
    public class ContatoDto : Base
    {
        public Nullable<System.Int32> COD_TIPO_CONTATO { get; set; }
        public System.String NOME { get; set; }
        public System.String EMAIL { get; set; }
        public System.String DESCRICAO { get; set; }
        public Nullable<System.DateTime> DTH_CONTATO { get; set; }
        public Nullable<System.DateTime> DTH_LEITURA { get; set; }
        public System.String STATUS { get; set; }
    }
}

