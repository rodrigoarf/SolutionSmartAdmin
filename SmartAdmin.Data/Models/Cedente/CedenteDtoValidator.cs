using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace SmartAdmin.Dto
{
    public class CedenteDtoValidator : Base
    {
        [Required(ErrorMessage = "Este campo é obrigatório.")]
        public System.Int32 COD_BANCO { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [StringLength(100, ErrorMessage = "A descrição deve ter no máximo 100 caracteres.")]
        public System.String NOME { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [StringLength(100, ErrorMessage = "A descrição deve ter no máximo 100 caracteres.")]
        public System.String ENDERECO { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [StringLength(100, ErrorMessage = "A descrição deve ter no máximo 100 caracteres.")]
        public System.String BAIRRO { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [StringLength(100, ErrorMessage = "A descrição deve ter no máximo 100 caracteres.")]
        public System.String CIDADE { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [StringLength(2, ErrorMessage = "A descrição deve ter no máximo 2 caracteres.")]
        public System.String UF { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        public System.DateTime DTH_NASCIMENTO { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [StringLength(10, ErrorMessage = "A descrição deve ter no máximo 10 caracteres.")]
        public System.String NUM_AGENCIA { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [StringLength(10, ErrorMessage = "A descrição deve ter no máximo 10 caracteres.")]
        public System.String NUM_CONTA_CORRENTE { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [StringLength(1, ErrorMessage = "A descrição deve ter no máximo 1 caracteres.")]
        public System.String STATUS { get; set; }

    }
}

