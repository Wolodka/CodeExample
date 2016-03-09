using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.Entities;

namespace IntraserviceTasks.BL.ContractSync
{
    interface CrmContractSearchByNumber
    {
        List<Contract> Find(string contractNumber);
    }
}
