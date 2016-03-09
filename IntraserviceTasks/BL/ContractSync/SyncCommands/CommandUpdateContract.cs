using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.CrossCuttingConcern;
using IntraserviceTasks.Entities;

namespace IntraserviceTasks.BL.ContractSync.SyncCommands
{
    class CommandUpdateContract : SyncCommand
    {
        private Guid _contractCrmGuid;
        private Contract _contract;

        public CommandUpdateContract(Guid contractCrmGuid, Contract contract) 
        {
            _contractCrmGuid = contractCrmGuid;
            _contract = contract;
        }

        public override void Execute()
        {
            _contract.MustBeChecked = false;

            CrmContractsRepository crmDb = ExternalSystems.CrmContractsRepository.Get();
            crmDb.UpdateContract(_contractCrmGuid, _contract);
        }
    }
}
