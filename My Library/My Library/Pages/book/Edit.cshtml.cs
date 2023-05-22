using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace My_Library.Pages.book
{
	public class EditModel : PageModel
	{
		public Clientinfo clientinfo = new Clientinfo();
		public String errorMessage = "";
		public String successMessage = "";
		public void OnGet()
		{
			String id = Request.Query["Sl_no"];

			try
			{

				String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=My_library;Integrated Security=True";

				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					String sql = "SELECT * FROM book WHERE id = @Sl_no";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@Sl_no", id);
						using (SqlDataReader reader = command.ExecuteReader())
						{
							if (reader.Read())
							{

								clientinfo.Sl_no = "" + reader.GetInt32(0);
								clientinfo.Reg_Number = reader.GetString(1);
								clientinfo.name = reader.GetString(2);
								clientinfo.bookname = reader.GetString(3);
								clientinfo.phone = reader.GetString(4);
								clientinfo.IssueDate = reader.GetDateTime(5).ToString();
								clientinfo.ReturnDate = reader.GetString(6);

							}
						}
					}
				}

			}


			catch (Exception ex)
			{
				errorMessage = ex.Message;
			}
		}


		public void OnPost()
		{
			clientinfo.Sl_no = Request.Form["Sl_no"];
			clientinfo.Reg_Number = Request.Form["Reg_Number"];
			clientinfo.name = Request.Form["name"];
			clientinfo.bookname = Request.Form["bookname"];
			clientinfo.phone = Request.Form["phone"];
			clientinfo.ReturnDate = Request.Form["ReturnDate"];

			if (clientinfo.Sl_no.Length == 0 || clientinfo.Reg_Number.Length == 0 || clientinfo.name.Length == 0 ||
				clientinfo.bookname.Length == 0 || clientinfo.phone.Length == 0)
			{
				errorMessage = "All the Fields are required";
				return;
			}

			try
			{
				String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=My_library;Integrated Security=True";
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					String sql = "UPDATE book" +
								"SET Reg_Number=@Reg_Number, name=@name, bookname=@bookname, phone =@phone, ReturnDate=@ReturnDate" +
								"WHERE Sl_no =@Sl_no";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@Reg_Number", clientinfo.Reg_Number);
						command.Parameters.AddWithValue("@name", clientinfo.name);
						command.Parameters.AddWithValue("@bookname", clientinfo.bookname);
						command.Parameters.AddWithValue("@phone", clientinfo.phone);
						command.Parameters.AddWithValue("@ReturnDate", clientinfo.ReturnDate);
						command.Parameters.AddWithValue("@Sl_no", clientinfo.Sl_no);

						command.ExecuteNonQuery();
					}


				}

			}


			catch (Exception ex)
			{
					errorMessage = ex.Message;
				return;

			}
			Response.Redirect("/book/index");

		}
	}
}
