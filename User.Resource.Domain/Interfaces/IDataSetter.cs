using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Resource.Domain.Entities;

namespace User.Resource.Domain.Interfaces
{
    public interface IDataSetter
    {
        public Task<UserData> set(UserData nUserDAta);
    }
}
