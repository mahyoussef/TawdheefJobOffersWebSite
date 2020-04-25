using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace WebApplication1.Models
{
    public class UserType
    {
        public byte Id { get; set; }
        public string Type { get; set; }

    }
}