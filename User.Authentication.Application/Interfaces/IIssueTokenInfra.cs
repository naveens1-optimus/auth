using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Authentication.Domain.Entities;

namespace User.Authentication.Application.Interfaces
{
    public interface IIssueTokenInfra
    {
        public Task<string> IssueToken(AppUser user);
    }
}
