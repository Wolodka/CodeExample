using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.BL.WorkingTimeCalculation;
using IntraserviceTasks.Entities;
using IntraserviceTasks.Res;

namespace IntraserviceTasks.BL.OverdueNotif
{
    class NotificationExtractorFromLifeTime
    {
        Contract _contract;
        TaskLifeTime _lifetime;

        public NotificationExtractorFromLifeTime(Contract contract, TaskLifeTime lifetime)
        {
            _contract = contract;
            _lifetime = lifetime;
        }

        public TaskNotification ExtractNotification()
        {
            string taskTypeString = GetTaskTypeString(_contract.Type);
            bool typeIsImportant = GetTaskTypeImportance(_contract.Type);
            
            return new TaskNotification
            {
                TaskId = _lifetime.TaskId,
                Name = _contract.Name,
                TaskType = taskTypeString,
                TaskTypeIsImportant = typeIsImportant,
                StartOfApproval = GetFormattedDateTime(_lifetime.StartOfApproval),
                Deadline = GetFormattedDateTime(_lifetime.ApprovalDeadline),
                OverdueTime = GetOverduedTimeValue(_lifetime)
            };
        }

        private string GetTaskTypeString(Entities.Enums.ContractType taskType)
        {
            
            if (ConstantsOverdueNotifier.CONTRACT_TYPE_NAMES.Keys.Contains(taskType))
                return ConstantsOverdueNotifier.CONTRACT_TYPE_NAMES[taskType];

            return "";
        }

        private bool GetTaskTypeImportance(Entities.Enums.ContractType taskType)
        {
            if (ConstantsOverdueNotifier.CONTRACT_TYPE_IMPORTANCE.Keys.Contains(taskType))
                return ConstantsOverdueNotifier.CONTRACT_TYPE_IMPORTANCE[taskType];

            return false;
        }

        private string GetFormattedDateTime(DateTime dt)
        {
            return dt.ToString(ConstantsOverdueNotifier.DATE_FORMAT);
        }

        private string GetOverduedTimeValue(TaskLifeTime lt)
        {
            DateTime start = lt.ApprovalDeadline;
            DateTime finish = DateTime.Now;
            TimeSpan interval = new WorkTimeCalc().GetInterval(start, finish);

            return GetOverdueFormattedString(interval);
        }

        private string GetOverdueFormattedString(TimeSpan interval)
        {
            double durationOfWorkingDay = Constants.WORK_TIME_FINISH_OF_DAY - Constants.WORK_TIME_START_OF_DAY;
            double totalDays = interval.TotalHours / durationOfWorkingDay;

            if (totalDays < 1)
                return String.Format(
                    ConstantsOverdueNotifier.DURATION_FORMAT_IN_HOURS, 
                    interval.TotalHours.ToString(ConstantsOverdueNotifier.DURATION_FORMAT));
            else
                return String.Format(
                    ConstantsOverdueNotifier.DURATION_FORMAT_IN_DAYS,
                    totalDays.ToString(ConstantsOverdueNotifier.DURATION_FORMAT));
        }
    }
}
