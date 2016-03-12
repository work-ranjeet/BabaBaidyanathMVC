using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Baidyanath.Filters;
using Baidyanath.Models;
using Baidyanath.Resources;

namespace Baidyanath.Controllers
{
    [LanguageSwitcher]
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            return await Task.Run(() =>
            {
                return View();
            });
        }

    
    }


}
