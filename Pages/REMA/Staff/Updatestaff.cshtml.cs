using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Environmental_Clreance_MGT.Pages.REMA.Staff
{
    public class UpdatestaffModel : PageModel
    {
        public Staffinfo staffInfo = new Staffinfo();
        public String errmessage="";
        public void OnGet()
        {
            
            String id = Request.Query["id"];
            try
            {
                String conString = @"Data Source=MERIC\SQLEXPRESS;Initial Catalog=Einvironmental_DB;Integrated Security=True";
                using (SqlConnection conn = new SqlConnection(conString))
                {
                    conn.Open();
                    String sqlinsert = "Select * from staff where id=@id";
                    using (SqlCommand cmd = new SqlCommand(sqlinsert, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                staffInfo.id = "" + reader.GetInt32(0);
                                staffInfo.fullname = reader.GetString(1);
                                staffInfo.phone = reader.GetString(3);
                                staffInfo.Role = reader.GetString(5);
                                staffInfo.level_of_education = reader.GetString(6);
                            }
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                errmessage = ex.Message;
                return;
            }
        }
    }
}
