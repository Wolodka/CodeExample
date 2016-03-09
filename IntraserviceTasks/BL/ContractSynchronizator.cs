using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.BL.ContractSync;
using IntraserviceTasks.BL.ContractSync.SyncCommands;
using IntraserviceTasks.CrossCuttingConcern;
using IntraserviceTasks.Entities;
using NLog;
using IntraserviceTasks.Res;

namespace IntraserviceTasks.BL
{
    public class ContractSynchronizator
    {
        private List<Contract> _contracts = new List<Contract>();
        private Logger _logger = LogManager.GetCurrentClassLogger();

        public void Sync()
        {
            RetrieveContractsFromIS();

            foreach (var contract in _contracts)
                TryToSyncSingleContract(contract);
        }

        private void RetrieveContractsFromIS()
        {
            _contracts = ExternalSystems.ISContractRetriever.Get().GetAll();
        }

        private void TryToSyncSingleContract(Contract contract)
        {
            try
            {
                SyncSingleContract(contract);
            }
            catch (Exception exc)
            {
                _logger.Error(Exceptions.ERROR_WHILE_SYNCING_CONTRACT, contract.Id.ToString(), exc.Message);
            }
        }

        private void SyncSingleContract(Contract contract)
        {

            SyncCommandDiscoverer discoverer = new SyncCommandDiscovererImpl(contract);
            List<SyncCommand> commands = discoverer.Discover();
            foreach (var command in commands)
                command.Execute();

            _logger.Info(LogMessages.FINISHED_SYNC_OF_SINGLE_CONTRACT_WITH_ID, contract.Id.ToString());
        }
    }
}
