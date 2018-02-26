﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Inventario.Framework.Security;

namespace Inventario
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            if (FormsAuthentication.CookiesSupported == true)
            {
                HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

                if (authCookie != null)
                {
                    try
                    {
                        FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                       // string value = CookieStore.GetCookie("InterviewPanelId");
                        HttpContext.Current.User = new AdminPrincipal(authTicket.Name, authTicket);
                    }
                    catch (Exception)
                    {
                        //somehting went wrong
                    }
                }
            }
        }
    }

}
