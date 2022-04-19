using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Mvc;
using MVC_TTCS.Models;
using System.Data;


namespace MVC_TTCS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            DataTable table = getdata("select object_id as Id, name as Name from sys.tables where name <> 'sysdiagrams' and is_ms_shipped <> 1");
            DataTable colunm = getdata("SELECT C.name AS Name, C.object_id as SubId FROM sys.objects AS T JOIN sys.columns AS C ON T.object_id = C.object_id WHERE T.type_desc = 'USER_TABLE' and T.is_ms_shipped <> 1 and C.name <> 'rowguid' and T.name <> 'sysdiagrams'");
            List<Models.ModelTable> tables = new List<ModelTable>();
            List<Models.ModelColunm> columns = new List<ModelColunm>();
           // Models.ModelTable mdtable = new ModelTable();
            if (table != null && table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    mdtable.tables.Add(row["Name"].ToString());
                    mdtable.id.Add(Int32.Parse(row["Id"].ToString()));
                    //tables.Add();
                    //id = Int32.Parse(row["Id"].ToString())
                }
            }

            
            Models.ModelTable md = new ModelTable();      
            return View(md);
        }
        //public void connectt()
        //{
        //    SqlConnection con = new SqlConnection();
        //    String tmp_connect = "Data Source=.;Initial Catalog=QLVT;Integrated Security=True ";
        //    String tmp_connect = "Data Source=.;Initial Catalog=QLVT;us ";
        //    con.ConnectionString = tmp_connect;
        //}

        public DataTable getdata(string query)
        {
            SqlConnection con = connectt();
            DataTable dt = new DataTable();
            SqlDataAdapter Adpt = new SqlDataAdapter(query, con);
            try
            {
                Adpt.Fill(dt);
            }
            catch (SqlException ex)
            {
                dt = null;
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            finally
            {
                if (con != null)
                    if (con.State == ConnectionState.Open) con.Close();
                Adpt.Dispose();
            }
            return dt;
        }
        public SqlConnection connectt()
        {
            SqlConnection con = new SqlConnection();
            String tmp_connect = "Data Source=.;Initial Catalog=QLVT;Integrated Security=True";
            con.ConnectionString = tmp_connect;
            return con;
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