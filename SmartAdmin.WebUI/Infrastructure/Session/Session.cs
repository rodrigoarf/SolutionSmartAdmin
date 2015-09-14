﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartAdmin.WebUI.Infrastructure.Session
{
    public class SessionManager : ISessionOperation<Administrator>
    {
        /// <summary>
        /// Inicia uma sessão de acesso autorizado a aplicação.
        /// </summary>
        public void Start(Administrator Model)
        {
            HttpContext.Current.Session["SmartAdminSession"] = Model;
        }

        /// <summary>
        /// Finaliza uma sessão de acesso autorizado a aplicação.
        /// </summary>
        public void Finish()
        {
            if (HttpContext.Current.Session["SmartAdminSession"] != null) { HttpContext.Current.Session.Abandon(); }
        }

        /// <summary>
        /// Retorna [true] se a sessão estiver ativa ou [false] se a sessão estiver inativa.
        /// </summary>
        public bool IsActive()
        {
            return ((HttpContext.Current.Session["SmartAdminSession"] != null) ? true : false);
        }

        /// <summary>
        /// Retorna o id da sessão autorizada.
        /// </summary>
        public string GetSessionId()
        {
            if  (HttpContext.Current.Session["SmartAdminSession"] != null)
            {
                 return (HttpContext.Current.Session.SessionID);
            }
            else
            {
                return (String.Empty);
            } 
        }

        /// <summary>
        /// Retorna o objeto Administrator da armazenado na sessão atual.
        /// </summary>
        public Administrator GetAdministrator()
        {
           var Model = (Administrator)HttpContext.Current.Session["SmartAdminSession"];
           return (Model);
        }
    }
}