using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BloodBankManagementSystem.Models;


namespace BloodBankManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuisnessDonateBloodAndCheck : ControllerBase
    {
        private readonly string connectionString = "Data Source=LTIN167901;Initial Catalog=DD_design;Integrated Security=True";


        [HttpPost]
        public IActionResult SaveDonateBloodRequestToDB(DonateBlood donate)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string insertQuery = @"INSERT INTO donateblood (DonorId,FullName,DOB,Gender,Blood_Group,City,
                                            Weight,Date_Of_Last_Donation,How_Many_times,Phone_Number,EmailId,Status)
                                            VALUES (@Donor_Id,@FullName,@DOB,@Gender,@Blood_Group,@City,@Weight,
                                            @Date_Of_Last_Donation,@How_Many_Times,@Phone_Number,@EmailId,@Status);";

                    using (var command = new SqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Donor_Id",donate.DonorId);
                        command.Parameters.AddWithValue("@FullName",donate.FullName);
                        command.Parameters.AddWithValue("@DOB",donate.DOB);
                        command.Parameters.AddWithValue("@Gender",donate.Gender);
                        command.Parameters.AddWithValue("@Blood_Group", donate.Blood_Group);
                        command.Parameters.AddWithValue("@City", donate.City);
                        command.Parameters.AddWithValue("@Weight", donate.Weight);
                        command.Parameters.AddWithValue("@Date_Of_Last_Donation",donate.Date_Of_Last_Donation);
                        command.Parameters.AddWithValue("How_Many_Times", donate.How_Many_Times);
                        command.Parameters.AddWithValue("@Phone_Number", donate.Phone_Number);
                        command.Parameters.AddWithValue("@EmailId", donate.EmailId);
                        command.Parameters.AddWithValue("@Status",donate.Status);

                        command.ExecuteNonQuery();

                    }

                }

                return Ok("Donate details saved successfully.");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{donorId}")]
        public IActionResult GetDonorStatus(string donorId)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    var query = "SELECT * FROM donor WHERE DonorId = @DonorId";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@DonorId", donorId);
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string DonorId = reader["DonorId"].ToString();
                                string Firstname = reader["FirstName"].ToString();
                                string LastName = reader["LastName"].ToString();
                                string Password = reader["Password"].ToString();
                                string DOB = reader["DOB"].ToString();
                                string Address = reader["Address"].ToString();
                                string ContactNo = reader["ContactNo"].ToString();
                                string EmailId = reader["EmailId"].ToString();
                                string Gender = reader["Gender"].ToString();
                                string BloodGroup = reader["BloodGroup"].ToString();

                                var donor = new
                                {
                                    DonorId = donorId,
                                    Firstname = Firstname,
                                    LastName = LastName,
                                    Password = Password,
                                    DOB = DOB,
                                    Address = Address,
                                    ContactNo = ContactNo,
                                    EmailId = EmailId,
                                    Gender = Gender,
                                    BloodGroup = BloodGroup
                                };
                                return Ok(donor);

                            }
                        }

                    }
                        return null;
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Error Occurred while retrieving the requestor details : " + ex.Message);
            }
        }

    }
}

















/*public BuisnessDonateBloodAndCheck(IConfiguration configuration)
        private readonly string connectionString = "Data Source=LTIN167901;Initial Catalog=DD_design;Integrated Security=True";
{
    connectionString = configuration.GetConnectionString("DefaultConnection");

}

[HttpPost]
public IActionResult SaveDonateBloodRequestToDB(DonateBlood donate)
{
            /* try
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
            
            catch (Exception ex)
            {
                return BadRequest("Error Occurred"+ex.Message);
            }
            
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
         */