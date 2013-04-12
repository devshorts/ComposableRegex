using System;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using ComposableRegex.Controllers.RegexWorkers;
using ComposableRegex.Models;

namespace ComposableRegex.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View(new HomeModel());
        }

        [HttpPost]
        public ActionResult Index(HomeModel regex)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    regex.TransformedRegex = new Regexer(regex.ComposableRegex).Regex;
                    regex.IsMatch = !String.IsNullOrEmpty(regex.TargetText) && Regex.IsMatch(regex.TargetText, regex.TransformedRegex);

                    return View(regex);
                }
                catch (Exception ex)
                {
                    return View(new HomeModel
                                {
                                    IsMatch = false,
                                    TransformedRegex = "Something didn't work right!"
                                });
                }
            }
            return Index();
        }
    }
}
