using AutoMapper;
using ElmaSecondTry.Helpers;
using ElmaSecondTry.Models.User;
using ElmaSecondTryBase.Entities;
using ElmaSecondTryBase.Enums;
using ElmaSecondTryBase.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElmaSecondTry.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        /// <summary>
        /// Поля данных DependencyInjection
        /// </summary>
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Комтруктор контроллера
        /// </summary>
        /// <param name="userRepository"></param>
        /// <param name="mapper"></param>
        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        /// <summary>
        /// Отображение данных пользователя
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public ActionResult ShowUser(string login)
        {
            var user = _mapper.Map<UserBase, ShowUser>(_userRepository.FindUser(login));
            return user != null ? View(user) :  View("~/Views/Home/Index.cshtml") ;         //TOREDO
        }

        /// <summary>
        /// Отображение формы редактирования пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EditUser(Guid id)
        {
            var user = _mapper.Map<UserBase, EditUser>(_userRepository.FindUser(id));
            if (user == null)
            {
                return View("~/Views/Home/Index.cshtml");
            }
            var availableRoles = User.IsInRole("Admin")
                ? Enum.GetValues(typeof(UserRoles)).Cast<UserRoles>().Where(x => x.ToString() != "All")
                : Enum.GetValues(typeof(UserRoles)).Cast<UserRoles>().Where(x => x.ToString() != "All" && x.ToString() != "Admin");
            user.AvailableRoles = availableRoles.Select(x => new SelectListItem { Value = x.ToString(), Text = x.ToString() }).ToList();
            return View(user);
        }

        /// <summary>
        /// Метод редактирования данных пользователя
        /// </summary>
        /// <param name="editUser"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditUser(EditUser editUser)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home"); //TOREDO
            }
            var userFromDb = _userRepository.FindUser(editUser.Id);
            var updatingUser = _mapper.Map<EditUser, UserBase>(editUser);
            if (userFromDb == null || updatingUser==null)
            {
                return RedirectToAction("Index", "Home"); //TOREDO
            }
            updatingUser.Login = userFromDb.Login;
            updatingUser.Password = userFromDb.Password;
            updatingUser.RegisterDate = userFromDb.RegisterDate;
            var savedUser = _userRepository.UpdateUser(updatingUser);
            return savedUser != null ? RedirectToAction("ShowUser","User", new { savedUser.Login }) : RedirectToAction("Index", "Home");         //TOREDO
        }

        /// <summary>
        /// Отображение формы фильтрации пользователей
        /// </summary>
        /// <returns></returns>
        public ActionResult FilterUser ()
        {
            var newUser = new FilterUser { Registered = (DateTime.Parse("01.01.2020"), DateTime.Now), Role = UserRoles.All };
            return View(newUser);
        }

        /// <summary>
        /// Фильтрация пользователей
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult FilterUser(FilterUser filterUser)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home"); //TOREDO
            }

            var nhibFilterUser = _mapper.Map<FilterUser, UserBase>(filterUser);
            var filteredUsers = _userRepository.FilterUsers(nhibFilterUser, (filterUser.Registered.Min, filterUser.Registered.Max));
            return RedirectToAction("ShowListUsers", "User", new { filteredUsers });
        }
    }
}