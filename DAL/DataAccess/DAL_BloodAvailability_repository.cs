using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using Azure;
using DataAccessLayer.Contracts;
using DataAccessLayer.Models;

namespace DataAccessLayer.DataAccess
{
    public class DAL_BloodAvailability_repository : IDAL_BloodAvailability_repository
    {

        private readonly string connectionString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=DD_design;Data Source=LTIN178740\\MSSQLSERVER1";

        // it is a method which takes blood group as an argument and returns the no. of units of blood available in the blood bank
        public int GetBloodStatus(string bloodGroup)
        {
            var response = 0;
          
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"SELECT COUNT(*) AS Availability FROM Donor WHERE BloodGroup=@bloodGroup";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@BloodGroup", bloodGroup);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        int availability = Convert.ToInt32(reader["Availability"]);
                        response = availability;
                        reader.Close();
                    }
                    return response;
                }
            }
        }
        
    }
}