using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace SmartAdmin.Dto
{

    [MetadataType(typeof(UsuarioDtoValidator))]
    public class UsuarioDto : Base
    {
        public System.String NOME { get; set; }
        public System.String USUARIO { get; set; }
        public System.String SENHA { get; set; }
        public System.String EMAIL { get; set; }
        public System.DateTime DTH_CRIACAO { get; set; }
        public System.String USR_MASTER { get; set; }
        public System.String STATUS { get; set; }
        public System.String SEXO { get; set; }
    }
}

