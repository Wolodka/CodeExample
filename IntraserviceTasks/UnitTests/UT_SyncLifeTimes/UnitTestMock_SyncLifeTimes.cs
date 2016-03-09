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
    public class UnitTestMock_SyncLifeTimes
    {
        [TestInitialize()]
        public void Init()
        {
            InitISLifeTimesRetriever();
            InitCrmRepositories();
            InitCalendar();
        }

        private void InitISLifeTimesRetriever()
        {
            List<TaskLifeTime> lifetimes = new List<TaskLifeTime>()
            {
                new TaskLifeTime {
                    Id = 100,
                    TaskId = 17000, 
                    TaskPrioriry = TaskPriority.Medium, 
                    TaskStatus = Entities.Enums.TaskStatus.ExpertApproval,
                    StartOfApproval = new DateTime(2015, 10, 11), 
                    ApprovalPerson = new Person { Name = "Bill Murray" } },
                new TaskLifeTime {
                    Id = 101,
                    TaskId = 17000, 
                    TaskPrioriry = TaskPriority.Medium, 
                    StartOfApproval = new DateTime(2015, 12, 11), 
                    ApprovalPerson = new Person { Name = "Patrick" } },
                new TaskLifeTime {
                    Id = 102,
                    TaskId = 17001, 
                    TaskPrioriry = TaskPriority.High, 
                    StartOfApproval = new DateTime(2015, 10, 11), 
                    ApprovalPerson = new Person { Name = "Jacky Jack" } },
                new TaskLifeTime {
                    Id = 103,
                    TaskId = 17000, 
                    TaskPrioriry = TaskPriority.Medium, 
                    TaskStatus = Entities.Enums.TaskStatus.ExpertApproval,
                    StartOfApproval = new DateTime(2015, 10, 14), 
                    ApprovalPerson = new Person { Name = "Bill Murray" } },
            };

            ExternalSystems.ISTaskLifeTimesRetriever.Init(new Mock.MockTaskLifeTimesRetriever(lifetimes));
        }

        private void InitCrmRepositories()
        {
            List<TaskLifeTime> lifetimes = new List<TaskLifeTime>()
            {
                new TaskLifeTime {
                    Id = 100,
                    TaskId = 17000, 
                    TaskPrioriry = TaskPriority.Medium, 
                    StartOfApproval = new DateTime(2015, 10, 11), 
                    ApprovalPerson = new Person { Name = "Bill Murray"}  },
                
                new TaskLifeTime {
                    Id = 102,
                    TaskId = 17001, 
                    TaskPrioriry = TaskPriority.Low, 
                    StartOfApproval = new DateTime(2015, 10, 16), 
                    ApprovalPerson = new Person { Name = "Jack Sunders"}  },
            };

            ExternalSystems.CrmTaskLifeTimesRetriever.Init(new Mock.MockTaskLifeTimesRetriever(lifetimes));
            ExternalSystems.CrmTaskLifeTimesRepository.Init(new Mock.MockTaskLifeTimesDestination(lifetimes));
        }

        private void InitCalendar()
        {
            List<DateTime> restDays = new List<DateTime>()
            {
                new DateTime(2015, 10, 10),
                new DateTime(2015, 10, 11),
                new DateTime(2015, 12, 12),
                new DateTime(2015, 12, 13)
            };

            List<DateTime> shortDays = new List<DateTime>() { };

            ExternalSystems.ProductionCalendar.Init(new Mock.MockProductionCalendar(restDays, shortDays));

        }

        [TestMethod]
        public void TestMockSyncLifeTimes()
        {
            new LifeTimesSynchronizator().Sync();

            var crmRetriever = ExternalSystems.CrmTaskLifeTimesRetriever.Get();
            var crmLifetimes = crmRetriever.Get();

            Assert.AreEqual(4, crmLifetimes.Count);

            var secondLT = crmLifetimes.First(o => o.Id == 101);
            Assert.AreEqual(17000, secondLT.TaskId);
            Assert.AreEqual(TaskPriority.Medium, secondLT.TaskPrioriry);
            Assert.AreEqual(new DateTime(2015, 12, 11), secondLT.StartOfApproval);
            Assert.AreEqual("Patrick", secondLT.ApprovalPerson.Name);

            var thirdLT = crmLifetimes.First(o => o.Id == 102);
            Assert.AreEqual(17001, thirdLT.TaskId);
            Assert.AreEqual(TaskPriority.High, thirdLT.TaskPrioriry);
            Assert.AreEqual(new DateTime(2015, 10, 11), thirdLT.StartOfApproval);
            Assert.AreEqual("Jacky Jack", thirdLT.ApprovalPerson.Name);
        }

        [TestMethod]
        public void TestMockUpdateLifeTimeStatus()
        {
            TestOneUpdateStatusOperation(ApprovalStatus.LeftComment);
            TestOneUpdateStatusOperation(ApprovalStatus.Approved);
        }

        private void TestOneUpdateStatusOperation(ApprovalStatus newApprovalStatus)
        {
            TaskLifeTime lifeTime = ExternalSystems.ISTaskLifeTimesRetriever.Get().Get().First(o => o.Id == 100);
            lifeTime.ApprovalStatus = newApprovalStatus;

            var crmRepo = ExternalSystems.CrmTaskLifeTimesRepository.Get();
            crmRepo.Update(lifeTime);

            var crmRetriever = ExternalSystems.CrmTaskLifeTimesRetriever.Get();
            var updatedLifeTime = crmRetriever.Get().First(o => o.Id == 100);

            Assert.AreEqual(newApprovalStatus, updatedLifeTime.ApprovalStatus);
        }

        [TestMethod]
        public void TestMockUpdateLifeTimeFinishOfApproval()
        {
            TaskLifeTime lifeTime = ExternalSystems.ISTaskLifeTimesRetriever.Get().Get().First(o => o.Id == 100);
            DateTime endOfApproval = new DateTime(2015, 10, 15, 16, 30, 0);
            lifeTime.ApprovalDate = endOfApproval;

            var crmRepo = ExternalSystems.CrmTaskLifeTimesRepository.Get();
            crmRepo.Update(lifeTime);

            var crmRetriever = ExternalSystems.CrmTaskLifeTimesRetriever.Get();
            var updatedLifeTime = crmRetriever.Get().First(o => o.Id == 100);

            Assert.AreEqual(endOfApproval, updatedLifeTime.ApprovalDate);
        }

        [TestMethod]
        public void TestMockSyncLifeTimeWithApprovedOnes()
        {
            ChangeOneTaskLifetime();

            new LifeTimesSynchronizator().Sync();

            var crmRetriever = ExternalSystems.CrmTaskLifeTimesRetriever.Get();
            var crmLifetimes = crmRetriever.Get();

            var extracted = crmLifetimes.First(o => o.Id == 103);
            Assert.AreEqual(DateTime.MinValue, extracted.ApprovalDeadline);
        }

        private void ChangeOneTaskLifetime()
        {
            var ISRep = ExternalSystems.ISTaskLifeTimesRetriever.Get();
            var lifetimes = ISRep.Get();

            var approvedLifetime = lifetimes.First(o => o.Id == 103);
            approvedLifetime.ApprovalStatus = ApprovalStatus.Approved;
        }
    }
}
