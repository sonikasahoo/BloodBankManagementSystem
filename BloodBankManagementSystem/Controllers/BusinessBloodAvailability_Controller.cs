using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using DataAccessLayer.DataAccess;
using DataAccessLayer.Models;
using BusinessAccessLayer.Services;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace BloodBankManagementSystem.Controllers
{
    [Route("api/Business_Blood_Availability")]
    [ApiController]
    public class BusinessBloodAvailability_Controller : ControllerBase
    {

        // creates a object of the IserviceBloodAvailability interface
        public IServiceBloodAvailability _IServiceBloodAvailability;

        // initialises the object created
        public BusinessBloodAvailability_Controller(IServiceBloodAvailability ServiceBloodAvailability)
        {
            _IServiceBloodAvailability = ServiceBloodAvailability;
        }



        // To get the quantity of available blood as per the requested Blood Group
        [HttpGet("{bloodGroup}")]
        [Authorize(Roles = "Requestor")]
        public IActionResult GetBloodStatus(string bloodGroup)
        {
            try
            {
                int DonorDetails = _IServiceBloodAvailability.GetBloodCount(bloodGroup);
                return Ok(DonorDetails);
            }
            catch (Exception ex)
            {
                return BadRequest("Error Occurred while retrieving the Donor details : " + ex.Message);
            }
        }
    }
}
