using JobOffersWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobOffersWebsite.ViewModels
{
    public class JobCategoryViewModel
    {
        public IList<Category> Categories { get; set; }
        public IList<Job> Jobs { get; set; }
    }
}