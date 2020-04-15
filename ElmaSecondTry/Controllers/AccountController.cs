using ElmaSecondTryBase.Enums;
using ElmaSecondTry.Models.Account;
using ElmaSecondTry.Models.User;
using ElmaSecondTryBase.Entities;
using ElmaSecondTryBase.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using ElmaSecondTry.WebEnums;
using AutoMapper;

namespace ElmaSecondTry.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        
        public AccountController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        private static readonly List<TempUser> tempUsers = new List<TempUser>
        { 
            new TempUser {Login="Serg", Password="111", Id=Guid.NewGuid(), RegisterDate=DateTime.Now, Name="Сергей", Role=WebUserRoles.Jobseeker, About="AboutSerg", Email="SergPodlec@mail.ru", Phone="+79091319724"},
            new TempUser {Login="Lena", Password="222", Id=Guid.NewGuid(), RegisterDate=DateTime.Now, Name="Лена", Role=WebUserRoles.Employee, About="AboutElena", Email="ElenaShevnina@mail.ru", Phone="+79091398584"},
            new TempUser {Login="Ksyu", Password="333", Id=Guid.NewGuid(), RegisterDate=DateTime.Now, Name="Ксения", Role=WebUserRoles.Admin, About="AboutKsyu", Email="Kseniya@mail.ru", Phone="+79090000000"},
        };

        public ActionResult Authorization()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authorization(Authorization authorization)
        {
            if(ModelState.IsValid)
            {
                var authUser = tempUsers.FirstOrDefault(x => x.Login == authorization.Login && x.Password == authorization.Password); //TOREDO
                FormsAuthentication.SetAuthCookie(authUser.Login, true);
            }
            return RedirectToAction("Index", "Home"); //TOREDO
        }

        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(Registration registration)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home"); //TOREDO
            }
            _userRepository.Create(_mapper.Map<Registration, UserBase>(registration));
            //tempUsers.Add(new TempUser { Id = Guid.NewGuid(), Login = registration.Login, Password = registration.Password, Role = registration.Role, RegisterDate = DateTime.Now }); //TOREDO
            FormsAuthentication.SetAuthCookie(registration.Login, true);
            return RedirectToAction("Index", "Home"); // TOREDO
        }

        [Authorize]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Authorization", "Account");
        }
    }
}