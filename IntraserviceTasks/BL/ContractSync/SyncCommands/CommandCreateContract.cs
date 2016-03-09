using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.CrossCuttingConcern;
using IntraserviceTasks.Entities;

namespace IntraserviceTasks.BL.ContractSync.SyncCommands
{
    class CommandCreateContract : SyncCommand
    {
        private Contract _contract;

        public CommandCreateContract(Contract contract) 
        {
            _contract = contract;
        }

        public override void Execute()
        {
            CrmContractsRepository crmDb = ExternalSystems.CrmContractsRepository.Get();
            crmDb.CreateContract(_contract);
        }
    }
}
