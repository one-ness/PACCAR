// using System.Linq;
// using PaccarAPI.Data;
// using PaccarAPI.Models;
// using System.Threading.Tasks;
// using System.Collections.Generic;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.AspNetCore.Mvc;

// namespace PaccarAPI.Controllers
// {
//     [Route("api/[controller]")]
//     [ApiController]
//     public class UserController : ControllerBase
//     {
//         private readonly PaccarDbContext db;
        
//         public UserController(PaccarDbContext context) {
//             db = context;
//             if(db.User.Count() == 0) {
//                 db.User.Add(new User{ FirstName = "Keegan", LastName = "Fisher" });
//                 db.SaveChanges();
//             }
//         }

//         // GET: api/User
//         [HttpGet]
//         public async Task<ActionResult<IEnumerable<User>>> GetUsers()
//         {
//             return await db.User.ToListAsync();
//         }

//         // GET: api/User/5
//         [HttpGet("{id}")]
//         public async Task<ActionResult<User>> GetUser(int id)
//         {
//             var user = await db.User.FindAsync(id);
//             if (user == null)
//             {
//                 return NotFound();
//             }
//             return user;
//         }

//         // POST: api/User
//         [HttpPost]
//         public async Task<ActionResult<User>> PostUser(User user)
//         {
//             db.User.Add(user);
//             await db.SaveChangesAsync();
//             return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, user);
//         }

//         // PUT: api/User/5
//         [HttpPut("{id}")]
//         public async Task<IActionResult> PutUser(int id, User user)
//         {
//             if (id != user.UserId)
//             {
//                 return BadRequest();
//             }
//             db.Entry(user).State = EntityState.Modified;
//             await db.SaveChangesAsync();
//             return NoContent();
//         }

//         // DELETE: api/User/5
//         [HttpDelete("{id}")]
//         public async Task<IActionResult> DeleteUser(int id)
//         {
//             var user = await db.User.FindAsync(id);

//             if (user == null)
//             {
//                 return NotFound();
//             }

//             db.User.Remove(user);
//             await db.SaveChangesAsync();

//             return NoContent();
//         }

//     }
// }