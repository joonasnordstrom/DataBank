using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace DocSite.Controllers
{
    public class LanguageController : Controller
    {
        // GET: Language
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Change(String LanguageAbbrevation)
        {
            if (LanguageAbbrevation != null)
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(LanguageAbbrevation);
                Thread.CurrentThread.CurrentUICulture =new CultureInfo(LanguageAbbrevation);
            }

            HttpCookie cookieTemp = new HttpCookie("Language");
            cookieTemp.Value = LanguageAbbrevation;
            Response.Cookies.Add(cookieTemp);

            return RedirectToAction("Index","Products");
        }


    }
}