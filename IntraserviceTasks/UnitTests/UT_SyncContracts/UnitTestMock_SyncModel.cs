using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.BL;
using IntraserviceTasks.BL.ContractSync;
using IntraserviceTasks.BL.ContractSync.SyncCommands;
using IntraserviceTasks.CrmAPI.Repositories;
using IntraserviceTasks.CrossCuttingConcern;
using IntraserviceTasks.CrossCuttingConcern.SystemInterfaces;
using IntraserviceTasks.Entities;
using IntraserviceTasks.Entities.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntraserviceTasks.UnitTests
{
    [TestClass]
    public class UnitTestSyncMockModel
    {
        [TestInitialize()]
        public void Init()
        {
            InitISDatabase();
            InitCrmDatabase();
            InitCrmContragents();
        }

        private void InitISDatabase()
        {
            ContractRetriever ISRetriever = new Mock.MockISContractRetriever(
                new List<Contract> 
                {
                    new Contract { Id = 100, Number = "Number №11111 End", Type = ContractType.Income, OpportunityNumber = 100 },
                    new Contract { Id = 101, Number = "Number №11112 End", Type = ContractType.Income, OpportunityNumber = 101 },
                    new Contract { Id = 102, Number = "Number №11113 End", Type = ContractType.Outgo, OpportunityNumber = 102, MustBeChecked = true, TotalAmount = 1000 },
                    new Contract { Id = 103, Number = "Number №11114 End", Type = ContractType.Income, OpportunityNumber = 103, MustBeChecked = false },
                    new Contract { Id = 104, Number = "Number №11115 End", Type = ContractType.Income, OpportunityNumber = 104 },
                    new Contract { Id = 105, Number = "Number №11116 End", Type = ContractType.Income, OpportunityNumber = 105 },
                    new Contract { Id = 106, Number = "Number №11117 End", Type = ContractType.Income, OpportunityNumber = 106, TotalAmount = 4000 },
                    new Contract { Id = 107, Number = "Number №11118 End", Type = ContractType.Outgo, OpportunityNumber = 107 },
                    new Contract { Id = 108, Number = "Number №11119 End", Type = ContractType.Income, OpportunityNumber = 108 } //* should be created
                });

            ExternalSystems.ISContractRetriever.Init(ISRetriever);
        }

        private void InitCrmDatabase()
        {
            CrmContractsRepository CrmDb = new Mock.MockCrmContractsRepository(
                new List<Contract> 
                {
                    new Contract { Id = 100, Number = "Number №11111 End", Type = ContractType.Income, OpportunityNumber = 100 },
                    new Contract { Id = 101, Number = "Number №11112 End", Type = ContractType.Income, OpportunityNumber = 101 },
                    new Contract { Id = 102, Number = "Number №11113 End", Type = ContractType.Outgo, OpportunityNumber = 102, MustBeChecked = true, TotalAmount = 3000 }, //*
                    new Contract { Id = 103, Number = "Number №11114 End", Type = ContractType.Income, OpportunityNumber = 81, MustBeChecked = false }, //*
                    new Contract { Id = 104, Number = "Number №11115 End", Type = ContractType.Income, OpportunityNumber = 104 },
                    new Contract { Id = 105, Number = "Number №11116 End", Type = ContractType.Income, OpportunityNumber = 105 },
                    new Contract { Id = 106, Number = "Number №11117 End", Type = ContractType.Income, OpportunityNumber = 116, TotalAmount = 3000 }, //*
                    new Contract { Id = 107, Number = "Number №11118 End", Type = ContractType.Outgo, OpportunityNumber = 107 },
                    new Contract { Id = 110, Number = "Number №11117 End", Type = ContractType.Income, OpportunityNumber = 117, TotalAmount = 5000 }, //*

                });

            ExternalSystems.CrmContractsRepository.Init(CrmDb);
        }

        private void InitCrmContragents()
        {
            CrmContragentRepository crmContragents = new Mock.MockCrmContragents(new List<Contragent>{
                new Contragent { INN = "11111" },
                new Contragent { INN = "21111" },
                new Contragent { INN = "31111" },
                new Contragent { INN = "41111" },
                new Contragent { INN = "51111" },
                new Contragent { INN = "61111" },
                new Contragent { INN = "71111" }
            });

            ExternalSystems.ContragentRepository.Init(crmContragents);
        }

        [TestMethod]
        public void TestSyncContracts()
        {
            var isContracts = ExternalSystems.ISContractRetriever.Get().GetAll();

            foreach (var contract in isContracts)
            {
                SyncCommandDiscoverer discoverer = new SyncCommandDiscovererImpl(contract);
                List<SyncCommand> commands = discoverer.Discover();
                foreach (var command in commands)
                    command.Execute();
            }

            AssertCrmContractsCount(10);
            AssertUpdatedContractTotalAmountAndMustBeChecked(102, 1000);
            AssertMustBeCheckedAsTrue(106);
            AssertMustBeCheckedAsTrue(103);
            AssertTotalAmountDidNOTChangedWhenMarkedAsMustBeChecked();
            AssertCreatedContract();
        }

        private void AssertCrmContractsCount(int expected)
        {
            var crmContracts = ExternalSystems.CrmContractsRepository.Get().GetAll();
            Assert.AreEqual(expected, crmContracts.Count);
        }

        private void AssertUpdatedContractTotalAmountAndMustBeChecked(int intraserviceId, decimal expected)
        {
            var contract = ExternalSystems.CrmContractsRepository.Get().GetContractByIntraserviceIdOrException(intraserviceId);
            Assert.AreEqual(expected, contract.TotalAmount);
            Assert.IsFalse(contract.MustBeChecked);
        }

        private void AssertMustBeCheckedAsTrue(int intraserviceId)
        {
            var contract = ExternalSystems.CrmContractsRepository.Get().GetContractByIntraserviceIdOrException(intraserviceId);
            Assert.IsTrue(contract.MustBeChecked);
        }

        private void AssertTotalAmountDidNOTChangedWhenMarkedAsMustBeChecked()
        {
            List<Contract> crmContracts = ExternalSystems.CrmContractsRepository.Get().GetAll();
            Assert.IsNotNull(crmContracts.FirstOrDefault(o => o.Id == 106 && o.TotalAmount == 3000));
            Assert.IsNotNull(crmContracts.FirstOrDefault(o => o.Id == 110 && o.TotalAmount == 3000));
        }

        private void AssertCreatedContract()
        {
            var contract = ExternalSystems.CrmContractsRepository.Get().GetContractByIntraserviceIdOrException(108);
            Assert.IsNotNull(contract);
        }
    }
}
