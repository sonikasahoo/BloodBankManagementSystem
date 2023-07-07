using DataAccessLayer.Contracts;
using DataAccessLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.Services
{
    public class ServiceBloodAvailability : IServiceBloodAvailability
    {

        public readonly IDAL_BloodAvailability_repository _iDAL_BloodAvailability_repository;

        public ServiceBloodAvailability(IDAL_BloodAvailability_repository DAL_BloodAvailability_repository)
        {
            _iDAL_BloodAvailability_repository = DAL_BloodAvailability_repository;
        }
        public int GetBloodCount(string bloodGroup)
        {
            return _iDAL_BloodAvailability_repository.GetBloodStatus(bloodGroup);
        }
    }
}
