using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using DataAccessLayer.Contracts;
using DataAccessLayer.DataAccess;
using DataAccessLayer.Models;




namespace DataAccessLayer.DataAccess
{
    public class DAL_RequestBloodAndCheck_repository : IDAL_RequestBloodAndCheck_repository
    {
        private readonly string connectionString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=DD_design;Data Source=LTIN178740\\MSSQLSERVER1";

        public RequestBlood MakeRequest(RequestBlood request)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = @"INSERT INTO requestblood(RequestorId, PatientName, Required_Blood_Group, City, Doctors_Name, Hospital_name_Address, Blood_Required_Date, Contact_Name, Contact_Number, Contact_Email_id, Message)
                                  VALUES
                                  (@RequestorId, @Patient_Name, @Requested_Blood_Group, @City, @DoctorName, @Hospital_Name_Address, @Blood_required_Date, @Contact_Name, @Contact_Number, @Contact_Email_Id, @Message)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@RequestorId", request.RequestorId);
                    command.Parameters.AddWithValue("@Patient_Name", request.Patient_Name);
                    command.Parameters.AddWithValue("@Requested_Blood_Group", request.Required_Blood_Group);
                    command.Parameters.AddWithValue("@City", request.City);
                    command.Parameters.AddWithValue("@DoctorName", request.DoctorName);
                    command.Parameters.AddWithValue("@Hospital_Name_Address", request.Hospital_Name_Address);
                    command.Parameters.AddWithValue("@Blood_required_Date", request.Blood_required_Date);
                    command.Parameters.AddWithValue("@Contact_Name", request.Contact_Name);
                    command.Parameters.AddWithValue("@Contact_Number", request.Contact_Number);
                    command.Parameters.AddWithValue("@Contact_Email_Id", request.Contact_Email_Id);
                    command.Parameters.AddWithValue("@Message", request.Message);

                    command.ExecuteNonQuery();
                }
                return (request);
            }
        }

        public IEnumerable<RequestStatus> GetRequestorStatus(string requestorId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM requeststatus WHERE RequestorId = @RequestorId";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@RequestorId", requestorId);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            var result = new List<RequestStatus>();
                            while (reader.Read())
                            {
                                var requestStatus = new RequestStatus
                                {
                                    RequestorId = reader.GetString("RequestorId"),
                                    PatientId = reader.GetString("PatientId"),
                                    Blood_Glucose_Level = reader.GetString("Blood_Glucose_Level"),
                                    Time_Of_The_Day = Convert.ToDateTime(reader["Time_Of_The_Day"]),
                                Notes = reader.GetString("Notes")
                                };
                                result.Add(requestStatus);
                            }
                            return result;
                        }
                    }
                    return null;
                }
            }
        }

     
    }

}
