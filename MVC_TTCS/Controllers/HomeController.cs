using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace MVC_TTCS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        //public void connectt()
        //{
        //    SqlConnection con = new SqlConnection();
        //    String tmp_connect = "Data Source=.;Initial Catalog=QLVT;Integrated Security=True ";
        //    String tmp_connect = "Data Source=.;Initial Catalog=QLVT;us ";
        //    con.ConnectionString = tmp_connect;
        //}


        //    public void connectt()
        //{
        //    SqlConnection con = new SqlConnection();
        //    String tmp_connect = "Data Source=DESKTOP-00N7FTR;User ID=sa;Password=123";
        //    con.ConnectionString = tmp_connect;
        //}
                 

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