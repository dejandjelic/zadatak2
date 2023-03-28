
using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

public class IndexModel : PageModel
{
    private readonly IConfiguration _config;

    public IndexModel(IConfiguration config)
    {
        _config = config;
    }

    [BindProperty]
    public string FirstName { get; set; }

    [BindProperty]
    public string LastName { get; set; }

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        try
        {
            string ConnectionStrings = _config.GetConnectionString("DefaultConnection");
            string query = "INSERT INTO dejan (FirstName, LastName) VALUES ('" + FirstName + "', '" + LastName + "')";
            using (SqlConnection connection = new SqlConnection(ConnectionStrings))
            {
                connection.Open();


                using (SqlCommand dCmd = new SqlCommand(query, connection))
                {

                    dCmd.ExecuteNonQuery();
                    return RedirectToPage("Index");
                }
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine(ex.ToString());
            throw;
        }
    }
}
