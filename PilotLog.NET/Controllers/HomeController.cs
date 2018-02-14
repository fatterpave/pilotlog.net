using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections;
using PilotLog.NET.Models;

namespace PilotLog.NET.Controllers
{
    public class HomeController : Controller
    {
        private LogContext db = new LogContext();

        public ActionResult Index()
        {
            string path = Request.Path;
            ViewBag.Message = "Melding";

            return View();
        }

        public JsonResult GetICAOList()
        {
            var logItems = from l in db.Log
                           select l.arr_airport;
            logItems = logItems.Distinct();

            return Json(logItems,JsonRequestBehavior.AllowGet);
        }

        public ActionResult LogFilterICAO(string icao)
        {
            if ("Alle".Equals(icao)) return PartialView("LogList",db.Log.ToList());

            var logItems = from l in db.Log
                           where l.dep_airport == icao orderby l.arr_airport==icao
                           select l;

            return PartialView("Loglist",logItems);
        }

        public ActionResult LogSortDate(bool asc)
        {
            var logItems = asc ? db.Log.OrderBy(o => o.date).ToList() : db.Log.OrderByDescending(o => o.date).ToList();
            return PartialView("Loglist", logItems);
        }

        public ActionResult LogFilterCallsign(string callsign)
        {           
            var logItems = from l in db.Log 
                                  where l.aircraft_callsign==callsign 
                                  select l;

            if ("Alle" == callsign) logItems = from row in db.Log select row;


            ViewBag.logItemsCount = logItems.Count();

            return PartialView("Loglist",logItems);
        }

        public ActionResult LogFilter(string callsign, string icao)
        {
            var logItems = from l in db.Log
                           where l.aircraft_callsign == callsign
                           select l;
            if ("Alle" == callsign) logItems = from row in db.Log select row;

            if ("Alle" == icao) logItems = from row in logItems select row;
            else logItems = from l in logItems
                       where l.arr_airport == icao orderby l.arr_airport == icao
                       select l;

            ViewBag.logItemsCount = logItems.Count();
            return PartialView("LogList", logItems);
        }

        //MÅTTE GJØRE DETTE:
        //Database.SetInitializer<LogContext>(null); i Global.asax
        public ActionResult Log()
        {
            ViewBag.Message = "Logg";
            DbSet<Log> logItems = db.Log;
            /*
            SqlConnection myConnection = new SqlConnection("user id=fatter;password=hjallis;server=213.162.246.227;Trusted_Connection=no;database=pilotlog;connection timeout=30");
            myConnection.Open();

            SqlCommand myCommand = new SqlCommand("SELECT * FROM log",myConnection);
            SqlDataReader myReader = null;
            myReader = myCommand.ExecuteReader();
            while (myReader.Read())
            {
                Log piss = new Log();
                piss.aircraft_callsign = myReader["aircraft_callsign"].ToString();
                logItems.Add(piss);
            }
            */
            ViewBag.logItemsCount = logItems.Count();

            return View(logItems.OrderBy(o => o.date).ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Hold kjeft";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Kontakt";

            return View();
        }
    }
}
