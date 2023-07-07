using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.Services
{
    public interface IServiceRequestBlood
    {
        public RequestBlood RequestForBlood(RequestBlood request);
        public IEnumerable<RequestStatus> GetRequestStatuses(string requestorId);

    }
}
