﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Facebook;
using Facebook.Web;
using Facebook.Web.Mvc;
using IFramedInBrowser.Web.Models;

namespace IFramedInBrowser.Web.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        [CanvasAuthorize(Permissions = "user_about_me")]
        public ActionResult Index()
        {

            var model = new FacebookToSilverlightViewModel
                            {
                                FbSessionKey = FacebookWebContext.Current.AccessToken
                            };

            return View(model);
        }
    }
}