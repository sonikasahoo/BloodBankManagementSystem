using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace BloodBankManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class test : ControllerBase
    {
        private readonly string connectionString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=DD_design;Data Source=LTIN178740\\MSSQLSERVER1";


        [HttpGet("{donorId}")]
        // [Authorize(Roles = "Donor")]
        public IActionResult GetDonorStatus(string donorId)
        {

            List<string> db = new List<string>();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = "SELECT Status FROM donateblood WHERE DonorId = @DonorId";
                // DataTable datatable = new DataTable();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DonorId", donorId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            db.Add(reader["DonorId"].ToString());

                            /*  db.DonorId = reader["DonorId"].ToString();
                              db.FullName = reader["FullName"].ToString();
                              db.Status = reader["Status"].ToString();
                              return (db.DonorId, db.FullName, db.Status);*/


                        }
                    }
                }
                return Ok(db);






            }
        }
    }
}