using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.Entities.Enums;

namespace IntraserviceTasks.BL.OverdueNotif
{
    class Notifications
    {
        private List<NotificationsByUserAndBusinessUnit> _notificationsPerUserAndBusinessUnit = new List<NotificationsByUserAndBusinessUnit>();
        public List<NotificationsByUserAndBusinessUnit> NotificationsByUserAndBusinessUnit
        {
            get
            {
                return _notificationsPerUserAndBusinessUnit;
            }
        }

        public void AppendNotification(string userLogin, BusinessUnit businessUnit, TaskNotification taskNotification)
        {
            NotificationsByUserAndBusinessUnit existingNotifs = GetNotificationsForUserOrNull(userLogin, businessUnit);

            if (existingNotifs == null)
            {
                existingNotifs = new NotificationsByUserAndBusinessUnit() { UserLogin = userLogin, BusinessUnit = businessUnit };
                _notificationsPerUserAndBusinessUnit.Add(existingNotifs);
            }

            existingNotifs.Notifications.Add(taskNotification);
        }

        private NotificationsByUserAndBusinessUnit GetNotificationsForUserOrNull(string userLogin, BusinessUnit businessUnit)
        {
            return _notificationsPerUserAndBusinessUnit.FirstOrDefault(o => o.UserLogin == userLogin && o.BusinessUnit == businessUnit);
        }
    }

}
