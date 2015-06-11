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

            Storage.Add("SCP_USUARIO", new ClassConfig() { ClassName = "Usuario", NameSpaceDto = "SmartAdmin.Dto", NameSpaceMapper = "SmartAdmin.Data.Mapper", NameSpaceDomain = "SmartAdmin.Domain", NameSpaceService = "SmartAdmin.Service" });
            Storage.Add("SCP_MENU", new ClassConfig() { ClassName = "Menu", NameSpaceDto = "SmartAdmin.Dto", NameSpaceMapper = "SmartAdmin.Data.Mapper", NameSpaceDomain = "SmartAdmin.Domain", NameSpaceService = "SmartAdmin.Service" });
            Storage.Add("SCP_PERM_USUARIO", new ClassConfig() { ClassName = "Permissao", NameSpaceDto = "SmartAdmin.Dto", NameSpaceMapper = "SmartAdmin.Data.Mapper", NameSpaceDomain = "SmartAdmin.Domain", NameSpaceService = "SmartAdmin.Service" });
            Storage.Add("SCP_COMPLEXIDADE", new ClassConfig() { ClassName = "Complexidade", NameSpaceDto = "SmartAdmin.Dto", NameSpaceMapper = "SmartAdmin.Data.Mapper", NameSpaceDomain = "SmartAdmin.Domain", NameSpaceService = "SmartAdmin.Service" });
            Storage.Add("SCP_TIPO_COMPLEXIDADE", new ClassConfig() { ClassName = "TipoComplexidade", NameSpaceDto = "SmartAdmin.Dto", NameSpaceMapper = "SmartAdmin.Data.Mapper", NameSpaceDomain = "SmartAdmin.Domain", NameSpaceService = "SmartAdmin.Service" });

            return (Storage);
        }
    }
}
