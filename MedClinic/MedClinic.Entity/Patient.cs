using System;

namespace MedClinic.Entity
{
    public class Patient
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public bool Sex { get; set; }
        public string PassData { get; set; }
        public string MedData { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        //public string Password { get; set; }
        public string Photo { get; set; }
    }
}
