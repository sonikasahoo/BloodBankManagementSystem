using DataAccessLayer.Contracts;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace BusinessAccessLayer.Services
{
    public class ServiceBusinessDonateBloodAndCheck : IServiceBusinessDonateBloodAndCheck
    {
        public readonly IDAL_BusinessDonateBloodAndCheck_repository _iDAL_BusinessDonateBloodAndCheck_repository;



        public ServiceBusinessDonateBloodAndCheck(IDAL_BusinessDonateBloodAndCheck_repository iDAL_BusinessDonateBloodAndCheck_repository)
        {
            _iDAL_BusinessDonateBloodAndCheck_repository = iDAL_BusinessDonateBloodAndCheck_repository;
        }



        public DonateBlood DonateBloodRequestToDB(DonateBlood donateBlood)
        {
            return _iDAL_BusinessDonateBloodAndCheck_repository.SaveDonateBloodRequestToDB(donateBlood);
        }



        public (string DonorId, string FullName, string Status) DonorStatus(string donorId)
        {
            return _iDAL_BusinessDonateBloodAndCheck_repository.GetDonorStatus(donorId);
        }
    }

}