using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartAdmin.Dto;

namespace SmartAdmin.WebUI.ModelView
{
    public class MensagemModelView
    {
        public int Id { get; set; }
        public UsuarioDto Usuario { get; set; }
        public DateTime? DataEnvio { get; set; }
        public string Titulo { get; set; } 
        public string Folder { get; set; }
        public bool IsRead { get; set; }
        public bool Checked { get; set; }
    }
}
