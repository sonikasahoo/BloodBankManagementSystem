using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.Services
{
    public interface IServiceBloodAvailability
    {
        public int GetBloodCount(string bloodGroup);
    }
}
