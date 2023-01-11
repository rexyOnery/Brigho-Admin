using System;
using System.Collections.Generic;

#nullable disable

namespace Vehicle.Models
{
    public partial class Login
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
        public string RecoveryCode { get; set; }
        public string Phone { get; set; }
    }
}
