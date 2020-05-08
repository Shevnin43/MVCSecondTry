using ElmaSecondTryBase.Entities;
using ElmaSecondTryBase.IRepositories;
using System.Web.Mvc;

namespace ElmaSecondTry.Controllers
{
    /// <summary>
    /// Контроллер представлений для неавторизованных пользователей
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Отображение страницы Index
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Отображение страницы About
        /// </summary>
        /// <returns></returns>
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        /// <summary>
        /// Отображение сообщения
        /// </summary>
        /// <param name="status"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ClientMessage(ActionStatus status, string message)
        {
            ViewBag.Message = message;
            ViewBag.Title = status;
            switch (status)
            {
                case ActionStatus.Fatal:
                    ViewBag.Color = "red";
                    break;
                case ActionStatus.Error:
                    ViewBag.Color = "orange";
                    break;
                case ActionStatus.Warning:
                    ViewBag.Color = "blue";
                    break;
                case ActionStatus.Success:
                    ViewBag.Color = "green";
                    break;
            }
            return PartialView();
        }
    }
}