using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.BL.ContractSync;
using IntraserviceTasks.CrossCuttingConcern;
using IntraserviceTasks.Entities;
using IntraserviceTasks.Entities.Enums;
using IntraserviceTasks.Res;

namespace IntraserviceTasks.UnitTests.Mock
{
    class MockCrmContractsRepository : CrmContractsRepository
    {
        private List<Contract> _contracts;

        public MockCrmContractsRepository(List<Contract> contracts)
        {
            _contracts = contracts;
        }

        public List<Contract> GetAll()
        {
            return _contracts;
        }

        public Contract GetContractByIntraserviceIdOrException(int intraserviceId)
        {
            return _contracts.First(o => o.Id == intraserviceId);
        }

        public void CreateContract(Contract contract)
        {
            _contracts.Add(contract);
        }

        public void UpdateContract(Guid contractCrmGuid, Contract contract)
        {
            Contract existing = GetContractByIntraserviceIdOrException(contract.Id);
            
            existing.Id = contract.Id;
            existing.MustBeChecked = contract.MustBeChecked;
            existing.TotalAmount = contract.TotalAmount;
            existing.CrmGuid = contract.CrmGuid;
        }

        public void MarkContractAsMustBeChecked(int intraserviceId, List<MustCheckContractReason> checkReasons)
        {
            Contract existing = GetContractByIntraserviceIdOrException(intraserviceId);
            existing.MustBeChecked = true;
        }
        
        public Contract Get(int taskId)
        {
            return _contracts.First(o => o.Id == taskId);
        }
    }
}
