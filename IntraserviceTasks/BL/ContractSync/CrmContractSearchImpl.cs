using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.CrossCuttingConcern;
using IntraserviceTasks.Entities;
using IntraserviceTasks.Res;

namespace IntraserviceTasks.BL.ContractSync
{
    class CrmContractSearchByNumberImpl : CrmContractSearchByNumber
    {
        private static List<Contract> _crmContractsCache;

        public CrmContractSearchByNumberImpl()
        {
            if (CacheIsEmpty())
                FillCache();
        }

        private bool CacheIsEmpty()
        {
            return _crmContractsCache == null || _crmContractsCache.Count == 0;
        }

        private void FillCache()
        {
            var contractRepository = ExternalSystems.CrmContractsRepository.Get();
            _crmContractsCache = contractRepository.GetAll();
        }

        public List<Contract> Find(string contractNumber)
        {
            string numberEnding = GetSearchEndingString(contractNumber);

            return GetContractsWithEndingOfNumber(numberEnding);
        }

        private string GetSearchEndingString(string wholeNumber)
        {
            int startIndex = wholeNumber.IndexOf(Constants.CONTRACT_NUMBER_PREVIOUS_SIGN);

            if ( startIndex < 0)
                return wholeNumber;

            return wholeNumber.Substring(startIndex).ToUpper();
        }

        private List<Contract> GetContractsWithEndingOfNumber(string numberEnding)
        {
            string numberEndingUPPER = numberEnding.Trim().ToUpper();
            return _crmContractsCache.Where(o => o.Number.Trim().ToUpper().EndsWith(numberEndingUPPER)).ToList();
        }
    }
}
