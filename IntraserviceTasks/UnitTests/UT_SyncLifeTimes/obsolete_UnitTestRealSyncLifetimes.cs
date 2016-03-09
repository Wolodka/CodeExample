using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.BL;
using IntraserviceTasks.CrmAPI.Repositories;
using IntraserviceTasks.CrossCuttingConcern;
using IntraserviceTasks.Entities;
using IntraserviceTasks.Entities.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntraserviceTasks.UnitTests.UT_SyncLifeTimes
{
    /*[TestClass]
    public class UnitTestRealSyncLifetimes
    {
        [TestInitialize()]
        public void Init()
        {
            EnvironmentSwitcher.Switch(EnvironmentType.Stage);
            ExternalSystems.ISTaskLifeTimesRetriever.Init(new IS_API.ISTaskLifetimeRetriever());
            ExternalSystems.CrmContractsRepository.Init(new CrmAPI.Repositories.CrmContractsRepositoryImpl());
            ExternalSystems.CrmTaskLifeTimesRetriever.Init(new CrmLifetimesRepository());
            ExternalSystems.CrmTaskLifeTimesRepository.Init(new CrmLifetimesRepository());
            ExternalSystems.CrmUserRepository.Init(new CrmUserRepositoryImpl());
            InitCalendar();
        }

        private void InitCalendar()
        {
            List<DateTime> restDays = new List<DateTime>()
            {
                new DateTime(2015, 12, 5),
                new DateTime(2015, 12, 6),
                new DateTime(2015, 12, 12),
                new DateTime(2015, 12, 13)
            };

            List<DateTime> shortDays = new List<DateTime>() { };

            ExternalSystems.ProductionCalendar.Init(new Mock.MockProductionCalendar(restDays, shortDays));
        }

        [TestMethod]
        public void TestSyncRealLifeTimes()
        {
            TaskLifeTimesRetriever ISRetriever = ExternalSystems.ISTaskLifeTimesRetriever.Get();
            var lifetimes = ISRetriever.Get();

            new LifeTimesSynchronizator().Sync(lifetimes);

            TaskLifeTimesRetriever crmRetriever = ExternalSystems.CrmTaskLifeTimesRetriever.Get();
            var crmLifetimes = crmRetriever.Get();

            Assert.AreEqual(lifetimes.Count, crmLifetimes.Count);
        }

        [TestMethod]
        public void TestUpdateRealLifeTimeStatus()
        {
            TestOneUpdateStatusOperation(ApprovalStatus.LeftComment);
            TestOneUpdateStatusOperation(ApprovalStatus.Approved);
        }

        private void TestOneUpdateStatusOperation(ApprovalStatus newApprovalStatus)
        {
            TaskLifeTime lifeTime = ExternalSystems.ISTaskLifeTimesRetriever.Get().Get().First(o => o.Id == 70000);
            lifeTime.ApprovalStatus = newApprovalStatus;

            var crmRepo = ExternalSystems.CrmTaskLifeTimesRepository.Get();
            crmRepo.Update(lifeTime);

            var crmRetriever = ExternalSystems.CrmTaskLifeTimesRetriever.Get();
            var updatedLifeTime = crmRetriever.Get().First(o => o.Id == 70000);

            Assert.AreEqual(newApprovalStatus, updatedLifeTime.ApprovalStatus);
        }
   
        [TestMethod]
        public void TestUpdateRealLifeTimeFinishOfApproval()
        {
            int ltId = 69975;
            TaskLifeTime lifeTime = ExternalSystems.ISTaskLifeTimesRetriever.Get().Get().First(o => o.Id == ltId);
            DateTime endOfApproval = new DateTime(2015, 10, 15, 16, 30, 45);
            lifeTime.ApprovalDate = endOfApproval;

            var crmRepo = ExternalSystems.CrmTaskLifeTimesRepository.Get();
            crmRepo.Update(lifeTime);

            var crmRetriever = ExternalSystems.CrmTaskLifeTimesRetriever.Get();
            var updatedLifeTime = crmRetriever.Get().First(o => o.Id == ltId);

            Assert.AreEqual(endOfApproval.AddHours(-4), updatedLifeTime.ApprovalDate);
        }
    }*/
}
