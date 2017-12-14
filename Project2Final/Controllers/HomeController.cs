using Project2Final.DAL;
using Project2Final.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Project2Final.Controllers
{
    public class HomeController : Controller
    {
        //link the database to this controller
        private MissionsContext db = new MissionsContext();

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

        //this method returns the login page
        public ActionResult Login()
        {
            return View();
        }

        //this method searches the databasw for a user with matching credentials to determine if they can login
        [HttpPost]
        public ActionResult Login(FormCollection form, bool rememberMe = false)
        {
            //get the email and password the user typed from the form
            String email = form["Email address"].ToString();
            String password = form["Password"].ToString();

            var findUserAndPass = "SELECT * FROM [User] WHERE userEmail = '" + email + "' AND password = '" + password + "'";

            //search the database for a matching username and password
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MissionsContext"].ConnectionString);
            con.Open();

            SqlCommand cmd = new SqlCommand(findUserAndPass, con);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0) //if the username and password was found in the database, login
            {
                FormsAuthentication.SetAuthCookie(email, rememberMe);

                return RedirectToAction("Index", "Home");

            }
            else //username and password was not found, return to login page with error message
            {
                ViewBag.LoginErrorMsg = "Invalid Login Credentials. Please try again.";
                return View();
            }
        }


    }
}