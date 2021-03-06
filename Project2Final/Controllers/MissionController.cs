﻿using System;
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

            string userEmail = null;

               // userEmail = TempData["email"].ToString();
                userEmail = TempData.Peek("email").ToString();

            
            IEnumerable<Mission> missions = db.Database.SqlQuery<Mission>("SELECT * FROM Mission WHERE ID = " + id + "");
            IEnumerable<MissionQuestions> questions = db.Database.SqlQuery<MissionQuestions>("SELECT * FROM MissionQuestions WHERE missionID = " + id + "");

            ViewBag.missions = missions;
            ViewBag.questions = questions;
            ViewBag.userEmail = userEmail;

            return View(mission);
        }
        
        [HttpPost]
        //public ActionResult SubmitNewQuestion()
        public ActionResult SubmitNewQuestion(FormCollection form)
        {
            //int missionID = ViewBag.missionID;

            string newQuestion = form["question"].ToString();
            string usersEmail = form[2].ToString(); //is the email not the userID?
            string missionID = form[1].ToString();

            db.Database.ExecuteSqlCommand("INSERT INTO MissionQuestions VALUES('" + newQuestion + "', null, '" + usersEmail + "', '" + missionID + "')");
            db.SaveChanges();

            //Mission mission = db.Missions.Find(missionID);
            //if (mission == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(mission);
            return RedirectToAction("Detail", "Mission", new { id = missionID });
        }

        [HttpPost]
        public ActionResult SubmitReply(FormCollection form)
        {
            string userReply = form["reply"].ToString();
            string questionID = form[1].ToString();
            string missionID = form[2].ToString();

            var updateQuery = "UPDATE MissionQuestions SET answer = '" + userReply + "' WHERE missionQuestionID = " + questionID + "";

            db.Database.ExecuteSqlCommand(updateQuery);
            db.SaveChanges();
            return RedirectToAction("Detail", "Mission", new { id = missionID });
        }

    }
}