using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedClinic.Interfaces;
using MedClinic.Models.Admin;
using Microsoft.AspNetCore.Mvc;

namespace MedClinic.Controllers
{
    public class AdminController : Controller
    {
        private readonly IDoctorService doctorService;
        private readonly IPatientService patientService;

        public AdminController(IDoctorService doctorService, IPatientService patientService)
        {
            this.doctorService = doctorService;
            this.patientService = patientService;
        }
        [HttpGet]
        public IActionResult Home()
        {
            var viewModel = new HomePageModel()
            { 
                Key=string.Empty,
                Doctors = new Dictionary<Guid, string>(),
                Patients = new Dictionary<Guid, string>()
            };
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Home(string key)
        {
            var viewMOdel = new HomePageModel()
            {
                Doctors = doctorService.FindDoctor(key),
                Patients = patientService.FindPatients(key),
                Key = key
            };
            return View(viewMOdel);
        }
    }
}