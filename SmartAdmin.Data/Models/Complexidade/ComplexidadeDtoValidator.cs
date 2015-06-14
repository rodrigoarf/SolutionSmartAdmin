using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace SmartAdmin.Dto
{
    public class ComplexidadeDtoValidator : Base
    {
        [Required(ErrorMessage = "Este campo é obrigatório.")]
        public System.Decimal COD_REGISTRO { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [StringLength(100, ErrorMessage = "A descrição deve ter no máximo 100 caracteres.")]
        public System.String NOME { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        public System.DateTime DTH_CADASTRO { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [StringLength(1, ErrorMessage = "A descrição deve ter no máximo 1 caracteres.")]
        public System.String SEXO { get; set; }

    }
}

