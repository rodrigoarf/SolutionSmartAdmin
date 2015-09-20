using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace SmartAdmin.Dto
{

    [MetadataType(typeof(BancoDtoValidator))]
    public class BancoDto : Base
    {
        public System.String NOME { get; set; }
        public System.String WEBSITE { get; set; }
        public System.String SIGLA { get; set; }
        public System.String FLAG_TIPO { get; set; }
        public System.String STATUS { get; set; }
    }
}

