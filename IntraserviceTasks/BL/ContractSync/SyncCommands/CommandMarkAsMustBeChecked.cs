using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.BL.ContractSync.SyncCommands;
using IntraserviceTasks.CrossCuttingConcern;
using IntraserviceTasks.Entities;
using IntraserviceTasks.Entities.Enums;

namespace IntraserviceTasks.BL.ContractSync
{
    class CommandMarkAsMustBeChecked : SyncCommand
    {
        private int _intraserviceId;
        private List<MustCheckContractReason> _checkReasons;

        public CommandMarkAsMustBeChecked(int intraserviceId, List<MustCheckContractReason> checkReasons)
        {
            _checkReasons = checkReasons;
            _intraserviceId = intraserviceId;
        }

        public override void Execute()
        {
            CrmContractsRepository crmDb = ExternalSystems.CrmContractsRepository.Get();
            crmDb.MarkContractAsMustBeChecked(_intraserviceId, _checkReasons);
        }
    }
}
