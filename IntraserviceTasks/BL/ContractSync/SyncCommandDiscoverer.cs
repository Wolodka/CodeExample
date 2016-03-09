using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.BL.ContractSync.SyncCommands;

namespace IntraserviceTasks.BL.ContractSync
{
    interface SyncCommandDiscoverer
    {
        List<SyncCommand> Discover();
    }
}
