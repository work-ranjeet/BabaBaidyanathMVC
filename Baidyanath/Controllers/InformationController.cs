using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Baidyanath.Filters;
using Baidyanath.Models.Informations;

namespace Baidyanath.Controllers
{
    [LanguageSwitcher]
    public class InformationController : Controller
    {
        public async Task<ActionResult> LatestPostDetail(Int64? InformationID)
        {
            return await Task.Factory.StartNew(() =>
            {
                LatestPostModel model = new LatestPostModel();
                return View("LatestPostDetail", model.LatestPostDescription(InformationID).Result);
            });
        }
       
    }
}
