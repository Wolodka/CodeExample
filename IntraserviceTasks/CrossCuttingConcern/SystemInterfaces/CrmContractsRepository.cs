using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.BL.ContractSync;
using IntraserviceTasks.Entities;
using IntraserviceTasks.Entities.Enums;

namespace IntraserviceTasks.CrossCuttingConcern
{
    public interface CrmContractsRepository : ContractRetriever
    {
        void CreateContract(Contract contract);
        void UpdateContract(Guid contractId, Contract contract);
        void MarkContractAsMustBeChecked(int intraserviceId, List<MustCheckContractReason> checkReasons);
        Contract GetContractByIntraserviceIdOrException(int intraserviceId);
    }
}
