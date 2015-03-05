using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Repository;
using Domain;

namespace Scheduling.Controllers
{
    public class CalendarController : Controller
    {       

        //
        // GET: /Calendar/
        private const string RSS_URL = "https://www3.kau.se/tentamenslista/rss.xml";
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Month/

        public ActionResult Month(string applicationCode)
        {
            ViewBag.Code = applicationCode.Trim(new char[] {'\''});
            return View();
        }

        //
        // GET: /Agenda/

        public ActionResult Agenda(string applicationCode)
        {
            ViewBag.Code = applicationCode.Trim(new char[] { '\'' });
            return View();
        }


        private ActionResult CreateCalendarView(string ViewName, string applicationCode)
        {
            if (!String.IsNullOrEmpty(applicationCode))
            {
                CalendarViewModel cvm = CreateCalenderViewModel(applicationCode);
                ViewBag.Code = applicationCode;
                if (cvm.lectures.Count > 0)
                    return PartialView(ViewName, cvm);
                return PartialView("NoLectures");
            }
            else
            {
                return PartialView("Index");
            }
        }

        private CalendarViewModel CreateCalenderViewModel(string applicationCodes)
        {
            CalendarViewModel cvm = new CalendarViewModel();
            ScheduleBuilder scheduleBuilder = new ScheduleBuilder();
            cvm.lectures = scheduleBuilder.LecturesFromApplicationCode(applicationCodes);
            cvm.AddBreaksBetweenLectures();
            //List<Domain.Lecture> examsList = XMLParser.FileParser(RSS_URL);
            //cvm.AddExams_New(examsList, CalendarViewModel.GetCourseCode(examsList, applicationCodes));
            return cvm;
        }

        private ActionResult CreateCalenderViewByCategory(string ViewName, string CategoryName) {
            ViewBag.CategoryName = CategoryName;
            Category category = null;
            foreach(Category cat in (Session["Categories"] as CategoryHandler).Categories){
                if(cat.Name.Trim().Equals(CategoryName)){
                    category = cat;
                    break;
                }
            }
            if(category==null){
                throw new Exception("Could not find category: " + CategoryName);
            }
            string applicationCodes="";
            foreach(Application app in category.Applications)
            {
                applicationCodes+=app.Code+",";
            }
            applicationCodes=applicationCodes.TrimEnd(new char[]{','});
            CalendarViewModel cvm=CreateCalenderViewModel(applicationCodes);
            cvm.Description = category.Description;
            if (cvm.lectures.Count > 0)
                return PartialView(ViewName, cvm);
            return PartialView("NoLectures");
        }

        [HttpPost]
        public ActionResult PartialMonth(string applicationCode)
        {
            if (!string.IsNullOrEmpty(applicationCode))
                applicationCode = applicationCode.Trim(new char[] { '\'' });
            return CreateCalendarView("PartialMonth", applicationCode);
        }

        public ActionResult PartialMonthCategory(string id)
        {
            return CreateCalenderViewByCategory("PartialMonth", id);
        }
       
        public ActionResult PartialAgendaCategory(string id)
        {
            return CreateCalenderViewByCategory("PartialAgenda", id);
        }


        [HttpPost]
        public ActionResult PartialAgenda(string applicationCode)
        {
            if (!string.IsNullOrEmpty(applicationCode))
                applicationCode = applicationCode.Trim(new char[] { '\'' });
            return CreateCalendarView("PartialAgenda", applicationCode);
        }
    }
}
