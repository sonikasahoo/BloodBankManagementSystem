using DataAccessLayer.Contracts;
using DataAccessLayer.DataAccess;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace BusinessAccessLayer.Services
{
    public class ServiceApproveReject : IServiceApproveReject
    {
        public readonly IDAL_ApproveReject_repository _iDAL_ApproveReject_repository;

        public ServiceApproveReject(IDAL_ApproveReject_repository DAL_ApproveReject_repository)
        {
            _iDAL_ApproveReject_repository = DAL_ApproveReject_repository;
        }

        public IEnumerable<Requestor> getreqdetails(string requestorId)
        {
            return _iDAL_ApproveReject_repository.GetRequestorDetails(requestorId).ToList();

        }
        public string saverequestorstatusinfotodb(string requestorId, RequestStatus status)
        {
            return _iDAL_ApproveReject_repository.SaveRequestorStatusInfoToDB(requestorId, status);


        }
        public IEnumerable<Donor> getdonordetails(string donorId)
        {
            return _iDAL_ApproveReject_repository.GetDonorDetails(donorId).ToList();




        }
        public string savedonorstatusinfotodb(string donorId, DonateBlood donateBlood)
        {
            return _iDAL_ApproveReject_repository.SaveDonorStatusInfoToDB(donorId, donateBlood);




        }
    }
}