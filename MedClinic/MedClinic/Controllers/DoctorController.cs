using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MedClinic.Interfaces;
using MedClinic.Model;
using MedClinic.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MedClinic.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IDoctorService doctorService;
        private readonly ICommonSerivce commonSerivce;
        private readonly IScheduleService scheduleService;
        public DoctorController(IDoctorService doctorService, ICommonSerivce commonSerivce, IScheduleService scheduleService)
        {
            this.doctorService = doctorService;
            this.commonSerivce = commonSerivce;
            this.scheduleService = scheduleService;
        }


        [HttpGet("Doctor/schedule/")]
        public IActionResult Schedule()
        {
            var viewModel = new ScheduleViewModel()
            {
                Date = DateTime.Today,
                CalendarMatrix = commonSerivce.GetCalendarMatrix(DateTime.Today)
            };
            return View(viewModel);
        }
        [HttpGet("Doctor/schedule/{year}/{month}")]
        public IActionResult Schedule(int year, int month)
        {
            var viewModel = new ScheduleViewModel()
            {
                Date =new DateTime(year, month,1),
                CalendarMatrix = commonSerivce.GetCalendarMatrix(DateTime.Today)
            };
            return View(viewModel);
        }
        [HttpGet]
        public IActionResult ScheduleDate(int year, int month, int day)
        {
            var doctor = doctorService.GetDoctor(User.Identity.Name);
            var date = new DateTime(year, month, day);
            var daySched = scheduleService.GetDoctorDaySchedule(date, doctor.Id);
            var model = new ScheduleDayViewModel()
            {
                ScheduleDay = daySched,
                Date = date
            };
            return View(model);
        }
        
        [HttpGet]
        public IActionResult OpenClosedSchedules(int year, int month, int day)
        {
            var doctor = doctorService.GetDoctor(User.Identity.Name);
            doctorService.OpenClosedSchedules(new DateTime(year, month, day), doctor.Id);
            return RedirectToAction("ScheduleDate", new {year=year, month = month, day=day });
        }
        [HttpGet]
        public IActionResult CloseOpenedSchedules(int year, int month, int day)
        {
            var doctor = doctorService.GetDoctor(User.Identity.Name);
            doctorService.CloseOpenedSchedules(new DateTime(year, month, day), doctor.Id);
            return RedirectToAction("ScheduleDate", new { year = year, month = month, day = day });
        }

        [HttpGet("doctor/")]
        public IActionResult Home()
        {
            var doctorEmail = User.Identity.Name;
            var doctor = doctorService.GetDoctor(doctorEmail);
            return View(doctor);
        }

        [HttpGet("doctor/{id}")]
        public IActionResult Home(Guid id)
        {
            var doctor = doctorService.GetDoctor(id);
            return View(doctor);
        }
        [HttpGet]
        public IActionResult Edit()
        {
            var doctorEmail = User.Identity.Name;
            var doctor = doctorService.GetDoctor(doctorEmail);
            var doctorEditModel = new DoctorEditModel()
            {
                Id = doctor.Id,
                FullName = doctor.FullName,
                HireDate = doctor.HireDate,
                Email = doctor.Email,
                Education = doctor.Education,
                Photo = doctor.Photo,
                SpecializationId = doctor.SpecializationId,
                Specializations = doctorService.GetSpecializations()
                .Select(x => new SelectListItem()
                {
                    Text = x.Key,
                    Value = x.Value.ToString(),
                    Selected = x.Value.ToString() == doctor.SpecializationId.ToString()
                })
            };

            return View(doctorEditModel);
        }

        [HttpPost]
        public IActionResult Edit(DoctorEditModel editModel)
        {
            if (ModelState.IsValid)
            {
                var doctorModel = MapPatientModel(editModel);
                doctorService.UpdateDoctor(doctorModel);
                return RedirectToAction("Home");
            }
            editModel.Specializations = doctorService.GetSpecializations()
                   .Select(x => new SelectListItem()
                   {
                       Text = x.Key,
                       Value = x.Value.ToString(),
                       Selected = x.Value.ToString() == editModel.SpecializationId.ToString()
                   });
            return View(editModel);
        }

        private DoctorModel MapPatientModel(DoctorEditModel editModel)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<DoctorEditModel, DoctorModel>());
            var mapper = new Mapper(config);
            var doctorModel = mapper.Map<DoctorModel>(editModel);

            if (editModel.PhotoFile != null)
            {
                byte[] imageData = null;
                // считываем переданный файл в массив байтов
                using (var binaryReader = new BinaryReader(editModel.PhotoFile.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)editModel.PhotoFile.Length);
                }
                doctorModel.Photo = $"data:image/jpeg;base64, {Convert.ToBase64String(imageData)}";
            }
            return doctorModel;
        }
    }
}