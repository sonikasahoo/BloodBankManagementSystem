using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BloodBankManagementSystem.Models;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Microsoft.AspNetCore.Authorization;

namespace BloodBankManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class BusinessApproveReject_Controller : ControllerBase
    {

        private readonly string connectionString;

        public BusinessApproveReject_Controller(IConfiguration configuration)
        {

            connectionString = configuration.GetConnectionString("DefaultConnection");

        }


        [HttpGet("GetRequestorDetails{requestorId}")]

        public IActionResult GetRequestorDetails(string requestorId)
        {

            try

            {

                using (var connection = new SqlConnection(connectionString))

                {

                    var query = "SELECT * FROM requestor WHERE RequestorId = @RequestorId";

                    var requestor_ = connection.Query(query, new { RequestorId = requestorId });

                    if (requestor_ == null)

                    {

                        return NotFound("Requestor details not found");

                    }

                    return Ok(requestor_);

                }

            }

            catch (Exception ex)

            {

                return BadRequest("Error :" + ex.Message);

            }

        }



        [HttpPut("SaveRequestorStatusInfoToDB{requestorId}")]

        public IActionResult SaveRequestorStatusInfoToDB(string requestorId, RequestStatus status)

        {

            try

            {

                using (var connection = new SqlConnection(connectionString))

                {

                    var query = "UPDATE requeststatus SET PatientId = @PatientId , Time_Of_The_Day= @Time_Of_The_Day, Blood_Glucose_Level=@Blood_Glucose_Level , Notes= @Notes WHERE RequestorId = @RequestorId ";

                    status.RequestorId = requestorId;

                    connection.Execute(query, status);

                }

                return Ok();

            }

            catch (Exception ex)

            {

                return BadRequest("Error :" + ex.Message);

            }

        }



        [HttpGet("GetDonorDetails{donorId}")]

        public IActionResult GetDonorDetails(string donorId)

        {

            try

            {

                using (var connection = new SqlConnection(connectionString))

                {

                    var query = "SELECT * FROM donor WHERE DonorId = @DonorId";

                    var donor_ = connection.Query(query, new { DonorId = donorId });

                    if (donor_ == null)

                    {

                        return NotFound("Requestor details not found");

                    }

                    return Ok(donor_);

                }

            }

            catch (Exception ex)

            {

                return BadRequest("Error :" + ex.Message);

            }

        }



        [HttpPut("SaveDonorStatusInfoToDB{donorId}")]

        public IActionResult SaveDonorStatusInfoToDB(string donorId, DonateBlood donateBlood)

        {

            try

            {

                using (var connection = new SqlConnection(connectionString))

                {

                    var query = "UPDATE donateblood SET status=@status WHERE DonorId=@DonorId ";

                    donateBlood.DonorId = donorId;

                    connection.Execute(query, donateBlood);

                }

                return Ok();

            }

            catch (Exception ex)

            {

                return BadRequest("Error :" + ex.Message);

            }

        }





    }

}
