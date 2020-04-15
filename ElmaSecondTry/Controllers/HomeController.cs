using ElmaSecondTryBase.Repositories;
using System.Web.Mvc;

namespace ElmaSecondTry.Controllers
{
    public class HomeController : Controller
    {
        private IEntityRepository _entityRepository;
        public HomeController(IEntityRepository entityRepository)
        {
            _entityRepository = entityRepository;
        }
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}