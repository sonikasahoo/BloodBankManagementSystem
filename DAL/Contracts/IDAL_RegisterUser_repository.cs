using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Contracts
{
    public interface IDAL_RegisterUser_repository
    {
        public Donor GetDonorDetails();

        public Requestor GetRequestorDetails();

        public Donor AddDonorDetails(Donor donorModel);

        public Requestor AddRequestorDetails(Requestor requestorModel);

        public string AuthenticateRequestor(string requestorname, string password);

        public string AuthenticateDonor(string requestorname, string password);

        public (string UserId, string Password, string Role) AuthenticateUser(Admin user);

        public string GenerateToken(Admin user , string Role);


    }
}
