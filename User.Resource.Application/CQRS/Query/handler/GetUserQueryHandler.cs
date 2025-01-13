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

namespace User.Resource.Application.CQRS.Query.handler
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, ReponseDTO>
    {
        public IDataGetter getdata;
        public GetUserQueryHandler(IDataGetter gt)
        {
            getdata= gt;    
            
        }
        public async Task<ReponseDTO> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            //Console.WriteLine(request.uname.UserName);
            var dataFound =await getdata.get(request.uname.UserName);
            //var response = new ReponseDTO()
            //{
            //    userName=dataFound.userName,
            //    address=dataFound.address,  
            //    phone=dataFound.phone,  

            //};
            var mapper = UserMapperConfig.InitializeAutomapper();

            ReponseDTO res = mapper.Map<ReponseDTO>(dataFound);
            return res ;
        }
    }
}
