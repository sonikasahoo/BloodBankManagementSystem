using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;

namespace DataAccessLayer.Contracts
{
    public interface IDAL_BloodAvailability_repository
    {
        public int GetBloodStatus(string bloodGroup);
    }
}
