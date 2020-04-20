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
        /// Отображение страницы Contact
        /// </summary>
        /// <returns></returns>
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}