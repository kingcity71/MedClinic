using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedClinic.Models
{
    public class DoctorEditModel
    {
        public Guid Id { get; set; }
        [Required]
        public string FullName { get; set; }
        public string Email { get; set; }
        [Required]
        public Guid? SpecializationId { get; set; }
        public IEnumerable<SelectListItem> Specializations { get; set; }

        [Required]
        public DateTime HireDate { get; set; }
        [Required]
        public string Education { get; set; }
        [Required]
        public string PlaceDefault { get; set; }
        public string Photo { get; set; }
        public IFormFile PhotoFile { get; set; }
    }
}
