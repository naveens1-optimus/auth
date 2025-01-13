using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Authentication.Application.DTO;

namespace User.Authentication.Application.Command
{
    public class RegisterCommand:IRequest<TokenResponseDTO>
    {
        public RegisterDTO data;
        public RegisterCommand(RegisterDTO newUserDAta)
        {
            data = new RegisterDTO()
            {
                Username = newUserDAta.Username,
                Password = newUserDAta.Password,
                Email = newUserDAta.Email,
                Role = newUserDAta.Role
            }; 
        }
    }
}
