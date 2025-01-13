using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
    public class DataGetter : IDataGetter
    {  public AppDbContext context;
        public  DataGetter(AppDbContext cnxt)
        {
            context= cnxt;
        }

       

        //public async Task<UserData> get(string username)
        //{   
        //    UserData found=await context.usersData.FirstOrDefaultAsync(x => x.userName==username);
        //    Console.WriteLine("ereachere");
        //    Console.WriteLine(found.userName);
        //    return found;
        //}
        public async Task<UserData> get(string username)
        {

            return await context.usersData.FirstOrDefaultAsync(x => x.userName == username );

        }
    }
}
