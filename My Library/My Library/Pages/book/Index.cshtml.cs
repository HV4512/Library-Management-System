using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace My_Library.Pages.book
{
    public class IndexModel : PageModel
    {
        public List<Clientinfo> listClients = new List<Clientinfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=My_library;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM book";
                    using (SqlCommand command =new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while(reader.Read())
                            {
                                Clientinfo clientinfo = new Clientinfo();
                                clientinfo.Sl_no = "" + reader.GetInt32(0);
                                clientinfo.Reg_Number=reader.GetString(1);
                                clientinfo.name=reader.GetString(2);
                                clientinfo.bookname=reader.GetString(3);
                                clientinfo.phone=reader.GetString(4);
                                clientinfo.IssueDate=reader.GetDateTime(5).ToString();
                                clientinfo.ReturnDate=reader.GetString(6);
                                listClients.Add(clientinfo);

                            }
                        }
                    }
                }
            }
            catch(Exception ex) 
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }

    public class Clientinfo
    {
        public string Sl_no;
        public string Reg_Number;
        public string name;
        public string bookname;
        public string phone;
        public string IssueDate;
        public string ReturnDate;
    }
}
