using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

 

namespace BusinessAccessLayer.Services
{
    public interface IServiceBusinessDonateBloodAndCheck
    {
        public DonateBlood DonateBloodRequestToDB(DonateBlood donateBlood);
        public (string DonorId, string FullName, string Status) DonorStatus(string donorId);
    }
}