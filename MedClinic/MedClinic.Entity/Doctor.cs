using System;

namespace MedClinic.Entity
{
    public class Doctor
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public Guid SpecializationId { get; set; }
        public DateTime HireDate { get; set; }
        public string Education { get; set; }
        public string Photo { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
