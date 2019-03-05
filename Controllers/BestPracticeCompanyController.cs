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

namespace PaccarAPI.Controllers
{
    [Route("api/[controller]")]
    public class BestPracticeCompanyController : ControllerBase
    {
        private readonly PaccarDbContext db;
        public BestPracticeCompanyController(PaccarDbContext dbContext)
        {
            db = dbContext;
        }

        // GET: api/BestPracticeCompany
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BestPracticeCompany>>> GetBestPracticeCompanies()
        {
            return await db.BestPracticeCompany.ToListAsync();
        }

        // GET: api/BestPracticeCompany/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BestPracticeCompany>> GetBestPracticeCompany(int id)
        {
            var bpc = await db.BestPracticeCompany.FindAsync(id);
            if (bpc == null)
            {
                return NotFound();
            }
            return bpc;
        }

        [HttpOptions]
        public HttpResponseMessage Options()
        {
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
        }

        // DELETE: api/BestPracticeCompany/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBestPracticeCompany(int id)
        {
            var bpc = await db.BestPracticeCompany
                            .Where(x => x.BestPracticeId == id)
                            .Select(x => x)
                            .ToListAsync();
            if (bpc == null)
            {
                return NotFound();
            }
            foreach(BestPracticeCompany bpc_id in bpc) {
                db.BestPracticeCompany.Remove(bpc_id);
            }
            await db.SaveChangesAsync();
            return NoContent();
        }
    }
}