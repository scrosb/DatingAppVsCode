
//We are only using C for the controller. 
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        //We have access to our dbcontext using Dependency injection and context. 

        private readonly DataContext _context;
        public UsersController(DataContext context)
        {
            _context = context;
        }

        //HttpGet sending back an Ienumerable. MAKE YOUR APP ASYNC IMMEDIATELY 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        //api/users/3- 3 is what we will be getting. 
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUser(int id)
        {
            return await _context.Users.FindAsync(id);
        }
    }
}