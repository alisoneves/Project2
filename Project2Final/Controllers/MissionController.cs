using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project2Final.DAL;
using Project2Final.Models;

namespace Project2Final.Controllers
{
    public class MissionController : Controller
    {
        private MissionsContext db = new MissionsContext();
        // GET: Mission
        public ActionResult Index()
        {
            //returns all missions in a list from the database
            var missions = db.Missions;
            return View(missions.ToList());
        }

        [Authorize]
        public ActionResult Detail(int? id)
        {
            //returns a single view for the detail page
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //finds the mission based on the id
            Mission mission = db.Missions.Find(id);
            if (mission == null)
            {
                return HttpNotFound();
            }

            IEnumerable<Mission> missions = db.Database.SqlQuery<Mission>("SELECT * FROM Mission WHERE ID = " + id + "");
            IEnumerable<MissionQuestions> questions = db.Database.SqlQuery<MissionQuestions>("SELECT * FROM MissionQuestions WHERE missionID = " + id + "");

            ViewBag.missions = missions;
            ViewBag.questions = questions;

            return View(mission);
        }
        
        [HttpPost]
        //public ActionResult SubmitNewQuestion()
        public ActionResult SubmitNewQuestion(FormCollection form)
        {
            //int missionID = ViewBag.missionID;

            string newQuestion = form["question"].ToString();
            var usersEmail = TempData["email"].ToString(); //is the email not the userID?
            string missionID = form[1].ToString();

            db.Database.ExecuteSqlCommand("INSERT INTO MissionQuestions VALUES(null, " + newQuestion + ", null, " + usersEmail + ", " + missionID + ")");
            db.SaveChanges();

            //Mission mission = db.Missions.Find(missionID);
            //if (mission == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(mission);
            return RedirectToAction("Index", "Mission");
        }

    }
}