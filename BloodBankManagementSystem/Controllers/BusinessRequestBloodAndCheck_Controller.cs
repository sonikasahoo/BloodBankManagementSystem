using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using BloodBankManagementSystem.Models;

namespace BloodBankManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessRequestBloodAndCheck_Controller : ControllerBase
    {
        private readonly string connectionString = "Data Source=LTIN237427;Initial Catalog=DD_design;Integrated Security=True";
        /*public BusinessRequestBloodAndCheck_Controller(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection"); ;
        }*/

        [HttpPost]
        public IActionResult MakeRequest(RequestBlood request)
        {
            try
            {
                using(var connection = new SqlConnection(connectionString))
                {
                    var query = @"INSERT INTO requestblood(RequestorId, PatientName, Required_Blood_Group, City, Doctors_Name, Hospital_name_Address, Blood_Required_Date, Contact_Name, Contact_Number, Contact_Email_id, Message)
                                  VALUES
                                  (@RequestorId, @Patient_Name, @Requested_Blood_Group, @City, @DoctorName, @Hospital_Name_Address, @Blood_required_Date, @Contact_Name, @Contact_Number, @Contact_Email_Id, @Message)";
                    connection.Execute(query, request);
                    return Ok("Blood Request successfully submitted.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred while making the blood request" + ex.Message);
            }
        }

        [HttpGet("{requestorId}")]
        public IActionResult GetRequestorStatus(string requestorId)
        {
            try
            {
                using(var connection = new SqlConnection(connectionString))
                {
                    var query = "SELECT * FROM requeststatus WHERE RequestorId = @RequestorId";
                    var result = connection.Query(query, new { RequestorId = requestorId});
                    if(result==null)
                    {
                        return NotFound("Requestor Details Not Found");
                    }
                    return Ok(result);

                }
            }
            catch(Exception ex)
            {
                return BadRequest("Error Occurred while retrieving the requestor details : " + ex.Message);
            }
        }

    }
}
