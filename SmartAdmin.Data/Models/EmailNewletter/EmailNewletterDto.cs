using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace SmartAdmin.Dto
{

    [MetadataType(typeof(EmailNewletterDtoValidator))]
    public class EmailNewletterDto : Base
    {
        public System.Int32 COD_TIPO { get; set; }
        public System.String NOME { get; set; }
        public Nullable<System.DateTime> DTH_CRIACAO { get; set; }
        public System.String STATUS { get; set; }
    }
}

