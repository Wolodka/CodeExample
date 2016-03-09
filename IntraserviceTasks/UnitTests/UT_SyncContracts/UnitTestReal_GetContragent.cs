using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.CrmAPI.Repositories;
using IntraserviceTasks.CrossCuttingConcern;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntraserviceTasks.UnitTests.UT_SyncContracts
{
    [TestClass]
    public class UnitTestReal_GetContragent
    {
        [TestInitialize()]
        public void Init()
        {
            ExternalSystems.ISContractRetriever.Init(new IS_API.ISContractRetriever());
            ExternalSystems.ContragentRepository.Init(new CrmContragentRepositoryImpl());
        }

        [TestMethod]
        public void TestGetRealContragentFromIS()
        {
            int intraserviceTaskId = 14848;

            var task = ExternalSystems.ISContractRetriever.Get().Get(intraserviceTaskId);

            Assert.AreEqual("7704773585", task.Contragent.INN);
        }

        [TestMethod]
        public void TestGetRealContragentFromCrm()
        { 
            
        }
    }
}
