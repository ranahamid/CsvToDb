using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Web;
using System.Web.Mvc;

namespace CSVWebApplication1.Controllers
{
    public class HomeController : Controller
    {
        
        private int num_rows =0;
        private int num_cols = 0;
        public ActionResult Index()
        {
   
         
            SqlConnection conn = null;
            SqlDataReader rdr = null;
            conn = new SqlConnection(@"Data Source=ATOMWS2012;Initial Catalog=FXTFPlusDev;User ID=devfxtf;Password=dev123456;Persist Security Info=false;");
            conn.Open();

            string[,] values = LoadCsv();
            for (int r = 0; r < num_rows; r++)
            {
                string[] temp=new string[num_cols];
                for (int c = 0; c < num_cols; c++)
                {
                      temp[c] = values[r, c].ToString();
                }

                int EmployeeID = int.Parse(temp[1]);
                string  UserName = temp[2];
                string Email = temp[3];
                string Password = temp[4];
                int IsChangedPassword = int.Parse(temp[5]);
                string lkpUserTypeID = temp[6];
                DateTime CreatedAt = DateTime.Parse(temp[7]);;
                int CreatedUserID = int.Parse(temp[8]);

                string UpdatedAt= temp[9]!= "NULL" ? temp[9] : "1/1/1900";
                int UpdatedUserID = temp[10] != "NULL" ? int.Parse(temp[10]):0;
                
                /*
                int EmployeeID = 18888;
                string UserName = "s";
                string Email = "temssp[3]@slk.co";
                string Password = "temp[4]";
                int IsChangedPassword = int.Parse("787");
                string lkpUserTypeID = "sf";
                DateTime CreatedAt = DateTime.Parse("1/1/2010"); ;
                int CreatedUserID = int.Parse("45");
                string UpdatedAt = "1/1/1900";
                int UpdatedUserID = int.Parse("4");
                */

                SqlCommand cmdUser = new SqlCommand("sp_UserInfo3", conn);
                cmdUser.CommandType = CommandType.StoredProcedure;
                cmdUser.Parameters.AddWithValue("@EmployeeID", EmployeeID);
                cmdUser.Parameters.AddWithValue("@UserName", UserName);
                cmdUser.Parameters.AddWithValue("@Email", Email);
                cmdUser.Parameters.AddWithValue("@Password", Password);
                cmdUser.Parameters.AddWithValue("@CreatedAt", CreatedAt);
                cmdUser.Parameters.AddWithValue("@CreatedUserID", CreatedUserID);
                cmdUser.Parameters.AddWithValue("@UpdatedAt", UpdatedAt);
                cmdUser.Parameters.AddWithValue("@UpdatedUserID", UpdatedUserID);
                cmdUser.Parameters.Add("@Msg", SqlDbType.NChar, 500);
                cmdUser.Parameters["@Msg"].Direction = ParameterDirection.Output;
                cmdUser.Parameters.AddWithValue("@pOptions", 1);
                cmdUser.ExecuteNonQuery();
                string s = (string)cmdUser.Parameters["@Msg"].Value;
            }
            conn.Close();
            return View();
        }

        // Load a CSV file into an array of rows and columns.
        // Assume there may be blank lines but every line has
        // the same number of fields.
        private string[,] LoadCsv()
        {
            string loc = System.Web.HttpContext.Current.Server.MapPath("~/CSV/UserInfo.csv");
            string whole_file = System.IO.File.ReadAllText(loc);
            whole_file = whole_file.Replace('\n', '\r');
            string[] lines = whole_file.Split(new char[] { '\r' },
            StringSplitOptions.RemoveEmptyEntries);
            num_rows = lines.Length;
            num_cols = lines[0].Split(',').Length;

  
            string[,] values = new string[num_rows, num_cols];
            for (int r = 0; r < num_rows; r++)
            {
                string[] line_r = lines[r].Split(',');
                for (int c = 0; c < num_cols; c++)
                {
                    values[r, c] = line_r[c];
                }
            }
            return values;
        }

    }

}
/*
select
       LOGIN                 Login                      ,
       GROUPS                Groups                     ,
       ENABLE                Enable                     ,
       ENABLECHANGEPASS      Enablechangepass           ,
       ENABLEREADONLY        Enablereadonly             ,
       PASSWORDPHONE         Passwordphone              ,
       NAME                  Name                       ,
       COUNTRY               Country                    ,
       CITY                  City                       ,
       STATE                 State                      ,
       ZIPCODE               Zipcode                    ,
       ADDRESS               Address                    ,
       PHONE                 Phone                      ,
       EMAIL                 Email                      ,
       COMMENT               Comment                    ,
       ID                    Id                         ,
       STATUS                Status                     ,
       REGDATE               Regdate                    ,
       LASTDATE              Lastdate                   ,
       LEVERAGE              Leverage                   ,
       AGENT_ACCOUNT         Tgent_account              ,
       TIMESTAMP             Timestamp                  ,
       BALANCE               Balance                    ,
       PREVMONTHBALANCE      Prevmonthbalance           ,
       PREVBALANCE           Prevbalance                ,
       CREDIT                Credit                     ,
       ERESTRATE             Erestrate                  ,
       TAXES                 Taxes                      ,
       SEND_REPORTS          Send_reports               ,
       MQID                  Mqid                       ,
       USER_COLOR            User_color                 ,
       EQUITY                Equity                     ,
       MARGIN                Margin                     ,
       MARGIN_LEVEL          Margin_level               ,
       MARGIN_FREE           Margin_free                ,
       CURRENCY              Currency                   ,
       API_DATA              Api_data                   ,
       MODIFY_TIME           Modify_time                ,
       ENABLE_OTP            Enable_otp                 ,
       LEAD_SOURCE           Lead_source                ,
       
 */