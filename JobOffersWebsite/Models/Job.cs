using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace JobOffersWebsite.Models
{
    public class Job
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Job Name")]
        public string JobTitle { get; set; }
        [Display(Name = "Job Description")]
        public string JobContent { get; set; }
        [Display(Name = "Job Image")]
        public string JobImage { get; set; }
        [Display(Name = "Job Category")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

    }
}