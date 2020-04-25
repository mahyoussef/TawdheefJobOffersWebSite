using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JobOffersWebsite.Models;
using JobOffersWebsite.ViewModels;
using Microsoft.AspNet.Identity;
using WebApplication1.Models;

namespace JobOffersWebsite.Controllers
{
    public class JobsController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        // GET: Jobs
        public ActionResult Index()
        {
            var jobs = _context.Jobs.Include(j => j.Category);
            return View(jobs.ToList());
        }

        // GET: Jobs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = _context.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // GET: Jobs/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(_context.Categories, "Id", "CategoryType");
            return View();
        }

        // POST: Jobs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Job job, HttpPostedFileBase uploadedImage)
        {
            if (ModelState.IsValid)
            {
                string path = Path.Combine(Server.MapPath("~/Uploads"),
                                          uploadedImage.FileName);
                uploadedImage.SaveAs(path);
                _context.Jobs.Add(job);
                job.JobImage = uploadedImage.FileName;
                job.UserId = User.Identity.GetUserId();
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(_context.Categories, "Id", "CategoryType", job.CategoryId);
            return View(job);
        }

        // GET: Jobs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = _context.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(_context.Categories, "Id", "CategoryType", job.CategoryId);
            return View(job);
        }

        // POST: Jobs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Job job, HttpPostedFileBase uploadedImage)
        {   
            var oldJobImage = Path.Combine(Server.MapPath("~/Uploads"), job.JobImage);
            if (ModelState.IsValid)
            {   
                if(uploadedImage != null)
                {
                    System.IO.File.Delete(oldJobImage);
                    string path = Path.Combine(Server.MapPath("~/Uploads"), uploadedImage.FileName);
                    uploadedImage.SaveAs(path);
                    job.JobImage = uploadedImage.FileName;
                }
                _context.Entry(job).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(_context.Categories, "Id", "CategoryType", job.CategoryId);
            return View(job);
        }

        // GET: Jobs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = _context.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Job job = _context.Jobs.Find(id);
            _context.Jobs.Remove(job);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [Route("/job/GetCategories/{id}")]
        public ActionResult GetCategories(int id)
        {
            var jobs = _context.Jobs.Where(m => m.CategoryId == id).ToList();
            var category = _context.Categories.Find(id);
            var viewModel = new CategoryJobsViewModel {
                Jobs = jobs,
                Category = category
            };
            return View(viewModel);
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
