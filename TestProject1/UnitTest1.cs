using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using User.Resource.Domain.Entities;
using User.Resource.Domain.Interfaces;
using User.Resource.Persistance.Data;

[TestClass]
public class UnitTest1
{
    private List<UserData> UsersList = new List<UserData>()
    {
        new UserData() { Id = 1, userName = "naveen", phone = "23432543242", Address = "delhi" }
    };

    private readonly Mock<DbSet<UserData>> mockedUsers;
    private Mock<AppDbContext> mockedAppDbContext;
    private Mock<IDataSetter> mockedDataSetter;

    public UnitTest1()
    {
        //mocking dbset using a list
        mockedUsers = new Mock<DbSet<UserData>>();
        mockedUsers.As<IQueryable<UserData>>().Setup(m => m.Provider).Returns(UsersList.AsQueryable().Provider);
        mockedUsers.As<IQueryable<UserData>>().Setup(m => m.Expression).Returns(UsersList.AsQueryable().Expression);
        mockedUsers.As<IQueryable<UserData>>().Setup(m => m.ElementType).Returns(UsersList.AsQueryable().ElementType);
        mockedUsers.As<IQueryable<UserData>>().Setup(m => m.GetEnumerator()).Returns(UsersList.AsQueryable().GetEnumerator());

        //setting up mock dbcontext
        mockedAppDbContext = new Mock<AppDbContext>();
        mockedAppDbContext.Setup(x => x.usersData).Returns(mockedUsers.Object);

        //setting up mocked data setter
        mockedDataSetter = new Mock<IDataSetter>();
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
    }

    [TestMethod]
    public async Task TestMethod1()
    {
        // Arrange
        UserData adduser = new UserData() { userName = "vaibhav", Address = "mysore", phone = "544544", Id = 2 };

        // Act
        var result = await mockedDataSetter.Object.set(adduser);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(adduser.Id, result.Id);
        Assert.AreEqual(adduser.userName, result.userName);
    }
}
