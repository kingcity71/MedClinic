using System;

namespace MedClinic.Model
{
    public class DoctorModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public Guid SpecializationId { get; set; }
        public string Specialization { get; set; }
        public DateTime HireDate { get; set; }
        public string WorkExperience
        {
            get
            {
                return ((DateTime.Now - HireDate).Days / 365).ToString();
            }
        }
        public string Education { get; set; }
        public string Photo { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ModelState ModelState { get; set; }
    }
}
