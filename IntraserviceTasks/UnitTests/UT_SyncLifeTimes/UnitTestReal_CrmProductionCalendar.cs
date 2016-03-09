using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.Configuration;
using IntraserviceTasks.CrmAPI.Repositories;
using IntraserviceTasks.CrossCuttingConcern;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntraserviceTasks.UnitTests
{
    [TestClass]
    public class UnitTestReal_CrmProductionCalendar
    {
        [TestInitialize()]
        public void Init()
        {
            Config.CrmType = CrmEnvironmentType.Stage;
            ExternalSystems.ProductionCalendar.Init(new CrmProductionCalendarImpl());
        }

        [TestMethod]
        public void TestCrmProdCalendarGetRestDay()
        {
            var calendar = ExternalSystems.ProductionCalendar.Get();
            DateTime restDay = new DateTime(2015, 12, 12);

            bool isRestDay = calendar.IsRestDay(restDay);

            Assert.IsTrue(isRestDay);
        }

        [TestMethod]
        public void TestCrmProdCalendarGetWorkingDay()
        {
            var calendar = ExternalSystems.ProductionCalendar.Get();
            DateTime workingDay = new DateTime(2015, 12, 11);

            bool isRestDay = calendar.IsRestDay(workingDay);

            Assert.IsFalse(isRestDay);
        }

        [TestMethod]
        public void TestCrmProdCalendarGetRestDayIn2016()
        {
            var calendar = ExternalSystems.ProductionCalendar.Get();
            DateTime restDay = new DateTime(2016, 1, 16);

            bool isRestDay = calendar.IsRestDay(restDay);

            Assert.IsTrue(isRestDay);
        }

        [TestMethod]
        public void TestCrmProdCalendarGetWorkingDayIn2016()
        {
            var calendar = ExternalSystems.ProductionCalendar.Get();
            DateTime workingDay = new DateTime(2016, 1, 20);

            bool isRestDay = calendar.IsRestDay(workingDay);

            Assert.IsFalse(isRestDay);
        }
    }
}
