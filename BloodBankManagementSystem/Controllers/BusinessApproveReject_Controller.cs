using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using DataAccessLayer.Models;
using DataAccessLayer.DataAccess;
using BusinessAccessLayer.Services;

namespace BloodBankManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessApproveReject_Controller : ControllerBase
    {

        public IServiceApproveReject _IServiceApproveReject;
        public BusinessApproveReject_Controller(IServiceApproveReject ServiceApproveReject)
        {
            _IServiceApproveReject = ServiceApproveReject;
        }

        // Controller which takes requestor id as parameter and returns requestor details list

        [HttpGet("GetRequestorDetails/{requestorId}")]
        [Authorize(Roles = "Requestor")]
        public IActionResult GetRequestorDetails(string requestorId)
        {
            try 
            {
				var rs = _IServiceApproveReject.getreqdetails(requestorId);
				return Ok(rs);
			}
			catch (Exception ex)
			{
				return BadRequest("Error Occurred while retrieving the requestor details : " + ex.Message);
			}

		}

        // Controller which takes requestor id and object of the class as parameter and updates the status of requestor

        [HttpPut("SaveRequestorStatusInfoToDB/{requestorId}")]
        [Authorize(Roles = "Requestor")]
        public IActionResult SaveRequestorStatusInfoToDB(string requestorId, RequestStatus status)
        {
            try
            {
				_IServiceApproveReject.saverequestorstatusinfotodb(requestorId, status);
                return Ok("Updated requestor status successfully.");
			}
            catch(Exception ex)
            {
				return BadRequest("Error Occurred while updating the requestor status : " + ex.Message);
			}
        }

		// Controller which takes donor id as parameter and returns donor details list

		[HttpGet("GetDonorDetails/{donorId}")]
        [Authorize(Roles = "Donor")]
        public IActionResult GetDonorDetails(string donorId)
        {
            try
            {
                var dr = _IServiceApproveReject.getdonordetails(donorId);
                return Ok(dr);
			}
            catch(Exception ex)
            {
				return BadRequest("Error Occurred while retrieving the donor details : " + ex.Message);
			}
                
            }

	    // Controller which takes donor id and object of the class as parameter and updates the status of donor


		[HttpPut("SaveDonorStatusInfoToDB/{donorId}")]
        [Authorize(Roles = "Donor")]
        public IActionResult SaveDonorStatusInfoToDB(string donorId, DonateBlood donateBlood)
        {
            try
            {
				_IServiceApproveReject.savedonorstatusinfotodb(donorId, donateBlood);
				return Ok("Donor status updated successfully.");
			}
            catch(Exception ex)
            {
				return BadRequest("Error Occurred while updating the donor status : " + ex.Message);

			}
		}
    }
}