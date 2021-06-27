
//We are only using C for the controller. 
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class UsersController : BaseApiController
    {
        //We have access to our dbcontext using Dependency injection and context. 

        private readonly DataContext _context;
        public UsersController(DataContext context)
        {
            _context = context;
        }

        //HttpGet sending back an Ienumerable. MAKE YOUR APP ASYNC IMMEDIATELY 
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }
        [Authorize]
        //api/users/3- 3 is what we will be getting. 
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUser(int id)
        {
            return await _context.Users.FindAsync(id);
        }
    }
}