using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SQLite;
using TaskLogger_B.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskLogger_B
{
    [Route("api/[controller]")]
    public class Task : Controller
    {
        SQLite.SQLiteConnection db;
        public Task()
        {
            var databasePath = Path.GetFullPath("C:\\sqliteadmin\\test.s3db");
            db = new SQLiteConnection(databasePath);
        }

        // GET: api/<controller>
        [HttpGet, Authorize]
        public IEnumerable<string> Get()
        {
            var s = db.Insert(new Master()
            {
                key = 1
            });
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
