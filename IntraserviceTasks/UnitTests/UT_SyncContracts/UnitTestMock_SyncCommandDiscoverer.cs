using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.BL.ContractSync;
using IntraserviceTasks.BL.ContractSync.SyncCommands;
using IntraserviceTasks.CrmAPI.Repositories;
using IntraserviceTasks.CrossCuttingConcern;
using IntraserviceTasks.CrossCuttingConcern.SystemInterfaces;
using IntraserviceTasks.Entities;
using IntraserviceTasks.Entities.Enums;
using IntraserviceTasks.UnitTests.Mock;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntraserviceTasks.UnitTests
{
    [TestClass]
    public class UnitTestMock_SyncCommandDiscoverer
    {
        [TestInitialize()]
        public void Init()
        {
            InitCrmContracts();
            InitCrmContragents();
        }

        private void InitCrmContracts()
        {
            List<Contract> contracts = new List<Contract>()
            {
                new Contract { Id = 100, Number = "Number №11111 End", Type = ContractType.Income, 
                    OpportunityNumber = 100, Contragent = new Contragent() { INN = "00001"} },
                new Contract { Id = 101, Number = "Number №11112 End", Type = ContractType.Income, 
                    OpportunityNumber = 101, Contragent = new Contragent() { INN = "00002"} },
                new Contract { Id = 102, Number = "Number №11113 End", Type = ContractType.Outgo, 
                    OpportunityNumber = 102, Contragent = new Contragent() { INN = "00003"} },
                new Contract { Id = 103, Number = "Number №11114 End", Type = ContractType.Income, 
                    OpportunityNumber = 103, Contragent = new Contragent() { INN = "00004"} },
                new Contract { Id = 104, Number = "Number №11115 End", Type = ContractType.Income, 
                    OpportunityNumber = 104, Contragent = new Contragent() { INN = "00005"} },
                new Contract { Id = 105, Number = "Number №11116 End", Type = ContractType.Income, 
                    OpportunityNumber = 105, Contragent = new Contragent() { INN = "00006"} },
                new Contract { Id = 106, Number = "Number №11117 End", Type = ContractType.Income, 
                    OpportunityNumber = 106, Contragent = new Contragent() { INN = "00007"} },
                new Contract { Id = 107, Number = "Number №11118 End", Type = ContractType.Outgo, 
                    OpportunityNumber = 107, Contragent = new Contragent() { INN = "00008"} },
                new Contract { Id = 108, Number = "Number №11119 End", Type = ContractType.Income, 
                    OpportunityNumber = 108, Contragent = new Contragent() { INN = "00009"} },
                new Contract { Id = 109, Number = "Number №aaaa9 End", Type = ContractType.Income, 
                    OpportunityNumber = 100, Contragent = new Contragent() { INN = "00009"} },
                new Contract { Id = 110, Number = "№aaaa9 End", Type = ContractType.Income, 
                    OpportunityNumber = 100, Contragent = new Contragent() { INN = "00009"} },
            };

            CrmContractsRepository crmDb = new MockCrmContractsRepository(contracts);
            ExternalSystems.CrmContractsRepository.Init(crmDb);
        }

        private void InitCrmContragents()
        {
            List<Contragent> contragents = new List<Contragent>
            {
                new Contragent { INN = "00001", Name = "Contr 1" },
                new Contragent { INN = "00002", Name = "Contr 2" },
                new Contragent { INN = "00003", Name = "Contr 3" },
                new Contragent { INN = "00004", Name = "Contr 4" },
                new Contragent { INN = "00005", Name = "Contr 5" },
                new Contragent { INN = "00006", Name = "Contr 6" },
                new Contragent { INN = "00009", Name = "Contr Nine" },
                new Contragent { INN = "00009", Name = "Contr Devyat" },
            };

            CrmContragentRepository crmContragents = new MockCrmContragents(contragents);
            ExternalSystems.ContragentRepository.Init(crmContragents);
        }

        [TestMethod]
        public void TestDiscoverOneCreateCommand()
        {
            Contract contractIS = new Contract { Number = "Number №55555 End", Type = ContractType.Income, OpportunityNumber = 100 };
            List<SyncCommand> commands = GetSyncDiscoverer(contractIS).Discover();
            Assert.AreEqual(1, commands.Count);

            string commandTypeName = new CommandCreateContract(contractIS).GetType().Name;
            Assert.AreEqual(commandTypeName, commands[0].GetType().Name);
        }

        [TestMethod]
        public void TestDiscoverOneCreateCommandAndMarkAsMustBeCheckedBecauseOfMultipleContragents()
        {
            Contract contractIS = new Contract
            {
                Number = "Number №55555 End",
                Type = ContractType.Income,
                OpportunityNumber = 100,
                Contragent = new Contragent { INN = "00009" }
            };

            var commands = GetSyncDiscoverer(contractIS).Discover();
            Assert.AreEqual(1, commands.Count);

            string commandTypeName = new CommandCreateAndCheck(contractIS, new List<MustCheckContractReason>()).GetType().Name;
            Assert.AreEqual(commandTypeName, commands[0].GetType().Name);
        }

        [TestMethod]
        public void TestDiscoverOneUpdateCommand()
        {
            Contract contractIS = new Contract
            {
                Id = 101,
                Number = "Some number №11112 End",
                Type = ContractType.Income,
                OpportunityNumber = 101,
                Contragent = new Contragent { INN = "00005" }
            };

            List<SyncCommand> commands = GetSyncDiscoverer(contractIS).Discover();

            string commandTypeName = new CommandUpdateContract(contractIS.CrmGuid, contractIS).GetType().Name;
            Assert.AreEqual(1, commands.Count);
            Assert.AreEqual(commandTypeName, commands[0].GetType().Name);
        }

        [TestMethod]
        public void TestDiscoverOneMustBeCheckedCommand()
        {
            Contract contractIS = new Contract { Id = 101, Number = "Some number №11112 End", Type = ContractType.Outgo, OpportunityNumber = 101 };

            List<SyncCommand> commands = GetSyncDiscoverer(contractIS).Discover();

            string commandTypeName = new CommandMarkAsMustBeChecked(contractIS.Id, new List<MustCheckContractReason>()).GetType().Name;
            Assert.AreEqual(1, commands.Count);
            Assert.AreEqual(commandTypeName, commands[0].GetType().Name);
        }

        [TestMethod]
        public void TestDiscoverTwoMustBeCheckedCommandsBecauseOfMultipleContragents()
        {
            Contract contractIS = new Contract
            {
                Number = "Number №aaaa9 End",
                Type = ContractType.Income,
                OpportunityNumber = 100,
                Contragent = new Contragent { INN = "00009" }
            };

            var commands = GetSyncDiscoverer(contractIS).Discover();
            Assert.AreEqual(2, commands.Count);

            string commandTypeName = new CommandMarkAsMustBeChecked(contractIS.Id, new List<MustCheckContractReason>()).GetType().Name;
            Assert.AreEqual(commandTypeName, commands[0].GetType().Name);
            Assert.AreEqual(commandTypeName, commands[1].GetType().Name);
        }

        private SyncCommandDiscoverer GetSyncDiscoverer(Contract contract)
        {
            return new SyncCommandDiscovererImpl(contract);
        }
    }
}
