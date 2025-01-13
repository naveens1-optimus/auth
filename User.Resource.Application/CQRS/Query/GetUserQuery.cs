using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Resource.Application.DTOs;
using User.Resource.Domain.Entities;

namespace User.Resource.Application.CQRS.Query
{    
    public class GetUserQuery:IRequest<ReponseDTO>
    {
        public GetUserDataDTO uname { get; set; }
    }

}