using ElmaSecondTry.Models.AccountModel;
using ElmaSecondTryBase.Entities;
using System;
using System.Web.Mvc;
using System.Web.Security;
using AutoMapper;
using ElmaSecondTryBase.IRepositories;
using System.Linq;

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
                TempData["message"] = $"Указанные данные не валидны.";
                TempData["status"] = ActionStatus.Error;
                return View("Authorization");
            }
            var repositoryResult = _userRepository.FindUser(authorization.Login);
            if (repositoryResult.Status != ActionStatus.Success)
            {
                TempData["Message"] = repositoryResult.Message;
                TempData["Status"] = repositoryResult.Status;
                return View("Authorization");
            }
            var authUser = repositoryResult.Entity.First() as UserBase;
            if (authUser.Password !=authorization.Password)
            {
                MessageForClient(ActionStatus.Error, $"Неверный пароль!");
                return View("Authorization");
            }
            FormsAuthentication.SetAuthCookie(authUser.Login, true);
            MessageForClient(ActionStatus.Success, $"Вы успешно авторизовались как ({authUser.Login}) !");
            return RedirectToAction("ShowUser", "User", new { authUser.Login });
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
                MessageForClient(ActionStatus.Error, $"Указанные данные не валидны.");
                return View("Registration");
            }
            var repositoryResult = _userRepository.FindUser(registration.Login);
            if (repositoryResult.Status == ActionStatus.Success)
            {
                MessageForClient(ActionStatus.Error, $"Пользователь с логином ({registration.Login}) уже существует, выберите пожалуйста другой логин!");
                return View("Registration"); 
            }
            var savedResult = _userRepository.CreateUser(_mapper.Map<Registration, UserBase>(registration));
            MessageForClient(savedResult.Status, savedResult.Message);
            if (savedResult.Status != ActionStatus.Success)
            {
                return View("Registration");
            }
            var savedUser = savedResult.Entity.First() as UserBase;
            FormsAuthentication.SetAuthCookie(savedUser.Login, true);
            return RedirectToAction("ShowUser", "User", new { savedUser.Login });
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
        public ActionResult EditAccount(string login)
        {
            var repositoryResult = _userRepository.FindUser(login);
            if (repositoryResult.Status != ActionStatus.Success)
            {
                MessageForClient(repositoryResult.Status, repositoryResult.Message);
                return View("~/Views/Home/Index.cshtml");
            }
            var userFromDb = repositoryResult.Entity.First() as UserBase;
            return View(_mapper.Map<UserBase, EditAccount>(userFromDb));
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
                MessageForClient(ActionStatus.Error, $"Указанные данные не валидны.");
                return View("~/Views/Account/EditAccount.cshtml");
            }
            var repositoryResult = _userRepository.FindUser(editAccount.Login);
            if (repositoryResult.Status != ActionStatus.Success)
            {
                MessageForClient(repositoryResult.Status, repositoryResult.Message);
                return View("~/Views/Home/Index.cshtml");
            }
            var userFromDb = repositoryResult.Entity.First() as UserBase;
            if (editAccount.Password!=userFromDb.Password)
            {
                MessageForClient(ActionStatus.Error, $"Введен неверный пароль!");
                return View("~/Views/Account/EditAccount.cshtml");
            }
            if (!string.IsNullOrWhiteSpace(editAccount.NewLogin))
            {
                if (_userRepository.FindUser(editAccount.NewLogin).Status == ActionStatus.Success)
                {
                    MessageForClient(ActionStatus.Error, $"Пользователь с логином ({editAccount.NewLogin}) уже существует, выберите пожалуйста другой логин!");
                    return View("~/Views/Account/EditAccount.cshtml");
                }
                userFromDb.Login = editAccount.NewLogin;
            }
            if (!string.IsNullOrWhiteSpace(editAccount.NewPassword))
            {
                userFromDb.Password = editAccount.NewPassword;
            }
            if (string.IsNullOrWhiteSpace(editAccount.NewPassword) && string.IsNullOrWhiteSpace(editAccount.NewLogin))
            {
                MessageForClient(ActionStatus.Success, $"Вы остались при своих регистрационных данных.");
                return RedirectToAction("ShowUser", "User", new { editAccount.Login });
            }
            var savedResult = _userRepository.UpdateUser(userFromDb);
            MessageForClient(savedResult.Status, savedResult.Message);
            if (savedResult.Status != ActionStatus.Success)
            {
                return RedirectToAction("ShowUser", "User", new { editAccount.Login });
            }
            var savedUser = savedResult.Entity.First() as UserBase;
            FormsAuthentication.SignOut();
            FormsAuthentication.SetAuthCookie(savedUser.Login, true);
            return RedirectToAction("ShowUser", "User", new { savedUser.Login }); 
        }

        /// <summary>
        /// Формирование сообщения для клиента
        /// </summary>
        /// <param name="status"></param>
        /// <param name="message"></param>
        private void MessageForClient(ActionStatus status, string message)
        {
            TempData["message"] = message;
            TempData["status"] = status;
        }
    }
}