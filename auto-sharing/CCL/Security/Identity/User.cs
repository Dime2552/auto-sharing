using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCL.Security.Identity
{
    public abstract class User
    {
        public User(int id, string name, string password, string userType)
        {
            Id = id;
            Name = name;
            Password = password;
            UserType = userType;
        }
        public int Id { get; }
        public string Name { get; }
        public string Password { get; }
        protected string UserType { get; }
    }
}
