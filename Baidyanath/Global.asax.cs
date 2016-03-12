using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Baidyanath
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            BundleMobileConfig.RegisterBundles(BundleTable.Bundles);
            DeviceConfig.EvaluateDisplayMode();
        }

        protected void Session_Start()
        {
            try
            {
                Baidyanath.Models.Shared.TotalHitModel model = new Models.Shared.TotalHitModel();
                model.InsertTotalHit();
                model.Dispose();
            }
            catch { }
        }

        //private void Application_BeginRequest(Object source, EventArgs e)
        //{
        //    string culture = null;
        //    HttpCookie languageCookie = Request.Cookies["language"];
        //    if (languageCookie != null)
        //    {
        //        culture = languageCookie.Value;
        //    }
        //    else
        //    {
        //        HttpApplication application = (HttpApplication)source;
        //        HttpContext context = application.Context;

        //        if (context.Request.UserLanguages != null && Request.UserLanguages.Length > 0)
        //        {
        //            culture = Request.UserLanguages[0];
        //        }
        //    }
        //    Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(culture);
        //    Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

        //}
    }
}