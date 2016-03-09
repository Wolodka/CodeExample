using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntraserviceTasks.Entities
{
    class Opportunity
    {
        public int Number { get; set; }
        public List<Contract> Contracts { get; set; }
    }
}
