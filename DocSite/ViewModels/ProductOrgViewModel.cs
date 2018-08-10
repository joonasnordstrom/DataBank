using DocSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocSite.ViewModels
{
    public class ProductOrgViewModel
    {
        public string Organization { get; set; }
        public List<Product> Products { get; set; }
    }
}