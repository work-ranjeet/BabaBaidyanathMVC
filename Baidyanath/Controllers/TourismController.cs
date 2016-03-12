using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Baidyanath.Filters;
using Baidyanath.Resources;

namespace Baidyanath.Controllers
{
    [LanguageSwitcher]
    public class TourismController : Controller
    {
        [HttpGet]
        public async Task<ActionResult> Tourism()
        {
            return await Task.Run(() =>
            {
                return View();
            });
        }

        [HttpPost]
        public async Task<JsonResult> Tourism(string TourismType)
        {
            return await Task.Run(() =>
            {
                if (TourismType == "PlaceToVisitContent")
                {
                    HtmlString str = new HtmlString(TourismResource.PlaceToVisitContent);
                    return Json(str.ToHtmlString());
                }
                else
                {
                    HtmlString str = new HtmlString(TourismResource.HowToReachContent);
                    return Json(str.ToHtmlString());
                }
            });
        }

    }
}
