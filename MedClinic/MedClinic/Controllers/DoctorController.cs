﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MedClinic.Interfaces;
using MedClinic.Model;
using MedClinic.Model.Conclusion;
using MedClinic.Models;
using MedClinic.Models.Doctor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MedClinic.Controllers
{
    public class DoctorController : Controller
    {
        private readonly ICommonSerivce commonSerivce;
        private readonly IDoctorService doctorService;
        private readonly IPatientService patientService;
        private readonly IConclusionService conclusionService;
        private readonly IScheduleService scheduleService;
        private readonly IPatientDataService patientDataService;
        public DoctorController(IDoctorService doctorService,
            ICommonSerivce commonSerivce, IScheduleService scheduleService,
            IPatientService patientService,
            IConclusionService conclusionService,
            IPatientDataService patientDataService)
        {
            this.doctorService = doctorService;
            this.commonSerivce = commonSerivce;
            this.scheduleService = scheduleService;
            this.patientService = patientService;
            this.conclusionService = conclusionService;
            this.patientDataService = patientDataService;
        }

        [HttpGet]
        public IActionResult Appointment(Guid schedId, int year = 0, int month = 0, int day = 0, int hour = 0)
        {
            var states = new[] { "Открыт", "Запись", "Закрыт", "Удалить" };
            var hours = new[] { "10", "11", "12", "13", "14", "15", "16", "17" };
            var doctor = doctorService.GetDoctor(User.Identity.Name);
            if (schedId != Guid.Empty)
            {
                var sched = scheduleService.GetSchedule(schedId);
                var view = sched != null ?
                    new AppointmentViewModel()
                    {
                        ScheduleId = sched.Id,
                        DateTime = sched.Date,
                        Patient = sched.PatientId != Guid.Empty ?
                        patientService.GetPatient(sched.PatientId)
                        : new PatientModel(),
                        State = sched.Status,
                        Place = sched.Place,
                        States = states.Select(x => new SelectListItem()
                        {
                            Selected = x == sched.Status,
                            Text = x,
                            Value = x
                        }),
                        Hour = sched.Date.Hour.ToString(),
                        Hours = hours.Select(x => new SelectListItem()
                        {
                            Selected = x == sched.Date.Hour.ToString(),
                            Text = x,
                            Value = x
                        })
                    }
                    : new AppointmentViewModel()
                    {
                        ScheduleId = Guid.NewGuid(),
                        DateTime = DateTime.Today,
                        Patient = new PatientModel(),
                        State = "Открыто",
                        Place = doctor.PlaceDefault,
                        States = states.Select(x => new SelectListItem()
                        {
                            Selected = x == "Открыто",
                            Text = x,
                            Value = x
                        }),
                        Hour = "10",
                        Hours = hours.Select(x => new SelectListItem()
                        {
                            Selected = x == "10",
                            Text = x,
                            Value = x
                        })
                    };
                return View(view);
            }
            if (year != 0 && month != 0 && day != 0 && hour != 0)
            {
                var view = new AppointmentViewModel()
                {
                    ScheduleId = Guid.NewGuid(),
                    DateTime = new DateTime(year, month, day, hour, 0, 0),
                    Patient = new PatientModel(),
                    State = "Открыто",
                    Place = doctor.PlaceDefault,
                    States = states.Select(x => new SelectListItem()
                    {
                        Selected = x == "Открыто",
                        Text = x,
                        Value = x
                    }),
                    Hour = hour.ToString(),
                    Hours = hours.Select(x => new SelectListItem()
                    {
                        Selected = x == hour.ToString(),
                        Text = x,
                        Value = x
                    })
                };
                return View(view);
            }
            return View();
        }

        [HttpPost]
        public IActionResult Appointment(AppointmentViewModel model)
        {
            var states = new[] { "Открыт", "Запись", "Закрыт", "Удалить" };
            var hours = new[] { "10", "11", "12", "13", "14", "15", "16", "17" };

            if (!ModelState.IsValid)
            {
                model.States = states.Select(x => new SelectListItem()
                {
                    Selected = x == "Открыто",
                    Text = x,
                    Value = x
                });
                model.Hours = hours.Select(x => new SelectListItem()
                {
                    Selected = model.Hour != null ?
                    x == model.Hour.ToString()
                    : false,
                    Text = x,
                    Value = x
                });
                return View(model);
            }

            var dateTime = new DateTime(model.DateTime.Year, model.DateTime.Month, model.DateTime.Day, int.Parse(model.Hour), 0, 0);

            if (model.State == "Удалить")
            {
                scheduleService.RemoveAppointment(model.ScheduleId);
                return RedirectToAction("ScheduleDate", new { model.DateTime.Year, model.DateTime.Month, model.DateTime.Day });
            }

            if (model.ScheduleId == Guid.Empty && scheduleService.IsAppointmentExist(dateTime, doctorService.GetDoctor(User.Identity.Name).Id))
            {
                ModelState.AddModelError("DateTime", "Запись на это время уже существует.");
                model.States = states.Select(x => new SelectListItem()
                {
                    Selected = x == "Открыто",
                    Text = x,
                    Value = x
                });
                model.Hours = hours.Select(x => new SelectListItem()
                {
                    Selected = x == model.Hour.ToString(),
                    Text = x,
                    Value = x
                });

                return View(model);
            }

            var doctor = doctorService.GetDoctor(User.Identity.Name);

            scheduleService.UpdateAppointment(new ScheduleTimeModel()
            {
                ScheduleId = model.ScheduleId,
                Status = model.State,
                DateTime = dateTime,
                Place = model.Place,
                Doctor = doctor
            });

            if (model.State == "Закрыт")
                return RedirectToAction("AppointmentResult",
                    new { schedId = model.ScheduleId });

            return RedirectToAction("ScheduleDate", new { model.DateTime.Year, model.DateTime.Month, model.DateTime.Day });
        }

        [HttpGet]
        public IActionResult CloseOpenedSchedules(int year, int month, int day)
        {
            var doctor = doctorService.GetDoctor(User.Identity.Name);
            doctorService.CloseOpenedSchedules(new DateTime(year, month, day), doctor.Id);
            return RedirectToAction("ScheduleDate", new { year = year, month = month, day = day });
        }

        [HttpGet("doctor/edit/{id}")]
        public IActionResult Edit(Guid id)
        {
            var doctor = doctorService.GetDoctor(id);
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
                }),
                PlaceDefault = doctor.PlaceDefault
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
                if (User.Identity.Name == "Admin")
                    return Redirect($"/doctor/{doctorModel.Id}");
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
        public IActionResult MySchedules(int currentPage, string status)
        {
            var doctor = doctorService.GetDoctor(User.Identity.Name);
            var states = new[] { "Открыт", "Запись", "Закрыт" };
            var viewModel = new MySchedulesViewModel()
            {
                CurrentPage = currentPage,
                Count = doctorService.GetSchedulesCount(doctor.Id, status),
                Doctor = doctor,
                Schedules = doctorService.GetSchedules(doctor.Id, status, 6, (currentPage - 1) * 6),
                Status = status ?? string.Empty,
                States = states.Select(x => new SelectListItem()
                {
                    Text = x,
                    Value = x,
                    Selected = status != null ?
                    status == x
                    : false
                })
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult OpenClosedSchedules(int year, int month, int day)
        {
            var doctor = doctorService.GetDoctor(User.Identity.Name);
            doctorService.OpenClosedSchedules(new DateTime(year, month, day), doctor.Id);
            return RedirectToAction("ScheduleDate", new { year = year, month = month, day = day });
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
                Date = new DateTime(year, month, 1),
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
            ViewBag.IsPlaceDefaultExist = !string.IsNullOrEmpty(doctor.PlaceDefault);
            return View(model);
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

        [HttpGet]
        public IActionResult AppointmentResult(Guid schedId)
        {
            var viewModel = GetAppointmentResultViewModel(schedId);
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Conclusion(Guid conclusionId, Guid patientId, Guid schedId)
        {
            var conclusionModel = conclusionService.GetConclusion(conclusionId);
            var viewModel = new ConclusionViewModel()
            {
                Date = conclusionModel.Date,
                ConclusionId = conclusionModel.Id,
                Patient = conclusionModel.Patient ?? patientService.GetPatient(patientId),
                Result = conclusionModel.Result,
                SchedId = conclusionModel.ScheduleId != Guid.Empty ?
                conclusionModel.ScheduleId :
                schedId
            };
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Conclusion(ConclusionViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var model = new ConclusionModel()
                {
                    Date = viewModel.Date,
                    Doctor = doctorService.GetDoctor(User.Identity.Name),
                    Id = viewModel.ConclusionId,
                    Patient = viewModel.Patient,
                    Result = viewModel.Result,
                    ScheduleId = viewModel.SchedId
                };
                if (viewModel.ConclusionId == Guid.Empty)
                    conclusionService.Create(model);
                else
                    conclusionService.Update(model);
                return RedirectToAction("AppointmentResult", new { schedId = viewModel.SchedId });
            }
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult PatientData(Guid id, Guid patientId, Guid schedId)
        {
            var patientData = patientDataService.GetById(id);
            if (patientData.ScheduleId == Guid.Empty) patientData.ScheduleId = schedId;
            if (patientData.PatientId == Guid.Empty) patientData.PatientId = patientId;
            var props = patientDataService.GetProps();
            var viewModel = new DoctorDateViewModel()
            {
                Patient = patientData.PatientId != Guid.Empty ?
                    patientService.GetPatient(patientData.PatientId)
                    : patientService.GetPatient(patientId),
                DoctorDatas = patientData,
                Properties = props.Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                    Selected = x.Name == patientData.PropName
                }).ToList()
            };
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult PatientData(DoctorDateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (viewModel.DoctorDatas.Id == Guid.Empty)
                    patientDataService.Create(viewModel.DoctorDatas);
                else
                    patientDataService.Update(viewModel.DoctorDatas);
                return RedirectToAction("AppointmentResult", new { schedId = viewModel.DoctorDatas.ScheduleId});
            }
            return View(viewModel);
        }
        private AppointmentResultViewModel GetAppointmentResultViewModel(Guid schedId)
        {
            var sched = scheduleService.GetSchedule(schedId);
            var patient = patientService.GetPatient(sched.PatientId);
            return new AppointmentResultViewModel()
            {
                SchedId = schedId,
                Patient = patient,
                AppointmentDate = sched.Date,
                ConclusionId = conclusionService.GetConclusionBySched(schedId).Id,
                PatientDatas = patientDataService.GetBySchedId(schedId)                
            };
        }
    }
}