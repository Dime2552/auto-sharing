using DAL.EF;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using Xunit;

namespace DAL.Tests
{
    public class BaseRepositoryUnitTests
    {
        [Fact]
        public void Create_InputUserInstance_CalledAddMethodOfDBSetWithUserInstance()
        {
            // Arrange
            DbContextOptions opt = new DbContextOptionsBuilder<AutoSharingContext>()
                .Options;
            var mockContext = new Mock<AutoSharingContext>(opt);
            var mockDbSet = new Mock<DbSet<User>>();
            mockContext
                .Setup(context =>
                    context.Set<User>(
                    ))
                .Returns(mockDbSet.Object);
            var repository = new TestUserRepository(mockContext.Object);

            var expectedUser = new Mock<User>().Object;

            //Act
            repository.Create(expectedUser);

            // Assert
            mockDbSet.Verify(
                dbSet => dbSet.Add(
                    expectedUser
                ), Times.Once);
        }

        [Fact]
        public void Get_InputId_CalledFindMethodOfDBSetWithCorrectId()
        {
            // Arrange
            DbContextOptions opt = new DbContextOptionsBuilder<AutoSharingContext>()
                .Options;
            var mockContext = new Mock<AutoSharingContext>(opt);
            var mockDbSet = new Mock<DbSet<User>>();
            mockContext
                .Setup(context =>
                    context.Set<User>(
                    ))
                .Returns(mockDbSet.Object);

            // Random id.
            var expectedUser = new User { Id = 1, LicenceNumber ="smth", Name = "smth", Password = "smth" };
            mockDbSet.Setup(dbSet => dbSet.Find(expectedUser.Id))
                .Returns(expectedUser);

            var repository = new TestUserRepository(mockContext.Object);

            // Act
            var actualUser = repository.GetById(expectedUser.Id);

            // Assert
            mockDbSet.Verify(
                dbSet => dbSet.Find(
                    expectedUser.Id
                ), Times.Once());
            Assert.Equal(expectedUser, actualUser);
        }

        [Fact]
        public void Delete_InputId_CalledFindAndRemoveMethodsOfDBSetWithCorrectArg()
        {
            // Arrange
            DbContextOptions opt = new DbContextOptionsBuilder<AutoSharingContext>()
                .Options;
            var mockContext = new Mock<AutoSharingContext>(opt);
            var mockDbSet = new Mock<DbSet<User>>();
            mockContext
                .Setup(context =>
                    context.Set<User>(
                    ))
                .Returns(mockDbSet.Object);

            // Random id.
            var expectedUser = new User { Id = 1, LicenceNumber = "smth", Name = "smth", Password = "smth" };
            mockDbSet.Setup(mock => mock.Find(expectedUser.Id))
                .Returns(expectedUser);

            var repository = new TestUserRepository(mockContext.Object);

            // Act
            repository.Delete(expectedUser.Id);

            // Assert
            mockDbSet.Verify(
                dbSet => dbSet.Remove(
                    expectedUser
                ), Times.Once);
        }
    }
}
