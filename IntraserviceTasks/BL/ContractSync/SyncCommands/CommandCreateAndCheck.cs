using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.CrossCuttingConcern;
using IntraserviceTasks.Entities;
using IntraserviceTasks.Entities.Enums;

namespace IntraserviceTasks.BL.ContractSync.SyncCommands
{
    class CommandCreateAndCheck : SyncCommand
    {
        private Contract _contract;

        public CommandCreateAndCheck(Contract contract, List<MustCheckContractReason> checkReasons) 
        {
            _contract = contract;
            _contract.CheckReasons = checkReasons;
            _contract.MustBeChecked = true;
        }

        public override void Execute()
        {
            CrmContractsRepository crmDb = ExternalSystems.CrmContractsRepository.Get();
            crmDb.CreateContract(_contract);
        }
    }
}
