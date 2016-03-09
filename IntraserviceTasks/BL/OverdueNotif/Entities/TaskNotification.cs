using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.Res;

namespace IntraserviceTasks.BL.OverdueNotif
{
    class TaskNotification
    {
        public int TaskId { get; set; }
        private string _name = "";
        public string Name 
        {
            get
            {
                if (String.IsNullOrWhiteSpace(_name))
                    return TaskUrl;

                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public string TaskUrl 
        {
            get
            {
                return String.Format(Constants.INTRASERVICE_TASK_URL_TEMPLATE, TaskId.ToString());
            }
        }
        public string TaskType { get; set; }
        public bool TaskTypeIsImportant { get; set; }
        public string StartOfApproval { get; set; }
        public string Deadline { get; set; }
        public string OverdueTime { get; set; }
    }
}
