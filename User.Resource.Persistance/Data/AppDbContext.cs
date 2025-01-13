

using User.Resource.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace User.Resource.Persistance.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        

         public virtual DbSet<UserData> usersData { get; set; }
    }
}
