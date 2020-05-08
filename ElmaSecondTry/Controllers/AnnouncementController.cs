using AutoMapper;
using ElmaSecondTry.Helpers;
using ElmaSecondTry.Models;
using ElmaSecondTry.Models.CandidateModel;
using ElmaSecondTry.Models.SelectAnnouncements;
using ElmaSecondTry.Models.VacancyModel;
using ElmaSecondTryBase.Entities;
using ElmaSecondTryBase.Enums;
using ElmaSecondTryBase.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ElmaSecondTry.Controllers
{
    [Authorize]
    public class AnnouncementController : Controller
    {
        private readonly IAnnouncementRepository _announcementRepository;
        private readonly IMapper _mapper;
        
        /// <summary>
        /// Конструктор с получением сервиса репозитория объявлений
        /// </summary>
        /// <param name="announcementRepository"></param>
        public AnnouncementController(IAnnouncementRepository announcementRepository, IMapper mapper)
        {
            _announcementRepository = announcementRepository;
            _mapper = mapper;
        }


        /// <summary>
        /// Отображение информации об объявлении 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Employee, HR, Admin, Jobseeker")]
        public ActionResult ShowAnnouncement(Guid id)
        {
            var repositoryResult = _announcementRepository.FindAnnouncement(id);
            if (repositoryResult.Status != ActionStatus.Success)
            {
                MessageForClient(repositoryResult.Status, repositoryResult.Message);
                return RedirectToAction("Index", "Home");
            }
            var dbAnnouncement = repositoryResult.Entity.First();
            if (dbAnnouncement is CandidateBase)
            {
                ViewBag.TypeOfAnnouncement = "Кандидата";
                return View(_mapper.Map<CandidateBase, MyCandidate>(dbAnnouncement as CandidateBase));
            }
            if (dbAnnouncement is VacancyBase)
            {
                ViewBag.TypeOfAnnouncement = "Вакансии";
                ViewBag.TimeJob = General.Employments[(dbAnnouncement as VacancyBase).Employment];
                return View(_mapper.Map<VacancyBase, MyVacancy>(dbAnnouncement as VacancyBase));
            }
            return RedirectToAction("Index", "Home");

        }

        /// <summary>
        /// Вывод формы выборки объявлений
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles ="Admin, Jobseeker, Employee, HR")]
        public ActionResult AnnouncementsSelect()
        {
            var announcementSelect = new GeneralSelect { IsBlocked = false};
            if (User.IsInRole("Admin"))
            {
                announcementSelect.IsBlocked = null;
            }
            if (User.IsInRole("Admin") || User.IsInRole("HR"))
            {
                announcementSelect.CanSelectCandidates = true;
                announcementSelect.CanSelectVacancyes = true;
                announcementSelect.IncludeCandidates = true;
                announcementSelect.IncludeVacancyes = true;
                announcementSelect.SelectCandidates = new SelectCandidates();
                announcementSelect.SelectVacancyes = new SelectVacancyes();
            }
            if (User.IsInRole("Employee"))
            {
                announcementSelect.CanSelectCandidates = true;
                announcementSelect.IncludeCandidates = true;
                announcementSelect.SelectCandidates = new SelectCandidates();
            }
            if (User.IsInRole("Jobseeker"))
            {
                announcementSelect.CanSelectVacancyes = true;
                announcementSelect.IncludeVacancyes = true;
                announcementSelect.SelectVacancyes = new SelectVacancyes ();
            }
            var timesJob = Enum.GetValues(typeof(TimeJob)).Cast<TimeJob>();
            ViewBag.Employments = timesJob.Select(x => new SelectListItem { Value = x.ToString(), Text = General.Employments[x] }).ToList();
            return View(announcementSelect);
        }

        /// <summary>
        /// Выборка объявлений
        /// </summary>
        /// <param name="generalSelect"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin, Jobseeker, Employee, HR")]
        [HttpPost]
        public ActionResult AnnouncementsSelect(GeneralSelect generalSelect)
        {
            var timesJob = Enum.GetValues(typeof(TimeJob)).Cast<TimeJob>();
            ViewBag.Employments = timesJob.Select(x => new SelectListItem { Value = x.ToString(), Text = General.Employments[x] }).ToList();

            if (!ModelState.IsValid )
            {
                var modelStateValues = ModelState.Values.ToArray();
                var modelStateKeys = ModelState.Keys.ToArray();
                if (modelStateValues.Any(x => x.Errors.Count > 0 && !General.NulableBoolPropertyes.Contains(modelStateKeys[Array.IndexOf(modelStateValues, x)])))
                {
                    ModelState.Clear();
                    MessageForClient(ActionStatus.Error, $"Отсутствует модель представления данных для выборки объявлений.");
                    return PartialView("~/Views/Announcement/ListAnnouncement.cshtml", new List<MyAnnouncement>());
                }
            }
            if (!User.IsInRole("Admin"))
            {
                generalSelect.IsBlocked = false;
            }
            var generalParams = generalSelect.GetType()?.GetProperties()?.ToDictionary(p => p.Name, p => p.GetValue(generalSelect));

            if (generalParams.Keys.Any(x => x == "IncludeVacancyes" && !(bool)generalParams[x]) && generalParams.Keys.Any(x => x == "IncludeCandidates" && !(bool)generalParams[x]))
            {
                ModelState.Clear();
                MessageForClient(ActionStatus.Warning, $"Вы не выбрали ни один тип объявлений для выборки.");
                return PartialView("~/Views/Announcement/ListAnnouncement.cshtml", new List<MyAnnouncement>());
            }
            var resultAnnouncements = new List<MyAnnouncement>();
            if (generalParams.Keys.Any(x => x == "IncludeVacancyes" && (bool)generalParams[x]))
            {
                var vacancyesParams = generalParams["SelectVacancyes"].GetType().GetProperties().ToDictionary(p => p.Name, p => p.GetValue(generalParams["SelectVacancyes"]));
                foreach (var param in generalParams.Where(x => !General.NotFilteredProperties.Contains(x.Key)))
                {
                    vacancyesParams.Add(param.Key, param.Value);
                }
                var vacancyesSelectResult = _announcementRepository.SelectAnnouncements(vacancyesParams);
                if (vacancyesSelectResult.Status == ActionStatus.Success)
                {
                    var vacancyAnnouncements = vacancyesSelectResult.Entity.Select(x => _mapper.Map<IAnnouncement, MyVacancy>(x as IAnnouncement));
                    resultAnnouncements.AddRange(vacancyAnnouncements);
                }
            }
            if (generalParams.Keys.Any(x => x == "IncludeCandidates" && (bool)generalParams[x]))
            {
                var candidatesParams = generalParams["SelectCandidates"].GetType().GetProperties().ToDictionary(p => p.Name, p => p.GetValue(generalParams["SelectCandidates"]));
                candidatesParams["BirthDayMin"] = DateTime.Now.AddYears(- (int)candidatesParams["AgeMax"]);
                candidatesParams["BirthDayMax"] = DateTime.Now.AddYears(- (int)candidatesParams["AgeMin"]);
                candidatesParams.Remove("AgeMin");
                candidatesParams.Remove("AgeMax");
                foreach (var param in generalParams.Where(x => !General.NotFilteredProperties.Contains(x.Key)))
                {
                    candidatesParams.Add(param.Key, param.Value);
                }
                
                var candidatesSelectResult = _announcementRepository.SelectAnnouncements(candidatesParams);
                if (candidatesSelectResult.Status == ActionStatus.Success)
                {
                    var vacancyAnnouncements = candidatesSelectResult.Entity.Select(x => _mapper.Map<IAnnouncement, MyCandidate>(x as IAnnouncement));
                    resultAnnouncements.AddRange(vacancyAnnouncements);
                }
            }
            ModelState.Clear();
            if (resultAnnouncements.Count() == 0)
            {
                MessageForClient(ActionStatus.Warning, $"Не найдены объявления, удовлетворяющие параметрам запроса.");
            }
            else
            {
                MessageForClient(ActionStatus.Success, $"Найдено {resultAnnouncements.Count()} объявлений.");
            }
            return PartialView("~/Views/Announcement/ListAnnouncement.cshtml", resultAnnouncements);
        }

        /// <summary>
        /// Заблокировать объявление
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        public ActionResult BlockAnnouncement(Guid id, bool block)
        {
            var repositoryResult = _announcementRepository.FindAnnouncement(id);
            if(repositoryResult.Status != ActionStatus.Success)
            {
                MessageForClient(ActionStatus.Error, $"В базе данных отсутствует данное объявление.");
                return RedirectToAction("Index", "Home");
            }
            var userFromDb = repositoryResult.Entity.First();
            (userFromDb as IAnnouncement).IsBlocked = block;
            repositoryResult = _announcementRepository.UpdateAnnouncement(userFromDb as IAnnouncement);
            MessageForClient(repositoryResult.Status, repositoryResult.Message);
            return repositoryResult.Status == ActionStatus.Success 
                ? RedirectToAction($"Show{(userFromDb as IAnnouncement).Type.ToString()}", $"{(userFromDb as IAnnouncement).Type.ToString()}", new { id }) 
                : RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Удаление объявления .
        /// </summary>
        /// <param name="id"></param>
        /// <param name="login"></param>
        /// <returns></returns>
        [Authorize(Roles = "Jobseeker, HR, Admin, Employee")]
        public ActionResult DeleteAnnouncement(Guid id, string login)
        {
            if(!User.IsInRole("Admin") && User.Identity.Name!=login)
            {
                MessageForClient(ActionStatus.Error, "У вас недостаточно прав для удаления данного объявления!");
                return RedirectToAction("ShowUser", "User", new { User.Identity.Name });
            }
            var repositoryResult = _announcementRepository.DeleteAnnouncement(id);
            MessageForClient(repositoryResult.Status, repositoryResult.Message);
            return RedirectToAction("ShowUser", "User", new { login });
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