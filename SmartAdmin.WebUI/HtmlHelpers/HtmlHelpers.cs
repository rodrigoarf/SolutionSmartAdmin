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

        public static MvcHtmlString DropDownListCustomPais(string Id, string DefaultValue, object HtmlAtributes = null)
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
            HtmlControl.Append("<option value=\"\" " + ((String.IsNullOrEmpty(DefaultValue)) ? "selected" : string.Empty) + ">Pais...</option>");  
            HtmlControl.Append("<option value=\"Afghanistan\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Afghanistan") ? "selected" : string.Empty) : string.Empty) + ">Afghanistan</option>");
            HtmlControl.Append("<option value=\"Aland Islands\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Aland Islands") ? "selected" : string.Empty) : string.Empty) + ">Åland Islands</option>");
            HtmlControl.Append("<option value=\"Albania\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Albania") ? "selected" : string.Empty) : string.Empty) + ">Albania</option>");
            HtmlControl.Append("<option value=\"Algeria\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Algeria") ? "selected" : string.Empty) : string.Empty) + ">Algeria</option>");
            HtmlControl.Append("<option value=\"American Samoa\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "American Samoa") ? "selected" : string.Empty) : string.Empty) + ">American Samoa</option>");
            HtmlControl.Append("<option value=\"Andorra\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Andorra") ? "selected" : string.Empty) : string.Empty) + ">Andorra</option>");
            HtmlControl.Append("<option value=\"Angola\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Angola") ? "selected" : string.Empty) : string.Empty) + ">Angola</option>");
            HtmlControl.Append("<option value=\"Anguilla\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Anguilla") ? "selected" : string.Empty) : string.Empty) + ">Anguilla</option>");
            HtmlControl.Append("<option value=\"Antarctica\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Antarctica") ? "selected" : string.Empty) : string.Empty) + ">Antarctica</option>");
            HtmlControl.Append("<option value=\"Antigua and Barbuda\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Antigua and Barbuda") ? "selected" : string.Empty) : string.Empty) + ">Antigua and Barbuda</option>");
            HtmlControl.Append("<option value=\"Argentina\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Argentina") ? "selected" : string.Empty) : string.Empty) + ">Argentina</option>");
            HtmlControl.Append("<option value=\"Armenia\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Armenia") ? "selected" : string.Empty) : string.Empty) + ">Armenia</option>");
            HtmlControl.Append("<option value=\"Aruba\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Aruba") ? "selected" : string.Empty) : string.Empty) + ">Aruba</option>");
            HtmlControl.Append("<option value=\"Australia\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Australia") ? "selected" : string.Empty) : string.Empty) + ">Australia</option>");
            HtmlControl.Append("<option value=\"Austria\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Austria") ? "selected" : string.Empty) : string.Empty) + ">Austria</option>");
            HtmlControl.Append("<option value=\"Azerbaijan\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Azerbaijan") ? "selected" : string.Empty) : string.Empty) + ">Azerbaijan</option>");
            HtmlControl.Append("<option value=\"Bahamas\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Bahamas") ? "selected" : string.Empty) : string.Empty) + ">Bahamas</option>");
            HtmlControl.Append("<option value=\"Bahrain\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Bahrain") ? "selected" : string.Empty) : string.Empty) + ">Bahrain</option>");
            HtmlControl.Append("<option value=\"Bangladesh\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Bangladesh") ? "selected" : string.Empty) : string.Empty) + ">Bangladesh</option>");
            HtmlControl.Append("<option value=\"Barbados\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Barbados") ? "selected" : string.Empty) : string.Empty) + ">Barbados</option>");
            HtmlControl.Append("<option value=\"Belarus\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Belarus") ? "selected" : string.Empty) : string.Empty) + ">Belarus</option>");
            HtmlControl.Append("<option value=\"Belgium\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Belgium") ? "selected" : string.Empty) : string.Empty) + ">Belgium</option>");
            HtmlControl.Append("<option value=\"Belize\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Belize") ? "selected" : string.Empty) : string.Empty) + ">Belize</option>");
            HtmlControl.Append("<option value=\"Benin\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Benin") ? "selected" : string.Empty) : string.Empty) + ">Benin</option>");
            HtmlControl.Append("<option value=\"Bermuda\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Bermuda") ? "selected" : string.Empty) : string.Empty) + ">Bermuda</option>");
            HtmlControl.Append("<option value=\"Bhutan\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Bhutan") ? "selected" : string.Empty) : string.Empty) + ">Bhutan</option>");
            HtmlControl.Append("<option value=\"Bolivia\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Bolivia") ? "selected" : string.Empty) : string.Empty) + ">Bolivia</option>");
            HtmlControl.Append("<option value=\"Bosnia and Herzegovina\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Bosnia and Herzegovina") ? "selected" : string.Empty) : string.Empty) + ">Bosnia and Herzegovina</option>");
            HtmlControl.Append("<option value=\"Botswana\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Botswana") ? "selected" : string.Empty) : string.Empty) + ">Botswana</option>");
            HtmlControl.Append("<option value=\"Bouvet Island\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Bouvet Island") ? "selected" : string.Empty) : string.Empty) + ">Bouvet Island</option>");
            HtmlControl.Append("<option value=\"Brazil\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Brazil") ? "selected" : string.Empty) : string.Empty) + ">Brazil</option>");
            HtmlControl.Append("<option value=\"British Indian Ocean Territory\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "British Indian Ocean Territory") ? "selected" : string.Empty) : string.Empty) + ">British Indian Ocean Territory</option>");
            HtmlControl.Append("<option value=\"Brunei Darussalam\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Brunei Darussalam") ? "selected" : string.Empty) : string.Empty) + ">Brunei Darussalam</option>");
            HtmlControl.Append("<option value=\"Bulgaria\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Bulgaria") ? "selected" : string.Empty) : string.Empty) + ">Bulgaria</option>");
            HtmlControl.Append("<option value=\"Burkina Faso\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Burkina Faso") ? "selected" : string.Empty) : string.Empty) + ">Burkina Faso</option>");
            HtmlControl.Append("<option value=\"Burundi\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Burundi") ? "selected" : string.Empty) : string.Empty) + ">Burundi</option>");
            HtmlControl.Append("<option value=\"Cambodia\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Cambodia") ? "selected" : string.Empty) : string.Empty) + ">Cambodia</option>");
            HtmlControl.Append("<option value=\"Cameroon\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Cameroon") ? "selected" : string.Empty) : string.Empty) + ">Cameroon</option>");
            HtmlControl.Append("<option value=\"Canada\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Canada") ? "selected" : string.Empty) : string.Empty) + ">Canada</option>");
            HtmlControl.Append("<option value=\"Cape Verde\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Cape Verde") ? "selected" : string.Empty) : string.Empty) + ">Cape Verde</option>");
            HtmlControl.Append("<option value=\"Cayman Islands\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Cayman Islands") ? "selected" : string.Empty) : string.Empty) + ">Cayman Islands</option>");
            HtmlControl.Append("<option value=\"Central African Republic\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Central African Republic") ? "selected" : string.Empty) : string.Empty) + ">Central African Republic</option>");
            HtmlControl.Append("<option value=\"Chad\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Chad") ? "selected" : string.Empty) : string.Empty) + ">Chad</option>");
            HtmlControl.Append("<option value=\"Chile\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Chile") ? "selected" : string.Empty) : string.Empty) + ">Chile</option>");
            HtmlControl.Append("<option value=\"China\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "China") ? "selected" : string.Empty) : string.Empty) + ">China</option>");
            HtmlControl.Append("<option value=\"Christmas Island\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Christmas Island") ? "selected" : string.Empty) : string.Empty) + ">Christmas Island</option>");
            HtmlControl.Append("<option value=\"Cocos (Keeling) Islands\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Cocos (Keeling) Islands") ? "selected" : string.Empty) : string.Empty) + ">Cocos (Keeling) Islands</option>");
            HtmlControl.Append("<option value=\"Colombia\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Colombia") ? "selected" : string.Empty) : string.Empty) + ">Colombia</option>");
            HtmlControl.Append("<option value=\"Comoros\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Comoros") ? "selected" : string.Empty) : string.Empty) + ">Comoros</option>");
            HtmlControl.Append("<option value=\"Congo, The Democratic Republic of The\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Congo, The Democratic Republic of The") ? "selected" : string.Empty) : string.Empty) + ">Congo, The Democratic Republic of The</option>");
            HtmlControl.Append("<option value=\"Cook Islands\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Cook Islands") ? "selected" : string.Empty) : string.Empty) + ">Cook Islands</option>");
            HtmlControl.Append("<option value=\"Costa Rica\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Costa Rica") ? "selected" : string.Empty) : string.Empty) + ">Costa Rica</option>");
            HtmlControl.Append("<option value=\"Costa do Marfim\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Costa do Marfim") ? "selected" : string.Empty) : string.Empty) + ">Costa do Marfim</option>");
            HtmlControl.Append("<option value=\"Croatia\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Croatia") ? "selected" : string.Empty) : string.Empty) + ">Croatia</option>");
            HtmlControl.Append("<option value=\"Cuba\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Cuba") ? "selected" : string.Empty) : string.Empty) + ">Cuba</option>");
            HtmlControl.Append("<option value=\"Cyprus\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Cyprus") ? "selected" : string.Empty) : string.Empty) + ">Cyprus</option>");
            HtmlControl.Append("<option value=\"Czech Republic\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Czech Republic") ? "selected" : string.Empty) : string.Empty) + ">Czech Republic</option>");
            HtmlControl.Append("<option value=\"Denmark\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Denmark") ? "selected" : string.Empty) : string.Empty) + ">Denmark</option>");
            HtmlControl.Append("<option value=\"Djibouti\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Djibouti") ? "selected" : string.Empty) : string.Empty) + ">Djibouti</option>");
            HtmlControl.Append("<option value=\"Dominica\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Dominica") ? "selected" : string.Empty) : string.Empty) + ">Dominica</option>");
            HtmlControl.Append("<option value=\"Dominican Republic\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Dominican Republic") ? "selected" : string.Empty) : string.Empty) + ">Dominican Republic</option>");
            HtmlControl.Append("<option value=\"Ecuador\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Ecuador") ? "selected" : string.Empty) : string.Empty) + ">Ecuador</option>");
            HtmlControl.Append("<option value=\"Egypt\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Egypt") ? "selected" : string.Empty) : string.Empty) + ">Egypt</option>");
            HtmlControl.Append("<option value=\"El Salvador\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "El Salvador") ? "selected" : string.Empty) : string.Empty) + ">El Salvador</option>");
            HtmlControl.Append("<option value=\"Equatorial Guinea\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Equatorial Guinea") ? "selected" : string.Empty) : string.Empty) + ">Equatorial Guinea</option>");
            HtmlControl.Append("<option value=\"Eritrea\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Eritrea") ? "selected" : string.Empty) : string.Empty) + ">Eritrea</option>");
            HtmlControl.Append("<option value=\"Estonia\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Estonia") ? "selected" : string.Empty) : string.Empty) + ">Estonia</option>");
            HtmlControl.Append("<option value=\"Ethiopia\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Ethiopia") ? "selected" : string.Empty) : string.Empty) + ">Ethiopia</option>");
            HtmlControl.Append("<option value=\"Falkland Islands (Malvinas)\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Falkland Islands (Malvinas)") ? "selected" : string.Empty) : string.Empty) + ">Falkland Islands (Malvinas)</option>");
            HtmlControl.Append("<option value=\"Faroe Islands\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Faroe Islands") ? "selected" : string.Empty) : string.Empty) + ">Faroe Islands</option>");
            HtmlControl.Append("<option value=\"Fiji\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Fiji") ? "selected" : string.Empty) : string.Empty) + ">Fiji</option>");
            HtmlControl.Append("<option value=\"Finland\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Finland") ? "selected" : string.Empty) : string.Empty) + ">Finland</option>");
            HtmlControl.Append("<option value=\"France\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "France") ? "selected" : string.Empty) : string.Empty) + ">France</option>");
            HtmlControl.Append("<option value=\"French Guiana\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "French Guiana") ? "selected" : string.Empty) : string.Empty) + ">French Guiana</option>");
            HtmlControl.Append("<option value=\"French Polynesia\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "French Polynesia") ? "selected" : string.Empty) : string.Empty) + ">French Polynesia</option>");
            HtmlControl.Append("<option value=\"French Southern Territories\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "French Southern Territories") ? "selected" : string.Empty) : string.Empty) + ">French Southern Territories</option>");
            HtmlControl.Append("<option value=\"Gabon\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Gabon") ? "selected" : string.Empty) : string.Empty) + ">Gabon</option>");
            HtmlControl.Append("<option value=\"Gambia\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Gambia") ? "selected" : string.Empty) : string.Empty) + ">Gambia</option>");
            HtmlControl.Append("<option value=\"Georgia\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Georgia") ? "selected" : string.Empty) : string.Empty) + ">Georgia</option>");
            HtmlControl.Append("<option value=\"Germany\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Germany") ? "selected" : string.Empty) : string.Empty) + ">Germany</option>");
            HtmlControl.Append("<option value=\"Ghana\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Ghana") ? "selected" : string.Empty) : string.Empty) + ">Ghana</option>");
            HtmlControl.Append("<option value=\"Gibraltar\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Gibraltar") ? "selected" : string.Empty) : string.Empty) + ">Gibraltar</option>");
            HtmlControl.Append("<option value=\"Greece\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Greece") ? "selected" : string.Empty) : string.Empty) + ">Greece</option>");
            HtmlControl.Append("<option value=\"Greenland\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Greenland") ? "selected" : string.Empty) : string.Empty) + ">Greenland</option>");
            HtmlControl.Append("<option value=\"Grenada\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Grenada") ? "selected" : string.Empty) : string.Empty) + ">Grenada</option>");
            HtmlControl.Append("<option value=\"Guadeloupe\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Guadeloupe") ? "selected" : string.Empty) : string.Empty) + ">Guadeloupe</option>");
            HtmlControl.Append("<option value=\"Guam\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Guam") ? "selected" : string.Empty) : string.Empty) + ">Guam</option>");
            HtmlControl.Append("<option value=\"Guatemala\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Guatemala") ? "selected" : string.Empty) : string.Empty) + ">Guatemala</option>");
            HtmlControl.Append("<option value=\"Guernsey\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Guernsey") ? "selected" : string.Empty) : string.Empty) + ">Guernsey</option>");
            HtmlControl.Append("<option value=\"Guinea\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Guinea") ? "selected" : string.Empty) : string.Empty) + ">Guinea</option>");
            HtmlControl.Append("<option value=\"Guinea-bissau\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Guinea-bissau") ? "selected" : string.Empty) : string.Empty) + ">Guinea-bissau</option>");
            HtmlControl.Append("<option value=\"Guyana\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Guyana") ? "selected" : string.Empty) : string.Empty) + ">Guyana</option>");
            HtmlControl.Append("<option value=\"Haiti\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Haiti") ? "selected" : string.Empty) : string.Empty) + ">Haiti</option>");
            HtmlControl.Append("<option value=\"Heard Island and Mcdonald Islands\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Heard Island and Mcdonald Islands") ? "selected" : string.Empty) : string.Empty) + ">Heard Island and Mcdonald Islands</option>");
            HtmlControl.Append("<option value=\"Holy See (Vatican City State)\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Holy See (Vatican City State)") ? "selected" : string.Empty) : string.Empty) + ">Holy See (Vatican City State)</option>");
            HtmlControl.Append("<option value=\"Honduras\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Honduras") ? "selected" : string.Empty) : string.Empty) + ">Honduras</option>");
            HtmlControl.Append("<option value=\"Hong Kong\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Hong Kong") ? "selected" : string.Empty) : string.Empty) + ">Hong Kong</option>");
            HtmlControl.Append("<option value=\"Hungary\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Hungary") ? "selected" : string.Empty) : string.Empty) + ">Hungary</option>");
            HtmlControl.Append("<option value=\"Iceland\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Iceland") ? "selected" : string.Empty) : string.Empty) + ">Iceland</option>");
            HtmlControl.Append("<option value=\"India\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "India") ? "selected" : string.Empty) : string.Empty) + ">India</option>");
            HtmlControl.Append("<option value=\"Indonesia\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Indonesia") ? "selected" : string.Empty) : string.Empty) + ">Indonesia</option>");
            HtmlControl.Append("<option value=\"Iran, Islamic Republic of\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Iran, Islamic Republic of") ? "selected" : string.Empty) : string.Empty) + ">Iran, Islamic Republic of</option>");
            HtmlControl.Append("<option value=\"Iraq\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Iraq") ? "selected" : string.Empty) : string.Empty) + ">Iraq</option>");
            HtmlControl.Append("<option value=\"Ireland\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Ireland") ? "selected" : string.Empty) : string.Empty) + ">Ireland</option>");
            HtmlControl.Append("<option value=\"Isle of Man\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Isle of Man") ? "selected" : string.Empty) : string.Empty) + ">Isle of Man</option>");
            HtmlControl.Append("<option value=\"Israel\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Israel") ? "selected" : string.Empty) : string.Empty) + ">Israel</option>");
            HtmlControl.Append("<option value=\"Italy\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Italy") ? "selected" : string.Empty) : string.Empty) + ">Italy</option>");
            HtmlControl.Append("<option value=\"Jamaica\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Jamaica") ? "selected" : string.Empty) : string.Empty) + ">Jamaica</option>");
            HtmlControl.Append("<option value=\"Japan\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Japan") ? "selected" : string.Empty) : string.Empty) + ">Japan</option>");
            HtmlControl.Append("<option value=\"Jersey\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Jersey") ? "selected" : string.Empty) : string.Empty) + ">Jersey</option>");
            HtmlControl.Append("<option value=\"Jordan\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Jordan") ? "selected" : string.Empty) : string.Empty) + ">Jordan</option>");
            HtmlControl.Append("<option value=\"Kazakhstan\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Kazakhstan") ? "selected" : string.Empty) : string.Empty) + ">Kazakhstan</option>");
            HtmlControl.Append("<option value=\"Kenya\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Kenya") ? "selected" : string.Empty) : string.Empty) + ">Kenya</option>");
            HtmlControl.Append("<option value=\"Kiribati\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Kiribati") ? "selected" : string.Empty) : string.Empty) + ">Kiribati</option>");
            HtmlControl.Append("<option value=\"Korea\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Korea") ? "selected" : string.Empty) : string.Empty) + ">Korea</option>");
            HtmlControl.Append("<option value=\"Kuwait\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Kuwait") ? "selected" : string.Empty) : string.Empty) + ">Kuwait</option>");
            HtmlControl.Append("<option value=\"Kyrgyzstan\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Kyrgyzstan") ? "selected" : string.Empty) : string.Empty) + ">Kyrgyzstan</option>");
            HtmlControl.Append("<option value=\"República Democrática Popular Lau\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "República Democrática Popular Lau") ? "selected" : string.Empty) : string.Empty) + ">República Democrática Popular Lau</option>");
            HtmlControl.Append("<option value=\"Latvia\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Latvia") ? "selected" : string.Empty) : string.Empty) + ">Latvia</option>");
            HtmlControl.Append("<option value=\"Lebanon\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Lebanon") ? "selected" : string.Empty) : string.Empty) + ">Lebanon</option>");
            HtmlControl.Append("<option value=\"Lesotho\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Lesotho") ? "selected" : string.Empty) : string.Empty) + ">Lesotho</option>");
            HtmlControl.Append("<option value=\"Liberia\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Liberia") ? "selected" : string.Empty) : string.Empty) + ">Liberia</option>");
            HtmlControl.Append("<option value=\"Libyan Arab Jamahiriya\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Libyan Arab Jamahiriya") ? "selected" : string.Empty) : string.Empty) + ">Libyan Arab Jamahiriya</option>");
            HtmlControl.Append("<option value=\"Liechtenstein\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Liechtenstein") ? "selected" : string.Empty) : string.Empty) + ">Liechtenstein</option>");
            HtmlControl.Append("<option value=\"Lithuania\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Lithuania") ? "selected" : string.Empty) : string.Empty) + ">Lithuania</option>");
            HtmlControl.Append("<option value=\"Luxembourg\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Luxembourg") ? "selected" : string.Empty) : string.Empty) + ">Luxembourg</option>");
            HtmlControl.Append("<option value=\"Macao\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Macao") ? "selected" : string.Empty) : string.Empty) + ">Macao</option>");
            HtmlControl.Append("<option value=\"Macedonia, The Former Yugoslav Republic of\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Macedonia, The Former Yugoslav Republic of") ? "selected" : string.Empty) : string.Empty) + ">Macedonia, The Former Yugoslav Republic of</option>");
            HtmlControl.Append("<option value=\"Madagascar\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Madagascar") ? "selected" : string.Empty) : string.Empty) + ">Madagascar</option>");
            HtmlControl.Append("<option value=\"Malawi\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Malawi") ? "selected" : string.Empty) : string.Empty) + ">Malawi</option>");
            HtmlControl.Append("<option value=\"Malaysia\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Malaysia") ? "selected" : string.Empty) : string.Empty) + ">Malaysia</option>");
            HtmlControl.Append("<option value=\"Maldives\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Maldives") ? "selected" : string.Empty) : string.Empty) + ">Maldives</option>");
            HtmlControl.Append("<option value=\"Mali\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Mali") ? "selected" : string.Empty) : string.Empty) + ">Mali</option>");
            HtmlControl.Append("<option value=\"Malta\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Malta") ? "selected" : string.Empty) : string.Empty) + ">Malta</option>");
            HtmlControl.Append("<option value=\"Marshall Islands\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Marshall Islands") ? "selected" : string.Empty) : string.Empty) + ">Marshall Islands</option>");
            HtmlControl.Append("<option value=\"Martinique\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Martinique") ? "selected" : string.Empty) : string.Empty) + ">Martinique</option>");
            HtmlControl.Append("<option value=\"Mauritania\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Mauritania") ? "selected" : string.Empty) : string.Empty) + ">Mauritania</option>");
            HtmlControl.Append("<option value=\"Mauritius\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Mauritius") ? "selected" : string.Empty) : string.Empty) + ">Mauritius</option>");
            HtmlControl.Append("<option value=\"Mayotte\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Mayotte") ? "selected" : string.Empty) : string.Empty) + ">Mayotte</option>");
            HtmlControl.Append("<option value=\"Mexico\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Mexico") ? "selected" : string.Empty) : string.Empty) + ">Mexico</option>");
            HtmlControl.Append("<option value=\"Micronesia, Federated States of\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Micronesia, Federated States of") ? "selected" : string.Empty) : string.Empty) + ">Micronesia, Federated States of</option>");
            HtmlControl.Append("<option value=\"Moldova, Republic of\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Moldova, Republic of") ? "selected" : string.Empty) : string.Empty) + ">Moldova, Republic of</option>");
            HtmlControl.Append("<option value=\"Monaco\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Monaco") ? "selected" : string.Empty) : string.Empty) + ">Monaco</option>");
            HtmlControl.Append("<option value=\"Mongolia\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Mongolia") ? "selected" : string.Empty) : string.Empty) + ">Mongolia</option>");
            HtmlControl.Append("<option value=\"Montenegro\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Montenegro") ? "selected" : string.Empty) : string.Empty) + ">Montenegro</option>");
            HtmlControl.Append("<option value=\"Montserrat\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Montserrat") ? "selected" : string.Empty) : string.Empty) + ">Montserrat</option>");
            HtmlControl.Append("<option value=\"Morocco\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Morocco") ? "selected" : string.Empty) : string.Empty) + ">Morocco</option>");
            HtmlControl.Append("<option value=\"Mozambique\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Mozambique") ? "selected" : string.Empty) : string.Empty) + ">Mozambique</option>");
            HtmlControl.Append("<option value=\"Myanmar\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Myanmar") ? "selected" : string.Empty) : string.Empty) + ">Myanmar</option>");
            HtmlControl.Append("<option value=\"Namibia\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Namibia") ? "selected" : string.Empty) : string.Empty) + ">Namibia</option>");
            HtmlControl.Append("<option value=\"Nauru\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Nauru") ? "selected" : string.Empty) : string.Empty) + ">Nauru</option>");
            HtmlControl.Append("<option value=\"Nepal\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Nepal") ? "selected" : string.Empty) : string.Empty) + ">Nepal</option>");
            HtmlControl.Append("<option value=\"Netherlands\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Netherlands") ? "selected" : string.Empty) : string.Empty) + ">Netherlands</option>");
            HtmlControl.Append("<option value=\"Netherlands Antilles\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Netherlands Antilles") ? "selected" : string.Empty) : string.Empty) + ">Netherlands Antilles</option>");
            HtmlControl.Append("<option value=\"New Caledonia\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "New Caledonia") ? "selected" : string.Empty) : string.Empty) + ">New Caledonia</option>");
            HtmlControl.Append("<option value=\"New Zealand\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "New Zealand") ? "selected" : string.Empty) : string.Empty) + ">New Zealand</option>");
            HtmlControl.Append("<option value=\"Nicaragua\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Nicaragua") ? "selected" : string.Empty) : string.Empty) + ">Nicaragua</option>");
            HtmlControl.Append("<option value=\"Niger\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Niger") ? "selected" : string.Empty) : string.Empty) + ">Niger</option>");
            HtmlControl.Append("<option value=\"Nigeria\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Nigeria") ? "selected" : string.Empty) : string.Empty) + ">Nigeria</option>");
            HtmlControl.Append("<option value=\"Niue\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Niue") ? "selected" : string.Empty) : string.Empty) + ">Niue</option>");
            HtmlControl.Append("<option value=\"Norfolk Island\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Norfolk Island") ? "selected" : string.Empty) : string.Empty) + ">Norfolk Island</option>");
            HtmlControl.Append("<option value=\"Northern Mariana Islands\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Northern Mariana Islands") ? "selected" : string.Empty) : string.Empty) + ">Northern Mariana Islands</option>");
            HtmlControl.Append("<option value=\"Norway\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Norway") ? "selected" : string.Empty) : string.Empty) + ">Norway</option>");
            HtmlControl.Append("<option value=\"Oman\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Oman") ? "selected" : string.Empty) : string.Empty) + ">Oman</option>");
            HtmlControl.Append("<option value=\"Pakistan\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Pakistan") ? "selected" : string.Empty) : string.Empty) + ">Pakistan</option>");
            HtmlControl.Append("<option value=\"Palau\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Palau") ? "selected" : string.Empty) : string.Empty) + ">Palau</option>");
            HtmlControl.Append("<option value=\"Palestinian Territory, Occupied\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Palestinian Territory, Occupied") ? "selected" : string.Empty) : string.Empty) + ">Palestinian Territory, Occupied</option>");
            HtmlControl.Append("<option value=\"Panama\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Panama") ? "selected" : string.Empty) : string.Empty) + ">Panama</option>");
            HtmlControl.Append("<option value=\"Papua New Guinea\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Papua New Guinea") ? "selected" : string.Empty) : string.Empty) + ">Papua New Guinea</option>");
            HtmlControl.Append("<option value=\"Paraguay\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Paraguay") ? "selected" : string.Empty) : string.Empty) + ">Paraguay</option>");
            HtmlControl.Append("<option value=\"Peru\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Peru") ? "selected" : string.Empty) : string.Empty) + ">Peru</option>");
            HtmlControl.Append("<option value=\"Philippines\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Philippines") ? "selected" : string.Empty) : string.Empty) + ">Philippines</option>");
            HtmlControl.Append("<option value=\"Pitcairn\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Pitcairn") ? "selected" : string.Empty) : string.Empty) + ">Pitcairn</option>");
            HtmlControl.Append("<option value=\"Poland\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Poland") ? "selected" : string.Empty) : string.Empty) + ">Poland</option>");
            HtmlControl.Append("<option value=\"Portugal\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Portugal") ? "selected" : string.Empty) : string.Empty) + ">Portugal</option>");
            HtmlControl.Append("<option value=\"Puerto Rico\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Puerto Rico") ? "selected" : string.Empty) : string.Empty) + ">Puerto Rico</option>");
            HtmlControl.Append("<option value=\"Qatar\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Qatar") ? "selected" : string.Empty) : string.Empty) + ">Qatar</option>");
            HtmlControl.Append("<option value=\"Reunion\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Reunion") ? "selected" : string.Empty) : string.Empty) + ">Reunion</option>");
            HtmlControl.Append("<option value=\"Romania\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Romania") ? "selected" : string.Empty) : string.Empty) + ">Romania</option>");
            HtmlControl.Append("<option value=\"Russian Federation\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Russian Federation") ? "selected" : string.Empty) : string.Empty) + ">Russian Federation</option>");
            HtmlControl.Append("<option value=\"Rwanda\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Rwanda") ? "selected" : string.Empty) : string.Empty) + ">Rwanda</option>");
            HtmlControl.Append("<option value=\"Saint Helena\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Saint Helena") ? "selected" : string.Empty) : string.Empty) + ">Saint Helena</option>");
            HtmlControl.Append("<option value=\"Saint Kitts and Nevis\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Saint Kitts and Nevis") ? "selected" : string.Empty) : string.Empty) + ">Saint Kitts and Nevis</option>");
            HtmlControl.Append("<option value=\"Saint Lucia\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Saint Lucia") ? "selected" : string.Empty) : string.Empty) + ">Saint Lucia</option>");
            HtmlControl.Append("<option value=\"Saint Pierre and Miquelon\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Saint Pierre and Miquelon") ? "selected" : string.Empty) : string.Empty) + ">Saint Pierre and Miquelon</option>");
            HtmlControl.Append("<option value=\"Saint Vincent and The Grenadines\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Saint Vincent and The Grenadines") ? "selected" : string.Empty) : string.Empty) + ">Saint Vincent and The Grenadines</option>");
            HtmlControl.Append("<option value=\"Samoa\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Samoa") ? "selected" : string.Empty) : string.Empty) + ">Samoa</option>");
            HtmlControl.Append("<option value=\"San Marino\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "San Marino") ? "selected" : string.Empty) : string.Empty) + ">San Marino</option>");
            HtmlControl.Append("<option value=\"Sao Tome and Principe\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Sao Tome and Principe") ? "selected" : string.Empty) : string.Empty) + ">Sao Tome and Principe</option>");
            HtmlControl.Append("<option value=\"Saudi Arabia\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Saudi Arabia") ? "selected" : string.Empty) : string.Empty) + ">Saudi Arabia</option>");
            HtmlControl.Append("<option value=\"Senegal\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Senegal") ? "selected" : string.Empty) : string.Empty) + ">Senegal</option>");
            HtmlControl.Append("<option value=\"Serbia\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Serbia") ? "selected" : string.Empty) : string.Empty) + ">Serbia</option>");
            HtmlControl.Append("<option value=\"Seychelles\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Seychelles") ? "selected" : string.Empty) : string.Empty) + ">Seychelles</option>");
            HtmlControl.Append("<option value=\"Sierra Leone\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Sierra Leone") ? "selected" : string.Empty) : string.Empty) + ">Sierra Leone</option>");
            HtmlControl.Append("<option value=\"Singapore\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Singapore") ? "selected" : string.Empty) : string.Empty) + ">Singapore</option>");
            HtmlControl.Append("<option value=\"Slovakia\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Slovakia") ? "selected" : string.Empty) : string.Empty) + ">Slovakia</option>");
            HtmlControl.Append("<option value=\"Slovenia\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Slovenia") ? "selected" : string.Empty) : string.Empty) + ">Slovenia</option>");
            HtmlControl.Append("<option value=\"Solomon Islands\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Solomon Islands") ? "selected" : string.Empty) : string.Empty) + ">Solomon Islands</option>");
            HtmlControl.Append("<option value=\"Somalia\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Somalia") ? "selected" : string.Empty) : string.Empty) + ">Somalia</option>");
            HtmlControl.Append("<option value=\"South Africa\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "South Africa") ? "selected" : string.Empty) : string.Empty) + ">South Africa</option>");
            HtmlControl.Append("<option value=\"South Georgia and The South Sandwich Islands\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "South Georgia and The South Sandwich Islands") ? "selected" : string.Empty) : string.Empty) + ">South Georgia and The South Sandwich Islands</option>");
            HtmlControl.Append("<option value=\"Spain\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Spain") ? "selected" : string.Empty) : string.Empty) + ">Spain</option>");
            HtmlControl.Append("<option value=\"Sri Lanka\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Sri Lanka") ? "selected" : string.Empty) : string.Empty) + ">Sri Lanka</option>");
            HtmlControl.Append("<option value=\"Sudan\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Sudan") ? "selected" : string.Empty) : string.Empty) + ">Sudan</option>");
            HtmlControl.Append("<option value=\"Suriname\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Suriname") ? "selected" : string.Empty) : string.Empty) + ">Suriname</option>");
            HtmlControl.Append("<option value=\"Svalbard and Jan Mayen\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Svalbard and Jan Mayen") ? "selected" : string.Empty) : string.Empty) + ">Svalbard and Jan Mayen</option>");
            HtmlControl.Append("<option value=\"Swaziland\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Swaziland") ? "selected" : string.Empty) : string.Empty) + ">Swaziland</option>");
            HtmlControl.Append("<option value=\"Sweden\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Sweden") ? "selected" : string.Empty) : string.Empty) + ">Sweden</option>");
            HtmlControl.Append("<option value=\"Switzerland\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Switzerland") ? "selected" : string.Empty) : string.Empty) + ">Switzerland</option>");
            HtmlControl.Append("<option value=\"Syrian Arab Republic\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Syrian Arab Republic") ? "selected" : string.Empty) : string.Empty) + ">Syrian Arab Republic</option>");
            HtmlControl.Append("<option value=\"Taiwan, Province of China\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Taiwan, Province of China") ? "selected" : string.Empty) : string.Empty) + ">Taiwan, Province of China</option>");
            HtmlControl.Append("<option value=\"Tajikistan\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Tajikistan") ? "selected" : string.Empty) : string.Empty) + ">Tajikistan</option>");
            HtmlControl.Append("<option value=\"Tanzania, United Republic of\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Tanzania, United Republic of") ? "selected" : string.Empty) : string.Empty) + ">Tanzania, United Republic of</option>");
            HtmlControl.Append("<option value=\"Thailand\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Thailand") ? "selected" : string.Empty) : string.Empty) + ">Thailand</option>");
            HtmlControl.Append("<option value=\"Timor-leste\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Timor-leste") ? "selected" : string.Empty) : string.Empty) + ">Timor-leste</option>");
            HtmlControl.Append("<option value=\"Togo\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Tokelau") ? "selected" : string.Empty) : string.Empty) + ">Togo</option>");
            HtmlControl.Append("<option value=\"Tokelau\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Tokelau") ? "selected" : string.Empty) : string.Empty) + ">Tokelau</option>");
            HtmlControl.Append("<option value=\"Tonga\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Tonga") ? "selected" : string.Empty) : string.Empty) + ">Tonga</option>");
            HtmlControl.Append("<option value=\"Trinidad and Tobago\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Trinidad and Tobago") ? "selected" : string.Empty) : string.Empty) + ">Trinidad and Tobago</option>");
            HtmlControl.Append("<option value=\"Tunisia\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Tunisia") ? "selected" : string.Empty) : string.Empty) + ">Tunisia</option>");
            HtmlControl.Append("<option value=\"Turkey\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Turkey") ? "selected" : string.Empty) : string.Empty) + ">Turkey</option>");
            HtmlControl.Append("<option value=\"Turkmenistan\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Turkmenistan") ? "selected" : string.Empty) : string.Empty) + ">Turkmenistan</option>");
            HtmlControl.Append("<option value=\"Turks and Caicos Islands\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Turks and Caicos Islands") ? "selected" : string.Empty) : string.Empty) + ">Turks and Caicos Islands</option>");
            HtmlControl.Append("<option value=\"Tuvalu\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Tuvalu") ? "selected" : string.Empty) : string.Empty) + ">Tuvalu</option>");
            HtmlControl.Append("<option value=\"Uganda\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Uganda") ? "selected" : string.Empty) : string.Empty) + ">Uganda</option>");
            HtmlControl.Append("<option value=\"Ukraine\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Ukraine") ? "selected" : string.Empty) : string.Empty) + ">Ukraine</option>"); ;
            HtmlControl.Append("<option value=\"United Arab Emirates\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "United Arab Emirates") ? "selected" : string.Empty) : string.Empty) + ">United Arab Emirates</option>");
            HtmlControl.Append("<option value=\"United Kingdom\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "United Kingdom") ? "selected" : string.Empty) : string.Empty) + ">United Kingdom</option>");
            HtmlControl.Append("<option value=\"United States\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "United States") ? "selected" : string.Empty) : string.Empty) + ">United States</option>");
            HtmlControl.Append("<option value=\"United States Minor Outlying Islands\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "United States Minor Outlying Islands") ? "selected" : string.Empty) : string.Empty) + ">United States Minor Outlying Islands</option>");
            HtmlControl.Append("<option value=\"Uruguay\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Uruguay") ? "selected" : string.Empty) : string.Empty) + ">Uruguay</option>");
            HtmlControl.Append("<option value=\"Uzbekistan\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Uzbekistan") ? "selected" : string.Empty) : string.Empty) + ">Uzbekistan</option>");
            HtmlControl.Append("<option value=\"Vanuatu\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Vanuatu") ? "selected" : string.Empty) : string.Empty) + ">Vanuatu</option>");
            HtmlControl.Append("<option value=\"Venezuela\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Venezuela") ? "selected" : string.Empty) : string.Empty) + ">Venezuela</option>");
            HtmlControl.Append("<option value=\"Viet Nam\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Viet Nam") ? "selected" : string.Empty) : string.Empty) + ">Viet Nam</option>");
            HtmlControl.Append("<option value=\"Virgin Islands, British\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Virgin Islands, British") ? "selected" : string.Empty) : string.Empty) + ">Virgin Islands, British</option>");
            HtmlControl.Append("<option value=\"Virgin Islands, U.S.\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Virgin Islands, U.S.") ? "selected" : string.Empty) : string.Empty) + ">Virgin Islands, U.S.</option>");
            HtmlControl.Append("<option value=\"Wallis and Futuna\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Wallis and Futuna") ? "selected" : string.Empty) : string.Empty) + ">Wallis and Futuna</option>");
            HtmlControl.Append("<option value=\"Western Sahara\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Western Sahara") ? "selected" : string.Empty) : string.Empty) + ">Western Sahara</option>");
            HtmlControl.Append("<option value=\"Yemen\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Yemen") ? "selected" : string.Empty) : string.Empty) + ">Yemen</option>");
            HtmlControl.Append("<option value=\"Zambia\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Zambia") ? "selected" : string.Empty) : string.Empty) + ">Zambia</option>");
            HtmlControl.Append("<option value=\"Zimbabwe\" " + ((!String.IsNullOrEmpty(DefaultValue)) ? ((DefaultValue == "Zimbabwe") ? "selected" : string.Empty) : string.Empty) + ">Zimbabwe</option>");
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