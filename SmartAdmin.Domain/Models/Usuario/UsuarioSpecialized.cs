using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using SmartAdmin.Data;
using SmartAdmin.Data.Model;

namespace SmartAdmin.Domain.Model
{
    public partial class UsuarioSpecialized : Usuario
    {
        public List<MenuDto> GetAllowedMenus(int Id)
        {
            var MenuFromUserDomain = new MenuUsuarioSpecialized();
            var MenuMainDomain = new MenuSpecialized();
            var Collection = new List<MenuDto>();
            var MenuFromUserCollection = MenuFromUserDomain.GetByFilter(_ => _.COD_USUARIO == Id);

            foreach (MenuUsuarioDto MenuFromUserItem in MenuFromUserCollection)
            {
                var MenuItem = MenuMainDomain.GetByFilter(_ => _.ID == MenuFromUserItem.COD_MENU).FirstOrDefault();
                Collection.Add(MenuItem);
            }

            return (Collection);
        }

        public List<MenuDto> GetAllowedMenusChild(int Id)
        {
            var Collection = new List<MenuDto>();
            var MenuMainDomain = new MenuSpecialized();
            var SubMenuFromMenuCollection = MenuMainDomain.GetByFilter(_ => _.COD_MENU_PAI == Id);

            foreach (MenuDto SubMenu in SubMenuFromMenuCollection)
            {
                Collection.Add(SubMenu);
            }

            return (Collection);

        }
    }
}

