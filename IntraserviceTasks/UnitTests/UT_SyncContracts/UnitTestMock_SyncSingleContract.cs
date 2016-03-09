using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.BL.ContractSync;
using IntraserviceTasks.BL.ContractSync.SyncCommands;
using IntraserviceTasks.CrossCuttingConcern;
using IntraserviceTasks.Entities;
using IntraserviceTasks.Entities.Enums;
using IntraserviceTasks.UnitTests.Mock;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntraserviceTasks.UnitTests
{
    [TestClass]
    public class UnitTestMockSyncSingleContract
    {
        private Contract _contract;
        private Contract _operatedContract
        {
            get
            {
                CrmContractsRepository crmDb = ExternalSystems.CrmContractsRepository.Get(); 
                return crmDb.GetContractByIntraserviceIdOrException(_contract.Id);
            }
        }

        [TestInitialize()]
        public void Init()
        {
            List<Contract> contracts = new List<Contract>()
            {
                new Contract { Number = "Number №11111 End", Type = ContractType.Income, OpportunityNumber = 100 },
                new Contract { Number = "Number №11112 End", Type = ContractType.Income, OpportunityNumber = 101 },
                new Contract { Number = "Number №11113 End", Type = ContractType.Outgo, OpportunityNumber = 102, MustBeChecked = true, TotalAmount = 1000 },
                new Contract { Number = "Number №11114 End", Type = ContractType.Income, OpportunityNumber = 103, MustBeChecked = false },
                new Contract { Number = "Number №11115 End", Type = ContractType.Income, OpportunityNumber = 104 },
                new Contract { Number = "Number №11116 End", Type = ContractType.Income, OpportunityNumber = 105 },
                new Contract { Number = "Number №11117 End", Type = ContractType.Income, OpportunityNumber = 106 },
                new Contract { Number = "Number №11118 End", Type = ContractType.Outgo, OpportunityNumber = 107 },
                new Contract { Number = "Number №11119 End", Type = ContractType.Income, OpportunityNumber = 108 },
            };

            CrmContractsRepository crmDb = new MockCrmContractsRepository(contracts);
            ExternalSystems.CrmContractsRepository.Init(crmDb);
        }

        [TestMethod]
        public void TestSyncSingleContractWithCreatingNewContract()
        { 
            _contract = new Contract { Number = "Number №51111 End", Type = ContractType.Income, OpportunityNumber = 100 };

            MakeOperationWithContract();

            Contract operated = _operatedContract;
            Assert.AreEqual(_contract.Type, operated.Type);
            Assert.AreEqual(_contract.OpportunityNumber, operated.OpportunityNumber);
        }

        [TestMethod]
        public void TestSyncSingleContractWithUpdatingContract()
        {
            _contract = new Contract
            {
                Number = "Number №11113 End", 
                Type = ContractType.Outgo, OpportunityNumber = 102, TotalAmount = 5000 };

            MakeOperationWithContract();

            Contract operated = _operatedContract;
            Assert.AreEqual(5000, operated.TotalAmount);
            Assert.IsFalse(operated.MustBeChecked);
        }

        [TestMethod]
        public void TestSyncSingleContractWithMustBeCheckedContract()
        {
            _contract = new Contract { Number = "Number №11114 End", Type = ContractType.Income, OpportunityNumber = 93 };

            MakeOperationWithContract();

            Assert.IsTrue(_operatedContract.MustBeChecked);
        }

        private void MakeOperationWithContract()
        {
            List<SyncCommand> commands = GetSyncDiscoverer(_contract).Discover();
            commands[0].Execute();
        }

        private SyncCommandDiscoverer GetSyncDiscoverer(Contract contract)
        {
            SyncCommandDiscoverer discoverer = new SyncCommandDiscovererImpl(contract);
            return discoverer;
        }
    }
}
