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

namespace DataAccessLayer.Repository

{
    public class DAL_DonateBloodAndCheck_repository : IDAL_BusinessDonateBloodAndCheck_repository
    {
        private readonly string connectionString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=DD_design;Data Source=LTIN178740\\MSSQLSERVER1";
      
        
        
        /*Method to enter the donor details who have requested to donate blood*/
        public DonateBlood SaveDonateBloodRequestToDB(DonateBlood donateBlood)
        {
            List<DonateBlood> donate = new List<DonateBlood>();
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string insertQuery = @"INSERT INTO donateblood (DonorId,FullName,DOB,Gender,Blood_Group,City,
                                            Weight,Date_Of_Last_Donation,How_Many_times,Phone_Number,EmailId,Status)
                                            VALUES (@DonorId,@FullName,@DOB,@Gender,@Blood_Group,@City,@Weight,
                                            @Date_Of_Last_Donation,@How_Many_Times,@Phone_Number,@EmailId,@Status);";

                using (var command = new SqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@DonorId", donateBlood.DonorId);
                    command.Parameters.AddWithValue("@FullName", donateBlood.FullName);
                    command.Parameters.AddWithValue("@DOB", donateBlood.DOB);
                    command.Parameters.AddWithValue("@Gender", donateBlood.Gender);
                    command.Parameters.AddWithValue("@Blood_Group", donateBlood.Blood_Group);
                    command.Parameters.AddWithValue("@City", donateBlood.City);
                    command.Parameters.AddWithValue("@Weight", donateBlood.Weight);
                    command.Parameters.AddWithValue("@Date_Of_Last_Donation", donateBlood.Date_Of_Last_Donation);
                    command.Parameters.AddWithValue("How_Many_Times", donateBlood.How_Many_Times);
                    command.Parameters.AddWithValue("@Phone_Number", donateBlood.Phone_Number);
                    command.Parameters.AddWithValue("@EmailId", donateBlood.EmailId);
                    command.Parameters.AddWithValue("@Status", donateBlood.Status);
                    command.ExecuteNonQuery();
                }
                return donateBlood;
            }
        }
        
       // Method to get the donor details from donor table
        public (string DonorId, string FullName, string Status) GetDonorStatus(string donorId)
        {
            DonateBlood donateblood = new DonateBlood();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = "SELECT DonorId,FullName,Status FROM donateblood WHERE DonorId = @DonorId";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DonorId", donorId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            donateblood.DonorId = reader["DonorId"].ToString();
                            donateblood.FullName = reader["FullName"].ToString();
                            donateblood.Status = reader["Status"].ToString();
                            return (donateblood.DonorId, donateblood.FullName, donateblood.Status);

                        }
                    }
                    return (null, null, null);
                }

            }
        }


    }
}





