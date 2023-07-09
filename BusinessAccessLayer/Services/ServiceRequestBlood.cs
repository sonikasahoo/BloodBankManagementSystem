using DataAccessLayer.Contracts;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.Services
{
    public class ServiceRequestBlood : IServiceRequestBlood
    {
        public readonly IDAL_RequestBloodAndCheck_repository _iDAL_RequestBloodAndCheck_repository;

        public ServiceRequestBlood(IDAL_RequestBloodAndCheck_repository DAL_RequestBloodAndCheckr_repository)
        {
            _iDAL_RequestBloodAndCheck_repository = DAL_RequestBloodAndCheckr_repository;
        }
        public RequestBlood RequestForBlood(RequestBlood request)
        {
            return _iDAL_RequestBloodAndCheck_repository.MakeRequest(request);
        }
        public IEnumerable<RequestStatus> GetRequestStatuses(string requestorId)
        { 
            return _iDAL_RequestBloodAndCheck_repository.GetRequestorStatus(requestorId).ToList();

        }

    }
}