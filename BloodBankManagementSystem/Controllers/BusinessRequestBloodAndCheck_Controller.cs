using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DataAccessLayer.DataAccess;
using DataAccessLayer.Models;
using BusinessAccessLayer.Services;
using Microsoft.AspNetCore.Authorization;

namespace BloodBankManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessRequestBloodAndCheck_Controller : ControllerBase
    {

        public IServiceRequestBlood _IserviceRequestBlood;
        public BusinessRequestBloodAndCheck_Controller(IServiceRequestBlood serviceRequestBlood)
        {
            _IserviceRequestBlood = serviceRequestBlood;
        }

        // To enter the request details in the 'requestBlood' Table when a request for blood is performed 
        [HttpPost("/SaveRequestDetailsToDB")]
        [Authorize(Roles = "Requestor")]
        public IActionResult SaveRequestDetailsToDB(RequestBlood request)
        {
            try
            {
                _IserviceRequestBlood.RequestForBlood(request);
                    return Ok("Blood Request successfully submitted.");
               
            }
            catch (Exception ex)
            {
                return BadRequest("Error occurred while making the blood request" + ex.Message);
            }
        }

        // To get the requestor status of a specific Requestor from 'requestStatus' Table
        [HttpGet("{requestorId}")]
        [Authorize(Roles = "Requestor")]
        public IActionResult GetRequestorStatus(string requestorId)
        {
            try
            {
                var rs =  _IserviceRequestBlood.GetRequestStatuses(requestorId);
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest("Error Occurred while retrieving the requestor details : " + ex.Message);
            }
        }

    }
}
