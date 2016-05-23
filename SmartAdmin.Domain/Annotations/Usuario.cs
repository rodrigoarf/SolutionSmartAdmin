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
    public abstract class Usuario : Base
    {
        [Required(ErrorMessage = "Campo NOME é obrigatório.")]
        [StringLength(200, ErrorMessage = "A descrição do campo NOME deve ter no máximo 200 caracteres.")]
        public virtual System.String NOME { get; set; }

        public System.String CPF_CNPJ { get; set; }

        [Required(ErrorMessage = "Campo LOGIN é obrigatório.")]
        [StringLength(20, ErrorMessage = "A descrição do campo LOGIN deve ter no máximo 20 caracteres.")]
        public virtual System.String LOGIN { get; set; }

        public System.String SENHA { get; set; }

        [Required(ErrorMessage = "Campo EMAIL é obrigatório.")]
        [StringLength(100, ErrorMessage = "A descrição do campo EMAIL deve ter no máximo 100 caracteres.")]
        public virtual System.String EMAIL { get; set; }

        public virtual Nullable<System.DateTime> DTH_CRIACAO { get; set; }

        public virtual Nullable<System.DateTime> DTH_CANCELAMENTO { get; set; }

        [Required(ErrorMessage = "Campo ENDERECO é obrigatório.")]
        [StringLength(200, ErrorMessage = "A descrição do campo ENDERECO deve ter no máximo 200 caracteres.")]
        public virtual System.String ENDERECO { get; set; }

        public Nullable<System.Decimal> NUMERO { get; set; }

        [Required(ErrorMessage = "Campo CIDADE é obrigatório.")]
        [StringLength(100, ErrorMessage = "A descrição do campo CIDADE deve ter no máximo 100 caracteres.")]
        public virtual System.String CIDADE { get; set; }

        [Required(ErrorMessage = "Campo UF é obrigatório.")]
        [StringLength(2, ErrorMessage = "A descrição do campo UF deve ter no máximo 2 caracteres.")]
        public virtual System.String UF { get; set; }

        [Required(ErrorMessage = "Campo BAIRRO é obrigatório.")]
        [StringLength(100, ErrorMessage = "A descrição do campo BAIRRO deve ter no máximo 100 caracteres.")]
        public virtual System.String BAIRRO { get; set; }

        [Required(ErrorMessage = "Campo PAIS é obrigatório.")]
        [StringLength(100, ErrorMessage = "A descrição do campo PAIS deve ter no máximo 100 caracteres.")]
        public virtual System.String PAIS { get; set; }

        public System.String CEP { get; set; }

        public System.String COMPLEMENTO { get; set; }

        public System.String TELEFONE { get; set; }

        public System.String CELULAR { get; set; }

        [Required(ErrorMessage = "Campo SEXO é obrigatório.")]
        [StringLength(1, ErrorMessage = "A descrição do campo SEXO deve ter no máximo 1 caracteres.")]
        public virtual System.String SEXO { get; set; }

        [Required(ErrorMessage = "Campo FLAG_ADM é obrigatório.")]
        [StringLength(1, ErrorMessage = "A descrição do campo FLAG_ADM deve ter no máximo 1 caracteres.")]
        public virtual System.String FLAG_ADM { get; set; }

        public System.String FOTO { get; set; }

        [Required(ErrorMessage = "Campo STATUS é obrigatório.")]
        [StringLength(1, ErrorMessage = "A descrição do campo STATUS deve ter no máximo 1 caracteres.")]
        public virtual System.String STATUS { get; set; }

    }
}

