using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Authentication.Application.DTO;
using User.Authentication.Application.Interfaces;
using User.Authentication.Domain.Entities;
using User.Authentication.Persistance.Data;

namespace User.Authentication.Persistance.Repositories
{
    public class RegisterRepository : IRegisterRepository
    {
        ApplicationDBContext context;

        public RegisterRepository(ApplicationDBContext context1)
        {
            context = context1;
        }
        public async Task<AppUser> Register(RegisterDTO data)
        {
            AppUser newUser = new AppUser()
            {
                Username = data.Username,
                Password = data.Password,
                Email = data.Email,
                Role = "User"

            };

            context.Users.Add(newUser);
            await context.SaveChangesAsync();
            return newUser;

        }
    }
}
