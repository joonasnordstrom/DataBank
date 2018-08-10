using DocSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DocSite.Controllers
{
    public class BaseController : Controller
    {
        public ApplicationDbContext _context;

        public BaseController()
        {
            _context = new ApplicationDbContext();
        }

        public string GetOrganizationByAuthorId(string authorId)
        {
            if (authorId != null)
            {
                var userRole = _context.Users.First(u => u.Id == authorId)
                .Roles.ToList()
                .SingleOrDefault(r => r.UserId == authorId);

                return _context.Roles.SingleOrDefault(r => r.Id == userRole.RoleId)?.Name;
            }
            return null;
        }
    }
}