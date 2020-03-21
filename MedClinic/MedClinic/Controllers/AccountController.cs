using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MedClinic.Model;
using MedClinic.Models;
using MedClinic.Data;
using Microsoft.EntityFrameworkCore;
using MedClinic.Entity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using MedClinic.Interfaces;

namespace MedClinic.Controllers
{
    public class AccountController : Controller
    {
        private readonly MedClinicContext db;
        private readonly IPatientService patientService;

        public AccountController(MedClinicContext context, IPatientService patientService)
        {
            this.db = context;
            this.patientService = patientService;
        }

        
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var patient = await db.Patients.FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
                if (patient != null)
                {
                    await Authenticate(model.Email); // аутентификация
                    return RedirectToAction("Home", "Patient");
                }
                var doctor = await db.Doctors.FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
                if (patient != null)
                {
                    await Authenticate(model.Email); // аутентификация
                    return RedirectToAction("Home", "Doctor");
                }

                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }
        
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await db.Patients.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (user == null)
                {
                    patientService.CreatePatient(new PatientModel()
                    {
                        FullName = model.FullName,
                        Email = model.Email,
                        BirthDate = model.BirthDate,
                        Password = model.Password
                    });
                   

                    await Authenticate(model.Email); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        private async Task Authenticate(string userEmail)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                 new Claim(ClaimsIdentity.DefaultNameClaimType, userEmail)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}