using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Text;
using System.ComponentModel;
using System.Collections;

namespace SmartAdmin.WebUI.HtmlHelpers
{
    public static class HtmlControls
    {
        public static MvcHtmlString TextBox(string Id, object HtmlAtributes = null)
        {
            var Atributes = String.Empty;

            if (HtmlAtributes != null)
            {
                foreach (PropertyDescriptor Property in TypeDescriptor.GetProperties(HtmlAtributes))
                {
                    Atributes += String.Format("{0}=\"{1}\" ", Property.Name.Replace('_', '-'), Property.GetValue(HtmlAtributes));
                }
            }

            var HtmlControl = new StringBuilder();
            HtmlControl.Append(String.Format("<input type=\"text\" name=\"{0}\" id=\"{0}\" {1} />", Id, Atributes));

            return (new MvcHtmlString(HtmlControl.ToString()));
        }

        public static MvcHtmlString Password(string Id, object HtmlAtributes = null)
        {
            var Atributes = String.Empty;

            if (HtmlAtributes != null)
            {
                foreach (PropertyDescriptor Property in TypeDescriptor.GetProperties(HtmlAtributes))
                {
                    Atributes += String.Format("{0}=\"{1}\" ", Property.Name.Replace('_', '-'), Property.GetValue(HtmlAtributes));
                }
            }

            var HtmlControl = new StringBuilder();
            HtmlControl.Append(String.Format("<input type=\"password\" name=\"{0}\" id=\"{0}\" {1} />", Id, Atributes));

            return (new MvcHtmlString(HtmlControl.ToString()));
        }

        public static MvcHtmlString DatePicker(string Name, object HtmlAtributes = null)
        {
            var Atributes = String.Empty;

            if (HtmlAtributes != null)
            {
                foreach (PropertyDescriptor Property in TypeDescriptor.GetProperties(HtmlAtributes))
                {
                    Atributes += String.Format("{0}=\"{1}\" ", Property.Name.Replace('_', '-'), Property.GetValue(HtmlAtributes));
                }
            }

            var HtmlControl = new StringBuilder();
            HtmlControl.Append(String.Format("<input type=\"text\" name=\"{0}\" placeholder=\"__/__/____\" class=\"datemask datepicker\" {1}>", Name, Atributes));

            return (new MvcHtmlString(HtmlControl.ToString()));
        }

        public static MvcHtmlString DropDownList(string Id, List<SelectListItem> Collection, object HtmlAtributes = null)
        {
            var Atributes = String.Empty;

            if (HtmlAtributes != null)
            {
                foreach (PropertyDescriptor Property in TypeDescriptor.GetProperties(HtmlAtributes))
                {
                    Atributes += String.Format("{0}=\"{1}\" ", Property.Name.Replace('_', '-'), Property.GetValue(HtmlAtributes));
                }
            }

            var HtmlControl = new StringBuilder();
            HtmlControl.Append(String.Format("<select name=\"{0}\" {1}>", Id, Atributes));
            HtmlControl.Append(String.Format("<option value=\"0\" selected=\"true\" disabled=\"true\">(Todos)</option>"));

            foreach (var item in Collection)
            {
                if (item.Selected)
                    HtmlControl.Append(String.Format("<option value=\"{0}\" selected=\"true\" >{1}</option>", item.Value, item.Text));
                else
                    HtmlControl.Append(String.Format("<option value=\"{0}\">{1}</option>", item.Value, item.Text));
            }

            HtmlControl.Append("</select>");

            return (new MvcHtmlString(HtmlControl.ToString()));
        }

        public static MvcHtmlString DropDownListCustom(string Id, EInputModel ModelType, object HtmlAtributes = null)
        {
            var Atributes = String.Empty;

            if (HtmlAtributes != null)
            {
                foreach (PropertyDescriptor Property in TypeDescriptor.GetProperties(HtmlAtributes))
                {
                    Atributes += String.Format("{0}=\"{1}\" ", Property.Name.Replace('_', '-'), Property.GetValue(HtmlAtributes));
                }
            }

            var HtmlControl = new StringBuilder();
            HtmlControl.Append(String.Format("<select name=\"{0}\" {1}>", Id, Atributes));
            HtmlControl.Append(String.Format("<option value=\"0\" selected=\"true\" disabled=\"true\">(Todos)</option>"));

            //foreach (var item in Collection)
            //{
            //    if (item.Selected)
            //        HtmlControl.Append(String.Format("<option value=\"{0}\" selected=\"true\" >{1}</option>", item.Value, item.Text));
            //    else
            //        HtmlControl.Append(String.Format("<option value=\"{0}\">{1}</option>", item.Value, item.Text));
            //}

            HtmlControl.Append("</select>");

            return (new MvcHtmlString(HtmlControl.ToString()));
        }

        public static MvcHtmlString ButtonSubmit(string Id, string Text, object HtmlAtributes = null)
        {
            var HtmlControl = new StringBuilder();

            HtmlControl.Append("<button type=\"submit\" class=\"btn btn-sm btn-primary\">");
            HtmlControl.Append("<i class=\"fa fa-search\"></i> " + Text);
            HtmlControl.Append("</button>");

            return (new MvcHtmlString(HtmlControl.ToString()));
        }

        public static MvcHtmlString ButtonImprimir(string Id, string Text, object HtmlAtributes = null)
        {
            var HtmlControl = new StringBuilder();

            HtmlControl.Append("<button type=\"button\" class=\"btn btn-lg btn-success\">");
            HtmlControl.Append("<i class=\"fa fa-print\"></i> " + Text);
            HtmlControl.Append("</button>");

            return (new MvcHtmlString(HtmlControl.ToString()));
        }

        public static MvcHtmlString ButtonInserir(string Id, string Text, object HtmlAtributes = null)
        {
            var HtmlControl = new StringBuilder();

            HtmlControl.Append("<button type=\"button\" class=\"btn btn-lg btn-warning\">");
            HtmlControl.Append("<i class=\"fa fa-cog\"></i> " + Text);
            HtmlControl.Append("</button>");

            return (new MvcHtmlString(HtmlControl.ToString()));
        }

        //public static MvcHtmlString FileUpload(string Id, object HtmlAtributes = null)
        //{ 
        
        //}
    }

    /// <summary>
    /// Modelos de listagens para dropdownlist
    /// </summary>
    public enum EInputModel
    {
        ModeloA,
        ModeloA,
        ModeloA,
        ModeloA,
        ModeloA,
        ModeloA,
    }

    /// <summary>
    /// Tipos de controles Html5
    /// </summary>
    public enum EInputTypes
    {
        Color,
        Date,
        Datetime,
        DateTimeLocal,
        Email,
        Month,
        Number,
        Range,
        Search,
        Tel,
        Time,
        Url,
        Week
    }

    /// <summary>
    /// Atributos de controles Html5
    /// </summary>
    public enum EInputAtributes
    {
        Disabled,
        Max,
        MaxLength,
        Min,
        Pattern,
        Readonly,
        Required,
        Size,
        Step,
        Value
    }




}