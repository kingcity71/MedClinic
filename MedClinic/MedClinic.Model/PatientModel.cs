using System;

namespace MedClinic.Model
{
    public class PatientModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public bool IsMan { get; set; }
        public bool IsWoman { get; set; }
        public string PassData { get; set; }
        public string MedData { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Photo { get; set; }
        public ModelState ModelState { get; set; }
    }
}
