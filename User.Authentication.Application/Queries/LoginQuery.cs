using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Authentication.Application.DTO;
using User.Authentication.Domain.Entities;

namespace User.Authentication.Application.Command
{
    public class LoginQuery : IRequest<TokenResponseDTO>
    {
        public LoginDTO loginData;
        public LoginQuery(LoginDTO rData)
        {
            loginData=rData;
        }
    }
}
