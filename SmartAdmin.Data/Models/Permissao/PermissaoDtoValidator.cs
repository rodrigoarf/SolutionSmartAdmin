using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace SmartAdmin.Dto
{
    public class PermissaoDtoValidator : Base
    {
        [Required(ErrorMessage = "Este campo é obrigatório.")]
        public System.Int32 COD_USUARIO { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        public System.Int32 COD_MENU { get; set; }

    }
}

