using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Resource.Domain.Entities;
using User.Resource.Domain.Interfaces;
using User.Resource.Persistance.Data;

namespace User.Resource.Persistance.Repository
{
    public class DataSetter : IDataSetter
    {
        AppDbContext context;
        public DataSetter(AppDbContext cnxt)
        {
            context = cnxt;
        }
        public async Task<UserData> set(UserData nUserDAta)
        {
            //Console.WriteLine("hello"+nUserDAta.userName);

            var found =  await context.usersData.FirstOrDefaultAsync(x => x.userName == nUserDAta.userName);
            
            
            if (found!= null)
            {
                found.Address = nUserDAta.Address;
                found.phone = nUserDAta.phone;

               // Console.WriteLine("address is"+nUserDAta.phone);
                
                context.SaveChanges();
                return await context.usersData.FirstOrDefaultAsync(x => x.userName == nUserDAta.userName);

            }
            else
                return null;


        }
    }
}
