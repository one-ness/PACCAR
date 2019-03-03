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
using Microsoft.EntityFrameworkCore;
using PaccarAPI.Controllers;

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

        // GET: api/BestPractice
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BestPractice>>> GetBestPractices()
        {
            return await db.BestPractice.ToListAsync();
        }

        // GET: api/BestPractice/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BestPractice>> GetBestPractice(int id)
        {
            var bp = await db.BestPractice.FindAsync(id);
            
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
            string comp = removeChars(bp.Company);
            string dept = removeChars(bp.Department);
            bp.Department = dept;
            db.BestPractice.Add(bp);
            await db.SaveChangesAsync();
            int id = bp.BestPracticeId;
            addCompanies(comp, id);
            await db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBestPractice), new {id}, bp);
        }

        // PUT: api/BestPractice/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBestPractice(int id, [FromBody]BestPractice bp)
        {
            if (id != bp.BestPracticeId)
            {
                return BadRequest();
            }
            db.Entry(bp).State = EntityState.Modified;
            await db.SaveChangesAsync();
            string comp = removeChars(bp.Company);
            BestPracticeCompanyController bpc_cont = new BestPracticeCompanyController(db);
            await bpc_cont.DeleteBestPracticeCompany(id);
            addCompanies(comp, id);
            await db.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/BestPractice/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBestPractice(int id)
        {
            var bp = await db.BestPractice.FindAsync(id);

            if (bp == null)
            {
                return NotFound();
            }
            db.BestPractice.Remove(bp);
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

        private void addCompanies(string comp, int id) {
            string[] companies = comp.Split(',');
            foreach(string c in companies) {
                string company = c.Trim();
                if(company.Equals("PACCAR", StringComparison.InvariantCultureIgnoreCase)) {
                    db.BestPracticeCompany.Add(new BestPracticeCompany{
                        BestPracticeId = id,
                        CompanyId = 1
                    });
                }
                else if(company.Equals("Peterbilt", StringComparison.InvariantCultureIgnoreCase)) {
                    db.BestPracticeCompany.Add(new BestPracticeCompany{
                        BestPracticeId = id,
                        CompanyId = 3
                    });
                }
                else if(company.Equals("DAF", StringComparison.InvariantCultureIgnoreCase)) {
                    db.BestPracticeCompany.Add(new BestPracticeCompany{
                        BestPracticeId = id,
                        CompanyId = 4
                    });
                }
                else if(company.Equals("Kenworth", StringComparison.InvariantCultureIgnoreCase)) {
                    db.BestPracticeCompany.Add(new BestPracticeCompany{
                        BestPracticeId = id,
                        CompanyId = 2
                    });
                }
            }
        }
    }
}
