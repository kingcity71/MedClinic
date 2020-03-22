using System;
using MedClinic.Interfaces;
using MedClinic.Model;
using MedClinic.Models;
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
        
        [HttpGet("patient/")]
        public IActionResult Home()
        {
            var patientEmail = User.Identity.Name;
            var patient = patienService.GetPatient(patientEmail);
            return View(patient);
        }

        [HttpGet("patient/{id}")]
        public IActionResult Home(Guid id)
        {
            var patient = patienService.GetPatient(id);
            return View(patient);
        }
        [HttpGet("patient/edit")]
        public IActionResult Edit()
        {
            var patientEmail = User.Identity.Name;
            var patient = patienService.GetPatient(patientEmail);
            return View(patient);
        }
        [HttpPost]
        public IActionResult Edit(PatientModel patient)
        {
            return View(patient);
        }


        [HttpGet("patientData/{patientId}")]
        public IActionResult PatientData(Guid patientId)
        {
            var patient = patienService.GetPatient(patientId);
            var patientData = patienService.GetPatientData(patientId);
            var patientDataViewModel = new PatientDataViewModel() { Patient = patient, PatientDatas = patientData };
            return View(patientDataViewModel);
        }

        [HttpGet("patientConclusions/{patientId}")]
        public IActionResult Conslusions(Guid patientId)
        {
            var patient = patienService.GetPatient(patientId);
            var conclusions = patienService.GetConclusions(patientId);
            var patientConclusionsViewModel = new PatientConclusionsViewModel() { Patient = patient, Conslusions = conclusions};
            return View(patientConclusionsViewModel);
        }
    }
}