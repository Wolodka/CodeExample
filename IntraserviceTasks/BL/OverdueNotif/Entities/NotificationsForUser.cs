using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.Entities.Enums;

namespace IntraserviceTasks.BL.OverdueNotif
{
    class NotificationsByUserAndBusinessUnit
    {
        public string UserLogin { get; set; }
        public BusinessUnit BusinessUnit { get; set; }

        public List<TaskNotification> Notifications { get; set; }

        public NotificationsByUserAndBusinessUnit()
        {
            Notifications = new List<TaskNotification>();
        }
    }
}
