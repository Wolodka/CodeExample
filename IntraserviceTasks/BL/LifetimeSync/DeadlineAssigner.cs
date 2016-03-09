using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.Entities;
using IntraserviceTasks.Entities.Enums;
using IntraserviceTasks.Res;

namespace IntraserviceTasks.BL
{
    class DeadlineAssigner
    {
        TaskLifeTime _lifetime;

        public DeadlineAssigner(TaskLifeTime lifetime)
        {
            _lifetime = lifetime;
        }

        public void Assign()
        {
            int hoursBeforeDeadline = GetHoursBeforeDeadline();

            _lifetime.ApprovalDeadline
                    = new WorkingHoursIncrementor(_lifetime.StartOfApproval)
                            .Add(hoursBeforeDeadline);
        }

        private int GetHoursBeforeDeadline()
        {
            if ((int)_lifetime.TaskPrioriry >= (int)TaskPriority.High)
                return Constants.OVERDUE_HOURS_FOR_IMPORTANT_LIFETIME;
            
            if (_lifetime.IsStandartContract)
                return Constants.OVERDUE_HOURS_FOR_STANDART_CONTRACT_LIFETIME;
            
            return Constants.OVERDUE_HOURS_FOR_CASUAL_LIFETIME;
        }
    }
}
