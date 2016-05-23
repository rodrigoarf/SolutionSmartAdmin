using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartAdmin.Domain.Filters
{
    public class UsuarioFilter : Base
    {
        public System.String NOME { get; set; }
        public System.String CPF_CNPJ { get; set; }
        public System.String LOGIN { get; set; }
        public System.String SENHA { get; set; }
        public System.String EMAIL { get; set; }
        public Nullable<System.DateTime> DTH_CRIACAO { get; set; }
        public Nullable<System.DateTime> DTH_CANCELAMENTO { get; set; }
        public System.String ENDERECO { get; set; }
        public Nullable<System.Decimal> NUMERO { get; set; }
        public System.String CIDADE { get; set; }
        public System.String UF { get; set; }
        public System.String BAIRRO { get; set; }
        public System.String PAIS { get; set; }
        public System.String CEP { get; set; }
        public System.String COMPLEMENTO { get; set; }
        public System.String TELEFONE { get; set; }
        public System.String CELULAR { get; set; }
        public System.String SEXO { get; set; }
        public System.String FLAG_ADM { get; set; }
        public System.String FOTO { get; set; }
        public System.String STATUS { get; set; }
    }
}

