using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Environmental_Clreance_MGT.Pages.REMA.Staff
{

    public class StaffRegisterModel : PageModel
    {
        
        public String ermessage = "";
        public void OnPost()
        {
            Staffinfo Staffs = new Staffinfo();
            Staffs.fullname = Request.Form["fullname"];
            Staffs.email = Request.Form["email"];
            Staffs.phone = Request.Form["phone"];
            Staffs.password = Request.Form["pwd"];
            Staffs.Role = Request.Form["type"];
            Staffs.level_of_education = Request.Form["level"];
            Staffs.confrimpassword = Request.Form["confrimpassword"];
           

            if (Staffs.fullname.Length == 0 ||Staffs.password.Length==0|| Staffs.email.Length==0|| Staffs.phone.Length==0)
            {
                ermessage = "All field Are Required";
                return;
            }else if (Staffs.password !=Staffs.confrimpassword)
            {
                ermessage = "Password are not Matches try Again";
                return;
            }
           
                try
                {
                    String conString = @"Data Source=MERIC\SQLEXPRESS;Initial Catalog=Einvironmental_DB;Integrated Security=True";
                    using (SqlConnection conn = new SqlConnection(conString))
                    {
                        conn.Open();
                        String sqlinsert = "INSERT INTO staff(fullname,email,phone,password,role,level_of_education)values(@fullname,@email,@phone,@password,@role,@level_of_education)";
                        using (SqlCommand cmd = new SqlCommand(sqlinsert, conn))
                        {
                        cmd.Parameters.AddWithValue("@fullname", Staffs.fullname);
                        cmd.Parameters.AddWithValue("@email",Staffs.email);
                        cmd.Parameters.AddWithValue("@phone",Staffs.phone);
                        cmd.Parameters.AddWithValue("@password", Staffs.password);
                        cmd.Parameters.AddWithValue("@role", Staffs.Role);
                        cmd.Parameters.AddWithValue("@level_of_education", Staffs.level_of_education);
                        cmd.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    ermessage = ex.Message;
                    return;
                }

                ermessage = "Recorded Worker Successful";
                Staffs.fullname = "";
                Staffs.email = "";
                Staffs.phone = "";
                Staffs.Role = "";
                Staffs.level_of_education = "";
                Staffs.password = "";

            

        }
    }
    public class Staffinfo
    {
        public String id;
        public String fullname;
        public String email;
        public String phone;
        public String password;
        public String confrimpassword;
        public String Role;
        public String level_of_education;
    }
}
