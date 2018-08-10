using DocSite.Models;
using DocSite.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DocSite.Controllers
{
    public class ProductsController : BaseController
    {

        public ProductsController()
        {
            _context = new ApplicationDbContext();
           
        }

        // GET: Product
        public ActionResult Index()
        {
            var userOrg = GetOrganizationByAuthorId(User.Identity.GetUserId());
            List<Product> products = _context.Products.ToList();

            var viewModel = new ProductOrgViewModel
            {
                Organization = userOrg,
                Products = products
            };

            return View("Index", viewModel);
        }

    }
}