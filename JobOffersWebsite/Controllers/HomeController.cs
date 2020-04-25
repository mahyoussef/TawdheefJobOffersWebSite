using JobOffersWebsite.Models;
using JobOffersWebsite.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        public ActionResult Index()
        {
            var categories = _context.Categories.ToList();
            var jobs = _context.Jobs.OrderByDescending(x => x.Id).Take(6).ToList();
            var viewModel = new JobCategoryViewModel {
                Jobs = jobs,
                Categories = categories
            };
            return View(viewModel);
        }

        public ActionResult Details(int jobId)
        {
            var job = _context.Jobs.Find(jobId);
            return View(job);
        }

        [Authorize]
        public ActionResult Apply(int jobId)
        {
            Session["JobId"] = jobId;
            var job = _context.Jobs.Find(jobId);
            var applyForJob = new ApplyForJob();
            var viewModel = new ApplyForJobViewModel {
                Job = job,
                ApplyForJob = applyForJob
            };
            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Apply(ApplyForJob applyForJob, HttpPostedFileBase uploadedCv)
        {
            var userId = User.Identity.GetUserId();
            var jobId = (int)Session["JobId"];
            var isAppliedOnceForJob = _context.ApplyForJobs.Where(a => a.JobId == jobId && a.UserId == userId).ToList();

            if (isAppliedOnceForJob.Count >= 1)
                ViewBag.Result = "You Have Applied Before.";

            else
            {
                if (ModelState.IsValid)
                {
                    string path = Path.Combine(Server.MapPath("~/Uploads"),
                                              uploadedCv.FileName);
                    uploadedCv.SaveAs(path);
                    var job = new ApplyForJob();
                    job.JobId = jobId;
                    job.UserId = userId;
                    job.Message = applyForJob.Message;
                    job.ApplyDate = DateTime.Now;
                    job.UserCv = uploadedCv.FileName;
                    _context.ApplyForJobs.Add(job);
                    _context.SaveChanges();
                    ViewBag.Result = "You Have Applied Successfully.";
                }
                else
                    return View(applyForJob);
            }
            return View();
        }

        public ActionResult MyJobs()
        {
            var userId = User.Identity.GetUserId();
            var jobs = _context.ApplyForJobs.Where(a => a.UserId == userId).OrderByDescending(a => a.Id).ToList();
            return View(jobs);
        }

        public ActionResult ApplicationJobDetails(int appliedJobId)
        {
            Session["JobId"] = appliedJobId;
            var appliedJob = _context.ApplyForJobs.Find(appliedJobId);
            if (appliedJob == null)
                return HttpNotFound();

            return View(appliedJob);
        }

        public ActionResult ApplicationJobEdit(int appliedJobId)
        {
            Session["JobId"] = appliedJobId;
            var appliedJob = _context.ApplyForJobs.Find(appliedJobId);
            if (appliedJob == null)
                return HttpNotFound();

            return View(appliedJob);
        }

        [HttpPost]
        public ActionResult ApplicationJobEdit(ApplyForJob applyForJob, HttpPostedFileBase uploadedCv)
        {
            var userId = User.Identity.GetUserId();
            var jobId = (int)Session["JobId"];
            var oldCV = Path.Combine(Server.MapPath("~/Uploads"), applyForJob.UserCv);
            if (ModelState.IsValid)
            {
                if (uploadedCv != null)
                {
                    System.IO.File.Delete(oldCV);
                    string path = Path.Combine(Server.MapPath("~/Uploads"), uploadedCv.FileName);
                    uploadedCv.SaveAs(path);
                    applyForJob.UserCv = uploadedCv.FileName;
                }

                applyForJob.JobId = jobId;
                applyForJob.UserId = userId;
                applyForJob.ApplyDate = DateTime.Now;

                _context.Entry(applyForJob).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(applyForJob);
        }

        public ActionResult Delete(int? appliedJobId)
        {
            if (appliedJobId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var applyForJob = _context.ApplyForJobs.Find(appliedJobId);
            if (applyForJob == null)
            {
                return HttpNotFound();
            }
            return View(applyForJob);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int appliedJobId)
        {
            var applyForJob = _context.ApplyForJobs.Find(appliedJobId);
            _context.ApplyForJobs.Remove(applyForJob);
            _context.SaveChanges();
            return RedirectToAction("MyJobs");
        }

        public ActionResult GetJobsByPublisher()
        {
            var userId = User.Identity.GetUserId();
            var jobs = from app in _context.ApplyForJobs
                       join job in _context.Jobs
                       on app.JobId equals job.Id
                       where job.User.Id == userId
                       select app;

            var jobGrouped = from j in jobs
                             group j by j.Job.JobTitle
                             into gr
                             select new JobByPublisherViewModel {
                                 JobTitle = gr.Key,
                                 Items = gr
                             };

            return View(jobGrouped.ToList());
        }

        public ActionResult DownloadCV(string filePath)
        {
            string fullName = Server.MapPath("~" + filePath);

            byte[] fileBytes = GetFile(fullName);
            return File(
                fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, filePath);
        }
        public ActionResult DownloadFile(string fileName)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";
            byte[] fileBytes = System.IO.File.ReadAllBytes(path + fileName);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
        byte[] GetFile(string s)
        {
            FileStream fs = System.IO.File.OpenRead(s);
            byte[] data = new byte[fs.Length];
            int br = fs.Read(data, 0, data.Length);
            if (br != fs.Length)
                throw new IOException(s);
            return data;
        }


        public ActionResult Search()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Search(string searchKey)
        {
            var result = _context.Jobs.Where(a => a.JobTitle.Contains(searchKey)
            || a.JobContent.Contains(searchKey)
            || a.Category.CategoryType.Contains(searchKey)
            || a.Category.CategoryDescription.Contains(searchKey)).ToList();
            
            return View(result);
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