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
    public abstract class NoticiaCategoria : Base
    {
        public System.String TITULO { get; set; }

        [Required(ErrorMessage = "Campo STATUS é obrigatório.")]
        [StringLength(1, ErrorMessage = "A descrição do campo STATUS deve ter no máximo 1 caracteres.")]
        public virtual System.String STATUS { get; set; }

    }
}

