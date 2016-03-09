using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.BL;
using IntraserviceTasks.CrossCuttingConcern;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntraserviceTasks.UnitTests
{
    [TestClass]
    public class UnitTestMock_RestTimeChecker
    {
        [TestInitialize()]
        public void Init()
        { 
            List<DateTime> restDays = new List<DateTime>()
            {
                new DateTime(2015, 12, 5),
                new DateTime(2015, 12, 6),
                new DateTime(2015, 12, 12),
                new DateTime(2015, 12, 13)
            };

            List<DateTime> shortDays = new List<DateTime>()
            {
                new DateTime(2015, 12, 4),
                new DateTime(2015, 12, 11)
            };

            ExternalSystems.ProductionCalendar.Init(new Mock.MockProductionCalendar(restDays, shortDays));
        }

        [TestMethod]
        public void TestRestTime()
        {
            DateTime time = new DateTime(2015, 12, 10, 23, 0, 0);
            bool isRestTime = new RestTimeChecker(time).IsRestTime();
            
            Assert.IsTrue(isRestTime);
        }

        [TestMethod]
        public void TestRestTimeInWeekend()
        {
            DateTime time = new DateTime(2015, 12, 12, 14, 0, 0);
            bool isRestTime = new RestTimeChecker(time).IsRestTime();

            Assert.IsTrue(isRestTime);
        }

        [TestMethod]
        public void TestNotRestTime()
        {
            DateTime time = new DateTime(2015, 12, 10, 16, 0, 0);
            bool isRestTime = new RestTimeChecker(time).IsRestTime();

            Assert.IsFalse(isRestTime);
        }
    }
}
