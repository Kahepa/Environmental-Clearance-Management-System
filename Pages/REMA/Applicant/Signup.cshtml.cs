using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Environmental_Clreance_MGT.Pages.REMA.Applicant
{
    public class SignupModel : PageModel
    {
        public String message="";
        public void OnGet()
        {
        }
        public void OnPost() { 
        Applicantinfo applicantinfo = new Applicantinfo();
            applicantinfo.firstname = Request.Form["firstname"];
            applicantinfo.lastname = Request.Form["lastname"];
            applicantinfo.email = Request.Form["email"];
            applicantinfo.password = Request.Form["password"];
            applicantinfo.phone = Request.Form["phone"];
            applicantinfo.confirmpassword = Request.Form["confrimpassword"];

            if(applicantinfo.firstname.Length==0 ||applicantinfo.phone.Length==0|| applicantinfo.lastname.Length==0||applicantinfo.email.Length==0|| applicantinfo.password.Length == 0)
            {
                message = "All Fields Are Required";
                return;
            }else if(applicantinfo.password!= applicantinfo.confirmpassword)
            {
                message = "Password not Match";
                return;
            }

            try
            {
                String conString = @"Data Source=MERIC\SQLEXPRESS;Initial Catalog=Einvironmental_DB;Integrated Security=True";
                using (SqlConnection conn = new SqlConnection(conString))
                {
                    conn.Open();
                    String sqlinsert = "INSERT INTO applicant(firstname,lastname,email,password,phone)values(@firstname,@lastname,@email,@password,@phone)";
                    using (SqlCommand cmd = new SqlCommand(sqlinsert, conn))
                    {
                        cmd.Parameters.AddWithValue("@firstname", applicantinfo.firstname);
                        cmd.Parameters.AddWithValue("@lastname", applicantinfo.lastname);
                        cmd.Parameters.AddWithValue("@email", applicantinfo.email);
                        cmd.Parameters.AddWithValue("@phone", applicantinfo.phone);
                        cmd.Parameters.AddWithValue("@password", applicantinfo.password);
                       
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return;
            }
            message = "Registered Successfull";
            applicantinfo.firstname = "";
            applicantinfo.lastname = "";
            applicantinfo.email = "";
            applicantinfo.password = "";
            applicantinfo.phone = "";
            applicantinfo.confirmpassword = "";


        }    

    }
    public class Applicantinfo
    {
        public String firstname;
        public String lastname;
        public String email;
        public String phone;
        public String password;
        public String confirmpassword;
    }
}
