using DAL.Entities;
using DAL.Repositories.Impl;
using Microsoft.EntityFrameworkCore;


namespace DAL.Tests
{
    internal class TestUserRepository (DbContext context)
        : BaseRepository<User>(context);
}
