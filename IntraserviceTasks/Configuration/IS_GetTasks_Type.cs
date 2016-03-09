using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntraserviceTasks.Configuration
{
    public enum IS_GetTasks_Type
    {
        Undefined = -1,

        GetSingleTask,
        GetTop1000Tasks,
        GetAllTasks_fromTaskLifeTimesView
    }
}
