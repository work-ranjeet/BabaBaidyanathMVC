using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Baidyanath.Filters;
using Baidyanath.Models.Services;

namespace Baidyanath.Controllers
{
    [LanguageSwitcher]
    public class ServiceController : Controller
    {
        //
        // GET: /Servicce/

        public async Task<ActionResult> Services()
        {
            return await Task.Factory.StartNew(() =>
            {
                ServicesModel model = new ServicesModel();
                return View("Services", model.Services.Result);
            });
        }

    }
}
