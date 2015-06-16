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
    public partial class Menu
    {
        private bool _disposed = false;
        private SmartAdmin.Data.UnitOfWork _unitOfWork = new SmartAdmin.Data.UnitOfWork();

        /// <summary>
        /// Salva um objeto
        /// </summary>
        public void Save(MenuDto model)
        {
            _unitOfWork.MenuRepository.AddItem(model);
            //kjhkjkhjkhkhkhk
        }

        /// <summary>
        /// Salva e retorna um objeto
        /// </summary>
        public MenuDto SaveGetItem(MenuDto model)
        {
            ///uiuihiuhihiuhuiu
           var retorno = _unitOfWork.MenuRepository.AddGetItem(model);
           return (retorno);
        }

        /// <summary>
        /// Salva uma lista de objetos
        /// </summary>
        public void SaveAll(List<MenuDto> model)
        {
            _unitOfWork.MenuRepository.AddAll(model);
        }

        /// <summary>
        /// Salva a edição de um objeto
        /// </summary>
        public void Edit(MenuDto model)
        {
            _unitOfWork.MenuRepository.Edit(model);
        }

        /// <summary>
        /// Retorna um único objeto buscado por expressão Lambda
        /// </summary>
        public MenuDto GetItem(Expression<Func<MenuDto, bool>> filter)
        {
            MenuDto model;
            model = _unitOfWork.MenuRepository.GetItem(filter);
            return (model);
        }

        /// <summary>
        /// Retorna um objeto do tipo List(T) de objetos buscados pela expressão Lambda
        /// </summary>
        public List<MenuDto> GetList(Expression<Func<MenuDto, bool>> filter)
        {
            List<MenuDto> collection;
            collection = _unitOfWork.MenuRepository.GetList(filter);
            return (collection);
        }

        /// <summary>
        /// Inativa um objeto para visualização
        /// </summary>
        public void ToInactive(int Id)
        {
            MenuDto model = _unitOfWork.MenuRepository.GetItem(_ => _.ID == Id && _.STATUS == "A");
            model.STATUS = "I";
            _unitOfWork.MenuRepository.Edit(model);
        }

        /// <summary>
        /// Anativa um objeto para visualização
        /// </summary>
        public void ToActive(int Id)
        {
            MenuDto model = _unitOfWork.MenuRepository.GetItem(_ => _.ID == Id && _.STATUS == "I");
            model.STATUS = "A";
            _unitOfWork.MenuRepository.Edit(model);
        }

        /// <summary>
        ///  Distroe objeto e recursos não gerenciados liberando memória
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

        ~Menu()
        {
            Clear(false);
        }
    }
}

