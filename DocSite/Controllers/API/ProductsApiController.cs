using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DocSite.Controllers.API
{
    public class ProductsApiController : ApiController
    {
        private BaseController baseController;
        public ProductsApiController()
        {
            baseController = new BaseController();
        }

        [Route("productsapi/GetOrganizationByUserName")]
        [HttpGet]
        public IHttpActionResult GetOrganizationByUserName()
        {
            var authorId = User.Identity.GetUserId();
            string organization = baseController.GetOrganizationByAuthorId(authorId);

            return Ok(organization);
        }
    }
}
