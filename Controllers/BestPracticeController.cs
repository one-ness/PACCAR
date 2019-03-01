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
using Microsoft.EntityFrameworkCore;

namespace PaccarAPI.Controllers
{
    [Route("api/[controller]")]
    public class BestPracticeController : ControllerBase
    {
        private readonly PaccarDbContext db;
        public BestPracticeController(PaccarDbContext dbContext)
        {
            db = dbContext;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BestPractice>>> GetBestPractices()
        {
            return await db.BestPractices.ToListAsync();
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
            // foreach(char c in bp.Company) {
            //     if(c != '\\' && c != '"' && c!= '[' && c!= ']') {
            //         comp += c;
            //     }
            //     if(c == ',') {
            //         comp += ' ';
            //     }
            // }
            string comp = removeChars(bp.Company);
            string dept = removeChars(bp.Department);
            // foreach(char c in bp.Department) {
            //     if(c != '\\' && c != '"' && c!= '[' && c!= ']') {
            //         dept += c;
            //     }
            //     if(c == ',') {
            //         dept += ' ';
            //     }
            // }
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

        // PUT: api/User/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBestPractice(int id, BestPractice bp)
        {
            if (id != bp.Id)
            {
                return BadRequest();
            }
            db.Entry(bp).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBestPractice(int id)
        {
            var bp = await db.BestPractices.FindAsync(id);

            if (bp == null)
            {
                return NotFound();
            }
            db.BestPractices.Remove(bp);
            await db.SaveChangesAsync();

            return NoContent();
        }

        private static string removeChars(string input) {
            string output = "";
            foreach(char c in input) {
                if(c != '\\' && c != '"' && c!= '[' && c!= ']') {
                    output += c;
                }
                if(c == ',') {
                    output += ' ';
                }
            }
            return output;
        }
    }
}
