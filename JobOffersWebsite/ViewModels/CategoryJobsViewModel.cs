using JobOffersWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobOffersWebsite.ViewModels
{
    public class CategoryJobsViewModel
    {
        public IList<Job> Jobs { get; set; }
        public Category Category { get; set; }
    }
}