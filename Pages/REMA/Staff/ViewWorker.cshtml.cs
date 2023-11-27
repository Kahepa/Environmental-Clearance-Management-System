using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Environmental_Clreance_MGT.Pages.REMA.Staff
{
    
    public class ViewWorkerModel : PageModel
    {
        public List<Staffinfo> listinfo = new List<Staffinfo>();
        public void OnGet()
        {
            listinfo.Clear();
            try
            {
                String conString = @"Data Source=MERIC\SQLEXPRESS;Initial Catalog=Einvironmental_DB;Integrated Security=True";
                using (SqlConnection conn = new SqlConnection(conString))
                {
                    conn.Open();
                    String sqlquery = "SELECT * FROM staff";
                    using (SqlCommand cmd = new SqlCommand(sqlquery, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                               Staffinfo staffInfo = new Staffinfo();
                                staffInfo.id = "" + reader.GetInt32(0);
                                staffInfo.fullname = reader.GetString(1);
                                staffInfo.email = reader.GetString(2);
                                staffInfo.phone = reader.GetString(3);
                                staffInfo.Role = reader.GetString(5);
                                staffInfo.level_of_education= reader.GetString(6);

                                listinfo.Add(staffInfo);
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
    }
    }

