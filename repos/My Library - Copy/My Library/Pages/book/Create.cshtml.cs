using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace My_Library.Pages.book
{
    public class CreateModel : PageModel
    {
        public Clientinfo clientInfo = new Clientinfo();
		public string errorMessage="";
        public string successMessage = "";

		public void OnGet()
        {
        }
        public void OnPost() {
            clientInfo.Reg_Number = Request.Form["Reg_Number"];
            clientInfo.name = Request.Form["name"];
            clientInfo.bookname = Request.Form["bookname"];
            clientInfo.phone = Request.Form["phone"];
            clientInfo.ReturnDate = Request.Form["ReturnDate"];
            if(clientInfo.Reg_Number.Length== 0|| clientInfo.name.Length==0 || 
                clientInfo.bookname.Length==0||clientInfo.phone.Length==0) {
                errorMessage = "All the Fields are required";
                return;
            }

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=My_library;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                     connection.Open();
                    String sql = "INSERT INTO book " +
                                "(Reg_Number,name,bookname,phone,ReturnDate) VALUES" +
                                "(@Reg_Number,@name,@bookname,@phone,@ReturnDate);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Reg_Number", clientInfo.Reg_Number);
                        command.Parameters.AddWithValue ("@name", clientInfo.name);
                        command.Parameters.AddWithValue("@bookname", clientInfo.bookname);
                        command.Parameters.AddWithValue("@phone", clientInfo.phone);
                        command.Parameters.AddWithValue("@ReturnDate", clientInfo.ReturnDate);

                        command.ExecuteNonQuery();
                    }


				}
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }


            clientInfo.Reg_Number = ""; clientInfo.name = "";clientInfo.bookname = "";
            clientInfo.phone = ""; clientInfo.ReturnDate = ""; clientInfo.IssueDate = "";
            successMessage = "New Client Added Successfully";
            Response.Redirect("/book/index");
        }
    }
}
