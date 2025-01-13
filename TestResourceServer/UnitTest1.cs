using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using Moq;
using System.Configuration;
using System.Linq.Expressions;
using User.Resource.Application.CQRS.Command;
using User.Resource.Application.CQRS.Command.handler;
using User.Resource.Application.DTOs;
using User.Resource.Domain.Entities;
using User.Resource.Domain.Interfaces;
using User.Resource.Persistance.Data;
using User.Resource.Persistance.Repository;


namespace TestResourceServer
{
    [TestClass]
    public class UnitTest1
    {
        private List<UserData> UsersList = new List<UserData>()
            {
                new UserData(){Id=1,userName="naveen",phone="23432543242",Address="delhi" }
            };

        private readonly Mock<DbSet<UserData>> mockedUsers;
        private Mock<AppDbContext> mockedAppDbContext;
        private Mock<IDataSetter> mockedDataSetter;
       // private Mock<SetUserCommandHandler> mockedSetUserCommandHandler;    
        
        public UnitTest1()
        {  
            //mocking dbset using a list
            mockedUsers=new Mock<DbSet<UserData>>();    
            mockedUsers.As<IQueryable<UserData>>().Setup(m => m.Provider).Returns(UsersList.AsQueryable().Provider);
            mockedUsers.As<IQueryable<UserData>>().Setup(m => m.Expression).Returns(UsersList.AsQueryable().Expression);
            mockedUsers.As<IQueryable<UserData>>().Setup(m => m.ElementType).Returns(UsersList.AsQueryable().ElementType);
            mockedUsers.As<IQueryable<UserData>>().Setup(m => m.GetEnumerator()).Returns(UsersList.AsQueryable().GetEnumerator());


            //setting up mock dbcontext
           
            mockedAppDbContext = new Mock<AppDbContext>();
            mockedAppDbContext.Setup(x => x.usersData).Returns(mockedUsers.Object);
            
            //settuing up mockeddatasetter

            mockedDataSetter=new Mock<IDataSetter>();
            mockedDataSetter.Setup(x => x.set(It.IsAny<UserData>())).ReturnsAsync((UserData nUserData) =>
            {
                var newUserData = new UserData()
                {
                    Id = nUserData.Id,
                    userName = nUserData.userName,
                    phone = nUserData.phone,
                    Address = nUserData.Address
                };
                    
                UsersList.Add(newUserData);
                return newUserData;
            });

            //setting up commandhandler
            //mockedSetUserCommandHandler=new Mock<SetUserCommandHandler>(new SetUserCommandHandler(new DataSetter(context)));
            //mockedSetUserCommandHandler.Setup(x => x.Handle());
        }

        [TestMethod]
        public async Task TestMethod1()
        {
            UserData adduser = new UserData() {userName="vaibhav",Address="mysore",phone="544544", Id=2};

           

            var result= await mockedDataSetter.Object.set(adduser);  

            Assert.IsNotNull(result);
            Assert.AreEqual(adduser.Id, result.Id);
            Assert.AreEqual(adduser.userName,result.userName);

        }
    }
}