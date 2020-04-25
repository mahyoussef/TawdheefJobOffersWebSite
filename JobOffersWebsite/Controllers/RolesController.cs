using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace JobOffersWebsite.Controllers
{
    public class RolesController : Controller
    {
        ApplicationDbContext _context = new ApplicationDbContext();
        // GET: Roles
        public ActionResult Index()
        {
            var roles = _context.Roles.ToList();
            return View(roles);
        }

        // GET: Roles/Details/5
        public ActionResult Details(string id)
        {
            var role = _context.Roles.Find(id);
            if (role == null)
                return HttpNotFound();

            return View(role);
        }

        // GET: Roles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Roles/Create
        [HttpPost]
        public ActionResult Create(IdentityRole role)
        {
                // TODO: Add insert logic here
                if(ModelState.IsValid)
                {
                    _context.Roles.Add(role);
                    _context.SaveChanges();
                }
                return RedirectToAction("Index");
        }

        // GET: Roles/Edit/5
        public ActionResult Edit(string id)
        {
            var role = _context.Roles.Find(id);
            if (role == null)
                return HttpNotFound();

            return View(role);
        }

        // POST: Roles/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include ="Id, Name")]IdentityRole role)
        {
               if(ModelState.IsValid)
            {
                _context.Entry(role).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(role);
            
        }

        // GET: Roles/Delete/5
        public ActionResult Delete(string id)
        {
            var role = _context.Roles.Find(id);
            if (role == null)
                return HttpNotFound();

            return View(role);
        }

        // POST: Roles/Delete/5
        [HttpPost]
        public ActionResult Delete(IdentityRole role)
        {
            var deletedRole = _context.Roles.Find(role.Id); 
            _context.Roles.Remove(deletedRole);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
