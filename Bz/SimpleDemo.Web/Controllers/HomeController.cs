using SimpleDemo.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SimpleDemo.Web.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILbzService _lbzService;

        public HomeController(ILbzService lbzService)
        {
            _lbzService = lbzService;
        }

        public async Task<ActionResult> Index()
        {
            var result = await _lbzService.GetAll();
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