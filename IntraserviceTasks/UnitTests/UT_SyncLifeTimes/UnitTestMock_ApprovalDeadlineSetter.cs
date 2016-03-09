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
    public class UnitTestMock_ApprovalDeadlineSetter
    {
        [TestInitialize()]
        public void Init()
        {
            InitISTaskLifeTimeRetriever();
            InitProductionCalendar();
        }

        private void InitISTaskLifeTimeRetriever()
        {
            List<TaskLifeTime> lifetimes = new List<TaskLifeTime>
            {
                new TaskLifeTime { 
                    Id = 100,
                    TaskId = 5111,
                    StartOfApproval = new DateTime(2015, 12, 11, 16, 0, 0),
                    TaskPrioriry = TaskPriority.High
                },
                new TaskLifeTime { 
                    Id = 101,
                    TaskId = 5111,
                    StartOfApproval = new DateTime(2015, 12, 11, 16, 0, 0),
                    TaskPrioriry = TaskPriority.Medium
                },
                new TaskLifeTime { 
                    Id = 102,
                    TaskId = 5111,
                    StartOfApproval = new DateTime(2015, 12, 11, 16, 0, 0),
                    IsStandartContract = true
                }
            };

            TaskLifeTimesRetriever isRetriever = new Mock.MockTaskLifeTimesRetriever(lifetimes);
            ExternalSystems.ISTaskLifeTimesRetriever.Init(isRetriever);
        }

        private void InitProductionCalendar()
        {
            List<DateTime> restDays = new List<DateTime>() 
            { 
                new DateTime(2015, 12, 12),
                new DateTime(2015, 12, 13)
            };
            List<DateTime> shortDays = new List<DateTime>();
            ExternalSystems.ProductionCalendar.Init(
                new Mock.MockProductionCalendar(restDays, shortDays));
        }

        [TestMethod]
        public void TestAssignApprovalDeadline()
        {
            TaskLifeTimesRetriever isLTRetriever = ExternalSystems.ISTaskLifeTimesRetriever.Get();
            var isLifetimes = isLTRetriever.Get();

            foreach (var lifetime in isLifetimes)
                new DeadlineAssigner(lifetime).Assign();

            var importantApproval = isLifetimes.First(o => o.Id == 100);
            Assert.AreEqual(new DateTime(2015, 12, 14, 15, 0, 0), importantApproval.ApprovalDeadline);

            var mediumApproval = isLifetimes.First(o => o.Id == 101);
            Assert.AreEqual(new DateTime(2015, 12, 16, 13, 0, 0), mediumApproval.ApprovalDeadline);

            var standartContractApproval = isLifetimes.First(o => o.Id == 102);
            Assert.AreEqual(new DateTime(2015, 12, 14, 15, 0, 0), standartContractApproval.ApprovalDeadline);
        }
    }
}
