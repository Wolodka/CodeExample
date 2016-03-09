using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.BL;
using IntraserviceTasks.CrossCuttingConcern;
using IntraserviceTasks.CrmAPI.CRUD_Lifetimes;
using IntraserviceTasks.Entities;

namespace IntraserviceTasks.CrmAPI.Repositories
{
    public class CrmLifetimesRepositoryImpl : CrmLifetimeRepository, TaskLifeTimesRetriever
    {
        public bool IfExists(TaskLifeTime lifetime)
        {
            return new LifetimesGetter().LifetimeExists(lifetime);
        }

        public void Update(TaskLifeTime lifetime)
        {
            new LifeTimeUpdater().Update(lifetime);
        }

        public void Create(TaskLifeTime lifetime)
        {
            new LifeTimeCreator().Create(lifetime);
        }

        public List<TaskLifeTime> Get()
        {
            return new LifetimesGetter().GetAll();
        }

        public List<TaskLifeTime> GetOverdued()
        {
            return new LifetimesGetter().GetOverdued();
        }
    }
}
