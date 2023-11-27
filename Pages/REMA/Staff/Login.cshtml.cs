using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Environmental_Clreance_MGT.Pages.REMA.Staff
{
   
    public class LoginModel : PageModel
    {
       
        public String message = "";
        public void OnPost()
        {
            admininfo admins = new admininfo();
            admins.email = Request.Form["email"];
            admins.password = Request.Form["password"];
            admins.Rolet = Request.Form["roles"];
            if (admins.email.Length == 0 || admins.password.Length == 0 || admins.Rolet.Length == 0)
            {
                message = "ALL Fields Are Required";
                return;
            }
            try
                {
                if (admins.Rolet == "Admin")
                {
                    String conString = @"Data Source=MERIC\SQLEXPRESS;Initial Catalog=Einvironmental_DB;Integrated Security=True";
                    using (SqlConnection con = new SqlConnection(conString))
                    {
                        con.Open();
                        String sqlquery = "SELECT * FROM Admin where email=@email AND password=@password AND Role=@roles";
                        using (SqlCommand cmd = new SqlCommand(sqlquery, con))
                        {
                            cmd.Parameters.AddWithValue("@email", admins.email);
                            cmd.Parameters.AddWithValue("@password", admins.password);
                            cmd.Parameters.AddWithValue("@roles", admins.Rolet);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    Response.Redirect("/REMA/Applicant/adminDash");
                                }
                                else
                                {
                                    message = "Incorrect Username or Password For Admin";
                                    return;
                                }
                            }
                        }
                        con.Close();
                    }

                }
                else if (admins.Rolet == "Reviewer")
                {
                    ///////////////////////////////////////////////////////////staffuser///////////////////
                    String connnString = @"Data Source=MERIC\SQLEXPRESS;Initial Catalog=Einvironmental_DB;Integrated Security=True";
                    using (SqlConnection conn = new SqlConnection(connnString))
                    {
                        conn.Open();
                        String sql = "SELECT * FROM staff where email=@email AND password=@password AND Role=@roles";
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            cmd.Parameters.AddWithValue("@email", admins.email);
                            cmd.Parameters.AddWithValue("@password", admins.password);
                            cmd.Parameters.AddWithValue("@roles", admins.Rolet);
                            using (SqlDataReader readerd = cmd.ExecuteReader())
                            {
                                if (readerd.Read())
                                {

                                    Response.Redirect("/REMA/Staff/Reviewerdash");
                                    message = "Review Dashbvord";

                                }
                                else
                                {
                                    message = "Incorrect Username or Password For Reviewer";
                                    return;
                                }
                            }
                        }
                        conn.Close();

                    }
                }
                else
                {
                    ///////////////////////////////////////////////////////////staffuser///////////////////
                    String connString = @"Data Source=MERIC\SQLEXPRESS;Initial Catalog=Einvironmental_DB;Integrated Security=True";
                    using (SqlConnection connection = new SqlConnection(connString))
                    {
                        connection.Open();
                        String sql = "SELECT * FROM staff where email=@email AND password=@password AND Role=@roles";
                        using (SqlCommand cmd = new SqlCommand(sql, connection))
                        {
                            cmd.Parameters.AddWithValue("@email", admins.email);
                            cmd.Parameters.AddWithValue("@password", admins.password);
                            cmd.Parameters.AddWithValue("@roles", "Assessor");
                            using (SqlDataReader readerde = cmd.ExecuteReader())
                            {
                                if (readerde.Read())
                                {

                                    Response.Redirect("/REMA/Staff/Assessordash");
                                    message = "Review Dashbvord";

                                }
                                else
                                {
                                    message = "Incorrect Username or Password For Assessor";
                                    return;
                                }
                            }
                        }
                        connection.Close();
                    }






                }


                    
                
                }
                catch (Exception ex)
                {
                message=ex.Message; return;

                }
            
        }


        public class admininfo
        {
            public String email;
            public String password;
            public String Rolet;
        }
        
    }
}
