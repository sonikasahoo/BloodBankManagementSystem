using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BloodBankManagementSystem.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
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
		
		[HttpGet("GetRequestorDetails/{requestorId}")]
		public IActionResult GetRequestorDetails(string requestorId)
		{
			try
			{
				using (var connection = new SqlConnection(connectionString))
				{
					connection.Open();
					var query = "SELECT * FROM requestor WHERE RequestorId = @RequestorId";
					var command = new SqlCommand(query, connection);
					command.Parameters.AddWithValue("@RequestorId",requestorId);

					var reader=command.ExecuteReader();
					if(reader.Read())
					{
						var requestor = new
						{
							RequestorId = reader["RequestorId"].ToString(),
						};
						reader.Close();
						return Ok(requestor);
					}
					reader.Close ();
					return NotFound("Requestor details not found");
				}
			}
			catch(Exception ex)
			{
				return BadRequest("Error :" + ex.Message);
			}
		}

		[HttpPut("SaveRequestorStatusInfoToDB/{requestorId}")]
		public IActionResult SaveRequestorStatusInfoToDB(string requestorId, RequestStatus status)
		{
			try
			{
				using (var connection = new SqlConnection(connectionString))
				{
					connection.Open(); 
					var query = "UPDATE requeststatus SET PatientId = @PatientId , Time_Of_The_Day= @Time_Of_The_Day, Blood_Glucose_Level=@Blood_Glucose_Level , Notes= @Notes WHERE RequestorId = @RequestorId ";
					var command = new SqlCommand(query, connection);
					command.Parameters.AddWithValue("@PatientId", status.PatientId);
					command.Parameters.AddWithValue("@Time_Of_The_Day", status.Time_Of_The_Day);
					command.Parameters.AddWithValue("@Blood_Glucose_Level", status.Blood_Glucose_Level);
					command.Parameters.AddWithValue("@Notes", status.Notes);
					command.Parameters.AddWithValue("@RequestorId", requestorId);

					var affectedRows = command.ExecuteNonQuery();
					if (affectedRows > 0)
					{
						return Ok();
					}

					return NotFound("Requestor not found or status not updated.");
				}
			}
			catch (Exception ex)
			{
				return BadRequest("Error :" + ex.Message);
			}
		}

		[HttpGet("GetDonorDetails/{donorId}")]
		public IActionResult GetDonorDetails(string donorId)
		{
			try
			{
				using (var connection = new SqlConnection(connectionString))
				{
					connection.Open(); 
					var query = "SELECT * FROM donor WHERE DonorId = @DonorId";
					var command = new SqlCommand(query, connection);
					command.Parameters.AddWithValue("@DonorId", donorId);

					var reader = command.ExecuteReader();
					if (reader.Read())
					{
						var donor = new
						{
							Donorld = reader["DonorId"].ToString(),
						};
						reader.Close();
						return Ok(donor);
					}

					reader.Close();
					return NotFound("Donor details not found");
				}
			}
			catch (Exception ex)
			{
				return BadRequest("Error :" + ex.Message);
			}
		}

		[HttpPut("SaveDonorStatusInfoToDB/{donorId}")]
		public IActionResult SaveDonorStatusInfoToDB(string donorId, DonateBlood donateBlood)
		{
			try
			{
				using (var connection = new SqlConnection(connectionString))
				{
					connection.Open();
					var query = "UPDATE donateblood SET Status = @Status WHERE DonorId = @DonorId";
					var command = new SqlCommand(query, connection);
					command.Parameters.AddWithValue("@Status", donateBlood.Status);
					command.Parameters.AddWithValue("@DonorId", donorId);

					var affectedRows = command.ExecuteNonQuery();
					if (affectedRows > 0)
					{
						return Ok();
					}

					return NotFound("Donor not found or status not updated.");
				}
			}
			catch (Exception ex)
			{
				return BadRequest("Error :" + ex.Message);
			}
		}


	}
}

