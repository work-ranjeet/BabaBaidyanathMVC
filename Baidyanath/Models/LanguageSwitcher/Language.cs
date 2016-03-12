using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Baidyanath.Resources;

namespace Baidyanath.Models.LanguageSwitcher
{
    public class Language
    {
        [Display(Name = "ChangeLanguage", ResourceType = typeof(Resource))]
        public string SelectedOption { get; set; }

        public string ReturnUrl { get; set; }


        public IEnumerable<SelectListItem> SelectOptions
        {
            get
            {
                bool selected = Thread.CurrentThread.CurrentCulture.Name == "en-US" ? true : false;

                return new List<SelectListItem>()
                    {
                            new SelectListItem { Value = "en-US", Text = "English", Selected=selected},
                            new SelectListItem { Value = "hi-IN", Text = "हिन्दी" , Selected=(selected==false)}
                    };
            }
        }


    }
}