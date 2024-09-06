using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
//using System.Data.SqlClient;

namespace MyStore.Pages.Clients
{
    //public class EditModel : PageModel
    //{
    //    public ClientInfo clientInfo = new ClientInfo();
    //    public string errorMessage = "";
    //    public string successMessage = "";
    //    public void OnGet()
    //    {
    //        string id = Request.Query["id"];

    //        try
    //        {
    //            string connString = "Data Source=.\\sqlexpress;Initial Catalog=mystore;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
    //            using (SqlConnection connection = new SqlConnection(connString))
    //            {
    //                connection.Open();
    //                string sql = "SELECT * FROM clients WHERE id = @id";
    //                using (SqlCommand command = new SqlCommand(sql, connection)) 
    //                {
    //                    command.Parameters.AddWithValue("id", id);
    //                    using (SqlDataReader reader = command.ExecuteReader())
    //                    {
    //                        if (reader.Read())
    //                        {
    //                            //clientInfo.id = " " + reader.GetInt32(0);
    //                            clientInfo.id = reader.GetInt32(0).ToString();
    //                            clientInfo.name = reader.GetString(1);
    //                            clientInfo.email = reader.GetString(2);
    //                            clientInfo.phone = reader.GetString(3);
    //                            clientInfo.address = reader.GetString(4);
    //                        }
    //                    }
    //                }

    //            }
    //        }
    //        catch (Exception ex) {
    //            errorMessage = ex.Message;
    //        }
    //    }

    //    public IActionResult OnPost()
    //    {
    //        clientInfo.id = Request.Form["id"];
    //        clientInfo.name = Request.Form["name"];
    //        clientInfo.email = Request.Form["email"];
    //        clientInfo.phone = Request.Form["phone"];
    //        clientInfo.address = Request.Form["address"];

    //        if (string.IsNullOrEmpty(clientInfo.name) || string.IsNullOrEmpty(clientInfo.email) ||
    //            string.IsNullOrEmpty(clientInfo.phone) || string.IsNullOrEmpty(clientInfo.address))
    //        {
    //            errorMessage = "Fill all fields";
    //            return Page();
    //        }
    //        try
    //        {
    //            string connString = "Data Source=.\\sqlexpress;Initial Catalog=mystore;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
    //            using (SqlConnection connection = new SqlConnection(connString))
    //            {
    //                connection.Open();
    //                string sql = "UPDATE clients" + " SET name=@name, email=@email, phone=@phone, address=@address" + "WHERE ID =@ID ";
    //                using (SqlCommand command = new SqlCommand(sql, connection))
    //                {
    //                    command.Parameters.AddWithValue("@name", clientInfo.name);
    //                    command.Parameters.AddWithValue("@email", clientInfo.email);
    //                    command.Parameters.AddWithValue("@phone", clientInfo.phone);
    //                    command.Parameters.AddWithValue("@address", clientInfo.address);
    //                    command.Parameters.AddWithValue("@id", clientInfo.id);

    //                    command.ExecuteNonQuery();
    //                }

    //            }
    //        }
    //        catch (Exception ex) {

    //            errorMessage = ex.Message;

    //        }
    //        //Response.Redirect("/Clients/Index");
    //        return RedirectToPage("/Clients/Index");
    //    }
    //}
    public class EditModel : PageModel
    {
        private readonly string _connectionString = "Data Source=.\\sqlexpress;Initial Catalog=mystore;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
        public ClientInfo clientInfo = new ClientInfo();
        public string errorMessage = string.Empty;
        public string successMessage = string.Empty;

        public IActionResult OnGet(string id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("SELECT * FROM clients WHERE id = @id", connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                clientInfo.id = reader.GetInt32(0).ToString();
                                clientInfo.name = reader.GetString(1);
                                clientInfo.email = reader.GetString(2);
                                clientInfo.phone = reader.GetString(3);
                                clientInfo.address = reader.GetString(4);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            clientInfo.id = Request.Form["id"];
            clientInfo.name = Request.Form["name"];
            clientInfo.email = Request.Form["email"];
            clientInfo.phone = Request.Form["phone"];
            clientInfo.address = Request.Form["address"];

            if (string.IsNullOrWhiteSpace(clientInfo.name) || string.IsNullOrWhiteSpace(clientInfo.email) ||
                string.IsNullOrWhiteSpace(clientInfo.phone) || string.IsNullOrWhiteSpace(clientInfo.address))
            {
                errorMessage = "Fill all fields";
                return Page();
            }

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("UPDATE clients SET name=@name, email=@email, phone=@phone, address=@address WHERE ID = @id", connection))
                    {
                        command.Parameters.AddWithValue("@name", clientInfo.name);
                        command.Parameters.AddWithValue("@email", clientInfo.email);
                        command.Parameters.AddWithValue("@phone", clientInfo.phone);
                        command.Parameters.AddWithValue("@address", clientInfo.address);
                        command.Parameters.AddWithValue("@id", clientInfo.id);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            return RedirectToPage("/Clients/Index");
        }

        public ClientInfo ClientInfo { get => clientInfo; set => clientInfo = value; }
        public string ErrorMessage { get => errorMessage; set => errorMessage = value; }
        public string SuccessMessage { get => successMessage; set => successMessage = value; }
    }
}
