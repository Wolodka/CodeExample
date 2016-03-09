using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.Entities;

namespace IntraserviceTasks.CrossCuttingConcern.SystemInterfaces
{
    public interface CrmContragentRepository
    {
        List<Contragent> GetByCompositeInnString(string INN);
        Contragent Get(Guid crmGuid);
    }
}
