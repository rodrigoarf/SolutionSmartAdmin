using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartAdmin.Dto;

namespace SmartAdmin.WebUI.ModelView
{
    public class MensagemModelView
    {
        public int MensagemId { get; set; }
        public UsuarioDto MensagemAutor { get; set; } // Autor da mensagem
        public virtual List<UsuarioDto> MensagemRemetentes { get; set; } // Coleção de Remetentes
        public DateTime? MensagemDataEnvio { get; set; }
        public DateTime? MensagemDataCriacao { get; set; }
        public string MensagemTitulo { get; set; }
        public string MensagemTexto { get; set; }
        public EMensagemFolder MensagemPasta { get; set; } //-- Box: 1 ENTRADA, 2 ENVIADOS, 3 IMPORTANTE, 4 LIXEIRA, 5 RASCUNHOS
        public bool MensagemLida { get; set; }    //-- Lida = true, Nao Lida = false
        public bool MensagemChecked { get; set; } //-- Checked no Front-end true or false
    }

    public enum EMensagemFolder
    { 
         CaixaDeEntrada = 1,
         CaixaDeSaida = 2,
         CaixaDeImportantes = 3,
         CaixaDeLixoEletronico = 4,
         CaixaDeRascunhos = 5
    }
}
