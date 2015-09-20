using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace SmartAdmin.Dto
{

    [MetadataType(typeof(CedenteDtoValidator))]
    public class CedenteDto : Base
    {
        public System.Int32 COD_BANCO { get; set; }
        public System.String NOME { get; set; }
        public System.String ENDERECO { get; set; }
        public Nullable<System.Decimal> NUMERO { get; set; }
        public System.String BAIRRO { get; set; }
        public System.String CIDADE { get; set; }
        public System.String UF { get; set; }
        public System.String CEP { get; set; }
        public System.DateTime DTH_NASCIMENTO { get; set; }
        public System.String CPF_CNPJ { get; set; }
        public System.String INSTRUCAO_BOLETO { get; set; }
        public System.String NUM_AGENCIA { get; set; }
        public System.String NUM_CONTA_CORRENTE { get; set; }
        public System.String STATUS { get; set; }
    }
}

