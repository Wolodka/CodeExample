using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.CrossCuttingConcern;
using IntraserviceTasks.Entities;
using IntraserviceTasks.Entities.Enums;

namespace IntraserviceTasks.UnitTests.Mock
{
    class MockTaskLifeTimesRetriever : TaskLifeTimesRetriever
    {
        private List<TaskLifeTime> _lifetimes;

        public MockTaskLifeTimesRetriever(List<TaskLifeTime> lifetimes)
        {
            _lifetimes = lifetimes;
        }

        public List<TaskLifeTime> Get()
        {
            return _lifetimes;
        }

        public List<TaskLifeTime> GetOverdued()
        {
            return _lifetimes.Where(o => o.ApprovalDeadline <= DateTime.Now).ToList();
        }
    }
}
