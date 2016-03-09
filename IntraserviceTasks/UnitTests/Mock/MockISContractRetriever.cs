using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.BL;
using IntraserviceTasks.BL.ContractSync;
using IntraserviceTasks.Entities;

namespace IntraserviceTasks.UnitTests.Mock
{
    class MockISContractRetriever : ContractRetriever
    {
        private List<Contract> _contracts;

        public MockISContractRetriever(List<Contract> contracts)
        {
            _contracts = contracts;
        }

        public List<Contract> GetAll()
        {
            return _contracts;
        }

        public Contract Get(int id)
        {
            return _contracts.First(o => o.Id == id);
        }
    }
}
