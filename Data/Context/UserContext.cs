using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ATEC_API.Context
{
    public class UserContext : IdentityDbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
           
        }
    }
}