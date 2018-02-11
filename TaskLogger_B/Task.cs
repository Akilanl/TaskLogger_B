using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SQLite;
using TaskLogger_B.Model;

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
        [HttpGet]
        public List<Master> Get(List<SearchModel> searchobj)
        {
            string querystring = "select * from Master";
            foreach (SearchModel data in searchobj)
            {
                dynamic searchvalue = string.Empty;
                if (data.FieldName.ToLower() == "ofdate" || data.FieldName.ToLower() == "modifieddate")
                    searchvalue = Convert.ToDateTime(data.SearchValue);
                if (data.FieldName.ToLower() == "task" || data.FieldName.ToLower() == "userid")
                    searchvalue = Convert.ToInt32(data.SearchValue);
                if (data.FieldName.ToLower() == "hoursspent")
                    searchvalue = Convert.ToDouble(data.SearchValue);
                if (data.ExactlyMatch == true)
                    querystring += "&&" + data.FieldName + "=" + searchvalue;
                else
                    querystring += "&&" + data.FieldName + "LIKE" + "'%" + searchvalue + "%'";
            }
            var res = db.Query<Master>(querystring);
            return res;
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]List<Master> tasksobj)
        {
            if (tasksobj != null)
            {
                var res = db.InsertAll(tasksobj);
            }
            return Ok();
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
