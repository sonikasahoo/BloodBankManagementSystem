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
    public class BusinessDonateBloodAndCheck_Controller : ControllerBase
    {



        public IServiceBusinessDonateBloodAndCheck _IServiceBusinessDonateBloodAndCheck;



        public BusinessDonateBloodAndCheck_Controller(IServiceBusinessDonateBloodAndCheck ServiceBusinessBloodAndCheck)
        {
            _IServiceBusinessDonateBloodAndCheck = ServiceBusinessBloodAndCheck;
        }



        /*To insert a donor detils who are donating blood*/
        [HttpPost("/SaveDonateBloodRequestToDB")]
        [Authorize(Roles = "Donor")]
        public IActionResult SaveDonateBloodRequestToDB(DonateBlood donateBlood)
        {
            try
            {
                _IServiceBusinessDonateBloodAndCheck.DonateBloodRequestToDB(donateBlood);
                return Ok("Blood Donater details entered successfully");
            }

            catch(Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }
           
        }

        /*To get the donor details with status from the donateblood table */
        [HttpGet("{donorId}")]
       [Authorize(Roles = "Donor")]
        public IActionResult GetDonorStatus(string donorId)
        {
            try
            {
                var DS =_IServiceBusinessDonateBloodAndCheck.DonorStatus(donorId);
                DonateBlood donateBlood = new DonateBlood();

                donateBlood.Status = DS.Status;
                donateBlood.DonorId = DS.DonorId;
                donateBlood.FullName = DS.FullName;

                return Ok(new { donateBlood.Status, donateBlood.DonorId, donateBlood.FullName });
            }
            
            catch(Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

        }

    }
}