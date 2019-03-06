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
using System.Text;
using Newtonsoft.Json;

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
            var bestPractices = await db.BestPractice
                                        .Include(bp => bp.BestPracticeCompany)
                                        .ToListAsync();
            return bestPractices;
        }

        // GET: api/BestPractice/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BestPractice>> GetBestPractice(int id)
        {
            var bp = await db.BestPractice
                             .Include(x => x.BestPracticeCompany)
                                .ThenInclude(x => x.Company)
                             .SingleOrDefaultAsync(x => x.BestPracticeId == id);
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
            string[] comps = removeChars(bp.Company).Split(",");
            string dept = removeChars(bp.Department);
            bp.Department = dept;
            db.BestPractice.Add(bp);
            int id = bp.BestPracticeId;
            // addCompanies
            IList<Company> companyList = new List<Company>();
            for(int i = 0; i< comps.Count(); i++) {
                companyList.Add(db.Company.Where(entity => entity.Name.Equals(comps[i], StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault());
            }
            for(int i = 0; i < companyList.Count(); i++) {
                db.BestPracticeCompany.Add(new BestPracticeCompany{
                        BestPracticeId = id,
                        CompanyId = companyList[i].CompanyId
                });
            }
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
            string[] comps = removeChars(bp.Company).Split(",");
            BestPracticeCompanyController bpc_cont = new BestPracticeCompanyController(db);
            await bpc_cont.DeleteBestPracticeCompany(id);
            // addCompanies
            addCompanies(id, comps);

            // IList<Company> companyList = new List<Company>();
            // for(int i = 0; i< comps.Count(); i++) {
            //     companyList.Add(db.Company.Where(entity => entity.Name.Equals(comps[i], StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault());
            // }
            // for(int i = 0; i < companyList.Count(); i++) {
            //     db.BestPracticeCompany.Add(new BestPracticeCompany{
            //             BestPracticeId = id,
            //             CompanyId = companyList[i].CompanyId
            //     });
            // }

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

        private string removeChars(string input) {
            string output = "";
            foreach(char c in input) {
                if(c != '\\' && c != '"' && c!= '[' && c!= ']' && c != ' ') {
                    output += c;
                }
            }
            return output;
        }

        private void addCompanies(int id, string[] comps) {
            IList<Company> companyList = new List<Company>();
            for(int i = 0; i< comps.Count(); i++) {
                companyList.Add(db.Company.Where(entity => entity.Name.Equals(comps[i], StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault());
            }
            for(int i = 0; i < companyList.Count(); i++) {
                db.BestPracticeCompany.Add(new BestPracticeCompany{
                        BestPracticeId = id,
                        CompanyId = companyList[i].CompanyId
                });
            }
        }
    }
}
