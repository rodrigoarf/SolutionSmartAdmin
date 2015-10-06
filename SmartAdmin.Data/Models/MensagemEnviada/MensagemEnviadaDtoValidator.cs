using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace SmartAdmin.Dto
{
    public class MensagemEnviadaDtoValidator : Base
    {
        [Required(ErrorMessage = "Este campo é obrigatório.")]
        public System.Int32 COD_MENSAGEM { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        public System.Int32 COD_AUTOR { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        public System.Int32 COD_REMETENTE { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [StringLength(1, ErrorMessage = "A descrição deve ter no máximo 1 caracteres.")]
        public System.String STATUS_AUTOR { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [StringLength(1, ErrorMessage = "A descrição deve ter no máximo 1 caracteres.")]
        public System.String STATUS_REMETENTE { get; set; }

    }
}

