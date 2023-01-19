using EFCoreMock.Fail.Data;
using EFCoreMock.Fail.Models;
using EntityFrameworkCoreMock;
using Microsoft.EntityFrameworkCore;
using System;

namespace EFCoreMock.Fail
{
    public class DemonstrateEFCore7Error
    {
        private readonly DbContextMock<DatabaseContext> _dbContextMock;

        public DemonstrateEFCore7Error()
        {
            //Set up a dummy database with a user ready to authenticate
            var transplantDummyOptions = new DbContextOptionsBuilder<DatabaseContext>().Options;
            _dbContextMock = new DbContextMock<DatabaseContext>(transplantDummyOptions);

            var user = new User()
            {
                Id = 2,
                EmailAddress = "mark@example.com",
            };
            _dbContextMock.CreateDbSetMock(x => x.Users, new List<User>() { user });

            var role = new Role()
            {
                Id = 3,
                Name = "Developer"
            };
            _dbContextMock.CreateDbSetMock(x => x.Roles, new List<Role>() { role });

            var userRole = new UserRole()
            {
                Id = 4,
                UserId = user.Id,
                User = user,
                RoleId = role.Id,
                Role = role
            };
            _dbContextMock.CreateDbSetMock(x => x.UserRoles, new List<UserRole>() { userRole });
        }

        [Fact]
        public void WorkingTest1()
        {
            var dbContext = _dbContextMock.Object;
            var query1 =
                from u in dbContext.Users
                select u.EmailAddress;

            var result1 = query1.ToList();
            Assert.Single(result1);
            Assert.Equal("mark@example.com", result1[0]);

            var query2 =
                from u in dbContext.Roles
                select u.Name;

            var result2 = query2.ToList();
            Assert.Single(result2);
            Assert.Equal("Developer", result2[0]);
        }

        [Fact]
        public void FailingTest1()
        {
            var dbContext = _dbContextMock.Object;

            var userId = 2;

            var query =
                from u in dbContext.Users
                where u.Id == userId
                select u.IsMaster || (
                    from ur in u.UserRoles
                    where
                        ur.Role.Name == "Developer"
                    select
                        1
                ).Any();


            // The line below incorrectly fails, because dbContext.Users[0].UserRoles is an empty list but it should have the one UserRole we created in it.
            // Set a break point and look it up in the debugger if you want to see for sure.
            // I believe this operation works OK in EF Core 5 and 6 but not EF Core 7
            Assert.True(query.Single());
        }
    }
}