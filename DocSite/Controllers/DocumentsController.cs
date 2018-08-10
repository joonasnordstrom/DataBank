using DocSite.Models;
using DocSite.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DocSite.Controllers
{
    public class DocumentsController : BaseController
    {

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [Route("Documents/Index/{id}")]
        public ActionResult Index(int id)
        {
            ErrorLocalization();
            string organization = _context.Products.Single(o => o.Id == id).Name;

            var viewModel = new ProductViewModel
            {
                Name = organization,
                Id = id
            };

            return View(viewModel);
        }

        [Route("Documents/Organization/")]
        [Authorize]
        public ActionResult Organization()
        {
            string authorId = User.Identity.GetUserId();
            string organization = GetOrganizationByAuthorId(authorId);
            var viewModel = new OrganizationViewModel
            {
                Name = organization
            };

            return View(viewModel);
        }

        [Authorize(Roles = "Timehouse")]
        public ActionResult Admin()
        {
            var rolesAndProducts = new RoleProductViewModel
            {
                Products = _context.Products.ToList(),
                Roles = _context.Roles.ToList()
            };

            return View(rolesAndProducts);
        }

        private int GenerateId()
        {
            int id = (int)DateTime.Now.Ticks;

            if (_context.Documents.SingleOrDefault(d => d.DocumentID == id) != null)
                GenerateId();

            return id;
        }


        [Route("documents/download/{id}")]
        [HttpGet]
        public ActionResult Download(int id)
        {
            var organization = GetOrganizationByAuthorId(User.Identity.GetUserId());

            var docsByOrganization = _context.Organizations.Where(o => o.Name == organization || o.Name == "Public");
            var documentInDb = _context.Documents.Single(d => d.Id == id);

            foreach (var doc in docsByOrganization)
            {
                if (doc.DocumentID == documentInDb.DocumentID)
                {
                    string filePath = documentInDb.FilePath;
                    string contentType = Path.GetExtension(filePath);
                    //byte[] fileBytes = GetFile(fullName);

                    //downloads the file
                    return File(filePath, contentType, documentInDb.Name);

                    //reads the file in browser (chrome)
                    //return new FileStreamResult(new FileStream(fullName, FileMode.Open), contentType);
                }
            }
            return View("Error");

        }

        [Authorize]
        [Route("documents/saveuploadedfile/{productId}")]
        [HttpPost]
        public ActionResult SaveUploadedFile(int? productId)
        {
            string authorId = User.Identity.GetUserId();
            string organization = GetOrganizationByAuthorId(authorId);
            string productRepo = organization;

            try
            {
                var newDocument = new Document();

                HttpPostedFileBase file = Request.Files[0];
                if (file != null && file.ContentLength > 0)
                {
                    var originalDirectory = new DirectoryInfo(string.Format("{0}DocumentRepository", Server.MapPath(@"\")));
                    string pathString = Path.Combine(originalDirectory.ToString(), productRepo);
                    var fileName1 = Path.GetFileName(file.FileName);
                    bool productRepositoryExists = Directory.Exists(pathString);

                    if (!productRepositoryExists)
                        Directory.CreateDirectory(pathString);

                    var path = $"{pathString}\\{file.FileName}";
                    file.SaveAs(path);

                    if (newDocument.FilePath == null)
                    {
                        newDocument = new Document()
                        {
                            DocumentID = GenerateId(),
                            Name = fileName1,
                            FilePath = path,
                            ProductId = productId,
                            LastModified = DateTime.Now.ToString("dd.MM.yyyy")
                        };

                        _context.Documents.Add(newDocument);
                    }

                    var newOrganization = new Organization
                    {
                        DocumentID = newDocument.DocumentID,
                        Name = organization,
                        FilePath = path
                    };

                    _context.Organizations.Add(newOrganization);
                    _context.SaveChanges();

                }

                return RedirectToAction("Admin", "Documents");
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return View("Error");
            }
        }


        [Authorize(Roles = "Timehouse")]
        [Route("documents/adminuploaddocument/")]
        [HttpPost]
        public ActionResult AdminUploadDocument(RoleProductViewModel model)
        {
            try
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {

                    foreach (string organization in model.Organizations)
                    {
                        string productRepo = organization;
                        HttpPostedFileBase file = Request.Files[i];
                        if (file != null && file.ContentLength > 0)
                        {
                            var originalDirectory = new DirectoryInfo(string.Format("{0}DocumentRepository", Server.MapPath(@"\")));
                            string pathString = Path.Combine(originalDirectory.ToString(), productRepo);
                            var fileName1 = Path.GetFileName(file.FileName);

                            if (!Directory.Exists(pathString))
                                Directory.CreateDirectory(pathString);

                            var path = string.Format("{0}\\{1}", pathString, file.FileName);
                            file.SaveAs(path);

                            var newDocument = new Document()
                            {
                                DocumentID = GenerateId(),
                                Name = fileName1,
                                FilePath = path,
                                ProductId = model.ProductId,
                                LastModified = DateTime.Now.ToString(),
                            };

                            if (model.ProductId == null)
                                newDocument.ProductId = 0;

                            _context.Documents.Add(newDocument);

                            var newOrganization = new Organization
                            {
                                DocumentID = newDocument.DocumentID,
                                Name = organization,
                                FilePath = path
                            };

                            _context.Organizations.Add(newOrganization);
                            _context.SaveChanges();

                        }
                    }
                }
                return RedirectToAction("Admin", "Documents");
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Json("Failed to upload document");
            }
        }

        [HttpGet]
        [Route("documents/getlanguage/")]
        public String GetLanguage()
        {
            var value = Request.Cookies["Language"].Value;

            return value;
        }

        public void ErrorLocalization()
        {
            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }
        }
    }
}

