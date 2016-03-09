using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.CrossCuttingConcern;
using IntraserviceTasks.Entities;
using IntraserviceTasks.Res;
using NLog;

namespace IntraserviceTasks.BL.OverdueNotif
{
    public class OverdueNotifier
    {
        private Logger _logger = LogManager.GetCurrentClassLogger();

        public void Notify()
        {
            var notifs = new NotificationGenerator().GetUsersNotifications();

            foreach (var notif in notifs) 
                TryToSendEmail(notif);        
        }

        private void TryToSendEmail(NotificationsByUserAndBusinessUnit notif)
        {
            try
            {
                SendEmail(notif);
            }
            catch(Exception exc)
            {
                _logger.Error(Exceptions.ERROR_SENDING_NOTIFICATION, notif.UserLogin, exc.Message);
            }
        }

        private void SendEmail(NotificationsByUserAndBusinessUnit notif)
        {
            var email = new EmailGenerator(notif).CreateEmail();

            var sender = ExternalSystems.EmailSender.Get();
            sender.Send(email);

            _logger.Info("Sent to {0}", notif.UserLogin);
        }
    }
}
