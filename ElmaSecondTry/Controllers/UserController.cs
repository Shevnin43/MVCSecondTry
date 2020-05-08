using AutoMapper;
using ElmaSecondTry.Helpers;
using ElmaSecondTry.Models.CandidateModel;
using ElmaSecondTry.Models.UserModel;
using ElmaSecondTry.Models.VacancyModel;
using ElmaSecondTryBase.Entities;
using ElmaSecondTryBase.Enums;
using ElmaSecondTryBase.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

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
        private readonly IAnnouncementRepository _announcementRepository;

        /// <summary>
        /// Комтруктор контроллера
        /// </summary>
        /// <param name="userRepository"></param>
        /// <param name="mapper"></param>
        public UserController(IUserRepository userRepository, IMapper mapper, IAnnouncementRepository announcementRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _announcementRepository = announcementRepository;
        }
        /// <summary>
        /// Отображение данных пользователя
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public ActionResult ShowUser(string login)
        {
            var repositoryResult = _userRepository.FindUser(login, User.IsInRole("Admin") || User.Identity.Name == login);
            if (repositoryResult.Status != ActionStatus.Success)
            {
                MessageForClient(repositoryResult.Status, repositoryResult.Message);
                return RedirectToAction("Index", "Home");
            }
            var savedUserBase = (UserBase)repositoryResult.Entity.First();
            var user = _mapper.Map<UserBase, ShowUser>(savedUserBase);
            return View(user);
        }

        /// <summary>
        /// Отображение формы редактирования пользователя
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public ActionResult EditUser(string login)
        {
            if (!User.IsInRole("Admin") && User.Identity.Name!=login )
            {
                MessageForClient(ActionStatus.Error, $"Вы не можете корректировать данные пользователя ({login}).");
                return RedirectToAction("Index", "Home");
            }
            var repositoryResult = _userRepository.FindUser(login);
            if (repositoryResult.Status != ActionStatus.Success)
            {
                MessageForClient(repositoryResult.Status, repositoryResult.Message);
                return RedirectToAction("Index", "Home");
            }
            var savedUserBase = (UserBase)repositoryResult.Entity.First();
            var user = _mapper.Map<UserBase, EditUser>(savedUserBase);
            return View(UserWithAvailableRoles(user));
        }

        /// <summary>
        /// Получение доступных ролей для пользователя
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private EditUser UserWithAvailableRoles (EditUser user)
        {
            var availableRoles = User.IsInRole("Admin")
                ? General.RolesForUserByAdmin
                : General.RolesForUserBySelf;
            ViewBag.AvailableRoles = availableRoles.Select(x => new SelectListItem { Value = x.ToString(), Text = General.RolesShownNames[x] }).ToList();
            return user;
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
                MessageForClient(ActionStatus.Error, $"Указанные данные не валидны.");
                return RedirectToAction("Index", "Home");
            }

            var updatingUser = _mapper.Map<EditUser, UserBase>(editUser);
            var repositoryResult = _userRepository.FindUser(editUser.Login);
            if (repositoryResult.Status != ActionStatus.Success)
            {
                MessageForClient(repositoryResult.Status, repositoryResult.Message);
                return View("~/Views/Home/Index.cshtml");
            }
            var userFromDb = repositoryResult.Entity.First() as UserBase;
            updatingUser.Password = userFromDb.Password;
            updatingUser.RegisterDate = userFromDb.RegisterDate;
            if (editUser.Role != userFromDb.Role)
            {
                var changeRoleResult = OnChangeRoles(editUser.Role, userFromDb.Announcements.ToArray());
                if (changeRoleResult.Status != ActionStatus.Success)
                {
                    MessageForClient(changeRoleResult.Status, changeRoleResult.Message);
                    return RedirectToAction("ShowUser", "User", new { editUser.Login });
                }
            }
            repositoryResult = _userRepository.UpdateUser(updatingUser);
            MessageForClient(repositoryResult.Status, repositoryResult.Message);
            return RedirectToAction("ShowUser","User", new { editUser.Login });
        }

        /// <summary>
        /// Отображение формы фильтрации пользователей
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        public ActionResult FilterUser ()
        {
            var newUser = new FilterUser { MinRegisterDate = DateTime.Parse("01.01.2020 00:00:00"), MaxRegisterDate = DateTime.Now, Role = UserRoles.All };
            ViewBag.AvailableRoles = General.RolesForFilter.Select(x => new SelectListItem { Value = x.ToString(), Text = General.RolesShownNames[x] }).ToList();
            return View(newUser);
        }

        /// <summary>
        /// Отображение отфильтрованных пользователей (через Ajax форму)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult ShowFilteredUsers (FilterUser filterUser)
        {
            if (!ModelState.IsValid)
            {
                MessageForClient(ActionStatus.Error, $"Указанные данные не валидны.");
                return View("~/Views/Home/Index.cshtml");
            }
            var registerDates = new Dictionary<string, DateTime> { ["min"] = filterUser.MinRegisterDate, ["max"] = filterUser.MaxRegisterDate };
            var repositoryResult = _userRepository.FilterUsers(_mapper.Map<FilterUser, UserBase>(filterUser), registerDates);
            if (repositoryResult.Status != ActionStatus.Success)
            {
                MessageForClient(repositoryResult.Status, repositoryResult.Message);
                return RedirectToAction("FilterUser", "User");
            }
            var showUsers = repositoryResult.Entity.Select(x => _mapper.Map<UserBase, ShowUser>(x as UserBase)).ToList();
            return PartialView("~/Views/User/ShowListUsers.cshtml", showUsers);
        }
        /// <summary>
        /// Удаление пользователя.
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public ActionResult DeleteUser(string login)
        {
            if (string.IsNullOrWhiteSpace(login))
            {
                MessageForClient(ActionStatus.Error, $"Не указаны данные удаляемого пользователя.");
                return RedirectToAction("Index", "Home");
            }
            var repositoryResult = _userRepository.FindUser(login);
            if (repositoryResult.Status != ActionStatus.Success)
            {
                MessageForClient(repositoryResult.Status, repositoryResult.Message);
                return RedirectToAction("Index", "Home");
            }

            var userFromDb = repositoryResult.Entity.First() as UserBase;

            repositoryResult = _userRepository.DeleteUser(userFromDb);
            MessageForClient(repositoryResult.Status, repositoryResult.Message);
            if (repositoryResult.Status != ActionStatus.Success)
            {
                return RedirectToAction("ShowUser", "User", new { login });
            }
            if (User.Identity.Name == login)
            {
                FormsAuthentication.SignOut();
            }
            return RedirectToAction("Index", "Home");
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

        /// <summary>
        /// Удаление объявлений при изменении роли пользователя
        /// </summary>
        /// <param name="role"></param>
        /// <param name="announcements"></param>
        /// <returns></returns>
        private RepositoryResult OnChangeRoles(UserRoles role, IAnnouncement[] announcements)
        {
            var removingAnnouncements = new List<IAnnouncement>();
            switch (role)
            {
                case UserRoles.Admin:
                case UserRoles.All:
                case UserRoles.None:
                    removingAnnouncements = announcements.ToList();
                    break;
                case UserRoles.Jobseeker:
                    removingAnnouncements = announcements.Where(x => x.Type != AnnouncementType.Candidate).ToList();
                    break;
                case UserRoles.Employee:
                    removingAnnouncements = announcements.Where(x => x.Type != AnnouncementType.Vacancy).ToList();
                    break;
                case UserRoles.HR:
                    removingAnnouncements = announcements.Where(x => x.Type != AnnouncementType.Candidate && x.Type != AnnouncementType.Vacancy).ToList();
                    break;
            }
            var repositoryResults = new List<RepositoryResult>();
            foreach (var announcement in removingAnnouncements)
            {
                if (announcement.Type == AnnouncementType.Candidate)
                {
                    repositoryResults.Add(_announcementRepository.DeleteAnnouncement(announcement.Id));
                }
                if (announcement.Type == AnnouncementType.Vacancy)
                {
                    repositoryResults.Add(_announcementRepository.DeleteAnnouncement(announcement.Id));
                }
            }
            return repositoryResults.All(x => x.Status == ActionStatus.Success)
                ? new RepositoryResult { Status = ActionStatus.Success }
                : new RepositoryResult { Status = ActionStatus.Error, Message = string.Concat("При удалении объявлений пользователя возникли ошибки: ", repositoryResults.Where(x => x.Status != ActionStatus.Success).Select(x => x.Message)) };
        }
    }
}