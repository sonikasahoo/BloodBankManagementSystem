using Microsoft.AspNetCore.Mvc;
using Dapper;
using System.Data.SqlClient;
using BloodBankManagementSystem.Models;
using Microsoft.AspNetCore.Http;
using System.Web.Http;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Data;

namespace BloodBankManagementSystem.Controllers
{
    [Route("api/Register_User")]
    [ApiController]
    public class BusinessRegisterUser_Controller : ControllerBase
    {
        private readonly string connectionString;
        private IConfiguration? _config;

        public BusinessRegisterUser_Controller(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
            _config = configuration;
        }


        [HttpGet("GetDonorDetails")]
        public IActionResult GetDonorDetails()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Donor";
                    DataTable donorDetails = new DataTable();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(donorDetails);
                        }
                    }
                    if (donorDetails.Rows.Count > 0)
                    {
                        return Ok(ConvertDataTableToList(donorDetails));
                    }

                   return null;
                }
            }

            catch (Exception ex)
            {
                return BadRequest("No Donor details found");
            }
        }
        // Method to convert the datatable to list 
        private List<Dictionary<string, object>> ConvertDataTableToList(DataTable dataTable)
        {
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();

            foreach (DataRow row in dataTable.Rows)
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();

                foreach (DataColumn col in dataTable.Columns)
                {
                    dict[col.ColumnName] = row[col];
                }

                list.Add(dict);
            }

            return list;
        }

        [HttpGet("GetRequestorDetails")]
        public IActionResult GetRequestorDetails()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Requestor";
                    DataTable requestorDetails = new DataTable();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(requestorDetails);
                        }
                    }

                    if (requestorDetails.Rows.Count > 0)
                    {
                        return Ok(ConvertDataTableToList(requestorDetails));
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Error :" + ex.Message);
            }
        }

        [HttpPost("AddDonorDetails")]
        public IActionResult AddDonorDetails(Donor donorModel)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"INSERT INTO Donor (DonorId, FirstName, LastName, DOB, EmailId, ContactNo, BloodGroup, Address, Gender)
                                    VALUES (@DonorId, @FirstName, @LastName, @DOB, @EmailId, @ContactNo, @BloodGroup, @Address, @Gender)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@DonorId", donorModel.DonorId);
                        command.Parameters.AddWithValue("@FirstName", donorModel.FirstName);
                        command.Parameters.AddWithValue("@LastName", donorModel.LastName);
                        command.Parameters.AddWithValue("@DOB", donorModel.DOB);
                        command.Parameters.AddWithValue("@EmailId", donorModel.EmailId);
                        command.Parameters.AddWithValue("@ContactNo", donorModel.ContactNo);
                        command.Parameters.AddWithValue("@BloodGroup", donorModel.BloodGroup);
                        command.Parameters.AddWithValue("@Address", donorModel.Address);
                        command.Parameters.AddWithValue("@Gender", donorModel.Gender);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }

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
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"INSERT INTO Requestor (RequestorId, FirstName, LastName, DOB, EmailId, ContactNo, BloodGroup, Address, Gender)
                                    VALUES (@RequestorId, @FirstName, @LastName, @DOB, @EmailId, @ContactNo, @BloodGroup, @Address, @Gender)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@RequestorId", requestorModel.RequestorId);
                        command.Parameters.AddWithValue("@FirstName", requestorModel.FirstName);
                        command.Parameters.AddWithValue("@LastName", requestorModel.LastName);
                        command.Parameters.AddWithValue("@DOB", requestorModel.DOB);
                        command.Parameters.AddWithValue("@EmailId", requestorModel.EmailId);
                        command.Parameters.AddWithValue("@ContactNo",

                        requestorModel.ContactNo);
                        command.Parameters.AddWithValue("@BloodGroup", requestorModel.BloodGroup);
                        command.Parameters.AddWithValue("@Address", requestorModel.Address);
                        command.Parameters.AddWithValue("@Gender", requestorModel.Gender);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }

                    return Ok("Requestor details added successfully");
                }

            }

            catch (Exception ex)
            {
                return BadRequest("Error :" + ex.Message);
            }
        }

        [HttpPost("AuthenticateRequestor")]
        public IActionResult AuthenticateRequestor(string requestorname, string password, Requestor requestor)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT RequestorId FROM Requestor WHERE FirstName = @FirstName AND Password = @Password";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", loginModel.UserId);
                    command.Parameters.AddWithValue("@Password", loginModel.Password);

                    connection.Open();
                    string requestorId = (string)command.ExecuteScalar();

                    if (!string.IsNullOrEmpty(requestorId))
                    {
                        return Ok(new { RequestorId = requestorId, Message = "Authentication successful" });
                    }
                }

                return null;
            }
        }

        [HttpPost("AuthenticateDonor")]
        public IActionResult AuthenticateDonor(string firstname, string password, Donor donor)
        {

            using (var connection = new SqlConnection(connectionString))
            {

                string query = "SELECT DonorId FROM Donor WHERE FirstName = @FirstName AND Password = @Password";
                donor.FirstName = firstname;
                donor.Password = password;
                var donorId = connection.QueryFirstOrDefault<Donor>(query, donor);

                if (donorId != null)
                {

                    return Ok(new { DonorId = donorId, Message = "Authentication successful" });
                }
                return BadRequest("Not found any donor with the given credentials");
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //------------------------LOGIN PART---------------------------------------

        private Admin AuthenticateUser(Admin user)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                string query = "SELECT DonorId AS UserId, Password FROM Donor WHERE FirstName = @UserId " + "UNION " +
                   "SELECT RequestorId AS UserId, Password FROM Requestor WHERE FirstName = @UserId";
                var result = connection.QueryFirstOrDefault(query, user);

                if (result != null && result.Password == user.Password)
                {
                    string userId = result.UserId;
                    return user;
                }
                return null;
            }
        }
        private string GenerateToken(Admin user)
        {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"], null,
                expires: DateTime.Now.AddMinutes(20),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        [HttpPost("LoginValidate")]
        public IActionResult LoginValidate(Admin user)
        {
            IActionResult response = Unauthorized();
            var user_ = AuthenticateUser(user);
            if (user_ != null)
            {
                var token = GenerateToken(user_);
                response = Ok(new { token = token });
            }
            return response;


        }











    }
}

