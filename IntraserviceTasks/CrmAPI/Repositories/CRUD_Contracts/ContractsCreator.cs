using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.CrmAPI.Converters;
using IntraserviceTasks.CrmAPI.Converters.AttrConverters;
using IntraserviceTasks.Entities;
using Microsoft.Xrm.Sdk;

namespace IntraserviceTasks.CrmAPI.CRUD_Contracts
{
    class ContractsCreator
    {
        public void Create(Contract contract)
        {
            Entity crmContract = new ContractConverter().ConvertToCrmEntity(contract);

            var crmService = CrmService.GetProxy();
            crmService.Create(crmContract);
        }
    }
}
