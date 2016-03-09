using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.Entities;

namespace IntraserviceTasks.CrossCuttingConcern
{
    public interface CrmLifetimeRepository
    {
        bool IfExists(TaskLifeTime lifetime);
        void Update(TaskLifeTime lifetime);
        void Create(TaskLifeTime lifetime);
    }
}
