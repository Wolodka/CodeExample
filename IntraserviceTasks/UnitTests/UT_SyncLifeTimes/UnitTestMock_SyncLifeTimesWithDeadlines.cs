using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.BL;
using IntraserviceTasks.CrossCuttingConcern;
using IntraserviceTasks.Entities;
using IntraserviceTasks.Entities.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntraserviceTasks.UnitTests.UT_SyncLifeTimes
{
    [TestClass]
    public class UnitTestMock_SyncLifeTimesWithDeadlines
    {
        [TestInitialize()]
        public void INit()
        {
            InitISLTRetriever();
            InitProductionCalendar();
            InitCrmRepositories();
        }

        private void InitISLTRetriever()
        {
            List<TaskLifeTime> lifetimes = new List<TaskLifeTime>
            {
                new TaskLifeTime
                {
                    Id = 100,
                    TaskStatus = Entities.Enums.TaskStatus.ExpertApproval,
                    TaskPrioriry = TaskPriority.Critical,
                    StartOfApproval = new DateTime(2015, 12, 10, 15, 0, 0)
                },
                new TaskLifeTime
                {
                    Id = 101,
                    TaskPrioriry = TaskPriority.Critical,
                    StartOfApproval = new DateTime(2015, 12, 10, 15, 0, 0)
                }
            };

            ExternalSystems.ISTaskLifeTimesRetriever.Init(
                new Mock.MockTaskLifeTimesRetriever(lifetimes));
        }

        private void InitProductionCalendar()
        {
            ExternalSystems.ProductionCalendar.Init(
                new Mock.MockProductionCalendar(
                        new List<DateTime>(), 
                        new List<DateTime>()
                    )
                );
        }

        private void InitCrmRepositories()
        {
            List<TaskLifeTime> lifetimes = new List<TaskLifeTime>();

            ExternalSystems.CrmTaskLifeTimesRetriever.Init(new Mock.MockTaskLifeTimesRetriever(lifetimes));
            ExternalSystems.CrmTaskLifeTimesRepository.Init(new Mock.MockTaskLifeTimesDestination(lifetimes));
        }

        [TestMethod]
        public void TestSyncImportantLifeTime()
        {
            new LifeTimesSynchronizator().Sync();

            var crmRetriever = ExternalSystems.CrmTaskLifeTimesRetriever.Get();
            var crmLifetimes = crmRetriever.Get();

            var importantLifeTime = crmLifetimes.First(o => o.Id == 100);
            Assert.AreEqual(new DateTime(2015, 12, 11, 14, 0, 0), importantLifeTime.ApprovalDeadline);
        }

        [TestMethod]
        public void TestSyncLifeTimeWithoutDeadline()
        {
            new LifeTimesSynchronizator().Sync();

            var crmRetriever = ExternalSystems.CrmTaskLifeTimesRetriever.Get();
            var crmLifetimes = crmRetriever.Get();

            var lifeTime = crmLifetimes.First(o => o.Id == 101);
            Assert.AreEqual(DateTime.MinValue, lifeTime.ApprovalDeadline);
        }
    }
}
