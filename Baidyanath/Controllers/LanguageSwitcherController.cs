using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Baidyanath.Models.LanguageSwitcher;

namespace Baidyanath.Controllers
{
    public class LanguageSwitcherController : Controller
    {
        [HttpGet]
        public async Task<ActionResult> SwitchLanguage()
        {
            return await Task.Run(() =>
            {
                return RedirectToAction("Index", "Home");
            });
        }

        [HttpPost]
        public ActionResult SwitchLanguage(string SelectedOption, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                HttpCookie cookie = new HttpCookie("language");
                cookie.Value = SelectedOption;

                HttpContext.Response.Cookies.Add(cookie);

            }
            if (ReturnUrl.Split(',')[0] == "LatestPostDetail")
                return RedirectToAction(ReturnUrl.Split(',')[0], ReturnUrl.Split(',')[1], new { InformationID = ReturnUrl.Split(',')[2] });

            return RedirectToAction(ReturnUrl.Split(',')[0], ReturnUrl.Split(',')[1]);
        }

    }
}
