using AutoMapper;
using BLL.DTO;
using BLL.Services.Interfaces;
using CCL.Security;
using CCL.Security.Identity;
using DAL.Entities;
using DAL.UnitOfWork;

namespace BLL.Services.Impl
{
    public class ReservationService : IReservationService
    {
        private readonly IUnitOfWork _datebase;
        private int pageSize = 10;

        public ReservationService (IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null) throw new ArgumentNullException(nameof(unitOfWork));
            _datebase = unitOfWork;
        }
        /// <exception cref="MethodAccessException"></exception>
        
        public IEnumerable<ReservationDTO> GetReservations(int pageNumber)
        {
            var user = SecurityContext.GetUser();
            if (user is null || !user.Roles.Contains(Role.Client))
            {
                throw new MethodAccessException();
            }

            var userId = user.UserId;
            var reservationEntities = _datebase.Reservations.Find(o => o.UserId == userId, pageNumber, pageSize);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Reservation, ReservationDTO>()).CreateMapper();
            var reservationDTO = mapper.Map<IEnumerable<Reservation>, List<ReservationDTO>>(reservationEntities);

            return reservationDTO;
        }
    }
}
