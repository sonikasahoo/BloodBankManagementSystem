using BusinessAccessLayer.Services;
using DataAccessLayer.DataAccess;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace BloodBankManagementSystem.Controllers
{
    [Route("api/Register_User")]
    [ApiController]
    public class BusinessRegisterUser_Controller : ControllerBase
    {

        // creates a object of the interface IServiceRegisterUser
        public IServiceRegisterUser _IserviceRegisterUser;
        public BusinessRegisterUser_Controller(IServiceRegisterUser ServiceRegisterUser)
        {
            _IserviceRegisterUser = ServiceRegisterUser;
        }
        



        // It is a method which shows the details of all the donor
        [HttpGet("GetDonorDetails")]
        [Authorize(Roles = "Donor")]
        public IActionResult GetDonorDetails()
        {
            try
            {
                var sr = _IserviceRegisterUser.GetAllDonor();
                return Ok(sr);

            }

            catch (Exception ex)
            {
                return BadRequest("Error Occurred while retriving the donor details : " + ex.Message);

            }
        }


        // It is a method which shows the details of all the requestor
        [HttpGet("GetRequestorDetails")]
        [Authorize(Roles = "Requestor")]
        public IActionResult GetRequestorDetails()
        {
            try
            {

                var sr = _IserviceRegisterUser.GetAllRequestor();
                return Ok(sr);
            }
            catch(Exception ex)
            {
                return BadRequest("Error Occurred while retriving the requestor details : " + ex.Message);
            }
        }



        // It is a method which adds the details of the donor

        [HttpPost("/AddDonorDetails")]
        //[Authorize(Roles = "Donor")]
        public IActionResult AddDonorDetails(Donor donor)
        {
            try
            {
            
            _IserviceRegisterUser.AddDonor(donor);
            return Ok("Nomination Details Submitted");
            }

            catch (Exception ex)
            {
                return BadRequest("Error Occurred while Adding the Donor : " + ex.Message);

            }

        }


        // It is a method which adds the details of the requestor
        [HttpPost("/AddRequestorDetails")]
       // [Authorize(Roles = "Requestor")]
        public IActionResult AddRequestorDetails(Requestor requestor)
        {
            try
            {

                _IserviceRegisterUser.AddRequestor(requestor);
                return Ok("Nomination Details Submitted");

            }
            catch (Exception ex)
            {
                return BadRequest("Error Occurred while Adding the requestor : " + ex.Message);

            }
        }


        // It is a method which authenticate the requestor on the basis of requestorname and password which is already present in the database
        [HttpPost("AuthenticateRequestor")]
        [Authorize(Roles = "Requestor")]
        public IActionResult AuthenticateRequestor(string requestorname, string password)
        {
            try 
            {
                    string result = _IserviceRegisterUser.AuthenticateRequestor(requestorname, password);
                    if (result != null)
                    {
                        return Ok(new { result, Message = "Authentication successful" });
                    }

                    else
                        return BadRequest("Invalid User");
            }
            catch (Exception ex) 
            {
                return BadRequest("Error Occurred while Authenticating the requestor : " + ex.Message);

            }

        }


        // It is a method which authenticate the donor on the basis of donororname and password which is already present in the database
        [HttpPost("AuthenticateDonor")]
        [Authorize(Roles = "Donor")]
        public IActionResult AuthenticateDonor(string Donorname, string password)
        {
            try
            {

                string result = _IserviceRegisterUser.AuthenticateDonor(Donorname, password);
                if (result != null)
                {
                    return Ok(new { result, Message = "Authentication successful" });
                }

                else
                    return BadRequest("Invalid User");

            }
            catch(Exception ex)
            {
                return BadRequest("Error Occurred while Authenticating the donor : " + ex.Message);

            }
        }



        // it is a method to login and identify the role of the user for the role based authentication.
        [HttpPost("LoginValidate")]
        public IActionResult LoginValidate(Admin user)
        {
            try
            {

                IActionResult response = Unauthorized();
                var (user_, password, role) = _IserviceRegisterUser.AuthenticateUser(user);
                if (user_ != null)
                {
                    var token = _IserviceRegisterUser.GenerateToken(new Admin { UserId = user_, Password = password }, role);
                    response = Ok(new { token = token, role });
                }
                return response;
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}

