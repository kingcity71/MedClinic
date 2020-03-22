using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedClinic.Models
{
    public class PatientEditModel
    {
        public Guid Id { get; set; }
       
        public string Email { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public bool? Sex { get; set; }

        //public bool IsMan { get; set; }
        
            //public bool IsWoman { get; set; }
        [Required]
        public string MedData { get; set; }
        [Required]
        public string PassData { get; set; }
        public string Photo { get; set; }
        public IFormFile PhotoFile { get; set; }
    }
}
