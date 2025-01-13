using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Authentication.Application.DTO;
using User.Authentication.Application.Interfaces;
using User.Authentication.Domain.Entities;


namespace User.Authentication.Application.Command.Handler
{   
    public class LoginQueryHandler : IRequestHandler<LoginQuery, TokenResponseDTO>
    {
        IGetUserRepository repo;
        IIssueTokenInfra tokenGenerator;
        public LoginQueryHandler(IGetUserRepository repo,IIssueTokenInfra tGen)
        {
            this.repo = repo;
            this.tokenGenerator = tGen;
        }


        public async Task<TokenResponseDTO> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
             var foundUser=  await repo.GetUser(request.loginData.Username,request.loginData.Password);

            if (foundUser == null) {

                return null;

            }

            String token = await tokenGenerator.IssueToken(foundUser);
            // Returns a 200 OK response, encapsulating the JWT token in an anonymous object.
            return new TokenResponseDTO() { token = token };
        }

    }
    }

