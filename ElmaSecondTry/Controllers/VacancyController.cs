using AutoMapper;
using ElmaSecondTry.Helpers;
using ElmaSecondTry.Models.UserModel;
using ElmaSecondTry.Models.VacancyModel;
using ElmaSecondTryBase.Entities;
using ElmaSecondTryBase.Enums;
using ElmaSecondTryBase.IRepositories;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ElmaSecondTry.Controllers
{
    [Authorize]
    public class VacancyController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAnnouncementRepository _announcementRepository;
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Конструктор с получением необходимых сервисов
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="announcementRepository"></param>
        /// <param name="userRepository"></param>
        public VacancyController(IMapper mapper, IAnnouncementRepository announcementRepository, IUserRepository userRepository)
        {
            _mapper = mapper;
            _announcementRepository = announcementRepository;
            _userRepository = userRepository;
        }

        [Authorize(Roles = "Employee, HR, Admin")]
        public ActionResult CreateVacancy(string login)
        {
            if (string.IsNullOrWhiteSpace(login))
            {
                MessageForClient(ActionStatus.Error, $"Не указан владелец создаваемого объявления.");
                return RedirectToAction("Index", "Home");
            }
            var timesJob = Enum.GetValues(typeof(TimeJob)).Cast<TimeJob>().Where(x => x!=TimeJob.All);
            ViewBag.Employments = timesJob.Select(x => new SelectListItem { Value = x.ToString(), Text = General.Employments[x] }).ToList();
            return View(new MyVacancy { OwnerLogin = login, ValidDay = DateTime.Now, Type=AnnouncementType.Vacancy, Employment = TimeJob.None });
        }

        /// <summary>
        /// Метод создания нового объявления-вакансии
        /// </summary>
        /// <param name="vacancy"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Employee, HR, Admin")]
        public ActionResult CreateVacancy(MyVacancy vacancy)
        {
            if (!ModelState.IsValid)
            {
                MessageForClient(ActionStatus.Error, $"Указанные данные не валидны.");
                return RedirectToAction("Index", "Home");
            }
            var savingVacancy = _mapper.Map<MyVacancy, VacancyBase>(vacancy);
            var identityUser = _userRepository.FindUser(User.Identity.Name);
            if (identityUser.Status != ActionStatus.Success)
            {
                MessageForClient(ActionStatus.Error, $"Не удалось найти пользователя, которому требуется добавить объявление.");
                return RedirectToAction("Index", "Home");
            }
            savingVacancy.Creator = identityUser.Entity.First() as UserBase;
            savingVacancy.LastEditor = savingVacancy.Creator;
            var updatedResult = _announcementRepository.CreateAnnouncement(savingVacancy);
            MessageForClient(updatedResult.Status, updatedResult.Message);
            if (updatedResult.Status != ActionStatus.Success)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("ShowUser", "User", new { (updatedResult.Entity.First()as VacancyBase).Creator.Login });
        }

        /// <summary>
        /// Отображение представления редактирования объявления вакансии
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Employee, HR, Admin")]
        public ActionResult EditVacancy(Guid id)
        {
            var repositoryResult = _announcementRepository.FindAnnouncement(id);
            if (repositoryResult.Status != ActionStatus.Success)
            {
                MessageForClient(repositoryResult.Status, repositoryResult.Message);
                return RedirectToAction("Index", "Home");
            }
            var editingVacancy = repositoryResult.Entity.First() as VacancyBase;

            if (!User.IsInRole("Admin") && User.Identity.Name != editingVacancy.Creator.Login)
            {
                MessageForClient(ActionStatus.Error, "У вас недостаточно прав для редактирования данного объявления!");
                return RedirectToAction("ShowAnnouncement", "Announcement", new { id });
            }

            var timesJob = Enum.GetValues(typeof(TimeJob)).Cast<TimeJob>().Where(x => x != TimeJob.All);
            ViewBag.Employments = timesJob.Select(x => new SelectListItem { Value = x.ToString(), Text = General.Employments[x] }).ToList();
            return View(_mapper.Map<VacancyBase, MyVacancy>(editingVacancy));
        }

        /// <summary>
        /// Редактирование объявления вакансии
        /// </summary>
        /// <param name="editingVacancy"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Employee, HR, Admin")]
        public ActionResult EditVacancy(MyVacancy editingVacancy)
        {
            if (!ModelState.IsValid)
            {
                MessageForClient(ActionStatus.Error, $"Указанные данные не валидны.");
                return RedirectToAction("Index", "Home");
            }
            var repositoryResult = _announcementRepository.FindAnnouncement(editingVacancy.Id);
            if (repositoryResult.Status != ActionStatus.Success)
            {
                MessageForClient(repositoryResult.Status, repositoryResult.Message);
                return RedirectToAction("Index", "Home");
            }
            if ((repositoryResult.Entity.First() as VacancyBase).Creator.Login != User.Identity.Name && !User.IsInRole("Admin"))
            {
                MessageForClient(ActionStatus.Error, "У вас недостаточно прав для редактирования данного объявления!");
                return RedirectToAction("ShowAnnouncement", "Announcement", new { editingVacancy.Id });
            }
             repositoryResult = _userRepository.FindUser(User.Identity.Name);
            if (repositoryResult.Status != ActionStatus.Success)
            {
                MessageForClient(repositoryResult.Status, $"Не найден авторизованный пользователь ({User.Identity.Name}).");
                return RedirectToAction("Index", "Home");
            }
            var updatingVacancy = _mapper.Map<MyVacancy, VacancyBase>(editingVacancy);
            updatingVacancy.LastEditor = repositoryResult.Entity.First() as UserBase;
            repositoryResult = _announcementRepository.UpdateAnnouncement(updatingVacancy);
            MessageForClient(repositoryResult.Status, repositoryResult.Message);

            return repositoryResult.Status == ActionStatus.Success
                ? RedirectToAction("ShowAnnouncement", "Announcement", new { (repositoryResult.Entity.First() as VacancyBase).Id })
                : RedirectToAction("Index", "Home");
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