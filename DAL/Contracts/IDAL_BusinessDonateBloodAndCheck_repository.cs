using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace DataAccessLayer.Contracts
{
    public interface IDAL_BusinessDonateBloodAndCheck_repository
    {
        public DonateBlood SaveDonateBloodRequestToDB(DonateBlood donateBlood);
        public (string DonorId, string FullName, string Status) GetDonorStatus(string donorId);



    }
}