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
    public abstract class Menu : Base
    {
        public Nullable<System.Int32> COD_MENU_PAI { get; set; }

        [Required(ErrorMessage = "Campo NOME é obrigatório.")]
        [StringLength(100, ErrorMessage = "A descrição do campo NOME deve ter no máximo 100 caracteres.")]
        public virtual System.String NOME { get; set; }

        public System.String CONTROLLER { get; set; }

        public System.String ACTION { get; set; }

        public System.String DESCRICAO { get; set; }

        public virtual Nullable<System.DateTime> DTH_CADASTRO { get; set; }

        public System.String ICONE { get; set; }

        [Required(ErrorMessage = "Campo STATUS é obrigatório.")]
        [StringLength(1, ErrorMessage = "A descrição do campo STATUS deve ter no máximo 1 caracteres.")]
        public virtual System.String STATUS { get; set; }

    }
}

