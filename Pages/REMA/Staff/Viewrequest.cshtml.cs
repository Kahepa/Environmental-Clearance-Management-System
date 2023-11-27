using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Runtime;

namespace Environmental_Clreance_MGT.Pages.REMA.Staff
{
  
    public class ViewrequestModel : PageModel
    {
     public List<requestinf> Listrequestinfo = new List<requestinf>();
        public void OnGet()
        {

            Listrequestinfo.Clear();
            try
            {
                String conString = @"Data Source=MERIC\SQLEXPRESS;Initial Catalog=Einvironmental_DB;Integrated Security=True";
                using (SqlConnection conn = new SqlConnection(conString))
                {
                    conn.Open();
                    String sqlquery = "SELECT * FROM request where status='Pending'";
                    using (SqlCommand cmd = new SqlCommand(sqlquery, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                requestinf requestin= new requestinf();
                                requestin.applicantid = reader.GetString(0);
                                requestin.applicantemail = reader.GetString(1);
                                requestin.request = reader.GetString(2);
                                requestin.status = "New";

                                Listrequestinfo.Add(requestin);
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
    public class requestinf
    {
        public String applicantid;
        public String applicantemail;
        public String request;
        public String status;
    }
}
