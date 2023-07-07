using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Contracts
{
    public interface IDAL_RequestBloodAndCheck_repository
    {
        public RequestBlood MakeRequest(RequestBlood request);
        public IEnumerable<RequestStatus> GetRequestorStatus(string requestorId);

    }
}