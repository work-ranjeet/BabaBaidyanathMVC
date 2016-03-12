using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Baidyanath.Filters;

namespace Baidyanath.Controllers
{
    [LanguageSwitcher]
    public class AboutUsController : Controller
    {
        public async Task<ActionResult> AboutUs()
        {
            return await Task.Run(() =>
            {
                return View();
            });
        }
        
    }
}
