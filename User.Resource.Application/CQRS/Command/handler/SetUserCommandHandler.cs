using AutoMapperDemo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Resource.Application.DTOs;
using User.Resource.Domain.Entities;
using User.Resource.Domain.Interfaces;

namespace User.Resource.Application.CQRS.Command.handler
{
    public class SetUserCommandHandler : IRequestHandler<SetUserCommand, ReponseDTO>
    {
         public IDataSetter setter;
        public SetUserCommandHandler(IDataSetter dS)
        {
            setter= dS;
        }

        
        public  async Task<ReponseDTO> Handle(SetUserCommand request, CancellationToken cancellationToken)
        {
            var mapper = UserMapperConfig.InitializeAutomapper();
            UserData userData = mapper.Map<SetUserDataDTO,UserData>(request.stData);
            
            var setData= await setter.set(userData);
            ReponseDTO res = mapper.Map<ReponseDTO>(setData);
           
            return res;
        }
    }
}
