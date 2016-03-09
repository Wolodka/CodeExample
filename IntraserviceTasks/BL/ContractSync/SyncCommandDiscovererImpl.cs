using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.BL.ContractSync.SyncCommands;
using IntraserviceTasks.CrossCuttingConcern;
using IntraserviceTasks.Entities;
using IntraserviceTasks.Entities.Enums;
using IntraserviceTasks.Res;

namespace IntraserviceTasks.BL.ContractSync
{
    class SyncCommandDiscovererImpl : SyncCommandDiscoverer
    {
        private Contract _intraserviceTask;
        private List<Contract> _crmContracts;
        private List<MustCheckContractReason> _checkReasons = new List<MustCheckContractReason>();
        private List<SyncCommand> _syncCommands = new List<SyncCommand>();

        public SyncCommandDiscovererImpl(Contract contract)
        {
            _intraserviceTask = contract;
        }

        public List<SyncCommand> Discover()
        {
            FindCrmContracts();
            AddCheckReasonByFewContractsWithSameNumber();
            AddCheckReasonByFoundInCrmContragents();

            if (IfNoCrmContracts())
                return GetCreateCommand();
            else
                return GetCommandsForSeveralCrmContracts();
        }

        private void FindCrmContracts()
        {
            CrmContractSearchByNumber contractSearch = new CrmContractSearchByNumberImpl();
            _crmContracts = contractSearch.Find(_intraserviceTask.Number);
        }

        private void AddCheckReasonByFewContractsWithSameNumber()
        {
            if (_crmContracts.Count > 1)
                _checkReasons.Add(MustCheckContractReason.FewContractsWithSameNumber);
        }

        private void AddCheckReasonByFoundInCrmContragents()
        {
            var contragentRepository = ExternalSystems.ContragentRepository.Get();
            var crmContragents = contragentRepository.GetByCompositeInnString(_intraserviceTask.Contragent.INN);

            if (crmContragents.Count == 0)
                _checkReasons.Add(MustCheckContractReason.ContragentNotFound);
            
            if (crmContragents.Count > 1)
                _checkReasons.Add(MustCheckContractReason.ContragentIsNotUnique);
        }

        private bool IfNoCrmContracts()
        {
            return _crmContracts.Count == 0;
        }

        private bool NeedToCheckBySeveralContragents()
        {
            return _checkReasons.Contains(MustCheckContractReason.ContragentIsNotUnique);
        }

        private List<SyncCommand> GetCreateCommand()
        {
            SyncCommand command;
            if (NeedToCheckBySeveralContragents())
                command = new CommandCreateAndCheck(_intraserviceTask, _checkReasons);
            else
                command = new CommandCreateContract(_intraserviceTask);

            return new List<SyncCommand> { command };
        }

        //---

        private List<SyncCommand> GetCommandsForSeveralCrmContracts()
        {
            List<SyncCommand> syncCommands = new List<SyncCommand>();
            foreach (Contract contract in _crmContracts)
                AppendSyncCommandForCrmContract(syncCommands, contract);

            return syncCommands;
        }

        private void AppendSyncCommandForCrmContract(List<SyncCommand> syncCommands, Contract crmContract)
        {
            List<MustCheckContractReason> reasons = GetReasonsToCheckCrmContract(crmContract);

            if( reasons.Count == 0 )
                syncCommands.Add(new CommandUpdateContract(crmContract.CrmGuid, _intraserviceTask));
            else
                syncCommands.Add(new CommandMarkAsMustBeChecked(crmContract.Id, reasons));
        }

        private List<MustCheckContractReason> GetReasonsToCheckCrmContract(Contract crmContract)
        {
            List<MustCheckContractReason> reasons = new List<MustCheckContractReason>();
            reasons.AddRange(_checkReasons);

            if (crmContract.OpportunityNumber != _intraserviceTask.OpportunityNumber)
                reasons.Add(MustCheckContractReason.WrongOpportunityNumber);

            if (crmContract.Type != _intraserviceTask.Type)
                reasons.Add(MustCheckContractReason.WrongContractType);

            return reasons;
        }

    }
}
