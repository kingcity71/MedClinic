using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedClinic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MedClinic.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IDoctorService doctorService;
        public DoctorController(IDoctorService doctorService)
        {
            this.doctorService = doctorService;
        }

        [HttpGet("doctor/{id}")]
        public IActionResult Home(Guid id)
        {
            var doctor = doctorService.GetDoctor(id);
            return View(doctor);
        }
    }
}