using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UserRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<MemberDto> GetMemberAsync(string username)
        {
            //Get only the fields we want from the database. 
            return await _context.Users
            .Where(x => x.UserName == username)
            .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
           .SingleOrDefaultAsync();
        }

        public async Task<PagedList<MemberDto>> GetMembersAsync(UserParams userParams)
        {
            var query = _context.Users.AsQueryable();
            // .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
            // .AsNoTracking()
            // .AsQueryable();

            //do something with this query
            //return all of the user except the logged in uwer
            query = query.Where(u => u.UserName != userParams.CurrentUsername);
            query = query.Where(u => u.Gender == userParams.Gender);

            var minDob = DateTime.Today.AddYears(-userParams.MaxAge - 1);
            var maxDob = DateTime.Today.AddYears(-userParams.MinAge);

            query = query.Where(u => u.DateOfBirth >= minDob && u.DateOfBirth <= maxDob);

            //Check for the last active. 

            //new switch statement, case "created" is order by created,
            //case LastActive is Order by LastActive. 
            query = userParams.OrderBy switch
            {
                "created" => query.OrderByDescending(u => u.Created),
                _ => query.OrderByDescending(u => u.LastActive)
            };

            return await PagedList<MemberDto>.CreateAsync(query.ProjectTo<MemberDto>(
                _mapper.ConfigurationProvider).AsNoTracking(), userParams.PageNumber, userParams.pageSize);
        }

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            //this is quicker than the get user by username async. 
            return await _context.Users.FindAsync(id);
        }

        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
            .Include(p => p.Photos)
            .SingleOrDefaultAsync(x => x.UserName == username);
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            //Eager loading photos circular reference error, AppUser is in photos and a collection of photos
            //is in each user. 
            return await _context.Users
            .Include(p => p.Photos)
            .ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            //Save changes returns an int with a value that has the number of changes. 
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(AppUser user)
        {
            //Lets entity framework add a flag to that entity to say its been modified. 
            _context.Entry(user).State = EntityState.Modified;
        }
    }
}