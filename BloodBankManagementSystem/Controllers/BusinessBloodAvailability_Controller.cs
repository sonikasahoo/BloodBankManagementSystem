using Microsoft.AspNetCore.Mvc;
using Dapper;
using System.Data.SqlClient;
using BloodBankManagementSystem.Models;



namespace BloodBankManagementSystem.Controllers
{
    [Route("api/Business_Blood_Availability")]
    [ApiController]
    public class BusinessBloodAvailability_Controller : ControllerBase
    {
        private readonly string connectionString;

        public BusinessBloodAvailability_Controller(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        public IActionResult GetBloodStatus(string bloodGroup)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                string query = @"SELECT COUNT(*) AS Availability
                             FROM RequestBlood rb
                             INNER JOIN DonateBlood db ON rb.RequestorId = db.DonorId
                             WHERE rb.Required_Blood_Group = @BloodGroup";

                var result = connection.QueryFirstOrDefault(query, new { BloodGroup = bloodGroup });

                if (result != null)
                {
                    int availability = Convert.ToInt32(result.Availability);
                   // int totalQuantity = Convert.ToInt32(result.TotalQuantity);

                    var response = new
                    {
                        Availability = availability,
                        //TotalQuantity = totalQuantity
                    };

                    return Ok(response);
                    }
                    return NotFound();
                
            }
        }
    }
}
