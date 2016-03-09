using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntraserviceTasks.Entities.Enums
{
    public enum TaskPriority : int
    {
        Undefine = -1,
        Low,
        Medium,
        High,
        Critical,
        VIP
    }
}
