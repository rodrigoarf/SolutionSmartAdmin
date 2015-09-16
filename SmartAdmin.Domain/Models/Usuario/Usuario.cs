using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using SmartAdmin.Data;
using SmartAdmin.Dto;

namespace SmartAdmin.Domain
{
    public partial class Usuario
    {
        private bool _disposed = false;
        private SmartAdmin.Data.UnitOfWork _unitOfWork = new SmartAdmin.Data.UnitOfWork();

        /// <summary>
        /// Salva um objeto<T>
        /// </summary>
        public void Save(UsuarioDto model)
        {
            try
            {
                _unitOfWork.UsuarioRepository.Save(model);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }               
        }

        /// <summary>
        /// Salva e retorna o objeto<T> salvo
        /// </summary>
        public UsuarioDto SaveGetItem(UsuarioDto model)
        {
           var retorno = _unitOfWork.UsuarioRepository.SaveGetItem(model);
           return (retorno);
        }

        /// <summary>
        /// Salva uma lista de objetos List<T>
        /// </summary>
        public void SaveAll(List<UsuarioDto> model)
        {
            _unitOfWork.UsuarioRepository.SaveAll(model);
        }

        /// <summary>
        /// Salva a edição de um objeto<T>
        /// </summary>
        public void Edit(UsuarioDto model)
        {
            _unitOfWork.UsuarioRepository.Edit(model);
        }

        /// <summary>
        /// Retorna um único objeto<T> buscado por expressão Lambda
        /// </summary>
        public UsuarioDto GetItem(Expression<Func<UsuarioDto, bool>> filter)
        {
            UsuarioDto model;
            model = _unitOfWork.UsuarioRepository.GetItem(filter);
            return (model);
        }

        /// <summary>
        /// Retorna uma lista List(T) de objetos buscados pela expressão Lambda
        /// </summary>
        public List<UsuarioDto> GetList(Expression<Func<UsuarioDto, bool>> filter = null)
        {
            List<UsuarioDto> collection;
            collection = _unitOfWork.UsuarioRepository.GetList(filter);
            return (collection);
        }

        /// <summary>
        /// Retorna uma lista de objetos de menu que o usuário tem acesso, mediante informado o Id do Usuário.
        /// </summary>
        public List<MenuDto> GetAllowedMenus(int Id)
        {
            var CollectionMenu = _unitOfWork.MenuRepository.GetList(_ => _.COD_MENU_PAI == 0 && _.STATUS == "A"); //--> Pega somente os menus pais, ativos e abilitados para o usuario corrente.
            var CollectionMenuUsuario = _unitOfWork.MenuUsuarioRepository.GetList(_ => _.COD_USUARIO == Id);
            var Collection = CollectionMenu.Join(CollectionMenuUsuario, x => x.ID, y => y.COD_MENU,
                                                //(MenuDto, MenuUsuarioDto) => new { MenuDto, MenuUsuarioDto }).Where(m => m.MenuUsuarioDto.COD_USUARIO == Id)
                                                (MenuDto, MenuUsuarioDto) => new { MenuDto, MenuUsuarioDto })
                                                .Select (_ => new MenuDto
                                                {
                                                    ID = _.MenuDto.ID,
                                                    COD_MENU_PAI = _.MenuDto.COD_MENU_PAI,
                                                    NOME = _.MenuDto.NOME,
                                                    CONTROLLER = _.MenuDto.CONTROLLER,
                                                    ACTION = _.MenuDto.ACTION,
                                                    DESCRICAO = _.MenuDto.DESCRICAO,
                                                    DTH_CADASTRO = _.MenuDto.DTH_CADASTRO,
                                                    ICONE = _.MenuDto.ICONE,
                                                    STATUS = _.MenuDto.STATUS
                                                }).ToList();

            return (Collection);
        }

        /// <summary>
        /// Retorna uma lista de objetos de submenu de Id de menu pai.
        /// </summary>
        public List<MenuDto> GetSubMenuFromMenu(int Id)
        {
            var CollectionMenu = _unitOfWork.MenuRepository.GetList(_ => _.COD_MENU_PAI == Id && _.STATUS == "A").OrderBy(_=>_.NOME).ToList(); //--> Pega somente os menus pais, ativos e abilitados para o usuario corrente.
            return (CollectionMenu);
        }

        /// <summary>
        /// Inativa um objeto para visualização
        /// </summary>
        public void ToInactive(int Id)
        {
            UsuarioDto model = _unitOfWork.UsuarioRepository.GetItem(_ => _.ID == Id && _.STATUS == "A");
            model.STATUS = "I";
            _unitOfWork.UsuarioRepository.Edit(model);
        }

        /// <summary>
        /// Ativa um objeto para visualização
        /// </summary>
        public void ToActive(int Id)
        {
            UsuarioDto model = _unitOfWork.UsuarioRepository.GetItem(_ => _.ID == Id && _.STATUS == "I");
            model.STATUS = "A";
            _unitOfWork.UsuarioRepository.Edit(model);
        }    

        /// <summary>
        /// Verifica se existe o objeto no banco de dados retorna somente true ou false. 
        /// </summary>
        public UsuarioDto IsExists(UsuarioDto Model)
        {
            var Retorno = GetItem(_ => _.LOGIN == Model.LOGIN || _.EMAIL == Model.EMAIL);

            if (Retorno != null)
                return (Retorno);
            else
                return (null);
        }

        /// <summary>
        /// Verifica se existe o objeto passado somente para login. Este metodo retorna um objeto pois sera colocando em Sessão se existe, por isso nao retorna true ou false. 
        /// </summary>
        public UsuarioDto Authentication(UsuarioDto Model)
        {
            var Criptografia = new SmartAdmin.Domain.Security.Cryptography();
            Model.SENHA = Criptografia.Encrypt(Model.SENHA);

            var Retorno = GetItem(_ => _.LOGIN == Model.LOGIN && _.SENHA == Model.SENHA && _.STATUS == "A");

            if (Retorno != null)
                return (Retorno);
            else
                return (null);
        }

        /// <summary>
        ///  Distroe o objeto e recursos não gerenciados liberando memória
        /// </summary>
        public void Dispose()
        {
            Clear(true);
            GC.SuppressFinalize(this);
        }

        private void Clear(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                    _unitOfWork.Dispose();
            }
            _disposed = true;
        }

        ~Usuario()
        {
            Clear(false);
        }
    }
}