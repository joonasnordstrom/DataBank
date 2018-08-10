using DocSite.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocSite.ViewModels
{
    public class RoleProductViewModel
    {
        public RoleProductViewModel()
        {
            Files = new List<HttpPostedFileBase>();
        }

        public List<HttpPostedFileBase> Files { get; set; }
        public List<Product> Products { get; set; }
        public List<IdentityRole> Roles { get; set; }
        public int? ProductId { get; set; }
        public List<string> Organizations { get; set; }

    }
}