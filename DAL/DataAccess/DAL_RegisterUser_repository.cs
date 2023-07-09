using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using DataAccessLayer.Models;
using DataAccessLayer.Contracts;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace DataAccessLayer.DataAccess
{
    public class DAL_RegisterUser_repository : IDAL_RegisterUser_repository
    {
        private readonly string connectionString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=DD_design;Data Source=LTIN178740\\MSSQLSERVER1";
        private IConfiguration? _config;
        public DAL_RegisterUser_repository(IConfiguration configuration)
        {
            _config = configuration;
        }

        public Donor GetDonorDetails()
        {
            Donor donorList = new Donor();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Donor";


                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            Donor donor = new Donor
                            {
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Password = reader["Password"].ToString(),
                                DOB = Convert.ToDateTime(reader["DOB"]),
                                Address = reader["Address"].ToString(),
                                ContactNo = reader["ContactNo"].ToString(),
                                EmailId = reader["EmailId"].ToString(),
                                Gender = reader["Gender"].ToString(),
                                BloodGroup = reader["BloodGroup"].ToString(),
                                DonorId = reader["DonorId"].ToString(),
                            };

                           return donor;
                        }
                    }

                }
            }
            return null;
        }


        public Requestor GetRequestorDetails()
        {
            Requestor requestorList = new Requestor();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Requestor";
                DataTable donorDetails = new DataTable();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            Requestor requestor = new Requestor
                            {
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Password = reader["Password"].ToString(),
                                DOB = Convert.ToDateTime(reader["DOB"]),
                                Address = reader["Address"].ToString(),
                                ContactNo = reader["ContactNo"].ToString(),
                                EmailId = reader["EmailId"].ToString(),
                                Gender = reader["Gender"].ToString(),
                                // BloodGroup = reader["BloodGroup"].ToString(),
                                RequestorId = Convert.ToInt32(reader["RequestorId"])
                            };
                            requestorList = requestor ;
                        }
                    }
                    return requestorList;
                }
            }
        }


        public Donor AddDonorDetails(Donor donorModel)
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

                return donorModel;
            }
        }


        public Requestor AddRequestorDetails(Requestor requestorModel)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO Requestor (RequestorId, FirstName, LastName, DOB, EmailId, ContactNo, Password, Address, Gender)
                                    VALUES (@RequestorId, @FirstName, @LastName, @DOB, @EmailId, @ContactNo, @Password, @Address, @Gender)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@RequestorId", requestorModel.RequestorId);
                    command.Parameters.AddWithValue("@FirstName", requestorModel.FirstName);
                    command.Parameters.AddWithValue("@LastName", requestorModel.LastName);
                    command.Parameters.AddWithValue("@DOB", requestorModel.DOB);
                    command.Parameters.AddWithValue("@EmailId", requestorModel.EmailId);
                    command.Parameters.AddWithValue("@ContactNo", requestorModel.ContactNo);
                    command.Parameters.AddWithValue("@Password", requestorModel.Password);
                    command.Parameters.AddWithValue("@Address", requestorModel.Address);
                    command.Parameters.AddWithValue("@Gender", requestorModel.Gender);

                    connection.Open();
                    command.ExecuteNonQuery();
                }

                return requestorModel;
            }

        }


        public string AuthenticateRequestor(string requestorname, string password)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT RequestorId FROM Requestor WHERE FirstName = @FirstName AND Password = @Password";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", requestorname);
                    command.Parameters.AddWithValue("@Password", password);

                    connection.Open();
                    string requestorId = (string)command.ExecuteScalar();


                    return requestorId;

                }
            }
        }
        public string AuthenticateDonor(string requestorname, string password)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT DonorId FROM Donor WHERE FirstName = @FirstName AND Password = @Password";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", requestorname);
                    command.Parameters.AddWithValue("@Password", password);

                    connection.Open();
                    string donorId = (string)command.ExecuteScalar();

                    return donorId;
                }

            }
        }

            //----------------------------------------------------------------------------------------------
            //--------------------------LOGIN PART-------------------------------
            public string GenerateToken(Admin user, string role)
            {
                var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, user.UserId),
                    new Claim(ClaimTypes.Role , role)
                };
            
                var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                    _config["Jwt:Audience"],
                    
                    claims,
                    expires: DateTime.Now.AddMinutes(20),
                    signingCredentials: credentials
                    );
                return new JwtSecurityTokenHandler().WriteToken(token);

            }

            public (string UserId , string Password , string Role) AuthenticateUser(Admin user)
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT DonorId as UserId, Password, 'Donor' AS Role FROM Donor WHERE FirstName = @Userid" +
                    " UNION " +
                    "SELECT RequestorId as UserId, Password, 'Requestor' AS Role FROM Requestor WHERE FirstName = @Userid";
                    
                    
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Userid", user.UserId);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string UserId = reader["UserId"].ToString();
                            string password = reader["Password"].ToString();
                            string Role = reader["Role"].ToString();

                            if (password == user.Password)
                            {
                                user.UserId = UserId;
                                reader.Close();
                                return (UserId , password , Role);
                            }
                        }
                    }
                    reader.Close();
                    return (null, null, null);
                }






            }
            
        }
    }
