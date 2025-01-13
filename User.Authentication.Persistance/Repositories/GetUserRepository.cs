using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Authentication.Application.Interfaces;
using User.Authentication.Domain.Entities;
using User.Authentication.Persistance.Data;

namespace User.Authentication.Persistance.Repositories
{
    
    



    public class GetUserRepository : IGetUserRepository
    {
        ApplicationDBContext context;

        public GetUserRepository(ApplicationDBContext context1)
        {
            context = context1;
        }
        public  async Task<AppUser> GetUser(string username, string password)
        {

            return await context.Users.FirstOrDefaultAsync(x => x.Username == username && x.Password == password);

        }
    }
}
