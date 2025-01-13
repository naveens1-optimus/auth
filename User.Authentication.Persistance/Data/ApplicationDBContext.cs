using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Authentication.Domain.Entities;


namespace User.Authentication.Persistance.Data
{
    public class ApplicationDBContext : DbContext
    {
        

        public ApplicationDBContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<AppUser> Users {  get; set; }  

    }
    
}
