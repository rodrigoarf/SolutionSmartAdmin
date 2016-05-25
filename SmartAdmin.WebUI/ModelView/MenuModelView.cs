using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartAdmin.Data.Model;


namespace SmartAdmin.WebUI.ModelView
{
    public class MenuModelView
    {
        public MenuDto Menu { get; set; }
        public virtual List<MenuDto> CollectionSubMenu { get; set; }
    }
}
