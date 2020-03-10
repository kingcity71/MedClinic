using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedClinic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MedClinic.Controllers
{
    public class PatientController : Controller
    {
        private readonly IPatientService patienService;

        public PatientController(IPatientService patienService)
        {
            this.patienService = patienService;
        }
        [HttpGet("patient/{id}")]
        public IActionResult Home(Guid id)
        {
            var patient = patienService.GetPatient(id);
            return View(patient);
        }
    }
}