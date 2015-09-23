using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartAdmin.Dto;

namespace SmartAdmin.WebUI.ModelView
{
    public class PermissionModelView
    {
        public int ID { get; set; }
        public string NOME { get; set; }
        public virtual List<CheckBoxes> CollectionMenus { get; set; }
    }

    public class CheckBoxes
    {
        public int ID { get; set; }
        public string Value { get; set; }
        public string Text { get; set; }
        public bool Checked { get; set; }
    }
}
