using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.BL;
using IntraserviceTasks.CrossCuttingConcern;

namespace IntraserviceTasks.UnitTests.Mock
{
    class MockProductionCalendar : ProductionCalendar
    {
        private List<DateTime> _restDays;
        private List<DateTime> _shortDays;

        public MockProductionCalendar(List<DateTime> restDays, List<DateTime> shortDays)
        {
            _restDays = restDays;
            _shortDays = shortDays;
        }

        public bool IsRestDay(DateTime day)
        {
            if (_restDays.Contains(day.Date))
                return true;

            return false;
        }
    }
}
