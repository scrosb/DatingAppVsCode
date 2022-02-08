using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace API.Entities
{
    public class AppRole : IdentityRole<int>
    {
        //Many to many relationship between users and roles. 
        public ICollection<AppUserRole> UserRoles { get; set; }

    }
}