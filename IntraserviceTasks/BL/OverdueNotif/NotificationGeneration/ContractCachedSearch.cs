using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.BL.ContractSync;
using IntraserviceTasks.CrossCuttingConcern;
using IntraserviceTasks.Entities;

namespace IntraserviceTasks.BL.OverdueNotif
{
    class ContractCachedSearch
    {
        private Dictionary<int, Contract> _cachedContracts = new Dictionary<int, Contract>();
        private ContractRetriever _contractRetriever;
        private Contract _contract;

        public ContractCachedSearch()
        {
            _contractRetriever = ExternalSystems.ISContractRetriever.Get();
        }

        public bool Find(int intraserviceId)
        {
            if (TryToFindInCache(intraserviceId))
                return true;

            if (TryToFindInRepository(intraserviceId))
            {
                AddFoundContractToCache();
                return true;
            }

            ClearFoundContract();
            return false;
        }

        private bool TryToFindInCache(int id)
        {
            if (_cachedContracts.Keys.Contains(id))
            {
                _contract = _cachedContracts[id];
                return true;
            }

            return false;
        }

        private bool TryToFindInRepository(int id)
        {
            try
            {
                _contract = _contractRetriever.Get(id);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void AddFoundContractToCache()
        {
            _cachedContracts[_contract.Id] = _contract;
        }

        private void ClearFoundContract()
        {
            _contract = null;
        }

        public Contract FoundContract
        {
            get
            {
                return _contract;
            }
        }
    }

}
