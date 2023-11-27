using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Environmental_Clreance_MGT.Pages.REMA.Applicant
{

    public class requestModel : PageModel
    {
        public String message = "";
        public String id;
        public String email;
        public String phone;
        public void OnGet()
        {
            if (Request.Query.TryGetValue("id", out var userId) &&
                Request.Query.TryGetValue("email", out var userEmail))
            {
                 id = userId;
                 email = userEmail;
            }
        }
        public void OnPost() {
            OnGet();
            requestinfo requestdata= new requestinfo();
            requestdata.request = Request.Form["request"];
            requestdata.status = "Pending";
            if (requestdata.request.Length == 0)
            {
                message = "All Fields Are Request";
            }

            try
            {
                String conString = @"Data Source=MERIC\SQLEXPRESS;Initial Catalog=Einvironmental_DB;Integrated Security=True";
                using (SqlConnection conn = new SqlConnection(conString))
                {
                    conn.Open();
                    String sqlinsert = "INSERT INTO Request(applicantid,applicantemail,request,status)values(@applicantid,@applicantemail,@request,@status)";
                    using (SqlCommand cmd = new SqlCommand(sqlinsert, conn))
                    {
                        cmd.Parameters.AddWithValue("@applicantid", id);
                        cmd.Parameters.AddWithValue("@applicantemail",email);
                        cmd.Parameters.AddWithValue("@request", requestdata.request);
                        cmd.Parameters.AddWithValue("@status", requestdata.status);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return;
            }
            message = "Request Sented";
            requestdata.request = "";
        }
    }
    public class requestinfo
    {
        public string applicantid;
        public string applicantemail;
        public string request;
        public String status;
    }
}
