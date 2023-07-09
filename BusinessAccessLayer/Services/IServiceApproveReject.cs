using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace BusinessAccessLayer.Services
{
    public interface IServiceApproveReject
    {
        public IEnumerable<Requestor> getreqdetails(string requestorId);
        public string saverequestorstatusinfotodb(string requestorId, RequestStatus status);
        public IEnumerable<Donor> getdonordetails(string donorId);
        public string savedonorstatusinfotodb(string donorId, DonateBlood donateBlood);
    }
}