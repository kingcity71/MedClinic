using System;
using System.Collections.Generic;
using System.Text;

namespace MedClinic.Model
{
    public abstract class UserModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
