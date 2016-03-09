using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.CrossCuttingConcern;
using IntraserviceTasks.Res;

namespace IntraserviceTasks.BL
{
    class RestTimeChecker
    {
        DateTime _time;

        public RestTimeChecker(DateTime time)
        {
            _time = time;
        }

        public bool IsRestTime()
        {
            if( CheckIfDayIsRest() )
                return true;

            return CheckRestTimeInWorkingDay();
        }

        private bool CheckIfDayIsRest()
        { 
            var calendar = ExternalSystems.ProductionCalendar.Get();
            return calendar.IsRestDay(_time);
        }

        private bool CheckRestTimeInWorkingDay()
        {
            TimeSpan highThreshold = new TimeSpan(Constants.WORK_TIME_FINISH_OF_DAY, 0, 0);
            TimeSpan lowThreshold = new TimeSpan(Constants.WORK_TIME_START_OF_DAY, 0, 0);
            TimeSpan time = _time.TimeOfDay;

            return time < lowThreshold || time >= highThreshold;
        }
    }
}
