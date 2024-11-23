using DAL.Entities;
using DAL.Repositories.Impl;
using DAL.Repositories.Interfaces;
using DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EF
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private AutoSharingContext db;
        private UserRepository userRepository;
        private CarRepository carRepository;
        private ReservationRepository reservationRepository;

        public EFUnitOfWork (DbContextOptions options)
        {
            db = new AutoSharingContext (options);
        }

        public IRepository<User> Users
        {
            get
            {
                if (userRepository == null) userRepository = new UserRepository (db);
                return userRepository;
            }
        }
        public IRepository<Car> Cars
        {
            get
            {
                if (carRepository == null)
                    carRepository = new CarRepository(db);
                return carRepository;
            }
        }
        public IRepository<Reservation> Reservations
        {
            get
            {
                if (reservationRepository == null)
                    reservationRepository = new ReservationRepository(db);
                return reservationRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
