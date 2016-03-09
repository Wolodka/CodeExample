using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.BL;
using IntraserviceTasks.CrossCuttingConcern;
using IntraserviceTasks.Entities;

namespace IntraserviceTasks.UnitTests.Mock
{
    class MockTaskLifeTimesDestination : CrmLifetimeRepository
    {
        private List<TaskLifeTime> _lifetimes;

        public MockTaskLifeTimesDestination(List<TaskLifeTime> lifetimes)
        {
            _lifetimes = lifetimes;
        }

        public bool IfExists(TaskLifeTime lifetime)
        {
            return _lifetimes.Any(o => o.Id == lifetime.Id);
        }

        public void Update(TaskLifeTime lifetime)
        {
            TaskLifeTime existing = _lifetimes.First(o => o.Id == lifetime.Id);

            existing.TaskId = lifetime.TaskId;
            existing.TaskPrioriry = lifetime.TaskPrioriry;
            existing.IsStandartContract = lifetime.IsStandartContract;
            existing.TaskStatus = lifetime.TaskStatus;
            existing.StartOfApproval = lifetime.StartOfApproval;
            existing.ApprovalPerson.Name = lifetime.ApprovalPerson.Name;
            existing.ApprovalDate = lifetime.ApprovalDate;
            existing.ApprovalStatus = lifetime.ApprovalStatus;
        }

        public void Create(TaskLifeTime lifetime)
        {
            _lifetimes.Add(lifetime);
        }

        
    }
}
