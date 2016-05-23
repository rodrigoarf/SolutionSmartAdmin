using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartAdmin.Data.Model
{
    [Serializable]
    public class AcessoDto : Base
    {
        public Nullable<System.Int32> COD_USUARIO { get; set; }
        public System.String TIPO_USUARIO { get; set; }
        public System.String IP { get; set; }
        public System.String BROWSER { get; set; }
        public System.String PLATAFORMA { get; set; }
        public System.String RESOLUCAO { get; set; }
        public Nullable<System.DateTime> DTH_ACESSO { get; set; }
        public Nullable<System.Int32> DIA { get; set; }
        public Nullable<System.Int32> MES { get; set; }
        public Nullable<System.Int32> ANO { get; set; }
        public System.String HORA { get; set; }
        public System.String URL_ACESSO { get; set; }
        public System.String DISPOSITIVO { get; set; }
        public System.String DOMINIO { get; set; }
        public System.String SUPORTA_ACTIVEX { get; set; }
        public System.String SUPORTA_COOKIES { get; set; }
        public System.String SUPORTA_JAVA_APPLET { get; set; }
        public System.String COORD_LATITUDE { get; set; }
        public System.String COORD_LONGITUDE { get; set; }
        public System.String CIDADE { get; set; }
    }
}

