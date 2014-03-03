using MvcApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            int hour = DateTime.Now.Hour;
            ViewData["greating"] = (hour < 12 ? "Good morning" : "Good afternon");
            return View();
        }

        [AcceptVerbs (HttpVerbs.Get)]
        public ViewResult RSVPForm()
        {
            return View();
        }

        [AcceptVerbs (HttpVerbs.Post)]
        public ViewResult RSVPForm(GuestResponse guestResponse)
        {
            if (ModelState.IsValid)
            {
                guestResponse.Submit();
                return View("Thanks", guestResponse);
            }
            else return View();
        }
     }
}
