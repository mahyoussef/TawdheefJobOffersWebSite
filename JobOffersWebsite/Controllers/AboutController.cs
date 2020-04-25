using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace JobOffersWebsite.Controllers
{
    public class AboutController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();
        // GET: About
        public ActionResult Index()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}