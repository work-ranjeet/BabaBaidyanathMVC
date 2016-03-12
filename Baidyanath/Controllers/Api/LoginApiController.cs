using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Baidyanath.Controllers
{
    public class LoginApiController : ApiController
    {
        // GET api/loginapi
        //[WebGet(UriTemplate = "", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/loginapi/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/loginapi
        public void Post([FromBody]string value)
        {
        }

        // PUT api/loginapi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/loginapi/5
        public void Delete(int id)
        {
        }
    }
}
