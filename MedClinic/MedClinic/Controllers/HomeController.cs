using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MedClinic.Models;
using MedClinic.Interfaces;

namespace MedClinic.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPatientService patientService;
        private readonly IDoctorService doctorService;

        public HomeController(ILogger<HomeController> logger, IPatientService patientService, IDoctorService doctorService)
        {
            _logger = logger;
            this.patientService = patientService;
            this.doctorService = doctorService;
        }

        public IActionResult Index()
        {
            var currentUser = User.Identity.Name;
            if(patientService.GetPatient(currentUser)!=null) return RedirectToAction("Home", "Patient");
            if (doctorService.GetDoctor(currentUser) != null) return RedirectToAction("Home", "Doctor");
            return RedirectToAction("Login", "Account");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
