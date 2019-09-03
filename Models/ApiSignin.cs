using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItersTutoriov1.Models
{
    public class PasswordReset
    {
        public string Email { get; set; }
    }

    public class Signin
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class User
    {
        public Guid UniqueId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}
