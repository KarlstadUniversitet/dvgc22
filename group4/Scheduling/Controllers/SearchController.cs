using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Repository;
using Domain;

namespace Scheduling.Controllers
{
    public class SearchController : Controller
    {
        CodeHandler codeHandler;
        ScheduleBuilder scheduleBuilder;


        public ActionResult Index()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Search(Search search)
        {
            if (ModelState.IsValid)
            {
                codeHandler = new CodeHandler();
                scheduleBuilder = new ScheduleBuilder();
                ViewBag.SearchWords = search.SearchWord;
                return PartialView(codeHandler.GetApplicationCodeList(search.SearchWord));
            }
            return PartialView(new List<Application>());
          
        }

    }
}
