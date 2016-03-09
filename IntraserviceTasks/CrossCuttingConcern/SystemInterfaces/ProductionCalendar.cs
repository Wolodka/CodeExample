using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntraserviceTasks.CrossCuttingConcern
{
    public interface ProductionCalendar
    {
        bool IsRestDay(DateTime day);
    }
}
