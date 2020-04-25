using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JobOffersWebsite.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [Display(Name ="Category Type")]
        public string CategoryType { get; set; }
        [Required]
        [Display(Name = "Job Description")]
        public string CategoryDescription { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }
    }
}