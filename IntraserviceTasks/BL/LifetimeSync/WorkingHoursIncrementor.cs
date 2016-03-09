using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.CrossCuttingConcern;
using IntraserviceTasks.Res;

namespace IntraserviceTasks.BL
{
    class WorkingHoursIncrementor
    {
        DateTime _start;

        public WorkingHoursIncrementor(DateTime start)
        {
            _start = start;
        }

        public DateTime Add(int hours)
        {
            GetStartOfWorkingPeriod();

            int leftHours = hours;

            DateTime finalDate = _start;
            while (leftHours > 0)
            {
                int durationOfWorkingDay = Constants.WORK_TIME_FINISH_OF_DAY - _start.Hour;

                if (leftHours > durationOfWorkingDay)
                {
                    finalDate = _start.AddHours(durationOfWorkingDay);
                    _start = finalDate;
                    GetStartOfWorkingPeriod();
                    leftHours = leftHours - durationOfWorkingDay;
                }
                else
                {
                    return _start.AddHours(leftHours);
                }
            }

            return finalDate;
        }

        private void GetStartOfWorkingPeriod()
        {
            while (IfStartIsRestTime())
            {
                DateTime basisDay;

                if ( IsEveningTime() || IfStartIsWholeRestDay() )
                    basisDay = _start.AddDays(1);
                else
                    basisDay = _start;

                _start = new DateTime(basisDay.Year,
                                    basisDay.Month,
                                    basisDay.Day,
                                    Constants.WORK_TIME_START_OF_DAY, 
                                    basisDay.Minute, 
                                    basisDay.Second);
            }
        }

        private bool IsEveningTime()
        {
            return _start.Hour >= Constants.WORK_TIME_FINISH_OF_DAY;
        }

        private bool IfStartIsWholeRestDay()
        {
            return ExternalSystems.ProductionCalendar.Get().IsRestDay(_start);
        }

        private bool IfStartIsRestTime()
        {
            return new RestTimeChecker(_start).IsRestTime();
        }
    }
}
