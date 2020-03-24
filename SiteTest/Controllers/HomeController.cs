using SiteTest.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SiteTest.Controllers
{
    public class HomeController : Controller
    {
        static SqlConnection Con;
        static SqlCommand Cmd;        
        static SqlDataAdapter SqlDataAdapter;
        static string ConString, Query;
        static DataTable DataTable;
        public static bool DbConnection()
        {
            try
            {                
                ConString = @"Data Source=DESKTOP-3UFDSE4\ANURAGSQL;Initial Catalog=DBModel;User ID=sa;Password=8299156008";             
                Con = new SqlConnection(ConString);
                Con.Open();
                Con.Close();
                return true;
            }
            catch (Exception ex)
            {
                Con.Close();
                return false;
            }
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Create(UserModel data)
        {
            if (DbConnection())
            {
                Query = "insert into tbl_User values(@UserName,@Password)";
                Cmd = new SqlCommand(Query, Con);                
                Cmd.Parameters.AddWithValue("UserName", data.UserName);
                Cmd.Parameters.AddWithValue("Password", data.Password);                
                Con.Open();
                Cmd.ExecuteNonQuery();
                Con.Close();
                return Json("Inserted", JsonRequestBehavior.AllowGet);
            }    
            return Json(null,JsonRequestBehavior.AllowGet);
        }
        public ActionResult Update(int ID)
        {           
            return View();
        }
        public JsonResult GetDataByID(int ID)
        {           
            if (DbConnection())
            {
                List<UserModel> listrs = new List<UserModel>();
                DataTable = new DataTable();
                Query = "select * from tbl_User where ID=" + ID + " ";
                Cmd = new SqlCommand(Query, Con);
                SqlDataAdapter = new SqlDataAdapter(Cmd);
                SqlDataAdapter.Fill(DataTable);
                foreach (DataRow dr in DataTable.Rows)
                {
                    listrs.Add(new UserModel
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        UserName = dr["UserName"].ToString(),
                        Password = dr["Password"].ToString(),
                    });
                }
                return Json(listrs, JsonRequestBehavior.AllowGet);
            }
            return Json(null);
        }
        public JsonResult UpdateData(UserModel data)
        {
            if (DbConnection())
            {                
                Query = "update tbl_User set UserName='"+data.UserName+ "', Password='"+data.Password+"' where ID="+data.ID+" ";
                Cmd = new SqlCommand(Query, Con);                
                Con.Open();
                Cmd.ExecuteNonQuery();
                Con.Close();
                return Json("Updated", JsonRequestBehavior.AllowGet);
            }
            return Json(null,JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(int ID)
        {
            if (DbConnection())
            {
                UserModel data = new UserModel();
                Query = "delete from tbl_User where ID=" + ID + " ";
                Cmd = new SqlCommand(Query, Con);
                Con.Open();
                Cmd.ExecuteNonQuery();
                Con.Close();
                return Json("Deleted", JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Show()
        {
            return View();
        }
        public JsonResult Get_data()
        {
            if (DbConnection())
            {               
                List<UserModel> listrs = new List<UserModel>();
                DataTable = new DataTable();
                Query = "select * from tbl_User";
                Cmd = new SqlCommand(Query, Con);
                SqlDataAdapter = new SqlDataAdapter(Cmd);
                SqlDataAdapter.Fill(DataTable);
                foreach (DataRow dr in DataTable.Rows)
                {
                    listrs.Add(new UserModel
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        UserName = dr["UserName"].ToString(),
                        Password = dr["Password"].ToString(),
                    });
                }
                return Json(listrs, JsonRequestBehavior.AllowGet);
            }
            return Json(null);
        }
    }
}
