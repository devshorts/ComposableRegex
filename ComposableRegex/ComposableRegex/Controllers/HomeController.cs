using System;
using System.Collections.Generic;
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

        private static HomeModel DefaultMode
        {
            get
            {
                return new HomeModel
                       {
                           ComposableRegex = @"## 
## This composable sample block validates an email address
## and validates all sorts of weird edge case emails as well
##

weirdChars = (!|-|\+|\\|\$|\^|~|#|%|\?|{|}|_|/|=)
numbers = \d
characters = [A-z]
anyChars = (weirdChars|numbers|characters)

lettersFollowedBySingleDot = (anyChars+\.anyChars+)

names = anyChars|lettersFollowedBySingleDot

onlyQuotableCharacters = @|\s
quotedNames = ""(names|onlyQuotableCharacters)+""

anyValidStart = (names|quotedNames)+

group = (quotedNames:anyValidStart)|anyValidStart

local = ^(group)

ipv4 = ((\d{1,3}.){3}(\d{1,3}))

ipv6Entry = ([a-f]|[A-F]|[0-9]){4}? ## group of 4 hex values
ipv6 = ((ipv6Entry:){7}?ipv6Entry) ## 8 groups of ipv6 entries

comAddresses = (characters+(\.characters+)*) ## stuff like a.b.c.d etc
domain = (comAddresses|ipv6|ipv4)$ ## this has to be at the end

(local)@(domain)"
                       };
            }
        }

        public ActionResult Index()
        {
            return View(DefaultMode);
        }

        [HttpPost]
        public ActionResult Index(HomeModel regex)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var composed = new Regexer(regex.ComposableRegex);
                    regex.TransformedRegex = composed.Regex;
                    regex.IsMatch = !String.IsNullOrEmpty(regex.TargetText) && Regex.IsMatch(regex.TargetText, regex.TransformedRegex);
                    regex.Trace = composed.DebugTrace;

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

        [HttpPost]
        public ActionResult RegexTree(string composedRegex)
        {
            return new JsonResult
                   {
                       Data = (new Regexer(composedRegex).Groups)
                   };
        }
    }
}
