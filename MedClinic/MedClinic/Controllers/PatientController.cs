using System;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using MedClinic.Interfaces;
using MedClinic.Model;
using MedClinic.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedClinic.Controllers
{
    public class PatientController : Controller
    {
        private readonly IPatientService patienService;
        private readonly ICommonSerivce commonSerivce;

        public PatientController(IPatientService patienService, ICommonSerivce commonSerivce)
        {
            this.patienService = patienService;
            this.commonSerivce = commonSerivce;
        }
        
        [HttpGet("/patient/schedule/")]
        public IActionResult Schedule()
        {
            var viewModel = new ScheduleViewModel()
            {
                Date = DateTime.Today,
                CalendarMatrix = commonSerivce.GetCalendarMatrix(DateTime.Today)
            };
            return View(viewModel);
        }

        [HttpGet("/patient/schedule/{month}/{year}")]
        public IActionResult Schedule(int month, int year)
        {
            var viewModel = new ScheduleViewModel()
            {
                Date = new DateTime(year, month, 1),
                CalendarMatrix = commonSerivce.GetCalendarMatrix(new DateTime(year, month, 1))
            };
            return View(viewModel);
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
            var patientEditModel = new PatientEditModel()
            {
                Id = patient.Id,
                Photo = patient.Photo,
                Email = patient.Email,
                FullName = patient.FullName,
                MedData = patient.MedData,
                PassData = patient.PassData,
                Sex = patient.IsMan==true
            };
            return View(patientEditModel);
        }
       
        [HttpPost]
        public IActionResult Edit(PatientEditModel patientEditModel)
        {
            return View(patientEditModel);
            if (ModelState.IsValid)
            {
                var patientModel = MapPatientModel(patientEditModel);
                patienService.UpdatePatient(patientModel);
                return RedirectToAction("Home", "Patient");
            }
            else
                return View(patientEditModel);
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
        private PatientModel MapPatientModel(PatientEditModel editModel)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<PatientEditModel, PatientModel>());
            var mapper = new Mapper(config);
            var patientModel = mapper.Map<PatientModel>(editModel);
            
            if (editModel.PhotoFile != null)
            {
                byte[] imageData = null;
                // считываем переданный файл в массив байтов
                using (var binaryReader = new BinaryReader(editModel.PhotoFile.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)editModel.PhotoFile.Length);
                }
                patientModel.Photo = $"data:image/jpeg;base64, {Convert.ToBase64String(imageData)}";
            }

            return patientModel;
        }
    
    
    
    }
}