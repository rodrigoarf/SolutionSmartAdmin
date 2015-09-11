using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace SmartAdmin.Dto
{

    [MetadataType(typeof(MenuDtoValidator))]
    public class MenuDto : Base
    {
        public System.String NOME { get; set; }
        public System.String CONTROLLER { get; set; }
        public System.String ACTION { get; set; }
        public System.String DESCRICAO { get; set; }
        public Nullable<System.DateTime> DTH_CADASTRO { get; set; }
        public System.String ICONE { get; set; }
        public System.String STATUS { get; set; }
    }
}

