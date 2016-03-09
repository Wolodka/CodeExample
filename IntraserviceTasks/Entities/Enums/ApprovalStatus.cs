using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntraserviceTasks.Entities.Enums
{
    public enum ApprovalStatus : int
    {
        Undefined = -1,
        WasNotApproving = 1,
        LeftComment = 948140000,
        Approved = 948140001
    }
}
