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
        public DoctorController(IDoctorService doctorService)
        {
            this.doctorService = doctorService;
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
                Id=doctor.Id,
                FullName = doctor.FullName,
                HireDate= doctor.HireDate,
                Email = doctor.Email,
                Education = doctor.Education,
                Photo = doctor.Photo,
                SpecializationId = doctor.SpecializationId,
                Specializations = doctorService.GetSpecializations()
                .Select(x=> new SelectListItem()
                {
                    Text=x.Key,
                    Value = x.Value.ToString(),
                    Selected = x.Value.ToString()==doctor.SpecializationId.ToString()
                })
            };

            return View(doctorEditModel);
        }

        [HttpPost]
        public IActionResult Edit (DoctorEditModel editModel)
        {
            editModel.Specializations = doctorService.GetSpecializations()
                .Select(x => new SelectListItem()
                {
                    Text = x.Key,
                    Value = x.Value.ToString(),
                    Selected = x.Value.ToString() == editModel.SpecializationId.ToString()
                });
            return View(editModel);
            if (ModelState.IsValid)
            {
                var doctorModel = MapPatientModel(editModel);
                doctorService.UpdateDoctor(doctorModel);
                return RedirectToAction("Home");
            }
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