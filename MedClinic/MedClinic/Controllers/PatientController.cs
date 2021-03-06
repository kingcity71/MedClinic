﻿using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MedClinic.Interfaces;
using MedClinic.Model;
using MedClinic.Models;
using MedClinic.Models.Patient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedClinic.Controllers
{
    public class PatientController : Controller
    {
        private readonly IPatientService patienService;
        private readonly ICommonSerivce commonSerivce;
        private readonly IDoctorService doctorService;
        private readonly IScheduleService scheduleService;

        public PatientController(IPatientService patienService,
            ICommonSerivce commonSerivce,
            IDoctorService doctorService,
            IScheduleService scheduleService)
        {
            this.patienService = patienService;
            this.commonSerivce = commonSerivce;
            this.doctorService = doctorService;
            this.scheduleService = scheduleService;
        }

        [HttpGet("patient/schedule/")]
        public IActionResult Schedule()
        {
            var specializations = doctorService.GetSpecializations();
            var viewModel = new ScheduleViewModel()
            {
                Date = DateTime.Today,
                CalendarMatrix = commonSerivce.GetCalendarMatrix(DateTime.Today),
                Specializations = specializations,
                SpecializationId = specializations.First().Value
            };
            return View(viewModel);
        }

        [HttpGet("patient/schedule/{month}/{year}/{specializationId}")]
        public IActionResult Schedule(int month, int year, Guid specializationId)
        {
            var viewModel = new ScheduleViewModel()
            {
                Date = new DateTime(year, month, 1),
                CalendarMatrix = commonSerivce.GetCalendarMatrix(new DateTime(year, month, 1)),
                Specializations = doctorService.GetSpecializations(),
                SpecializationId = specializationId
            };
            return View(viewModel);
        }

        [HttpGet("patient/ScheduleDate/{year}/{month}/{day}/{specializationId}")]
        public IActionResult ScheduleDate(int year, int month, int day, Guid specializationId)
        {
            var date = new DateTime(year, month, day);
            var daySched = scheduleService.GetDaySchedule(date, specializationId);
            var model = new ScheduleDayViewModel()
            {
                ScheduleDay = daySched,
                Date = date,
                SpecialiazationId = specializationId,
                Specialiazation = commonSerivce.GetSpecialization(specializationId).Name
            };
            return View(model);
        }

        [HttpGet("patient/ScheduleTime/{year}/{month}/{day}/{hour}/{specializationId}")]
        public IActionResult ScheduleTime(
            int year,
            int month,
            int day,
            int hour,
            Guid specializationId)
        {
            var dateTime = new DateTime(year, month, day, hour, 0, 0);
            var schedModels = scheduleService.GetTimeSchedule(dateTime, specializationId);
            return View(schedModels);
        }


        [HttpGet]
        public IActionResult Appointment(Guid id, string doctor, string spec, 
            int year, int month, int day, int hour,
            string place)
        {
            var viewModel = new AppointmentViewModel()
            {
                Specialization=spec,
                Doctor = doctor,
                DateTime = new DateTime(year,month,day,hour,0,0),
                Place = place,    
                ScheduleId = id,
                PatientId = patienService.GetPatient(User.Identity.Name).Id
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Appointment(AppointmentViewModel viewModel)
        {
            var schedule = scheduleService.GetSchedule(viewModel.ScheduleId);
            if (scheduleService.IsTimeToMakeAppointment(viewModel.PatientId, schedule.Date))
            {
                scheduleService.MakeAppointment(viewModel.ScheduleId, viewModel.PatientId);
                return RedirectToAction("MySchedules");
            }
            else
            {
                ModelState.AddModelError("ScheduleId","У вас есть другая запись на это время или время записи уже неактуально");
                return View(viewModel);
            }
        }
        
        [HttpGet]
        public IActionResult CancelAppointment(Guid schedId)
        {
            scheduleService.CancelAppointment(schedId);
            return RedirectToAction("MySchedules");
        }

        [HttpGet("patient/MySchedules/")]
        public IActionResult MySchedules()
        {
            var patient = patienService.GetPatient(User.Identity.Name);
            var mySchedules = patienService.GetMySchedules(patient.Id);
            var viewModel = new MySchedulesViewModel() { Patient = patient, Schedules = mySchedules };
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
        [HttpGet("patient/edit/{id}")]
        public IActionResult Edit(Guid id)
        {
            var patient = patienService.GetPatient(id);
            var patientEditModel = new PatientEditModel()
            {
                Id = patient.Id,
                Photo = patient.Photo,
                Email = patient.Email,
                FullName = patient.FullName,
                MedData = patient.MedData,
                PassData = patient.PassData,
                Sex = patient.IsMan == true
            };
            return View(patientEditModel);
        }

        [HttpPost]
        public IActionResult Edit(PatientEditModel patientEditModel)
        {
            if (ModelState.IsValid)
            {
                var patientModel = MapPatientModel(patientEditModel);
                patienService.UpdatePatient(patientModel);
                if(User.Identity.Name=="Admin")
                    return Redirect($"/patient/{patientModel.Id}");
                return RedirectToAction("Home", "Patient");
            }
            else
                return View(patientEditModel);
        }

        [HttpGet("patientData/{patientId}")]
        public IActionResult PatientData(Guid patientId)
        {
            var patient = patienService.GetPatient(patientId);
            //if (patient.Email != User.Identity.Name) return Redirect($"/patient/{patientId}");

            var doctor = doctorService.GetDoctor(User.Identity.Name);
            

            if(patient.Email == User.Identity.Name ||
                (doctor != null && doctorService.IsTherapist(doctor.Id)))
            {
                var patientData = patienService.GetPatientData(patientId);
                var patientDataViewModel = new PatientDataViewModel()
                {
                    Patient = patient,
                    PatientDatas = patientData
                };
                return View(patientDataViewModel);
            }
            if (doctor != null)
            {
                var patientData = patienService.GetPatientData(patientId, doctor.SpecializationId);
                var patientDataViewModel = new PatientDataViewModel()
                {
                    Patient = patient,
                    PatientDatas = patientData
                };
                return View(patientDataViewModel);
            }
            return Redirect($"/patient/{patientId}");
        }

        [HttpGet("patientConclusions/{patientId}")]
        public IActionResult Conslusions(Guid patientId)
        {
            var patient = patienService.GetPatient(patientId);
            //if (patient.Email != User.Identity.Name) return Redirect($"/patient/{patientId}");

            var doctor = doctorService.GetDoctor(User.Identity.Name);

            if (patient.Email == User.Identity.Name ||
                (doctor != null && doctorService.IsTherapist(doctor.Id)))
            {
                var conclusions = patienService.GetConclusions(patientId);
                var patientConclusionsViewModel = new PatientConclusionsViewModel() { Patient = patient, Conslusions = conclusions };
                return View(patientConclusionsViewModel);
            }
            
            if (doctor != null)
            {
                var conclusions = patienService.GetConclusions(patientId, doctor.SpecializationId);
                var patientConclusionsViewModel = new PatientConclusionsViewModel() { Patient = patient, Conslusions = conclusions };
                return View(patientConclusionsViewModel);
            }
            return Redirect($"/patient/{patientId}");
        }
        private PatientModel MapPatientModel(PatientEditModel editModel)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<PatientEditModel, PatientModel>());
            var mapper = new Mapper(config);
            var patientModel = mapper.Map<PatientModel>(editModel);
            patientModel.IsMan = editModel.Sex == true;
            patientModel.IsWoman = editModel.Sex == false;
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