using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace DataAccessLayer.Contracts
{
    public interface IDAL_ApproveReject_repository
    {
        public IEnumerable<Requestor> GetRequestorDetails(string requestorId);
        public string SaveRequestorStatusInfoToDB(string requestorId, RequestStatus status);
        public IEnumerable<Donor> GetDonorDetails(string donorId);
        public string SaveDonorStatusInfoToDB(string donorId, DonateBlood donateBlood);
    }
}