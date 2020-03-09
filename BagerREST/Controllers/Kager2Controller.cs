using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BagerLib.DBUitl;
using BagerLib.model;
using BagerREST.DBUtil;

namespace BagerREST.Controllers
{
    public class Kager2Controller : ApiController
    {
        private readonly AbstractDBManager<Kage> mgr;
        private const String CString = @"Data Source=(localdb)\ProjectsV13;Initial Catalog=ClassDemo;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public Kager2Controller()
        {
            mgr = new AbstractDBManager<Kage>(CString);
            ManageKager2 kageDel = new ManageKager2();

            mgr.ReadNextElement = kageDel.ReadNextElement;
            mgr.GenerateInsertSQL = kageDel.GenerateInsertSQL;
            mgr.SetParameterInsertSQL = kageDel.SetParameterIntoInsertSQL;
            mgr.GenerateUpdateSQL = kageDel.GenerateUpdateSQL;
            mgr.SetParameterUpdateSQL = kageDel.SetParameterIntoUpdateSQL;
            mgr.Open();

        }

        // GET: api/Kager2
        public IEnumerable<Kage> Get()
        {
            return mgr.HentAlle();
        }

        // GET: api/Kager2/5
        public Kage Get(int id)
        {
            return mgr.HentEn(id);
        }

        // POST: api/Kager2
        public bool Post([FromBody]Kage value)
        {
            return mgr.Opret(value);
        }

        // PUT: api/Kager2/5
        public bool Put(int id, [FromBody]Kage value)
        {
            return mgr.Update(id, value);
        }

        // DELETE: api/Kager2/5
        public Kage Delete(int id)
        {
            return mgr.Delete(id);
        }
    }
}
