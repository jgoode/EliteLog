using EliteParse.Models;
using EliteParse.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EliteLog.Controllers {
    public class HomeController : AsyncController {
        IExpeditionRepository _expeditionRepository;

        public HomeController(IExpeditionRepository expeditionRepository) {
            _expeditionRepository = expeditionRepository;
        }

        public HomeController() : this(new ExpeditionRespository()) { }

        public async Task<ActionResult> IndexAsync() {
            Expedition exp = await _expeditionRepository.GetCurrent();
            return View(exp);
        }

        public ActionResult About() {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}