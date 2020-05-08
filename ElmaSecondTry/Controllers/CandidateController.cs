using AutoMapper;
using ElmaSecondTry.Models.CandidateModel;
using ElmaSecondTry.Models.UserModel;
using ElmaSecondTryBase.Entities;
using ElmaSecondTryBase.Enums;
using ElmaSecondTryBase.IRepositories;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElmaSecondTry.Controllers
{
    /// <summary>
    /// Класс для работы с объявлениями типа кандидат
    /// </summary>
    [Authorize]
    public class CandidateController : Controller
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
        public CandidateController(IMapper mapper, IAnnouncementRepository announcementRepository, IUserRepository userRepository)
        {
            _mapper = mapper;
            _announcementRepository = announcementRepository;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Метод отображения формы создания новго объявления кандидата
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [Authorize(Roles = "Jobseeker, HR, Admin")]
        public ActionResult CreateCandidate(string login)
        {
           if(string.IsNullOrWhiteSpace(login))
            {
                MessageForClient(ActionStatus.Error, $"Не указан владелец создаваемого объявления.");
                return RedirectToAction("Index", "Home");
            }
                return View(new MyCandidate { OwnerLogin = login, Type = AnnouncementType.Candidate });
        }

        /// <summary>
        /// Метод создания нового объявления-кандидата
        /// </summary>
        /// <param name="creatingCandidate"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles ="Jobseeker, HR, Admin")]
        public ActionResult CreateCandidate(MyCandidate candidate, HttpPostedFileBase imageFile)
        {
            if (!ModelState.IsValid)
            {
                MessageForClient(ActionStatus.Error, $"Указанные данные не валидны.");
                return RedirectToAction("Index", "Home");
            }
            var savingCandidate = _mapper.Map<MyCandidate, CandidateBase>(candidate);
            var identityUser = _userRepository.FindUser(User.Identity.Name);
            if (identityUser.Status != ActionStatus.Success)
            {
                MessageForClient(ActionStatus.Error, $"Не удалось найти пользователя, которому требуется добавить объявление.");
                return RedirectToAction("Index", "Home");
            }
            savingCandidate.Creator = identityUser.Entity.First() as UserBase;
            savingCandidate.LastEditor = savingCandidate.Creator;
            if (imageFile != null)
            {
                byte[] buf = new byte[imageFile.ContentLength];
                imageFile.InputStream.Read(buf, 0, imageFile.ContentLength);
                savingCandidate.Photo = buf;
            }
            var updatedResult = _announcementRepository.CreateAnnouncement(savingCandidate);
            MessageForClient(updatedResult.Status, updatedResult.Message);
            if (updatedResult.Status != ActionStatus.Success)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("ShowUser", "User", new { (updatedResult.Entity.First() as CandidateBase).Creator.Login });

        }

        /// <summary>
        /// Отображение информации об объявлении кандидата
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Employee, HR, Admin, Jobseeker")]
        public ActionResult ShowCandidate(Guid id)
        {
            var repositoryResult = _announcementRepository.FindAnnouncement(id);
            if (repositoryResult.Status != ActionStatus.Success)
            {
                MessageForClient(repositoryResult.Status, repositoryResult.Message);
                return RedirectToAction("Index", "Home");
            }
            var dbCandidate = repositoryResult.Entity.First() as CandidateBase;
            return View(_mapper.Map<CandidateBase, MyCandidate>(dbCandidate));
        }

        /// <summary>
        /// Отображение представления редактирования объявления кандидата
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Jobseeker, HR, Admin")]
        public ActionResult EditCandidate(Guid id)
        {
            var repositoryResult = _announcementRepository.FindAnnouncement(id);
            if (repositoryResult.Status != ActionStatus.Success)
            {
                MessageForClient(repositoryResult.Status, repositoryResult.Message);
                return RedirectToAction("Index", "Home");
            }
            var editingCandidate = repositoryResult.Entity.First() as CandidateBase;

            if (!User.IsInRole("Admin") && User.Identity.Name != editingCandidate.Creator.Login)
            {
                MessageForClient(ActionStatus.Error, "У вас недостаточно прав для редактирования данного объявления!");
                return RedirectToAction("ShowCandidate", "Candidate", new { id });
            }

            return View(_mapper.Map<CandidateBase, MyCandidate>(editingCandidate));
        }

        /// <summary>
        /// Редактирование объявления кандидата
        /// </summary>
        /// <param name="editingCandidate"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Jobseeker, HR, Admin")]
        public ActionResult EditCandidate(MyCandidate editingCandidate, HttpPostedFileBase imageFile)
        {
            if (!ModelState.IsValid)
            {
                MessageForClient(ActionStatus.Error, $"Указанные данные не валидны.");
                return RedirectToAction("Index", "Home");
            }
            var repositoryResult = _announcementRepository.FindAnnouncement(editingCandidate.Id);
            if(repositoryResult.Status != ActionStatus.Success)
            {
                MessageForClient(repositoryResult.Status, repositoryResult.Message);
                return RedirectToAction("Index", "Home");
            }
            if((repositoryResult.Entity.First() as CandidateBase).Creator.Login != User.Identity.Name && !User.IsInRole("Admin"))
            {
                MessageForClient(ActionStatus.Error, "У вас недостаточно прав для редактирования данного объявления!");
                return RedirectToAction("ShowCandidate", "Candidate", new { editingCandidate.Id });
            }
            repositoryResult = _userRepository.FindUser(User.Identity.Name);
            if (repositoryResult.Status != ActionStatus.Success)
            {
                MessageForClient(repositoryResult.Status, $"Не найден авторизованный пользователь ({User.Identity.Name}).");
                return RedirectToAction("Index", "Home");
            }
            var updatingCandidate = _mapper.Map<MyCandidate, CandidateBase>(editingCandidate);
            updatingCandidate.LastEditor = repositoryResult.Entity.First() as UserBase;
            if (imageFile != null)
            {
                byte[] buf = new byte[imageFile.ContentLength];
                imageFile.InputStream.Read(buf, 0, imageFile.ContentLength);
                updatingCandidate.Photo = buf;
            }
            repositoryResult = _announcementRepository.UpdateAnnouncement(updatingCandidate);
            MessageForClient(repositoryResult.Status, repositoryResult.Message);

            return repositoryResult.Status == ActionStatus.Success 
                ? RedirectToAction("ShowCandidate", "Candidate", new { (repositoryResult.Entity.First() as CandidateBase).Id })
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