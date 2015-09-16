using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace SmartAdmin.Dto
{
    public class UsuarioDtoValidator : Base
    {
        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [StringLength(200, ErrorMessage = "A descrição deve ter no máximo 200 caracteres.")]
        public System.String NOME { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [StringLength(20, ErrorMessage = "A descrição deve ter no máximo 20 caracteres.")]
        public System.String LOGIN { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [StringLength(100, ErrorMessage = "A descrição deve ter no máximo 100 caracteres.")]
        public System.String EMAIL { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [StringLength(1, ErrorMessage = "A descrição deve ter no máximo 1 caracteres.")]
        public System.String STATUS { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [StringLength(1, ErrorMessage = "A descrição deve ter no máximo 1 caracteres.")]
        public System.String SEXO { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [StringLength(200, ErrorMessage = "A descrição deve ter no máximo 200 caracteres.")]
        public System.String ENDERECO { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [StringLength(100, ErrorMessage = "A descrição deve ter no máximo 100 caracteres.")]
        public System.String CIDADE { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [StringLength(2, ErrorMessage = "A descrição deve ter no máximo 2 caracteres.")]
        public System.String UF { get; set; }

    }
}

