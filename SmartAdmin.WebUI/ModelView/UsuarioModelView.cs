using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartAdmin.Data.Model;

namespace SmartAdmin.WebUI.ModelView
{
    public class UsuarioModelView
    {
        public UsuarioDto Usuario { get; set; }
        public virtual List<MenuModelView> CollectionMenusAndSubMenus { get; set; }

        public UsuarioModelView()
        { 
        }

        public UsuarioModelView(UsuarioDto Model, List<MenuModelView> Collection)
        {
            Usuario = Model;
            CollectionMenusAndSubMenus = Collection;
        }


    }
}
