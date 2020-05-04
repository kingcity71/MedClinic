using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MedClinic.Data;
using MedClinic.Entity;
using MedClinic.Interfaces;
using MedClinic.Model;
using MedClinic.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MedClinic.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IPatientService patientService;
        private readonly IDoctorService doctorService;
        private readonly MedClinicContext db;

        public AccountController(UserManager<User> userManager, 
            RoleManager<IdentityRole> roleManager, 
            SignInManager<User> signInManager,
             IPatientService patientService, IDoctorService doctorService,
             MedClinicContext db)
        {
            _userManager = userManager;
            this.roleManager = roleManager;
            _signInManager = signInManager;
            this.patientService = patientService;
            this.doctorService = doctorService;
            this.db = db;
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
                var result =
                    await _signInManager.PasswordSignInAsync(model.Email, model.Password,false,false);
                
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(model.Email);
                    var claimsFactory = _signInManager.ClaimsFactory;
                    
                    var patient = await db.Patients.FirstOrDefaultAsync(u => user.SystemId == u.Id);
                    if (patient != null)
                    {
                        var roleName = "patient";
                        var role = await roleManager.FindByNameAsync(roleName);
                        //await roleManager.AddClaimAsync(role, new Claim("RoleType", roleName));
                        return RedirectToAction("Home", "Patient");
                    }
                    var doctor = await db.Doctors.FirstOrDefaultAsync(u => user.SystemId==u.Id);
                    if (doctor != null)
                    {
                        var roleName = "doctor";
                        var role = await roleManager.FindByNameAsync(roleName);
                        await roleManager.AddClaimAsync(role, new Claim("RoleType", roleName));
                        return RedirectToAction("Home", "Doctor");
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("Email", "Неправильный логин и (или) пароль");
                }
                return View(model);
            }
            return View(model);
        }

        private async Task CreateRole(string name)
        {
            await roleManager.CreateAsync(new IdentityRole(name));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Email = model.Email,
                    UserName = model.Email,
                    BirthDate = model.BirthDate,
                    SystemId = Guid.NewGuid()
                };
                // добавляем пользователя
                var result = await _userManager.CreateAsync(user, model.Password);
                user = await _userManager.FindByNameAsync(user.UserName);
                if (result.Succeeded)
                {
                    if (model.IsDoctor)
                    {
                        var doctor = await db.Doctors.FirstOrDefaultAsync(u => u.Email == model.Email);
                        if (doctor == null)
                        {
                            var roleName = "doctor";
                            var role = await roleManager.FindByNameAsync(roleName);
                            if (role == null)
                            {
                                await CreateRole(roleName);
                                role = await roleManager.FindByNameAsync(roleName);
                            }
                            await _userManager.AddToRoleAsync(user, roleName);
                            //await roleManager.AddClaimAsync(role, new Claim("RoleType", roleName));
                            doctorService.CreateDoctor(new DoctorModel()
                            {
                                Id = user.SystemId,
                                FullName = model.FullName,
                                Email = model.Email,
                                BirthDate = model.BirthDate,
                                Password = model.Password
                            });
                            user = await _userManager.FindByNameAsync(user.UserName);
                            // установка куки
                            await _signInManager.SignInAsync(user, false);
                            return RedirectToAction("Home", "Doctor");
                        }
                    }
                    else
                    {
                        var patient = await db.Patients.FirstOrDefaultAsync(u => u.Email == model.Email);
                        if (patient == null)
                        {
                            var roleName = "patient";
                            var role = await roleManager.FindByNameAsync(roleName);
                            if (role == null)
                            {
                                await CreateRole(roleName);
                                role = await roleManager.FindByNameAsync(roleName);
                            }
                            await _userManager.AddToRoleAsync(user, roleName);
                            //await roleManager.AddClaimAsync(role, new Claim("RoleType", roleName));
                            patientService.CreatePatient(new PatientModel()
                            {
                                Id = user.SystemId,
                                FullName = model.FullName,
                                Email = model.Email,
                                BirthDate = model.BirthDate,
                                Password = model.Password
                            });
                            user = await _userManager.FindByNameAsync(user.UserName);
                            // установка куки
                            await _signInManager.SignInAsync(user, false);
                            return RedirectToAction("Home", "Patient");
                        }
                    }
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }
    }
}