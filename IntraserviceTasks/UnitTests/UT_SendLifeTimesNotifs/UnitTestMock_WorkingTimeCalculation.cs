using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.BL.WorkingTimeCalculation;
using IntraserviceTasks.CrossCuttingConcern;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntraserviceTasks.UnitTests
{
    [TestClass]
    public class UnitTestMock_WorkingTimeCalculation
    {
        [TestInitialize()]
        public void Init()
        { 
            var calendar = new Mock.MockProductionCalendar(
                new List<DateTime>()
                {
                    new DateTime(2015, 1, 1),
                    new DateTime(2015, 1, 2),
                    new DateTime(2015, 1, 3),
                    new DateTime(2015, 1, 4),
                    new DateTime(2015, 1, 5),
                    new DateTime(2015, 1, 6),
                    new DateTime(2015, 1, 7),
                    new DateTime(2015, 1, 8),
                    new DateTime(2015, 1, 9),
                    new DateTime(2015, 1, 10)
                },
                new List<DateTime>()
                );

            ExternalSystems.ProductionCalendar.Init(calendar);
        }

        [TestMethod]
        public void Test_GetWorkingDuration_WithHoursAndMinutes()
        {
            DateTime start = new DateTime(2015, 1, 1, 14, 35, 0);
            DateTime finish = new DateTime(2015, 1, 2, 10, 13, 0);

            TimeSpan interval = new WorkTimeCalc().GetInterval(start, finish);
            /*
             interval = ( 18:00 - 14:35 = 3:25 ) + ( 10:13 - 9:00 = 1:13) = 3:25 + 1:13 = 4:38
             */

            Assert.AreEqual(4, (int)interval.TotalHours);
            Assert.AreEqual(38, interval.Minutes);
        }

        [TestMethod]
        public void Test_GetWorkingDuration_WithHolidays()
        {
            DateTime start = new DateTime(2014, 12, 31, 14, 35, 0);
            DateTime finish = new DateTime(2015, 1, 11, 10, 13, 0);

            TimeSpan interval = new WorkTimeCalc().GetInterval(start, finish);
            /*
             interval = ( 18:00 - 14:35 = 3:25 ) + ( 10:13 - 9:00 = 1:13) = 3:25 + 1:13 = 4:38
             * and from 1 jan for 10 jan - holidays
             */

            Assert.AreEqual(4, (int)interval.TotalHours);
            Assert.AreEqual(38, interval.Minutes);
        }

        [TestMethod]
        public void Test_GetWorkingDuration_ForSeveralDays()
        {
            DateTime start = new DateTime(2015, 1, 11, 14, 35, 0);
            DateTime finish = new DateTime(2015, 1, 18, 10, 13, 0);

            TimeSpan interval = new WorkTimeCalc().GetInterval(start, finish);
            /*
             interval = ( 18:00 - 14:35 = 3:25 ) + 6*9 + ( 10:13 - 9:00 = 1:13) = 3:25 + 54 + 1:13 = 58:38
             * and from 1 jan for 10 jan - holidays
             */

            Assert.AreEqual(58, (int)interval.TotalHours);
            Assert.AreEqual(38, interval.Minutes);
        }
    }
}
