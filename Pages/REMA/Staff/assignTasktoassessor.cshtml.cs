using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Environmental_Clreance_MGT.Pages.REMA.Staff
{
    public class assignTasktoassessorModel : PageModel
    {
        public List<assesordata> Listassessor=new List<assesordata>();
        requestdata listrequestinfo = new requestdata();
        public String message="";
        public void OnGet()
        {
            assdata();
            String id = Request.Query["applicantid"];
            try
            {
                String conString = @"Data Source=MERIC\SQLEXPRESS;Initial Catalog=Einvironmental_DB;Integrated Security=True";
                using (SqlConnection conn = new SqlConnection(conString))
                {
                    conn.Open();
                    String sqlquery = "SELECT * FROM applicant where applicant=@id";
                    using (SqlCommand cmd = new SqlCommand(sqlquery, conn))
                    {
                        cmd.Parameters.AddWithValue("id", id);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                listrequestinfo.applicantid = reader.GetString(0);
                                listrequestinfo.applicantemail = reader.GetString(1);
                                listrequestinfo.phone = reader.GetString(5);
                                listrequestinfo.fullname = reader.GetString(1)+""+ reader.GetString(1);
                                
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception :" + ex.Message);
            }
        }

        public void assdata()

        {
            Listassessor.Clear();
            try
            {
                String conString = @"Data Source=MERIC\SQLEXPRESS;Initial Catalog=Einvironmental_DB;Integrated Security=True";
                using (SqlConnection conn = new SqlConnection(conString))
                {
                    conn.Open();
                    String sqlquery = "SELECT * FROM staff where role='Assessor'";
                    using (SqlCommand cmd = new SqlCommand(sqlquery, conn))
                    {
                        
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                assesordata assesordatas = new assesordata();
                                assesordatas.fullname = reader.GetString(1);
                                assesordatas.idnumber =""+ reader.GetInt32(0);
                                Listassessor.Add(assesordatas);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception :" + ex.Message);
            }
        }
        public void OnPost()
        {
            OnGet();
            task taskdata=new task();
            taskdata.location = Request.Form["location"];
            taskdata.roadnumber = Request.Form["roadnumber"];
            taskdata.date = Request.Form["startdate"];
            taskdata.taskname = Request.Form["task"];
            if (taskdata.location.Length == 0 || taskdata.roadnumber.Length == 0 || taskdata.date.Length == 0 || taskdata.taskname.Length == 0)
            {
                message = "All Fields Are Required";
            }
            try
            {
                String conString = @"Data Source=MERIC\SQLEXPRESS;Initial Catalog=Einvironmental_DB;Integrated Security=True";
                using (SqlConnection conn = new SqlConnection(conString))
                {
                    conn.Open();
                    String sqlinsert = "INSERT INTO task(taskname,applicantid,applicantname,applicantphone,location,roadnumber,date)values(@taskname,@applicantid,@applicantname,@applicantphone,@location,@roadnumber,@date)";
                    using (SqlCommand cmd = new SqlCommand(sqlinsert, conn))
                    {
                        cmd.Parameters.AddWithValue("@taskname", taskdata.taskname);
                        cmd.Parameters.AddWithValue("@applicantid", listrequestinfo.applicantid);
                        cmd.Parameters.AddWithValue("@applicantname", listrequestinfo.fullname);
                        cmd.Parameters.AddWithValue("@applicantphone", listrequestinfo.phone);
                        cmd.Parameters.AddWithValue("@location", taskdata.location);
                        cmd.Parameters.AddWithValue("@roadnumber", taskdata.roadnumber);
                        cmd.Parameters.AddWithValue("@date", taskdata.date);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return;
            }

            message = "Task Assigned";
            taskdata.location = "";
            taskdata.roadnumber = "";
            taskdata.date = "";
            taskdata.taskname = "";
        }

    }
    public class requestdata
    {
        public string applicantid;
        public String applicantemail;
        public String phone;
        public String fullname;

    }
   public class assesordata
    {
        public String idnumber;
        public String fullname;
    }

    public class task
    {
        public String taskname;
        public String applicantname;
        public String applicantid;
        public String applicantphone;
        public String location;
        public String roadnumber;
        public String date;

    }
}

