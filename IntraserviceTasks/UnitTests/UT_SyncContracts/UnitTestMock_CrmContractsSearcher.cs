using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.BL.ContractSync;
using IntraserviceTasks.CrossCuttingConcern;
using IntraserviceTasks.Entities;
using IntraserviceTasks.Entities.Enums;
using IntraserviceTasks.UnitTests.Mock;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntraserviceTasks.UnitTests
{
    [TestClass]
    public class UnitTestCrmContractsSearcher
    {
        [TestInitialize()]
        public void Init()
        {
            List<Contract> contracts = new List<Contract>()
            {
                new Contract { Number = "Contract №12345 End", OpportunityNumber = 21111, Type = ContractType.Income },
                new Contract { Number = "New Contract №12345 End", OpportunityNumber = 41111, Type = ContractType.Income },
                new Contract { Number = "№12345 End", OpportunityNumber = 31111, Type = ContractType.Income },
                new Contract { Number = "Contract №12345 End Line", OpportunityNumber = 5555, Type = ContractType.Outgo },
                new Contract { Number = "Trulala a №1345345 bla", OpportunityNumber = 9999, Type = ContractType.Income },
                new Contract { Number = "Bla a №176756 bla", OpportunityNumber = 1000, Type = ContractType.Income },
                new Contract { Number = "Bla a №1777745 bla", OpportunityNumber = 2000, Type = ContractType.Outgo },
                new Contract { Number = "Bla a №123456789 bla", OpportunityNumber = 3000, Type = ContractType.Outgo },
            };

            CrmContractsRepository crmDb = new MockCrmContractsRepository(contracts);
            ExternalSystems.CrmContractsRepository.Init(crmDb);
        }

        [TestMethod]
        public void TestGetThreeCrmContracts()
        {
            Contract contractIS = new Contract()
            {
                Number = "Contract №12345 End",
                OpportunityNumber = 11111,
            };

            List<Contract> contractsCrm = GetCrmContractSearch().Find(contractIS.Number);
            Assert.AreEqual(3, contractsCrm.Count);
        }

        private CrmContractSearchByNumber GetCrmContractSearch()
        {
            return new CrmContractSearchByNumberImpl();
        }
    }
}
