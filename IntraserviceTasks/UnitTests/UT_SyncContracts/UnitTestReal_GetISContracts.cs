using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.BL;
using IntraserviceTasks.BL.ContractSync;
using IntraserviceTasks.CrossCuttingConcern;
using IntraserviceTasks.Entities;
using IntraserviceTasks.Entities.Enums;
using IntraserviceTasks.IS_API;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntraserviceTasks.UnitTests
{
    [TestClass]
    public class UnitTestGetRealISContracts
    {
        [TestInitialize()]
        public void Init()
        {
            ExternalSystems.ISContractRetriever.Init(new ISContractRetriever());
        }

        [TestMethod]
        public void TestGetRealISContracts()
        {
            var contractRetriever = ExternalSystems.ISContractRetriever.Get();

            var contracts = contractRetriever.GetAll();

            Assert.IsTrue(contracts.Count > 0);

            var contract14804 = contracts.First(o => o.Id == 14804);
            Assert.AreEqual("Договор №03/19-о от 01.08.2015", contract14804.Number);
        }

        [TestMethod]
        public void TestGetOneRealContract()
        {
            int realContractNumber = 14804;

            Contract contract = ExternalSystems.ISContractRetriever.Get().Get(realContractNumber);

            Assert.IsFalse(string.IsNullOrWhiteSpace(contract.Number));
            Assert.AreEqual("Договор №03/19-о от 01.08.2015", contract.Number);
        }

        [TestMethod]
        public void TestGetTypeOfRealContracts()
        {
            AssertSingleRealTypeOfContracts(14848, ContractType.Outgo);
            AssertSingleRealTypeOfContracts(17119, ContractType.Outgo);
            AssertSingleRealTypeOfContracts(1094, ContractType.Ramochnyi);
        }

        private void AssertSingleRealTypeOfContracts(int contractId, ContractType expectedType)
        {
            var contract = ExternalSystems.ISContractRetriever.Get().Get(contractId);
            Assert.AreEqual(expectedType, contract.Type);
        }
    }
}
