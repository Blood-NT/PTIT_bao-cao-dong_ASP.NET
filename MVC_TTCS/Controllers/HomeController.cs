using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Mvc;
using MVC_TTCS.Models;
using System.Data;
using MVC_TTCS.report;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting;

namespace MVC_TTCS.Controllers
{

    public class HomeController : Controller
    {
        String Id_Table = "table";
        String Id_Column = "column";
        String Type_column = "type";
        public ActionResult Index()
        {
            DataTable table = getdata("select object_id as Id, name as Name from sys.tables where name <> 'sysdiagrams' and is_ms_shipped <> 1");
            DataTable colunm = getdata("SELECT C.name AS Name, C.object_id as SubId FROM sys.objects AS T JOIN sys.columns AS C ON T.object_id = C.object_id WHERE T.type_desc = 'USER_TABLE' and T.is_ms_shipped <> 1 and C.name <> 'rowguid' and T.name <> 'sysdiagrams'");
            DataTable type = getdata("SELECT COLUMN_NAME, TABLE_NAME,DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS where TABLE_NAME in (select name from sys.tables where name <> 'sysdiagrams' and is_ms_shipped <> 1)");
            if (table != null && table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {

                    Id_Table += '_';
                    Id_Table += Int32.Parse(row["Id"].ToString());
                    Id_Table += '.';
                    Id_Table += row["name"].ToString();
                }
            };
            ViewBag.gettable = Id_Table;


            if (type != null && type.Rows.Count > 0)
            {
                foreach (DataRow row in type.Rows)
                {

                    Type_column += '_';
                    Type_column += row["TABLE_NAME"].ToString();
                    Type_column += '.';
                    Type_column += row["COLUMN_NAME"].ToString();
                    Type_column += '-';
                    Type_column += row["DATA_TYPE"].ToString();

                }
            };
            ViewBag.gettype = Type_column;




            if (colunm != null && colunm.Rows.Count > 0)
            {
                foreach (DataRow row in colunm.Rows)
                {

                    Id_Column += '_';
                    Id_Column += Int32.Parse(row["SubId"].ToString());
                    Id_Column += '.';
                    Id_Column += row["name"].ToString();
                }
            }
            ViewBag.getcolumn = Id_Column;

            //report

            return View();
        }

        [HttpPost]
        public ActionResult Index(gui_query QQ)
        {
            String strQuery = QQ.gui_Lenh_SQL;
            SqlConnection con = connectt();
            SqlCommand sqlcmd = new SqlCommand(strQuery, con);
            sqlcmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataAdapter Adpt = new SqlDataAdapter(strQuery, con);
            try
            {
                sqlcmd.ExecuteNonQuery();
                return RedirectToAction("reportt", new { message = strQuery });
            }
            catch (SqlException ex)
            {

                ViewBag.Error = ex.Message;
                con.Close();
                return View("Index");
            }


        }
        public ActionResult reportt(string message)
        {
            String qr = message;

            SqlConnection cnn = connectt();
            DataSet dt = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand(qr, cnn);
            da.Fill(dt);
            XtraReport1 xrp = new XtraReport1();
            xrp.DataSource = dt;
            CreateBands(xrp);
            InitializeBandsUsingXRTable(xrp);
            return View(xrp);
        }


        //report creat

        public void CreateBands(XtraReport1 report)
        {
            var detail = new DetailBand() { HeightF = 20 };
            var pageHeader = new PageHeaderBand() { HeightF = 20 };
            var reportFooter = new ReportFooterBand() { HeightF = 380 };
            report.Bands.AddRange(new Band[] { detail, pageHeader, reportFooter });
        }

        public void InitializeBandsUsingXRTable(XtraReport1 report)
        {
            var ds = (report.DataSource as DataSet);
            int colCount = ds.Tables[0].Columns.Count;
            int colWidth = (report.PageWidth - (report.Margins.Left + report.Margins.Right)) / colCount;

            // Create a table header.
            var tableHeader = new XRTable();
            tableHeader.Height = 20;
            tableHeader.Width = (report.PageWidth - (report.Margins.Left + report.Margins.Right));

            var headerRow = new XRTableRow();
            headerRow.Width = tableHeader.Width;
            tableHeader.Rows.Add(headerRow);

            tableHeader.BeginInit();

            // Create a table body.
            var tableBody = new XRTable();
            tableBody.Height = 20;
            tableBody.Width = (report.PageWidth - (report.Margins.Left + report.Margins.Right));

            var bodyRow = new XRTableRow();
            bodyRow.Width = tableBody.Width;
            tableBody.Rows.Add(bodyRow);
            tableBody.EvenStyleName = "EvenStyle";
            tableBody.OddStyleName = "OddStyle";

            tableBody.BeginInit();

            // Initialize table header and body cells.
            for (int i = 0; i < colCount; i++)
            {
                var headerCell = new XRTableCell();
                headerCell.Width = colWidth;
                headerCell.Text = ds.Tables[0].Columns[i].Caption;

                var bodyCell = new XRTableCell();
                bodyCell.Width = colWidth;
                bodyCell.DataBindings.Add("Text", null, ds.Tables[0].Columns[i].Caption);

                if (i == 0)
                {
                    headerCell.Borders = BorderSide.Left | BorderSide.Top | BorderSide.Bottom;
                    bodyCell.Borders = BorderSide.Left | BorderSide.Top | BorderSide.Bottom;
                }
                else
                {
                    headerCell.Borders = BorderSide.All;
                    bodyCell.Borders = BorderSide.All;
                }

                headerRow.Cells.Add(headerCell);
                bodyRow.Cells.Add(bodyCell);
            }

            tableHeader.EndInit();
            tableBody.EndInit();

            // Add the table header and body to the corresponding report bands.
            report.Bands[BandKind.PageHeader].Controls.Add(tableHeader);
            report.Bands[BandKind.Detail].Controls.Add(tableBody);
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
            String tmp_connect = "Data Source=.;Initial Catalog=QLVT;User ID=sa;Password=123";
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