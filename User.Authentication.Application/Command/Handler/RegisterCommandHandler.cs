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
    public class RegisterCommandHandler:IRequestHandler<RegisterCommand,TokenResponseDTO>
    {
        IRegisterRepository registerer;
        IIssueTokenInfra token_generator;
        IGetUserRepository user_finder;
        public RegisterCommandHandler(IRegisterRepository r,IIssueTokenInfra tokens,IGetUserRepository u)
        {
            registerer = r;
            token_generator = tokens;
            user_finder = u;
        }

        public async Task<TokenResponseDTO> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            await  registerer.Register(request.data);
            AppUser newUser=await user_finder.GetUser(request.data.Username, request.data.Password);
            if (newUser != null)
            {
                return new TokenResponseDTO() { token = await token_generator.IssueToken(newUser) };
            }
            else
                return null;

        }
    }
}
