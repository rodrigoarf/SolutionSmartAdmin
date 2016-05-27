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
    public abstract class Noticia : Base
    {
        [Required(ErrorMessage = "Campo COD_CATEGORIA é obrigatório.")]
        public virtual System.Int32 COD_CATEGORIA { get; set; }

        [Required(ErrorMessage = "Campo COD_PUBLICADOR é obrigatório.")]
        public virtual System.Int32 COD_PUBLICADOR { get; set; }

        public System.String TITULO { get; set; }

        public System.String TEXTO { get; set; }

        public virtual Nullable<System.DateTime> DTH_CADASTRO { get; set; }

        public System.String STATUS { get; set; }

    }
}

