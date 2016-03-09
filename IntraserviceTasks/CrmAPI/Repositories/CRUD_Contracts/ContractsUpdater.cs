using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.CrmAPI.Converters;
using IntraserviceTasks.Entities;
using IntraserviceTasks.Entities.Enums;
using Microsoft.Xrm.Sdk;

namespace IntraserviceTasks.CrmAPI.CRUD_Contracts
{
    class ContractsUpdater
    {
        public void MarkAsMustBeChecked(int intraserviceId, List<MustCheckContractReason> checkReasons)
        {
            Contract contract = new ContractsGetter().GetContractByIntraserviceIdOrException(intraserviceId);
            contract.MustBeChecked = true;
            contract.CheckReasons = checkReasons;

            Update(contract);
        }

        public void Update(Contract contract)
        {
            var crmService = CrmService.GetProxy();
            Entity crmContract = new ContractConverter().ConvertToCrmEntity(contract);
            crmService.Update(crmContract);
        }
    }
}
