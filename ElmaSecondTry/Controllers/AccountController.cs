using ElmaSecondTryBase.Enums;
using ElmaSecondTry.Models.Account;
using ElmaSecondTry.Models.User;
using ElmaSecondTryBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using AutoMapper;
using ElmaSecondTryBase.IRepositories;

namespace ElmaSecondTry.Controllers
{
    /// <summary>
    /// Контроллер работы с аккаунтом
    /// </summary>
    public class AccountController : Controller
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
        public AccountController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Метод отображения формы авторизации пользователя
        /// </summary>
        /// <returns></returns>
        public ActionResult Authorization()
        {
            return View();
        }

        /// <summary>
        /// Авторизация существующего пользователя
        /// </summary>
        /// <param name="authorization"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Authorization(Authorization authorization)
        {
            if(!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home"); //TOREDO
            }
            var authUser = _userRepository.FindUser(authorization.Login);
            if (authUser!=null && authUser.Password==authorization.Password)
            {
                FormsAuthentication.SetAuthCookie(authUser.Login, true);
                return RedirectToAction("ShowUser", "User", new { authUser.Login });
            }
            return RedirectToAction("Index", "Home"); //TOREDO
        }

        /// <summary>
        /// Метод отображения формы регистрации нового пользователя
        /// </summary>
        /// <returns></returns>
        public ActionResult Registration()
        {
            return View();
        }

        /// <summary>
        /// Регистрация нового пользователя
        /// </summary>
        /// <param name="registration"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Registration(Registration registration)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home"); //TOREDO
            }
            var savedUser = _userRepository.CreateUser(_mapper.Map<Registration, UserBase>(registration));
            if (savedUser != null )
            {
                FormsAuthentication.SetAuthCookie(savedUser.Login, true);
                return RedirectToAction("ShowUser", "User", new { savedUser.Login });
            }
            return RedirectToAction("Index", "Home"); //TOREDO
        }

        /// <summary>
        /// Деавторизация пользователя
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Authorization", "Account");
        }

        /// <summary>
        /// Отображение формы редактирования логин а и пароля
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        public ActionResult EditAccount(Guid id)
        {
            var userFromDb = _userRepository.FindUser(id);
            return View(_mapper.Map< UserBase, EditAccount>(userFromDb));
        }

        /// <summary>
        /// Редактирование логина и пароля
        /// </summary>
        /// <param name="editAccount"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public ActionResult EditAccount(EditAccount editAccount)
        {
            if(!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home"); //TOREDO
            }
            var userFromDb = _userRepository.FindUser(editAccount.Id);
            if(userFromDb==null)
            {
                return RedirectToAction("Index", "Home"); //TOREDO
            }
            userFromDb.Login = editAccount.Login;
            userFromDb.Password = editAccount.Password;
            var savedUser = _userRepository.UpdateUser(userFromDb);

            return savedUser != null ? RedirectToAction("ShowUser", "User", new { savedUser.Login }) : RedirectToAction("Index", "Home");         //TOREDO
        }
    }
}