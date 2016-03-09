using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.Entities;

namespace IntraserviceTasks.CrossCuttingConcern
{
    public interface CrmUserRepository
    {
        Person GetByGuid(Guid crmGuid);
        Guid GetGuidByLogin(string loginWithoutDomain);
    }
}
