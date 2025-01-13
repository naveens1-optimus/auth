using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Authentication.Domain.Entities
{
    public class AppUser
    {
        // This property represents the unique identifier for the user in the database.
        public int Id { get; set; }
        // This property is typically used as a unique identifier for login purposes.
        public string Username { get; set; }
        // This property stores the user's password, which should be securely hashed before storage.
        public string Password { get; set; }
        // This stores the user's email address, used for communication or password recovery.
        public string Email { get; set; }
        // This list holds the roles assigned to the user, used for authorization.
        // It is initialized as a new list of strings, meaning it starts empty but ready to be added to.
        public string Role { get; set; } 
    }
}

