using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Baidyanath.Models.Shared;

namespace Baidyanath.Controllers
{
    public class VistorsController : Controller
    {
        //
        // GET: /Vistors/

        public ActionResult TotalVisiors()
        {
            TotalHitModel obj= new TotalHitModel();
            return PartialView(obj.GetTotalHit());
        }

    }
}
