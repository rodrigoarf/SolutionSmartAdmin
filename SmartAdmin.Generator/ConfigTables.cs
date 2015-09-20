using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartAdmin.Generator.Models;

namespace SmartAdmin.Generator
{
    public class ConfigTables
    {
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
            //Storage.Add("BOLETO", new ClassConfig() { ClassName = "Boleto", NameSpaceDto = "SmartAdmin.Dto", NameSpaceMapper = "SmartAdmin.Data.Mapper", NameSpaceDomain = "SmartAdmin.Domain", NameSpaceService = "SmartAdmin.Service" });

            return (Storage);
        }
    }
}
