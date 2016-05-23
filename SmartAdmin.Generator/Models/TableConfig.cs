using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartAdmin.Gerador.Models;

namespace SmartAdmin.Gerador.Models
{
    public sealed class TableToClass
    {          
        public Dictionary<String, ModelConfig> GetTableMapper()
        {
            var Storage = new Dictionary<String, ModelConfig>();

            Storage.Add("USUARIO", new ModelConfig() { ClassName = "Usuario", NameSpaceDto = "SmartAdmin.Dto", NameSpaceMapper = "SmartAdmin.Data.Mapper", NameSpaceDomain = "SmartAdmin.Domain", NameSpaceService = "SmartAdmin.Service", CreateController = true });
            Storage.Add("ACESSO", new ModelConfig() { ClassName = "Acesso", NameSpaceDto = "SmartAdmin.Dto", NameSpaceMapper = "SmartAdmin.Data.Mapper", NameSpaceDomain = "SmartAdmin.Domain", NameSpaceService = "SmartAdmin.Service", CreateController = true });
            Storage.Add("MENU", new ModelConfig() { ClassName = "Menu", NameSpaceDto = "SmartAdmin.Dto", NameSpaceMapper = "SmartAdmin.Data.Mapper", NameSpaceDomain = "SmartAdmin.Domain", NameSpaceService = "SmartAdmin.Service", CreateController = true });
            Storage.Add("MENU_USUARIO", new ModelConfig() { ClassName = "MenuUsuario", NameSpaceDto = "SmartAdmin.Dto", NameSpaceMapper = "SmartAdmin.Data.Mapper", NameSpaceDomain = "SmartAdmin.Domain", NameSpaceService = "SmartAdmin.Service", CreateController = false });

            //Storage.Add("ADESAO", new ModelConfig() { ClassName = "Adesao", NameSpaceDto = "Metronic.Dto", NameSpaceMapper = "Metronic.Data.Mapper", NameSpaceDomain = "Metronic.Domain", NameSpaceService = "Metronic.Service", CreateController = true });
            //Storage.Add("BUSCA", new ModelConfig() { ClassName = "Buscar", NameSpaceDto = "Metronic.Dto", NameSpaceMapper = "Metronic.Data.Mapper", NameSpaceDomain = "Metronic.Domain", NameSpaceService = "Metronic.Service", CreateController = true });
            //Storage.Add("CARGO", new ModelConfig() { ClassName = "Cargo", NameSpaceDto = "Metronic.Dto", NameSpaceMapper = "Metronic.Data.Mapper", NameSpaceDomain = "Metronic.Domain", NameSpaceService = "Metronic.Service", CreateController = true });
            //Storage.Add("CARTA_APRESENTACAO", new ModelConfig() { ClassName = "CartaApresentacao", NameSpaceDto = "Metronic.Dto", NameSpaceMapper = "Metronic.Data.Mapper", NameSpaceDomain = "Metronic.Domain", NameSpaceService = "Metronic.Service", CreateController = true });
            //Storage.Add("ESCOLARIDADE", new ModelConfig() { ClassName = "Escolaridade", NameSpaceDto = "Metronic.Dto", NameSpaceMapper = "Metronic.Data.Mapper", NameSpaceDomain = "Metronic.Domain", NameSpaceService = "Metronic.Service", CreateController = true });
            //Storage.Add("EXPERIENCIA_EXTERIOR", new ModelConfig() { ClassName = "ExperienciaExterior", NameSpaceDto = "Metronic.Dto", NameSpaceMapper = "Metronic.Data.Mapper", NameSpaceDomain = "Metronic.Domain", NameSpaceService = "Metronic.Service", CreateController = true });
            //Storage.Add("EXPERIENCIA_PROFISSIONAL", new ModelConfig() { ClassName = "ExperienciaProfissional", NameSpaceDto = "Metronic.Dto", NameSpaceMapper = "Metronic.Data.Mapper", NameSpaceDomain = "Metronic.Domain", NameSpaceService = "Metronic.Service", CreateController = true });
            //Storage.Add("FLUENCIA", new ModelConfig() { ClassName = "Fluencia", NameSpaceDto = "Metronic.Dto", NameSpaceMapper = "Metronic.Data.Mapper", NameSpaceDomain = "Metronic.Domain", NameSpaceService = "Metronic.Service", CreateController = true });
            //Storage.Add("FORMA_CONTRATO", new ModelConfig() { ClassName = "FormaContrato", NameSpaceDto = "Metronic.Dto", NameSpaceMapper = "Metronic.Data.Mapper", NameSpaceDomain = "Metronic.Domain", NameSpaceService = "Metronic.Service", CreateController = true });
            //Storage.Add("IDIOMA", new ModelConfig() { ClassName = "Idioma", NameSpaceDto = "Metronic.Dto", NameSpaceMapper = "Metronic.Data.Mapper", NameSpaceDomain = "Metronic.Domain", NameSpaceService = "Metronic.Service", CreateController = true });
            //Storage.Add("PAIS", new ModelConfig() { ClassName = "Pais", NameSpaceDto = "Metronic.Dto", NameSpaceMapper = "Metronic.Data.Mapper", NameSpaceDomain = "Metronic.Domain", NameSpaceService = "Metronic.Service", CreateController = false });
            //Storage.Add("PARCELA", new ModelConfig() { ClassName = "Parcela", NameSpaceDto = "Metronic.Dto", NameSpaceMapper = "Metronic.Data.Mapper", NameSpaceDomain = "Metronic.Domain", NameSpaceService = "Metronic.Service", CreateController = true });
            //Storage.Add("PESSOA", new ModelConfig() { ClassName = "Pessoa", NameSpaceDto = "Metronic.Dto", NameSpaceMapper = "Metronic.Data.Mapper", NameSpaceDomain = "Metronic.Domain", NameSpaceService = "Metronic.Service", CreateController = true });
            //Storage.Add("PLANO", new ModelConfig() { ClassName = "Plano", NameSpaceDto = "Metronic.Dto", NameSpaceMapper = "Metronic.Data.Mapper", NameSpaceDomain = "Metronic.Domain", NameSpaceService = "Metronic.Service", CreateController = true });
            //Storage.Add("TIPO_PLANO", new ModelConfig() { ClassName = "TipoPlano", NameSpaceDto = "Metronic.Dto", NameSpaceMapper = "Metronic.Data.Mapper", NameSpaceDomain = "Metronic.Domain", NameSpaceService = "Metronic.Service", CreateController = false });
            //Storage.Add("REGIAO", new ModelConfig() { ClassName = "Regiao", NameSpaceDto = "Metronic.Dto", NameSpaceMapper = "Metronic.Data.Mapper", NameSpaceDomain = "Metronic.Domain", NameSpaceService = "Metronic.Service", CreateController = false });
            //Storage.Add("TITULO", new ModelConfig() { ClassName = "Titulo", NameSpaceDto = "Metronic.Dto", NameSpaceMapper = "Metronic.Data.Mapper", NameSpaceDomain = "Metronic.Domain", NameSpaceService = "Metronic.Service", CreateController = true });
            //Storage.Add("UF", new ModelConfig() { ClassName = "Uf", NameSpaceDto = "Metronic.Dto", NameSpaceMapper = "Metronic.Data.Mapper", NameSpaceDomain = "Metronic.Domain", NameSpaceService = "Metronic.Service", CreateController = false });
            //Storage.Add("BANCO", new ModelConfig() { ClassName = "Banco", NameSpaceDto = "Metronic.Dto", NameSpaceMapper = "Metronic.Data.Mapper", NameSpaceDomain = "Metronic.Domain", NameSpaceService = "Metronic.Service", CreateController = true });
            //Storage.Add("EXAME", new ModelConfig() { ClassName = "Exame", NameSpaceDto = "Metronic.Dto", NameSpaceMapper = "Metronic.Data.Mapper", NameSpaceDomain = "Metronic.Domain", NameSpaceService = "Metronic.Service", CreateController = true });
            //Storage.Add("QUESTAO", new ModelConfig() { ClassName = "Questao", NameSpaceDto = "Metronic.Dto", NameSpaceMapper = "Metronic.Data.Mapper", NameSpaceDomain = "Metronic.Domain", NameSpaceService = "Metronic.Service", CreateController = true });
            //Storage.Add("VAGA", new ModelConfig() { ClassName = "Vaga", NameSpaceDto = "Metronic.Dto", NameSpaceMapper = "Metronic.Data.Mapper", NameSpaceDomain = "Metronic.Domain", NameSpaceService = "Metronic.Service", CreateController = true });
            //Storage.Add("CNAB", new ModelConfig() { ClassName = "Cnab", NameSpaceDto = "Metronic.Dto", NameSpaceMapper = "Metronic.Data.Mapper", NameSpaceDomain = "Metronic.Domain", NameSpaceService = "Metronic.Service", CreateController = false });
            //Storage.Add("LAYOUT", new ModelConfig() { ClassName = "Layout", NameSpaceDto = "Metronic.Dto", NameSpaceMapper = "Metronic.Data.Mapper", NameSpaceDomain = "Metronic.Domain", NameSpaceService = "Metronic.Service", CreateController = true });
            //Storage.Add("CEDENTE", new ModelConfig() { ClassName = "Cedente", NameSpaceDto = "Metronic.Dto", NameSpaceMapper = "Metronic.Data.Mapper", NameSpaceDomain = "Metronic.Domain", NameSpaceService = "Metronic.Service", CreateController = true });
            //Storage.Add("HIERARQUIA", new ModelConfig() { ClassName = "Hierarquia", NameSpaceDto = "Metronic.Dto", NameSpaceMapper = "Metronic.Data.Mapper", NameSpaceDomain = "Metronic.Domain", NameSpaceService = "Metronic.Service", CreateController = false });
   
            return (Storage);
        }
    }
}
