using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntraserviceTasks.BL.WorkingTimeCalculation
{
    class WorkTimeCalc
    {
        public TimeSpan GetInterval(DateTime startOfPeriod, DateTime endOfPeriod)
        {
            TimeSpan restTimeInPeriod = new RestTimeCalc(startOfPeriod, endOfPeriod)
                    .GetRestTimeInPeriod();

            TimeSpan wholeTimeInterval = endOfPeriod - startOfPeriod;

            return wholeTimeInterval - restTimeInPeriod;
        }
    }
}
