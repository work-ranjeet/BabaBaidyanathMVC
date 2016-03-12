using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Baidyanath.Filters;
using Baidyanath.Models.Mantra;

namespace Baidyanath.Controllers
{
    [LanguageSwitcher]
    public class MantraController : Controller
    {
        public async Task<ActionResult> Mantra()
        {
            return await Task.Factory.StartNew(() =>
            {
                MantraModel model = new MantraModel();
                return View("Mantra", model.Mantra.Result);
            });
        }


        public async Task<ActionResult> MantraByParentID(string ParentID)
        {

            return await Task.Factory.StartNew(() =>
            {              
                MantraModel model = new MantraModel();
                return View("Mantra", model.MantraByParentID(Convert.ToInt64(ParentID)).Result);
            });
        }

        public async Task<ActionResult> MantraDescription(string MantraID)
        {
            return await Task.Factory.StartNew(() =>
            {
                MantraModel model = new MantraModel();       
                return View("Mantra", model.MantraDescription(Convert.ToInt64(MantraID)).Result);
            });
        }

    }
}
