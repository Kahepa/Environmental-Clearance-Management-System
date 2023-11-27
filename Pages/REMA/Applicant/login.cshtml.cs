using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Environmental_Clreance_MGT.Pages.REMA.Applicant
{
    public class loginModel : PageModel
    {
        
        public String message = "";
        public String id;
        public String email;
        public String phonenumber;
        public void OnGet()
        {
        }
        public void OnPost() {
            applicalogin applicalogin = new applicalogin();
            applicalogin.email = Request.Form["email"];
            applicalogin.password = Request.Form["password"];

            if(applicalogin.email.Length==0 || applicalogin.password.Length == 0)
            {
                message = "All Field are Required";
                return;

            }

            try
            {
                String conString = @"Data Source=MERIC\SQLEXPRESS;Initial Catalog=Einvironmental_DB;Integrated Security=True";
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    String sqlquery = "SELECT * FROM applicant where email=@email AND password=@password";
                    using (SqlCommand cmd = new SqlCommand(sqlquery, con))
                    {
                        cmd.Parameters.AddWithValue("@email", applicalogin.email);
                        cmd.Parameters.AddWithValue("@password", applicalogin.password);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                id = ""+reader.GetInt32(0);
                                email = reader.GetString(3);

                                
                                Response.Redirect($"/REMA/Applicant/request?id={id}&email={email}");
               
                            }
                            else
                            {
                                message = "Incorrect Username or Password";
                                return;
                            }
                        }
                    }
                }

                }catch(Exception ex) { message = ex.Message;}
        }
    }

    public class applicalogin
    {
        public String email;
        public String password;  
    } 
}

