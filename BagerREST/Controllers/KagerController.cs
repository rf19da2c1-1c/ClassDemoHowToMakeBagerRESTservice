using System.Collections.Generic;
using System.Web.Http;
using BagerLib.model;
using BagerREST.DBUtil;

namespace BagerREST.Controllers
{
    [RoutePrefix("api/Kager")]
    public class KagerController : ApiController
    {
        private static ManagerKager mgr = new ManagerKager();

        // GET: api/Kager
        [Route("")]
        public IEnumerable<Kage> Get()
        {
            //ManagerKager mgr = new ManagerKager();
            return mgr.HentAlle();
        }

        // GET: api/Kager/5
        [Route("{key}")]
        public Kage Get(string key)
        {
            //ManagerKager mgr = new ManagerKager();
            return mgr.HentEn(key);
        }

        // POST: api/Kager
        [Route("")]
        public bool Post([FromBody]Kage value)
        {
            return mgr.Opret(value);
        }

        // PUT: api/Kager/5
        [Route("{key}")]
        public bool Put(string key, [FromBody]Kage value)
        {
            return mgr.Update(key, value);
        }

        // DELETE: api/Kager/5
        [Route("{key}")]
        public Kage Delete(string key)
        {
            return mgr.Delete(key);
        }
    }
}
