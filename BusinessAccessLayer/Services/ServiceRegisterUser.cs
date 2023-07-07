using DataAccessLayer.Contracts;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.Services
{
    public class ServiceRegisterUser : IServiceRegisterUser
    {
        public readonly IDAL_RegisterUser_repository _iDAL_RegisterUser_repository;
        
        public ServiceRegisterUser(IDAL_RegisterUser_repository DAL_RegisterUser_repository)
        {
            _iDAL_RegisterUser_repository = DAL_RegisterUser_repository;
        }

        public Donor GetAllDonor()
        {
           
                return _iDAL_RegisterUser_repository.GetDonorDetails();
           
        }
        public Requestor GetAllRequestor()
        {

            return _iDAL_RegisterUser_repository.GetRequestorDetails();
        }
        public Donor AddDonor(Donor donorModel)
        {
            return _iDAL_RegisterUser_repository.AddDonorDetails(donorModel);
        }

        public Requestor AddRequestor(Requestor requestorModel)
        {
            return _iDAL_RegisterUser_repository.AddRequestorDetails(requestorModel);
        }

        public string AuthenticateRequestor(string requestorname, string password)
        {
            return _iDAL_RegisterUser_repository.AuthenticateRequestor(requestorname,password);
        }

        public string AuthenticateDonor(string Donorname, string password)
        {
            return _iDAL_RegisterUser_repository.AuthenticateDonor(Donorname,password);

        }

        public (string UserId, string Password, string Role) AuthenticateUser(Admin user)
        {
            return _iDAL_RegisterUser_repository.AuthenticateUser(user);
        }

        public string GenerateToken(Admin user, string Role)
        {
            return _iDAL_RegisterUser_repository.GenerateToken(user , Role);
        }

    }
}
