using DataAccessLayer.Contracts;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DataAccess
{
    public class DAL_ApproveReject_repository : IDAL_ApproveReject_repository
    {

        private readonly string connectionString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=DD_design;Data Source=LTIN178740\\MSSQLSERVER1";
        
        // Method to get the requestor details of a specific requestor 

        public IEnumerable<Requestor> GetRequestorDetails(string requestorId)
        {
            List<Requestor> requestors = new List<Requestor>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM requestor WHERE RequestorId = @RequestorId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@RequestorId", requestorId);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Requestor req = new Requestor
                            {
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Password = reader["Password"].ToString(),
                                DOB = Convert.ToDateTime(reader["DOB"]),
                                Address = reader["Address"].ToString(),
                                ContactNo = reader["ContactNo"].ToString(),
                                EmailId = reader["EmailId"].ToString(),
                                Gender = reader["Gender"].ToString(),
                                RequestorId = Convert.ToInt32(reader["RequestorId"])
                            };
                            requestors.Add(req);
                        }
                    }
                    return requestors;
                }
            }
        }

        // Method to update requestor status details of a specific requestor

        public string SaveRequestorStatusInfoToDB(string requestorId, RequestStatus status)
        {
            var sr = " Unable";
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = "UPDATE requeststatus SET Notes = @Notes WHERE RequestorId = @RequestorId";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Notes", status.Notes);
                command.Parameters.AddWithValue("@RequestorId", requestorId);
                var affectedRows = command.ExecuteNonQuery();
                sr = "Successful";
                }
            return sr;
            }
       
        // Method to get the donor details of a specfic donor

        public IEnumerable<Donor> GetDonorDetails(string donorId)
        {
            List<Donor> donors = new List<Donor>();
            using (var connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Donor WHERE DonorId = @DonorId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DonorId", donorId);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Donor don = new Donor
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
                                DonorId = reader["DonorId"].ToString()
                            };
                            donors.Add(don);
                        }
                    }
                    return donors;
                }
            }
        }

		// Method to update donor status details of a specific donor

		public string SaveDonorStatusInfoToDB(string donorId, DonateBlood donateBlood)
        {
            var sr = " Unable";
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = "UPDATE donateblood SET Status = @Status WHERE DonorId = @DonorId";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Status", donateBlood.Status);
                command.Parameters.AddWithValue("@DonorId", donorId);
                var affectedRows = command.ExecuteNonQuery();
                sr = "Successful";  
                }
                return sr;
            }
        }
}









