using JobOffersWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace JobOffersWebsite.ViewModels
{
    public class ApplyForJobViewModel
    {
        public Job Job { get; set; }
        public ApplyForJob ApplyForJob { get; set; }

    }
}