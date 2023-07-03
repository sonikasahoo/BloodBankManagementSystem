using Microsoft.AspNetCore.Mvc;
using Dapper;
using System.Data.SqlClient;
using BloodBankManagementSystem.Models;
using Microsoft.AspNetCore.Http;


namespace BloodBankManagementSystem.Controllers
{
    [Route("api/Register_User")]
    [ApiController]
    public class BusinessRegisterUser_Controller : ControllerBase
    {
        private readonly string connectionString;

        public BusinessRegisterUser_Controller(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }


        [HttpGet("GetDonorDetails")]
        public IActionResult GetDonorDetails()
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Donor";
                    var donorDetails = connection.Query<Donor>(query);
                    return Ok(donorDetails);
                }
            }

            catch (Exception ex)
            {
                return BadRequest("No Donor details found");
            }

        }

        [HttpGet("GetRequestorDetails")]

         public IActionResult GetRequestorDetails()
         {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    var query = "SELECT * FROM Requestor";
                    var requestorDetails = connection.Query<Requestor>(query);
                    return Ok(requestorDetails);
                }
            }

            catch(Exception ex) 
            {
                return BadRequest("Error :" + ex.Message);
            
            }
         }

       [HttpPost("AddDonorDetails")]
        public IActionResult AddDonorDetails(Donor donorModel)
        {
            try
            {


                using (var connection = new SqlConnection(connectionString))
                {
                    string query = @"INSERT INTO Donor (DonorId, FirstName, LastName, DOB, EmailId, ContactNo, BloodGroup, Address, Gender)
                                 VALUES (@DonorId, @FirstName, @LastName, @DOB, @EmailId, @ContactNo, @BloodGroup, @Address, @Gender)";
                    connection.Execute(query, donorModel);
                    return Ok("Donor details added successfully");
                }

            }
            catch (Exception ex)
            {
                return BadRequest("Error :" + ex.Message);
            }
        }

        [HttpPost("AddRequestorDetails")]
        public IActionResult AddRequestorDetails(Requestor requestorModel)
        {
            try
            {


                using (var connection = new SqlConnection(connectionString))
                {
                    string query = @"INSERT INTO Requestor (RequestorId, FirstName, LastName, DOB, EmailId, ContactNo, BloodGroup, Address, Gender)
                                 VALUES (@RequestorId, @FirstName, @LastName, @DOB, @EmailId, @ContactNo, @BloodGroup, @Address, @Gender)";
                    connection.Execute(query, requestorModel);
                    return Ok("Requestor details added successfully");
                }

            }

            catch (Exception ex)
            {
                return BadRequest("Error :" + ex.Message);
            }
        }
/*
       [HttpPost("AuthenticateRequestor")]
        public IActionResult AuthenticateRequestor(Requestor loginModel)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                string query = "SELECT RequestorId FROM Requestor WHERE FirstName = @FirstName AND Password = @Password";
                var requestorId = connection.QueryFirstOrDefault<string>(query, loginModel);

                if (requestorId != null)
                {
                    return Ok(new { RequestorId = requestorId, Message = "Authentication successful" });
                }

                return NotFound();
            }
        }*/

       /* [HttpPost]
        public IHttpActionResult AuthenticateDonor([FromBody] LoginModel loginModel)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT DonorId FROM Donor WHERE FirstName = @FirstName AND Password = @Password";
                var donorId = connection.QueryFirstOrDefault<string>(query, loginModel);

                if (donorId != null)
                {
                    return Ok(new { DonorId = donorId, Message = "Authentication successful" });
                }

                return NotFound();
            }
        }*/

        /*[HttpPost]
        public IHttpActionResult LoginValidate([FromBody] LoginModel loginModel)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT DonorId AS UserId, Password FROM Donor WHERE DonorId = @UserId " +
                               "UNION " +
                               "SELECT RequestorId AS UserId, Password FROM Requestor WHERE RequestorId = @UserId";
                var result = connection.QueryFirstOrDefault<dynamic>(query, loginModel);

                if (result != null && result.Password == loginModel.Password)
                {
                    string userId = result.UserId;
                    string userType = userId.StartsWith("R") ? "Requestor" : "Donor";
                    return Ok($"{userType} page");
                }

                return NotFound();
            }
        }*/
    }











}

