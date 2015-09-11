using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace SmartAdmin.Dto
{

    [MetadataType(typeof(TipoNewletterDtoValidator))]
    public class TipoNewletterDto : Base
    {
        public System.String NOME { get; set; }
        public Nullable<System.DateTime> DTH_CRIACAO { get; set; }
        public System.String STATUS { get; set; }
    }
}

