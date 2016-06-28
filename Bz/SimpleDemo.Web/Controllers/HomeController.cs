using SimpleDemo.Application;
using SimpleDemo.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;

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
            var result1 = await _lbzService.GetTestAll();
            return View();
        }

        public ActionResult About()
        {
            SourceEntity source = new SourceEntity() {
                Id = 1,
                Name="Lbz"
            };
            DestEntity dest = Mapper.Map<DestEntity>(source);
            
            return Content("Lbz");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}