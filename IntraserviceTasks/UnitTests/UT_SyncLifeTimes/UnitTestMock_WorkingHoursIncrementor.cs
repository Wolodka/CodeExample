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
    public class UnitTestMock_WorkingHoursIncrementor
    {
        [TestInitialize()]
        public void Init()
        {
            List<DateTime> restDays = new List<DateTime>()
            {
                new DateTime(2015, 12, 12),
                new DateTime(2015, 12, 13)
            };

            List<DateTime> shortDays = new List<DateTime>();

            ProductionCalendar calendar = new Mock.MockProductionCalendar(restDays, shortDays);
            ExternalSystems.ProductionCalendar.Init(calendar);
        }

        [TestMethod]
        public void TestAdd8HoursForSimpleWorkingDays()
        {
            DateTime start = new DateTime(2015, 12, 10, 16, 0, 0);
            DateTime finish = new WorkingHoursIncrementor(start).Add(8);

            Assert.AreEqual(new DateTime(2015, 12, 11, 15, 0, 0), finish);
        }

        [TestMethod]
        public void TestAdd5HoursInWorkingDay()
        {
            DateTime start = new DateTime(2015, 12, 10, 10, 0, 0);
            DateTime finish = new WorkingHoursIncrementor(start).Add(5);

            Assert.AreEqual(new DateTime(2015, 12, 10, 15, 0, 0), finish);
        }

        [TestMethod]
        public void TestAdd24HoursThroughWeekend()
        {
            DateTime start = new DateTime(2015, 12, 11, 15, 0, 0);
            DateTime finish = new WorkingHoursIncrementor(start).Add(24);

            Assert.AreEqual(new DateTime(2015, 12, 16, 12, 0, 0), finish);
        }

        [TestMethod]
        public void TestAdd24HoursAndBasisTimeWithMinutes()
        {
            DateTime start = new DateTime(2015, 12, 11, 15, 17, 23);
            DateTime finish = new WorkingHoursIncrementor(start).Add(24);

            Assert.AreEqual(new DateTime(2015, 12, 16, 12, 17, 23), finish);
        }
    }
}
