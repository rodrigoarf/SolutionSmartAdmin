using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace SmartAdmin.Dto
{

    [MetadataType(typeof(ComplexidadeDtoValidator))]
    public class ComplexidadeDto : Base
    {
        public System.Decimal COD_REGISTRO { get; set; }
        public System.String NOME { get; set; }
        public System.DateTime DTH_CADASTRO { get; set; }
        public System.Decimal ESTADO_MENTAL { get; set; }
        public System.Decimal OXIGENACAO { get; set; }
        public System.Decimal SINAIS_VITAIS { get; set; }
        public System.Decimal MOTILIDADE { get; set; }
        public System.Decimal DEAMBULACAO { get; set; }
        public System.Decimal ALIMENTACAO { get; set; }
        public System.Decimal CUIDADO_CORPORAL { get; set; }
        public System.Decimal ELIMINACAO { get; set; }
        public System.Decimal TERAPEUTICA { get; set; }
        public System.Decimal TOTAL { get; set; }
        public System.Int32 TIPO_COMPLEXIDADE { get; set; }
        public System.String SEXO { get; set; }
    }
}

