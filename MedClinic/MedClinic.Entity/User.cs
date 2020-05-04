using Microsoft.AspNetCore.Identity;
using System;

namespace MedClinic.Entity
{
    public class User : IdentityUser
    {
        public Guid SystemId { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
