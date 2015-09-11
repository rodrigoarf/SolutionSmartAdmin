using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartAdmin.Dto;
using SmartAdmin.Data;   
using SmartAdmin.WebUI.HtmlHelpers;

namespace SmartAdmin.WebUI.Controllers
{
    public class BaseController : Controller
    {
        public SmartAdmin.Domain.UnitOfWork unitOfWork = new SmartAdmin.Domain.UnitOfWork();
        public int PageSize = 100;

        public AcessoDto GetUserInformation(string Screen)
        {
            var Model = new AcessoDto();
            var Capabilities = Request.Browser;

            Model.BROWSER = Capabilities.Browser;
            Model.SUPORTA_ACTIVEX = ((Capabilities.ActiveXControls == true) ? "T" : "F");
            Model.SUPORTA_COOKIES = ((Capabilities.Cookies == true) ? "T" : "F");
            Model.SUPORTA_JAVA_APPLET = ((Capabilities.JavaApplets == true) ? "T" : "F");
            Model.DISPOSITIVO = ((Capabilities.IsMobileDevice) ? Capabilities.MobileDeviceModel : null);
            Model.RESOLUCAO = Screen;
            Model.PLATAFORMA = Capabilities.Platform;
            Model.URL_ACESSO = HttpContext.Request.Url.AbsolutePath;
            Model.DTH_ACESSO = DateTime.Now;
            Model.DIA = DateTime.Now.Day;
            Model.MES = DateTime.Now.Month;
            Model.ANO = DateTime.Now.Year;
            Model.HORA = String.Format("{0}:{1}", DateTime.Now.Hour, DateTime.Now.Minute);
            Model.IP = SmartAdmin.Domain.Helpers.Untils.GetClientIpAddress();
            Model.TIPO_USUARIO = "A";
            //Model.DOMINIO = (String.IsNullOrWhiteSpace(System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName) ? System.Environment.MachineName : System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName);

            return Model;
        }

    }
}
