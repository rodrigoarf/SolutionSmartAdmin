using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartAdmin.Generator.Core;
using SmartAdmin.Generator.Models;

namespace SmartAdmin.Generator
{
    public class ConfigTables
    {                       
        /// <summary>
        /// Configurar qual o banco de dados onde as tabelas estao
        /// </summary>
        public static EDataBase DatabaseType = Core.EDataBase.MySql;
       
        /// <summary>
        /// Configurar as tabelas que se quer gerar as classes aqui
        /// </summary>
        public Dictionary<string, ClassConfig> GetTableMapper()
        {
            var Storage = new Dictionary<string, ClassConfig>();

            Storage.Add("USUARIO", new ClassConfig() { ClassName = "Usuario", NameSpaceDto = "SmartAdmin.Dto", NameSpaceMapper = "SmartAdmin.Data.Mapper", NameSpaceDomain = "SmartAdmin.Domain", NameSpaceService = "SmartAdmin.Service" });
            Storage.Add("TIPO_NEWSLETTER", new ClassConfig() { ClassName = "TipoNewletter", NameSpaceDto = "SmartAdmin.Dto", NameSpaceMapper = "SmartAdmin.Data.Mapper", NameSpaceDomain = "SmartAdmin.Domain", NameSpaceService = "SmartAdmin.Service" });
            Storage.Add("EMAIL_NEWSLETTER", new ClassConfig() { ClassName = "EmailNewletter", NameSpaceDto = "SmartAdmin.Dto", NameSpaceMapper = "SmartAdmin.Data.Mapper", NameSpaceDomain = "SmartAdmin.Domain", NameSpaceService = "SmartAdmin.Service" });
            Storage.Add("ENVIO_NEWSLETTER", new ClassConfig() { ClassName = "EnvioNewletter", NameSpaceDto = "SmartAdmin.Dto", NameSpaceMapper = "SmartAdmin.Data.Mapper", NameSpaceDomain = "SmartAdmin.Domain", NameSpaceService = "SmartAdmin.Service" });
            Storage.Add("ACESSO", new ClassConfig() { ClassName = "Acesso", NameSpaceDto = "SmartAdmin.Dto", NameSpaceMapper = "SmartAdmin.Data.Mapper", NameSpaceDomain = "SmartAdmin.Domain", NameSpaceService = "SmartAdmin.Service" });
            Storage.Add("TIPO_CONTATO", new ClassConfig() { ClassName = "TipoContato", NameSpaceDto = "SmartAdmin.Dto", NameSpaceMapper = "SmartAdmin.Data.Mapper", NameSpaceDomain = "SmartAdmin.Domain", NameSpaceService = "SmartAdmin.Service" });
            Storage.Add("CONTATO", new ClassConfig() { ClassName = "Contato", NameSpaceDto = "SmartAdmin.Dto", NameSpaceMapper = "SmartAdmin.Data.Mapper", NameSpaceDomain = "SmartAdmin.Domain", NameSpaceService = "SmartAdmin.Service" });
            Storage.Add("MENU", new ClassConfig() { ClassName = "Menu", NameSpaceDto = "SmartAdmin.Dto", NameSpaceMapper = "SmartAdmin.Data.Mapper", NameSpaceDomain = "SmartAdmin.Domain", NameSpaceService = "SmartAdmin.Service" });
            Storage.Add("MENU_USUARIO", new ClassConfig() { ClassName = "MenuUsuario", NameSpaceDto = "SmartAdmin.Dto", NameSpaceMapper = "SmartAdmin.Data.Mapper", NameSpaceDomain = "SmartAdmin.Domain", NameSpaceService = "SmartAdmin.Service" });
            Storage.Add("INBOX", new ClassConfig() { ClassName = "Inbox", NameSpaceDto = "SmartAdmin.Dto", NameSpaceMapper = "SmartAdmin.Data.Mapper", NameSpaceDomain = "SmartAdmin.Domain", NameSpaceService = "SmartAdmin.Service" });
            Storage.Add("BANCO", new ClassConfig() { ClassName = "Banco", NameSpaceDto = "SmartAdmin.Dto", NameSpaceMapper = "SmartAdmin.Data.Mapper", NameSpaceDomain = "SmartAdmin.Domain", NameSpaceService = "SmartAdmin.Service" });
            Storage.Add("CEDENTE", new ClassConfig() { ClassName = "Cedente", NameSpaceDto = "SmartAdmin.Dto", NameSpaceMapper = "SmartAdmin.Data.Mapper", NameSpaceDomain = "SmartAdmin.Domain", NameSpaceService = "SmartAdmin.Service" });
            Storage.Add("MENSAGEM", new ClassConfig() { ClassName = "Mensagem", NameSpaceDto = "SmartAdmin.Dto", NameSpaceMapper = "SmartAdmin.Data.Mapper", NameSpaceDomain = "SmartAdmin.Domain", NameSpaceService = "SmartAdmin.Service" });
            Storage.Add("MENSAGEM_ENVIADA", new ClassConfig() { ClassName = "MensagemEnviada", NameSpaceDto = "SmartAdmin.Dto", NameSpaceMapper = "SmartAdmin.Data.Mapper", NameSpaceDomain = "SmartAdmin.Domain", NameSpaceService = "SmartAdmin.Service" });

            return (Storage);
        }
        
    }
}
