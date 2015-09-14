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

        public static MvcHtmlString TextBoxCustom(string Id, string Name, object HtmlAtributes = null)
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
            HtmlControl.Append(String.Format("<input type=\"text\" id=\"{0}\" name=\"{1}\" {2} />", Id, Name, Atributes));

            return (new MvcHtmlString(HtmlControl.ToString()));
        }

        public static MvcHtmlString TextBoxEmail(string Id, object HtmlAtributes = null)
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
            HtmlControl.Append(String.Format("<input type=\"email\" name=\"{0}\" id=\"{0}\" {1} />", Id, Atributes));

            return (new MvcHtmlString(HtmlControl.ToString()));
        }

        public static MvcHtmlString TextBoxEmailCustom(string Id, string Name, object HtmlAtributes = null)
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
            HtmlControl.Append(String.Format("<input type=\"email\" id=\"{0}\" name=\"{1}\" {2} />", Id, Name, Atributes));

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

        public static MvcHtmlString PasswordCustom(string Id, string Name, object HtmlAtributes = null)
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
            HtmlControl.Append(String.Format("<input type=\"password\" id=\"{0}\" name=\"{1}\" {2} />", Id, Name, Atributes));

            return (new MvcHtmlString(HtmlControl.ToString()));
        }

        public static MvcHtmlString DatePicker(string Id, object HtmlAtributes = null)
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
            HtmlControl.Append(String.Format("<input type=\"text\" id=\"{0}\" name=\"{0}\" placeholder=\"__/__/____\" class=\"datemask datepicker\" {1}>", Id, Atributes));

            return (new MvcHtmlString(HtmlControl.ToString()));
        }

        public static MvcHtmlString DatePickerCustom(string id, string Name, object HtmlAtributes = null)
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
            HtmlControl.Append(String.Format("<input type=\"text\" id=\"{0}\" name=\"{1}\" placeholder=\"__/__/____\" class=\"datemask datepicker\" {2}>", id, Name, Atributes));

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
            HtmlControl.Append(String.Format("<select id=\"{0}\" name=\"{0}\" {1}>", Id, Atributes));
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

        public static MvcHtmlString DropDownList(string Id, EInputModel ModelType, object HtmlAtributes = null)
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
            HtmlControl.Append(String.Format("<select id=\"{0}\" name=\"{0}\" {1}>", Id, Atributes));
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

        public static MvcHtmlString DropDownListCustom(string Id, string Name, List<SelectListItem> Collection, object HtmlAtributes = null)
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
            HtmlControl.Append(String.Format("<select id=\"{0}\" name=\"{1}\" {2}>", Id, Name, Atributes));
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

        public static MvcHtmlString DropDownListCustom(string Id, string Name, EInputModel ModelType, object HtmlAtributes = null)
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
            HtmlControl.Append(String.Format("<select id=\"{0}\" name=\"{1}\" {2}>", Id, Name, Atributes));
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

        public static MvcHtmlString DropDownListCustomSexo(string Id, EInputSexo DefaultValue, bool AbbreviateLabel, object HtmlAtributes = null)
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
            HtmlControl.Append(String.Format("<select id=\"{0}\" name=\"{0}\" {1}>", Id, Atributes));
            HtmlControl.Append("<option value=\"\" " + ((DefaultValue == EInputSexo.None) ? "selected" : string.Empty) + ">Sexo...</option>");

            if (AbbreviateLabel)
            {
                HtmlControl.Append("<option value=\"M\" " + ((DefaultValue == EInputSexo.M) ? "selected" : string.Empty) + ">M</option>");
                HtmlControl.Append("<option value=\"F\" " + ((DefaultValue == EInputSexo.F) ? "selected" : string.Empty) + ">F</option>");
            }
            else
            {
                HtmlControl.Append("<option value=\"M\" " + ((DefaultValue == EInputSexo.F) ? "selected" : string.Empty) + ">Masculino</option>");
                HtmlControl.Append("<option value=\"F\" " + ((DefaultValue == EInputSexo.M) ? "selected" : string.Empty) + ">Feminino</option>");
            }

            HtmlControl.Append("</select>");

            return (new MvcHtmlString(HtmlControl.ToString()));
        }

        public static MvcHtmlString DropDownListCustomStatus(string Id, EInputStatus DefaultStatus, object HtmlAtributes = null)
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
            HtmlControl.Append(String.Format("<select id=\"{0}\" name=\"{0}\" {1}>", Id, Atributes));

            switch (DefaultStatus)
            {
                case EInputStatus.Ativo:
                    HtmlControl.Append("<option value=\"\">Selecione...</option>");
                    HtmlControl.Append("<option value=\"A\" selected=\"true\">Ativo</option>");
                    HtmlControl.Append("<option value=\"I\">Inativo</option>");
                    break;
                case EInputStatus.Inativo:
                    HtmlControl.Append("<option value=\"\">Selecione...</option>");
                    HtmlControl.Append("<option value=\"A\">Ativo</option>");
                    HtmlControl.Append("<option value=\"I\" selected=\"true\">Inativo</option>");
                    break;
                case EInputStatus.None:
                    HtmlControl.Append("<option value=\"\" selected=\"true\">Selecione...</option>");
                    HtmlControl.Append("<option value=\"A\">Ativo</option>");
                    HtmlControl.Append("<option value=\"I\" >Inativo</option>");
                    break;
            }

            HtmlControl.Append("</select>");

            return (new MvcHtmlString(HtmlControl.ToString()));
        }

        public static MvcHtmlString DropDownListCustomLogradouro(string Id, object HtmlAtributes = null)
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
            HtmlControl.Append(String.Format("<select id=\"{0}\" name=\"{0}\" {1}>", Id, Atributes));
            HtmlControl.Append("<option value=\"0\" selected=\"true\" disabled=\"true\">(Todos)</option>");
            HtmlControl.Append("<option value=\"ALAMEDA\">ALAMEDA</option>");
            HtmlControl.Append("<option value=\"APARTAMENTO\">APARTAMENTO</option>");
            HtmlControl.Append("<option value=\"AVENIDA\">AVENIDA</option>");
            HtmlControl.Append("<option value=\"BLOCO\">BLOCO</option>");
            HtmlControl.Append("<option value=\"ESTAÇÃO\">ESTAÇÃO</option>");
            HtmlControl.Append("<option value=\"ESTRADA\">ESTRADA</option>");
            HtmlControl.Append("<option value=\"FAZENDA\">FAZENDA</option>");
            HtmlControl.Append("<option value=\"GALERIA\">GALERIA</option>");
            HtmlControl.Append("<option value=\"PRAÇA\">PRAÇA</option>");
            HtmlControl.Append("<option value=\"PARQUE\">PARQUE</option>");
            HtmlControl.Append("<option value=\"QUADRA\">QUADRA</option>");
            HtmlControl.Append("<option value=\"RODOVIA\">RODOVIA</option>");
            HtmlControl.Append("<option value=\"RUA\">RUA</option>");
            HtmlControl.Append("<option value=\"VIADUTO\">VIADUTO</option>");
            HtmlControl.Append("<option value=\"VILA\">VILA</option>");
            HtmlControl.Append("</select>");

            return (new MvcHtmlString(HtmlControl.ToString()));
        }

        public static MvcHtmlString DropDownListCustomUF(string Id, EInputUF DefaultValue, object HtmlAtributes = null)
        {
            //https://msdn.microsoft.com/pt-br/library/essfb559(v=VS.110).aspx
            var Atributes = String.Empty;

            if (HtmlAtributes != null)
            {
                foreach (PropertyDescriptor Property in TypeDescriptor.GetProperties(HtmlAtributes))
                {
                    Atributes += String.Format("{0}=\"{1}\" ", Property.Name.Replace('_', '-'), Property.GetValue(HtmlAtributes));
                }
            }

            var HtmlControl = new StringBuilder();
            HtmlControl.Append(String.Format("<select id=\"{0}\" name=\"{0}\" {1}>", Id, Atributes));
            HtmlControl.Append("<option value=\"\" " + ((DefaultValue == EInputUF.None) ? "selected" : string.Empty) + ">Estado...</option>");
            HtmlControl.Append("<option value=\"AC\" " + ((DefaultValue == EInputUF.AC) ? "selected" : string.Empty) + ">AC</option>");
            HtmlControl.Append("<option value=\"AL\" " + ((DefaultValue == EInputUF.AL) ? "selected" : string.Empty) + ">AL</option>");
            HtmlControl.Append("<option value=\"AM\" " + ((DefaultValue == EInputUF.AM) ? "selected" : string.Empty) + ">AM</option>");
            HtmlControl.Append("<option value=\"AP\" " + ((DefaultValue == EInputUF.AP) ? "selected" : string.Empty) + ">AP</option>");
            HtmlControl.Append("<option value=\"BA\" " + ((DefaultValue == EInputUF.BA) ? "selected" : string.Empty) + ">BA</option>");
            HtmlControl.Append("<option value=\"CE\" " + ((DefaultValue == EInputUF.CE) ? "selected" : string.Empty) + ">CE</option>");
            HtmlControl.Append("<option value=\"DF\" " + ((DefaultValue == EInputUF.DF) ? "selected" : string.Empty) + ">DF</option>");
            HtmlControl.Append("<option value=\"ES\" " + ((DefaultValue == EInputUF.ES) ? "selected" : string.Empty) + ">ES</option>");
            HtmlControl.Append("<option value=\"GO\" " + ((DefaultValue == EInputUF.GO) ? "selected" : string.Empty) + ">GO</option>");
            HtmlControl.Append("<option value=\"MA\" " + ((DefaultValue == EInputUF.MA) ? "selected" : string.Empty) + ">MA</option>");
            HtmlControl.Append("<option value=\"MG\" " + ((DefaultValue == EInputUF.MG) ? "selected" : string.Empty) + ">MG</option>");
            HtmlControl.Append("<option value=\"MS\" " + ((DefaultValue == EInputUF.MS) ? "selected" : string.Empty) + ">MS</option>");
            HtmlControl.Append("<option value=\"MT\" " + ((DefaultValue == EInputUF.MT) ? "selected" : string.Empty) + ">MT</option>");
            HtmlControl.Append("<option value=\"PA\" " + ((DefaultValue == EInputUF.PA) ? "selected" : string.Empty) + ">PA</option>");
            HtmlControl.Append("<option value=\"PB\" " + ((DefaultValue == EInputUF.PB) ? "selected" : string.Empty) + ">PB</option>");
            HtmlControl.Append("<option value=\"PE\" " + ((DefaultValue == EInputUF.PE) ? "selected" : string.Empty) + ">PE</option>");
            HtmlControl.Append("<option value=\"PI\" " + ((DefaultValue == EInputUF.PI) ? "selected" : string.Empty) + ">PI</option>");
            HtmlControl.Append("<option value=\"PR\" " + ((DefaultValue == EInputUF.PR) ? "selected" : string.Empty) + ">PR</option>");
            HtmlControl.Append("<option value=\"RJ\" " + ((DefaultValue == EInputUF.RJ) ? "selected" : string.Empty) + ">RJ</option>");
            HtmlControl.Append("<option value=\"RN\" " + ((DefaultValue == EInputUF.RN) ? "selected" : string.Empty) + ">RN</option>");
            HtmlControl.Append("<option value=\"RS\" " + ((DefaultValue == EInputUF.RS) ? "selected" : string.Empty) + ">RS</option>");
            HtmlControl.Append("<option value=\"RO\" " + ((DefaultValue == EInputUF.RO) ? "selected" : string.Empty) + ">RO</option>");
            HtmlControl.Append("<option value=\"RR\" " + ((DefaultValue == EInputUF.RR) ? "selected" : string.Empty) + ">RR</option>");
            HtmlControl.Append("<option value=\"SC\" " + ((DefaultValue == EInputUF.SC) ? "selected" : string.Empty) + ">SC</option>");
            HtmlControl.Append("<option value=\"SE\" " + ((DefaultValue == EInputUF.SE) ? "selected" : string.Empty) + ">SE</option>");
            HtmlControl.Append("<option value=\"SP\" " + ((DefaultValue == EInputUF.SP) ? "selected" : string.Empty) + ">SP</option>");
            HtmlControl.Append("<option value=\"TO\" " + ((DefaultValue == EInputUF.TO) ? "selected" : string.Empty) + ">TO</option>");
            HtmlControl.Append("</select>");

            return (new MvcHtmlString(HtmlControl.ToString()));
        }

        public static MvcHtmlString ButtonBuscar(string Id, string Text, string CssClass = "btn btn-sm btn-primary")
        {
            var HtmlControl = new StringBuilder();

            HtmlControl.Append(String.Format("<button type=\"buttom\" id=\"{0}\" name=\"{0}\" class=\"{2}\" >", Id, CssClass));
            HtmlControl.Append(String.Format("<i class=\"fa fa-search\"></i> {0}", Text));
            HtmlControl.Append("</button>");

            return (new MvcHtmlString(HtmlControl.ToString()));
        }

        public static MvcHtmlString ButtonBuscarCustom(string Id, string Name, string Text, string CssClass = "btn btn-sm btn-primary")
        {
            var HtmlControl = new StringBuilder();

            HtmlControl.Append(String.Format("<button type=\"buttom\" id=\"{0}\" name=\"{1}\" {2} class=\"{2}\">", Id, Name, CssClass));
            HtmlControl.Append(String.Format("<i class=\"fa fa-search\"></i> {0}", Text));
            HtmlControl.Append("</button>");

            return (new MvcHtmlString(HtmlControl.ToString()));
        }

        public static MvcHtmlString ButtonImprimir(string Id, string Text, string CssClass = "btn btn-lg btn-success")
        {
            var HtmlControl = new StringBuilder();

            HtmlControl.Append(String.Format("<button type=\"button\" id=\"{0}\" name=\"{0}\" class=\"{1}\">", Id, CssClass));
            HtmlControl.Append(String.Format("<i class=\"fa fa-print\"></i> {0}", Text));
            HtmlControl.Append("</button>");

            return (new MvcHtmlString(HtmlControl.ToString()));
        }

        public static MvcHtmlString ButtonImprimirCustom(string Id, string Name, string Text, object HtmlAtributes = null)
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

            HtmlControl.Append(String.Format("<button type=\"button\" id=\"{0}\" name=\"{1}\" {2} class=\"btn btn-lg btn-success\">", Id, Name, Atributes));
            HtmlControl.Append(String.Format("<i class=\"fa fa-print\"></i> {0}", Text));
            HtmlControl.Append("</button>");

            return (new MvcHtmlString(HtmlControl.ToString()));
        }

        public static MvcHtmlString ButtonInserir(string Id, string Text, string CssClass = "btn btn-lg btn-warning")
        {
            var HtmlControl = new StringBuilder();

            HtmlControl.Append(String.Format("<button type=\"button\" id=\"{0}\" name=\"{0}\" class=\"{2}\">", Id, CssClass));
            HtmlControl.Append(String.Format("<i class=\"fa fa-cog\"></i> {0}" + Text));
            HtmlControl.Append("</button>");

            return (new MvcHtmlString(HtmlControl.ToString()));
        }

        public static MvcHtmlString ButtonInserirCustom(string Id, string Name, string Text, object HtmlAtributes = null)
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

            HtmlControl.Append(String.Format("<button type=\"button\" id=\"{0}\" name=\"{1}\" class=\"{2}\">", Id, Name, Atributes));
            HtmlControl.Append(String.Format("<i class=\"fa fa-cog\"></i> {0}" + Text));
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
        ModeloB,
        ModeloC,
        ModeloD,
        ModeloE,
        ModeloF,
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

    /// <summary>
    /// Define status padrão de um combo-box
    /// </summary>
    public enum EInputStatus
    {
        Ativo,
        Inativo,
        None
    }

    /// <summary>
    /// Define status padrão de um combo-box
    /// </summary>
    public enum EInputSexo
    {   
        None,
        M,
        F        
    }

    /// <summary>
    /// Definição meses abreviados
    /// </summary>
    public enum EInputMounths { 
        Janeiro, 
        Fevereiro, 
        Marco, 
        Abril, 
        Maio, 
        Junho, 
        Julho, 
        Agosto, 
        Setembro, 
        Outubro, 
        Novembro, 
        Dezembro 
    };

    /// <summary>
    /// Definição dias da semana
    /// </summary>
    public enum EInputWeekDays { 
        Domingo, 
        Segunda, 
        Terça, 
        Quarta, 
        Quinta, 
        Sexta, 
        Sabado 
    };

    /// <summary>
    /// Definição tipos de usuarios
    /// </summary>
    public enum EInputUser { 
        Administrador, 
        Usuario, 
        Visitante 
    };

    /// <summary>
    /// Definição tipos de status civil
    /// </summary>
    public enum EInputStatusCivel
    {
        Solteiro,
        Casado,
        Viuvo,
        Divorciado
    }

    /// <summary>
    /// Definição estados brasileiros
    /// </summary>
    public enum EInputUF
    { 
        None,
        AC, // Acre
        AL, // Alagoas
        AP, // Amapá
        AM, // Amazonas
        BA, // Bahia
        CE, // Ceará
        DF, // Distrito Federal
        ES, // Espírito Santo
        GO, // Goiás
        MA, // Maranhão
        MT, // Mato Grosso
        MS, // Mato Grosso do Sul
        MG, // Minas Gerais
        PA, // Pará
        PB, // Paraíba
        PR, // Paraná
        PE, // Pernambuco
        PI, // Piauí
        RR, // Roraima
        RO, // Rondônia
        RJ, // Rio de Janeiro
        RN, // Rio Grande do Norte
        RS, // Rio Grande do Sul
        SC, // Santa Catarina
        SP, // São Paulo
        SE, // Sergipe
        TO // Tocantins
    }


}