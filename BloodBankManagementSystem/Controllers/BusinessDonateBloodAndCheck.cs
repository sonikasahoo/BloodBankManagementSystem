using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Globalization;
using System.Data.SqlClient;
using BloodBankManagementSystem.Models;

namespace BloodBankManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuisnessDonateBloodAndCheck : ControllerBase
    {

        private readonly string connectionString;

        public BusinessRegisterUser_Controller(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }


        [HttpPost]
        public IActionResult SaveDonateBloodRequestToDB(DonateBlood donate)
        {
            
            using (var connection = new SqlConnection(connectionString))
            {
                string insertQuery = @"INSERT INTO donateblood (DonorId,FullName,DOB,Gender,Blood_Group,City,
                                            Weight,Date_Of_Last_Donation,How_Many_times,Phone_Number,EmailId,Status)
                                            VALUES (@Donor_Id,@FullName,@DOB,@Gender,@Blood_Group,@City,@Weight,
                                            @Date_Of_Last_Donation,@How_Many_Times,@Phone_Number,@EmailId,@Status);";
                connection.Execute(insertQuery,donate);
                }
                return Ok("Donor details saved successfully.");
            
            /*catch (Exception ex)
            {
                return BadRequest("Error Occurred"+ex.Message);
            }
            */
        }

        [HttpGet("{donorId}")]
        public IActionResult GetDonorStatus(string donorId)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    var query = "SELECT * FROM donor WHERE DonorId = @donorId";
                    var result = connection.Query(query, new { DonorId = donorId });
                    if (result == null)
                    {
                        return NotFound("Donor Details Not Found");
                    }
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Error Occurred while retrieving the requestor details : " + ex.Message);
            }
        }
    }
}
