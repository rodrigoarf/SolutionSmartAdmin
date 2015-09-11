using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace SmartAdmin.Dto
{

    [MetadataType(typeof(TipoContatoDtoValidator))]
    public class TipoContatoDto : Base
    {
        public System.String NOME { get; set; }
        public System.String DESCRICAO { get; set; }
        public System.String STATUS { get; set; }
    }
}

