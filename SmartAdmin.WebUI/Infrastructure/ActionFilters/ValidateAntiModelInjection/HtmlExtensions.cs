using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Linq.Expressions;
using System.Data.Linq;

namespace SmartAdmin.WebUI.Infrastructure.ActionFilters
{
    public static class HtmlExtensions
    {
        /// <summary>
        /// Developed by Adam Tuliper
        /// www.secure-coding.com
        /// adam@secure-coding.com
        /// Uses MvcHtmlString for backwards compatibility. 
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static MvcHtmlString AntiModelInjection(this HtmlHelper html, string modelPropertyName, object value)
        {
            return GenerateHiddenFormField(modelPropertyName, value);
        }

        /// <summary>
        /// Generates a hidden form field with a hashed value of the value in the model.
        /// This is used in conjunction with [ValidateAntiModelInjection] upon a form post to help ensure
        /// the form fields weren't tampered with.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static MvcHtmlString AntiModelInjectionFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            //Get the value from the model.
            object modelValue = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData).Model;

            //Get the name of the field from the model.
            string fieldName = ExpressionHelper.GetExpressionText(expression);

            return GenerateHiddenFormField(fieldName, modelValue);
        }

        /// <summary>
        /// Generates a hidden input type string for the given value after hashing the value.
        /// The field for ex. CustomerId would be named _CustomerIdToken
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="modelValue"></param>
        /// <returns></returns>
        private static MvcHtmlString GenerateHiddenFormField(string fieldName, object modelValue)
        {
            TagBuilder builder = new TagBuilder("input");
            builder.Attributes["type"] = "hidden";
            //If we have a field named CustomerId, then the token will be _CustomerIdToken
            builder.Attributes["name"] = string.Format("_{0}Token", fieldName);

            string value = GetValueFromModelValue(modelValue);

            //Now use the machine key to encrypt the value (ya, its called encode)
            value = MachineKey.Encode(Encoding.Unicode.GetBytes(value), MachineKeyProtection.Encryption);

            builder.Attributes["value"] = value.ToString();
            return new MvcHtmlString(builder.ToString(TagRenderMode.SelfClosing));

        }

        /// <summary>
        /// Gets the value from the model as a string. Binary types are converted to base 64 strings.
        /// </summary>
        /// <param name="formValue"></param>
        /// <returns></returns>
        private static string GetValueFromModelValue(object formValue)
        {
            //Test to determine if its binary data. If it is, we need to convert it to a base64 string.
            System.Data.Linq.Binary binaryValue = formValue as System.Data.Linq.Binary;
            if (binaryValue != null)
            {
                formValue = binaryValue.ToArray();
            }
            //If the above conversion to an array worked, then the following will cast as a byte array and convert.
            byte[] byteArrayValue = formValue as byte[];
            if (byteArrayValue != null)
            {
                formValue = Convert.ToBase64String(byteArrayValue);
            }
            return formValue.ToString();

        }  
    }
}