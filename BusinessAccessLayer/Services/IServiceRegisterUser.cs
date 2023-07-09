using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.Services
{
    public interface IServiceRegisterUser
    {
        public Donor GetAllDonor();

        public Requestor GetAllRequestor();

        public Donor AddDonor(Donor donorModel);

        public Requestor AddRequestor(Requestor requestorModel);


        public string AuthenticateRequestor(string requestorname, string password);

        public string AuthenticateDonor(string requestorname, string password);

        public (string UserId, string Password, string Role) AuthenticateUser(Admin user);

        public string GenerateToken(Admin user , string Role);
    }
}
