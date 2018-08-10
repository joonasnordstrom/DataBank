using AutoMapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Data.Entity;
using DocSite.Models;
using Microsoft.AspNet.Identity;

namespace DocSite.Controllers.Api
{
    public class DocumentsController : ApiController
    {
        private ApplicationDbContext _context;
        private BaseController baseController;

        public DocumentsController()
        {
            _context = new ApplicationDbContext();
            baseController = new BaseController();
        }

        [Route("api/documentsapi/getdocuments/{id}")]
        [HttpGet]
        public IHttpActionResult GetDocuments(int id)
        {

            List<Document> documents = new List<Document>();

            string organization = "";

            var documentsInDbByProductId = _context.Documents.Where(d => d.ProductId == id).ToList();
            if (documentsInDbByProductId.Any()) //ei ole koskaan null => katsotaan onko yhtään tulosta
            {
                try
                {
                    var authorId = User.Identity.GetUserId();
                    organization = baseController.GetOrganizationByAuthorId(authorId);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }

                foreach (var document in documentsInDbByProductId)
                {
                    //document Ids by product in organizations datatable
                    var docsByProductInOrganizationsDb = _context.Organizations
                        .Where(o => o.DocumentID == document.DocumentID);

                    //check if document is authorized for logged in user or document is "public" and all users
                    if (docsByProductInOrganizationsDb.Any())
                    {
                        foreach (var org in docsByProductInOrganizationsDb)
                        {
                            if (org.Name == organization || org.Name == "Public")
                                documents.Add(documentsInDbByProductId
                                    .SingleOrDefault(d => d.DocumentID == org.DocumentID));
                        }
                    }
                }
            }
            return Ok(documents);
        }

        [Route("api/documentsapi/getorganizationdocuments/{organization}")]
        [HttpGet]
        public IHttpActionResult GetOrganizationDocuments(string organization)
        {
            List<Document> documents = new List<Document>();

            //Documents where productId == 0 = organizational documents
            var documentsInDb = _context.Documents.Where(d => d.ProductId == 0).ToList();

            if (documentsInDb.Any())
            {
                foreach (var document in documentsInDb)
                {
                    //document Ids by product in organizations datatable
                    var docsInOrganizationsDb = _context.Organizations
                        .Where(o => o.DocumentID == document.DocumentID);

                    //check if document is authorized for logged in user or document is "public" and all users
                    foreach (var org in docsInOrganizationsDb)
                    {
                        if (docsInOrganizationsDb.Any() && org.Name == organization)
                            documents.Add(document);
                    }
                }
            }
            return Ok(documents);
        }

        [HttpDelete]
        public IHttpActionResult DeleteDocument(int id)
        {
            var documentInDb = _context.Documents.SingleOrDefault(d => d.Id == id);

            if (documentInDb == null)
                return NotFound();

            /*_context.Documents.Remove(documentInDb);
            _context.SaveChanges();
            File.Delete(documentInDb.FilePath);

            return Ok();*/


            var organization = baseController.GetOrganizationByAuthorId(User.Identity.GetUserId());
            var documentsInOrganizationsDb = _context.Organizations.Where(d => d.DocumentID == documentInDb.DocumentID);

            Organization found = new Organization();

            if (documentsInOrganizationsDb.Any())
                found = documentsInOrganizationsDb.FirstOrDefault(x => x.Name == organization || x.Name == "Public");

            var documentsRemaining = documentsInOrganizationsDb.ToList();
            documentsRemaining.Remove(found);

            if(found.Name == "Public" && organization == "Timehouse")
            {
                _context.Organizations.Remove(found);
                if (!documentsRemaining.Any())
                {
                    _context.Organizations.Remove(found);
                    _context.Documents.Remove(documentInDb);
                }
                _context.SaveChanges();
                try
                {
                    File.Delete(found.FilePath);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            else if (found != null && found.Name != "Public")
            {
                _context.Organizations.Remove(found);
                if (!documentsRemaining.Any())
                {
                    _context.Organizations.Remove(found);
                    _context.Documents.Remove(documentInDb);
                }
                _context.SaveChanges();
                try
                {
                    File.Delete(found.FilePath);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

            }
            else
                return BadRequest();


            return Ok();
        }

    }
}
