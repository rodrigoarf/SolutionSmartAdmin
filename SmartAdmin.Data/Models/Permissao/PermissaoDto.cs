using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace SmartAdmin.Dto
{

    [MetadataType(typeof(PermissaoDtoValidator))]
    public class PermissaoDto : Base
    {
        public System.Int32 COD_USUARIO { get; set; }
        public System.Int32 COD_MENU { get; set; }
    }
}

