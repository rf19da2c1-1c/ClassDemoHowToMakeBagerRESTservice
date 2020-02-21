using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BagerLib.model;
using BagerREST.DBUtil;

namespace BagerREST.Controllers
{
    public class KagerController : ApiController
    {
        // GET: api/Kager
        public IEnumerable<Kage> Get()
        {
            ManagerKager mgr = new ManagerKager();
            return mgr.HentAlle();
        }

        // GET: api/Kager/5
        public Kage Get(string key)
        {
            ManagerKager mgr = new ManagerKager();
            return mgr.HentEn(key);
        }

        // POST: api/Kager
        public void Post([FromBody]Kage value)
        {
        }

        // PUT: api/Kager/5
        public void Put(string key, [FromBody]Kage value)
        {
        }

        // DELETE: api/Kager/5
        public void Delete(string key)
        {
        }
    }
}
