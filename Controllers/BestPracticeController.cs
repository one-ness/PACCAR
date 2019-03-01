using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using PaccarAPI.Models;
using PaccarAPI.Data;
using System.Threading.Tasks;
using System.Web.Http;
using System.Net.Http;
using System.Net;
using System.Text.RegularExpressions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PaccarAPI.Controllers
{
    [Route("api/[controller]")]
    public class BestPracticeController : Controller
    {
        private readonly PaccarDbContext db;
        public BestPracticeController(PaccarDbContext dbContext)
        {
            db = dbContext;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BestPractice>> GetBestPractice(int id)
        {
            var bp = await db.BestPractices.FindAsync(id);
            if (bp == null)
            {
                return NotFound();
            }
            return bp;
        }

        [HttpOptions]
        public HttpResponseMessage Options()
        {
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
        }

        // POST: api/BestPractice
        [HttpPost]
        public async Task<ActionResult<BestPractice>> PostBestPractice([FromBody]BestPractice bp)
        {
            // Regex rgx = new Regex("[^a-zA-Z0-9 -]");
            // bp.Department = rgx.Replace(bp.Department, "");
            string comp = "";
            foreach(char c in bp.Company) {
                if(c != '\\' && c != '"' && c!= '[' && c!= ']') {
                    comp += c;
                }
                if(c == ',') {
                    comp += ' ';
                }
            }

            string dept = "";
            foreach(char c in bp.Department) {
                if(c != '\\' && c != '"' && c!= '[' && c!= ']') {
                    dept += c;
                }
                if(c == ',') {
                    dept += ' ';
                }
            }
            BestPractice bpmodel = new BestPractice {
                Title = bp.Title,
                Summary = bp.Summary,
                Date = bp.Date,
                Company = comp,
                Department = dept,
                PN = bp.PN,
                Practice = bp.Practice
            };
            db.BestPractices.Add(bpmodel);
            await db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBestPractice), new { id = bpmodel.Id }, bp);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}
