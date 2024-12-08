using BLL.Services.Impl;
using CCL.Security.Identity;
using CCL.Security;
using DAL.UnitOfWork;
using Moq;
using BLL.Services.Interfaces;
using System.IO;
using DAL.Repositories.Interfaces;
using DAL.Entities;
using User = CCL.Security.Identity.User;

namespace BLL.Tests
{
    public class ReservationServiseTest
    {
        [Fact]
        public void Ctor_InputNull_ThrowArgumentNullException()
        {
            // Arrange
            IUnitOfWork nullUnitOfWork = null;

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(
                () => new ReservationService(nullUnitOfWork));
        }

        [Fact]
        public void GetReservations_UserIsClient_ThrowMethodAccessException()
        {
            // Arrange
            var user = new User(1, Role.Client);
            SecurityContext.SetUser(user);

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            IReservationService reservationService = new ReservationService(mockUnitOfWork.Object);

            // Act
            var actualGetReservationsFunc = () => reservationService.GetReservations(0);
            var exception = Record.Exception(actualGetReservationsFunc);

            // Assert
            Assert.IsNotType<MethodAccessException>(exception);
        }

        [Fact]
        public void GetReservations_ReservationFromDAL_CorrectMappingToReservationDTO()
        {
            // Arrange
            User user = new User(1, Role.Client);
            SecurityContext.SetUser(user);
            var reservationService = GetReservationService();

            // Act
            var actualReservationDTO = reservationService.GetReservations(0).First();

            // Assert
            Assert.True(
                actualReservationDTO.Id == 1
                && actualReservationDTO.CarId == 1
                && actualReservationDTO.Status == DAL.Enums.ReservationStatus.InProgress
                && actualReservationDTO.StartTime == DateTime.MinValue
                && actualReservationDTO.EndTime == DateTime.MaxValue
                && actualReservationDTO.UserId == 1
            );
        }
        IReservationService GetReservationService()
        {
            var mockContext = new Mock<IUnitOfWork>();
            var expectedReservation = new Reservation()
            {
                Id = 1,
                CarId = 1,
                Status = DAL.Enums.ReservationStatus.InProgress,
                StartTime = DateTime.MinValue,
                EndTime = DateTime.MaxValue,
                UserId = 1
            };
            var mockDbSet = new Mock<IReservationRepository>();
            mockDbSet.Setup(z => z.Find(It.IsAny<Func<Reservation, bool>>(), 
                It.IsAny<int>(), 
                It.IsAny<int>()))
                .Returns( new List<Reservation>() { expectedReservation } );
            mockContext.Setup(context => context.Reservations)
                .Returns(mockDbSet.Object);
            IReservationService reservationService = new ReservationService(mockContext.Object);
            return reservationService;
        }
    }
}