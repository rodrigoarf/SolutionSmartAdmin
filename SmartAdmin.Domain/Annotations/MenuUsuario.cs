using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace SmartAdmin.Domain.Annotations
{
    [Serializable]
    public abstract class MenuUsuario : Base
    {
        [Required(ErrorMessage = "Campo COD_MENU é obrigatório.")]
        public virtual System.Int32 COD_MENU { get; set; }

        [Required(ErrorMessage = "Campo COD_USUARIO é obrigatório.")]
        public virtual System.Int32 COD_USUARIO { get; set; }

    }
}

